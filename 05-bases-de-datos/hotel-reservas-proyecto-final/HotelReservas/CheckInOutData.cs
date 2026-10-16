using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class CheckInOutData
    {
        // =========================
        // OBTENER ESTADO POR RESERVA
        // =========================
        public static DataRow ObtenerPorReserva(int idReserva)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        c.IdCheckInOut,
                        c.IdReserva,
                        c.IdEmpleadoCheckIn,
                        c.IdEmpleadoCheckOut,
                        c.FechaCheckIn,
                        c.FechaCheckOut,
                        c.ObservacionIn,
                        c.ObservacionOut,
                        c.Estado
                    FROM dbo.CheckInOut c
                    WHERE c.IdReserva = @IdReserva;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // =========================
        // REGISTRAR CHECK-IN
        // =========================
        public static void RegistrarCheckIn(int idReserva, string observacion)
        {
            // Primero verificar que la reserva esté CONFIRMADA
            string estadoReserva = ObtenerEstadoReserva(idReserva);
            if (!estadoReserva.Equals("CONFIRMADA", StringComparison.OrdinalIgnoreCase))
                throw new Exception("El check-in solo puede realizarse en reservas con estado CONFIRMADA. Estado actual: " + estadoReserva);

            // Verificar que no haya ya un check-in
            DataRow existing = ObtenerPorReserva(idReserva);
            if (existing != null)
            {
                string estadoCio = existing["Estado"] == DBNull.Value ? "" : existing["Estado"].ToString();
                if (estadoCio == "CHECKED_IN" || estadoCio == "CHECKED_OUT")
                    throw new Exception("Esta reserva ya tiene un check-in registrado (Estado: " + estadoCio + ").");
            }

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.CheckInOut
                    (
                        IdReserva,
                        IdEmpleadoCheckIn,
                        FechaCheckIn,
                        ObservacionIn,
                        Estado
                    )
                    VALUES
                    (
                        @IdReserva,
                        @IdEmpleadoCheckIn,
                        GETDATE(),
                        @ObservacionIn,
                        'CHECKED_IN'
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    cmd.Parameters.AddWithValue("@IdEmpleadoCheckIn",
                        SesionUsuario.IdUsuario > 0 ? (object)SesionUsuario.IdUsuario : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ObservacionIn",
                        string.IsNullOrWhiteSpace(observacion) ? (object)DBNull.Value : observacion.Trim());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // REGISTRAR CHECK-OUT
        // =========================
        public static void RegistrarCheckOut(int idReserva, string observacion)
        {
            DataRow cio = ObtenerPorReserva(idReserva);
            if (cio == null)
                throw new Exception("No existe un check-in registrado para esta reserva.");

            string estadoCio = cio["Estado"] == DBNull.Value ? "" : cio["Estado"].ToString();
            if (!estadoCio.Equals("CHECKED_IN", StringComparison.OrdinalIgnoreCase))
                throw new Exception("El check-out solo puede realizarse después de un check-in. Estado actual: " + estadoCio);

            int idCheckInOut = Convert.ToInt32(cio["IdCheckInOut"]);

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Actualizar registro de CheckInOut
                    string sqlUpdate = @"
                        UPDATE dbo.CheckInOut
                        SET
                            IdEmpleadoCheckOut = @IdEmpleadoCheckOut,
                            FechaCheckOut = GETDATE(),
                            ObservacionOut = @ObservacionOut,
                            Estado = 'CHECKED_OUT'
                        WHERE IdCheckInOut = @IdCheckInOut;";

                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdCheckInOut", idCheckInOut);
                        cmd.Parameters.AddWithValue("@IdEmpleadoCheckOut",
                            SesionUsuario.IdUsuario > 0 ? (object)SesionUsuario.IdUsuario : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ObservacionOut",
                            string.IsNullOrWhiteSpace(observacion) ? (object)DBNull.Value : observacion.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    // Generar factura si no existe — leer ITBIS de ConfiguracionSistema
                    bool facturaExiste;
                    using (SqlCommand cmdCheck = new SqlCommand(
                        "SELECT COUNT(*) FROM dbo.Factura WHERE IdReserva = @IdReserva;",
                        conn, transaction))
                    {
                        cmdCheck.Parameters.AddWithValue("@IdReserva", idReserva);
                        facturaExiste = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0;
                    }

                    if (!facturaExiste)
                    {
                        decimal porcItbis = 18;
                        using (SqlCommand cmdCfg = new SqlCommand(
                            "SELECT Valor FROM dbo.ConfiguracionSistema WHERE Clave = 'ITBIS_PORCENTAJE';",
                            conn, transaction))
                        {
                            object cfgVal = cmdCfg.ExecuteScalar();
                            if (cfgVal != null && cfgVal != DBNull.Value)
                            {
                                decimal parsed;
                                if (decimal.TryParse(cfgVal.ToString(), out parsed))
                                    porcItbis = parsed;
                            }
                        }

                        using (SqlCommand cmdFact = new SqlCommand("dbo.sp_GenerarFactura", conn, transaction))
                        {
                            cmdFact.CommandType = CommandType.StoredProcedure;
                            cmdFact.Parameters.AddWithValue("@IdReserva", idReserva);
                            cmdFact.Parameters.AddWithValue("@PorcImpuesto", porcItbis);
                            cmdFact.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // =========================
        // HELPER PRIVADO
        // =========================
        private static string ObtenerEstadoReserva(int idReserva)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT er.NombreEstado
                    FROM dbo.Reserva r
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    WHERE r.IdReserva = @IdReserva;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return "";

                    return result.ToString();
                }
            }
        }
    }
}
