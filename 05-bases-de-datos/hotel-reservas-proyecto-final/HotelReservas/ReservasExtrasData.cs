using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class ReservasExtrasData
    {
        // =========================
        // HABITACIONES DISPONIBLES
        // =========================
        public static DataTable ObtenerHabitacionesDisponiblesCombo(DateTime fechaEntrada, DateTime fechaSalida, int idReservaActual = 0)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        h.IdHabitacion,
                        h.Numero + ' - ' + th.NombreTipo + ' - ' + s.NombreSucursal AS Habitacion
                    FROM dbo.Habitacion h
                    INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion
                    WHERE eh.NombreEstado <> 'MANTENIMIENTO'
                      AND NOT EXISTS
                      (
                          SELECT 1
                          FROM dbo.Reserva r
                          INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                          WHERE r.IdHabitacion = h.IdHabitacion
                            AND r.IdReserva <> @IdReservaActual
                            AND er.NombreEstado IN ('PENDIENTE', 'CONFIRMADA')
                            AND @FechaEntrada < r.FechaSalida
                            AND @FechaSalida > r.FechaEntrada
                      )
                    ORDER BY h.Numero;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FechaEntrada", fechaEntrada.Date);
                    cmd.Parameters.AddWithValue("@FechaSalida", fechaSalida.Date);
                    cmd.Parameters.AddWithValue("@IdReservaActual", idReservaActual);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static decimal ObtenerPrecioHabitacion(int idHabitacion)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT PrecioPorNoche FROM dbo.Habitacion WHERE IdHabitacion = @IdHabitacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return 0;

                    return Convert.ToDecimal(result);
                }
            }
        }

        // =========================
        // SERVICIOS ADICIONALES
        // =========================
        public static DataTable ObtenerServiciosAdicionalesCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdServicioAdicional,
                        NombreServicio
                    FROM dbo.ServicioAdicional
                    WHERE Estado = 1
                    ORDER BY NombreServicio;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerServiciosAdicionalesAdmin()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdServicioAdicional,
                        NombreServicio,
                        Precio,
                        Estado,
                        CASE WHEN Estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END AS EstadoTexto
                    FROM dbo.ServicioAdicional
                    ORDER BY IdServicioAdicional DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarServiciosAdicionales(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdServicioAdicional,
                        NombreServicio,
                        Precio,
                        Estado,
                        CASE WHEN Estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END AS EstadoTexto
                    FROM dbo.ServicioAdicional
                    WHERE NombreServicio LIKE @texto
                    ORDER BY IdServicioAdicional DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@texto", "%" + texto.Trim() + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static void InsertarServicioAdicional(string nombreServicio, decimal precio, bool estado)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.ServicioAdicional
                    (
                        NombreServicio,
                        Precio,
                        Estado
                    )
                    VALUES
                    (
                        @NombreServicio,
                        @Precio,
                        @Estado
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NombreServicio", nombreServicio);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarServicioAdicional(int idServicioAdicional, string nombreServicio, decimal precio, bool estado)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.ServicioAdicional
                    SET
                        NombreServicio = @NombreServicio,
                        Precio = @Precio,
                        Estado = @Estado
                    WHERE IdServicioAdicional = @IdServicioAdicional;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdServicioAdicional", idServicioAdicional);
                    cmd.Parameters.AddWithValue("@NombreServicio", nombreServicio);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EliminarServicioAdicional(int idServicioAdicional)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.ServicioAdicional
                    SET Estado = 0
                    WHERE IdServicioAdicional = @IdServicioAdicional;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdServicioAdicional", idServicioAdicional);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // PROMOCIONES
        // =========================
        public static DataTable ObtenerPromocionesActivasCombo(DateTime fechaReferencia)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdPromocion,
                        NombrePromocion
                    FROM dbo.Promocion
                    WHERE Estado = 1
                      AND @FechaReferencia BETWEEN FechaInicio AND FechaFin
                    ORDER BY NombrePromocion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FechaReferencia", fechaReferencia.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerPromocionesAdmin()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdPromocion,
                        NombrePromocion,
                        TipoDescuento,
                        ValorDescuento,
                        FechaInicio,
                        FechaFin,
                        Estado,
                        CASE WHEN Estado = 1 THEN 'ACTIVA' ELSE 'INACTIVA' END AS EstadoTexto
                    FROM dbo.Promocion
                    ORDER BY IdPromocion DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarPromociones(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdPromocion,
                        NombrePromocion,
                        TipoDescuento,
                        ValorDescuento,
                        FechaInicio,
                        FechaFin,
                        Estado,
                        CASE WHEN Estado = 1 THEN 'ACTIVA' ELSE 'INACTIVA' END AS EstadoTexto
                    FROM dbo.Promocion
                    WHERE NombrePromocion LIKE @texto
                    ORDER BY IdPromocion DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@texto", "%" + texto.Trim() + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static void InsertarPromocion(
            string nombrePromocion,
            string tipoDescuento,
            decimal valorDescuento,
            DateTime fechaInicio,
            DateTime fechaFin,
            bool estado)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.Promocion
                    (
                        NombrePromocion,
                        TipoDescuento,
                        ValorDescuento,
                        FechaInicio,
                        FechaFin,
                        Estado
                    )
                    VALUES
                    (
                        @NombrePromocion,
                        @TipoDescuento,
                        @ValorDescuento,
                        @FechaInicio,
                        @FechaFin,
                        @Estado
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NombrePromocion", nombrePromocion);
                    cmd.Parameters.AddWithValue("@TipoDescuento", tipoDescuento);
                    cmd.Parameters.AddWithValue("@ValorDescuento", valorDescuento);
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarPromocion(
            int idPromocion,
            string nombrePromocion,
            string tipoDescuento,
            decimal valorDescuento,
            DateTime fechaInicio,
            DateTime fechaFin,
            bool estado)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Promocion
                    SET
                        NombrePromocion = @NombrePromocion,
                        TipoDescuento = @TipoDescuento,
                        ValorDescuento = @ValorDescuento,
                        FechaInicio = @FechaInicio,
                        FechaFin = @FechaFin,
                        Estado = @Estado
                    WHERE IdPromocion = @IdPromocion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPromocion", idPromocion);
                    cmd.Parameters.AddWithValue("@NombrePromocion", nombrePromocion);
                    cmd.Parameters.AddWithValue("@TipoDescuento", tipoDescuento);
                    cmd.Parameters.AddWithValue("@ValorDescuento", valorDescuento);
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EliminarPromocion(int idPromocion)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Promocion
                    SET Estado = 0
                    WHERE IdPromocion = @IdPromocion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPromocion", idPromocion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // SERVICIOS EN RESERVA
        // =========================
        public static DataTable ObtenerServiciosReserva(int idReserva)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        rs.IdReserva,
                        rs.IdServicioAdicional,
                        sa.NombreServicio,
                        rs.Cantidad,
                        rs.PrecioUnitario,
                        rs.Subtotal
                    FROM dbo.ReservaServicio rs
                    INNER JOIN dbo.ServicioAdicional sa ON sa.IdServicioAdicional = rs.IdServicioAdicional
                    WHERE rs.IdReserva = @IdReserva
                    ORDER BY sa.NombreServicio;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerPromocionesReserva(int idReserva)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        rp.IdReserva,
                        rp.IdPromocion,
                        p.NombrePromocion,
                        p.TipoDescuento,
                        p.ValorDescuento,
                        rp.DescuentoAplicado
                    FROM dbo.ReservaPromocion rp
                    INNER JOIN dbo.Promocion p ON p.IdPromocion = rp.IdPromocion
                    WHERE rp.IdReserva = @IdReserva
                    ORDER BY p.NombrePromocion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerTotalesExtrasReserva(int idReserva)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        ISNULL(r.Subtotal, 0) AS Hospedaje,
                        ISNULL(srv.TotalServicios, 0) AS TotalServicios,
                        ISNULL(pro.TotalPromociones, 0) AS DescuentoPromociones,
                        CAST(ISNULL(r.Subtotal, 0) + ISNULL(srv.TotalServicios, 0) - ISNULL(pro.TotalPromociones, 0) AS DECIMAL(10,2)) AS TotalGeneral
                    FROM dbo.Reserva r
                    OUTER APPLY
                    (
                        SELECT SUM(Subtotal) AS TotalServicios
                        FROM dbo.ReservaServicio
                        WHERE IdReserva = r.IdReserva
                    ) srv
                    OUTER APPLY
                    (
                        SELECT SUM(DescuentoAplicado) AS TotalPromociones
                        FROM dbo.ReservaPromocion
                        WHERE IdReserva = r.IdReserva
                    ) pro
                    WHERE r.IdReserva = @IdReserva;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static void AgregarServicioAReserva(int idReserva, int idServicioAdicional, int cantidad)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    decimal precioUnitario = 0;

                    using (SqlCommand cmdPrecio = new SqlCommand(@"
                        SELECT Precio
                        FROM dbo.ServicioAdicional
                        WHERE IdServicioAdicional = @IdServicioAdicional
                          AND Estado = 1;", conn, transaction))
                    {
                        cmdPrecio.Parameters.AddWithValue("@IdServicioAdicional", idServicioAdicional);

                        object result = cmdPrecio.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            throw new Exception("El servicio adicional no está disponible.");

                        precioUnitario = Convert.ToDecimal(result);
                    }

                    int existe = 0;

                    using (SqlCommand cmdExiste = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM dbo.ReservaServicio
                        WHERE IdReserva = @IdReserva
                          AND IdServicioAdicional = @IdServicioAdicional;", conn, transaction))
                    {
                        cmdExiste.Parameters.AddWithValue("@IdReserva", idReserva);
                        cmdExiste.Parameters.AddWithValue("@IdServicioAdicional", idServicioAdicional);

                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar());
                    }

                    if (existe > 0)
                    {
                        using (SqlCommand cmdUpdate = new SqlCommand(@"
                            UPDATE dbo.ReservaServicio
                            SET
                                Cantidad = Cantidad + @Cantidad,
                                Subtotal = (Cantidad + @Cantidad) * PrecioUnitario
                            WHERE IdReserva = @IdReserva
                              AND IdServicioAdicional = @IdServicioAdicional;", conn, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@Cantidad", cantidad);
                            cmdUpdate.Parameters.AddWithValue("@IdReserva", idReserva);
                            cmdUpdate.Parameters.AddWithValue("@IdServicioAdicional", idServicioAdicional);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmdInsert = new SqlCommand(@"
                            INSERT INTO dbo.ReservaServicio
                            (
                                IdReserva,
                                IdServicioAdicional,
                                Cantidad,
                                PrecioUnitario,
                                Subtotal
                            )
                            VALUES
                            (
                                @IdReserva,
                                @IdServicioAdicional,
                                @Cantidad,
                                @PrecioUnitario,
                                @Subtotal
                            );", conn, transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@IdReserva", idReserva);
                            cmdInsert.Parameters.AddWithValue("@IdServicioAdicional", idServicioAdicional);
                            cmdInsert.Parameters.AddWithValue("@Cantidad", cantidad);
                            cmdInsert.Parameters.AddWithValue("@PrecioUnitario", precioUnitario);
                            cmdInsert.Parameters.AddWithValue("@Subtotal", precioUnitario * cantidad);
                            cmdInsert.ExecuteNonQuery();
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

        public static void QuitarServicioReserva(int idReserva, int idServicioAdicional)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    DELETE FROM dbo.ReservaServicio
                    WHERE IdReserva = @IdReserva
                      AND IdServicioAdicional = @IdServicioAdicional;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    cmd.Parameters.AddWithValue("@IdServicioAdicional", idServicioAdicional);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AplicarPromocionAReserva(int idReserva, int idPromocion)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int existe = 0;

                    using (SqlCommand cmdExiste = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM dbo.ReservaPromocion
                        WHERE IdReserva = @IdReserva
                          AND IdPromocion = @IdPromocion;", conn, transaction))
                    {
                        cmdExiste.Parameters.AddWithValue("@IdReserva", idReserva);
                        cmdExiste.Parameters.AddWithValue("@IdPromocion", idPromocion);
                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar());
                    }

                    if (existe > 0)
                        throw new Exception("Esta promoción ya fue aplicada a la reserva.");

                    decimal subtotalReserva = 0;
                    DateTime fechaEntrada = DateTime.Today;

                    using (SqlCommand cmdReserva = new SqlCommand(@"
                        SELECT Subtotal, FechaEntrada
                        FROM dbo.Reserva
                        WHERE IdReserva = @IdReserva;", conn, transaction))
                    {
                        cmdReserva.Parameters.AddWithValue("@IdReserva", idReserva);

                        using (SqlDataReader dr = cmdReserva.ExecuteReader())
                        {
                            if (!dr.Read())
                                throw new Exception("La reserva no existe.");

                            subtotalReserva = Convert.ToDecimal(dr["Subtotal"]);
                            fechaEntrada = Convert.ToDateTime(dr["FechaEntrada"]);
                        }
                    }

                    string tipoDescuento = "";
                    decimal valorDescuento = 0;

                    using (SqlCommand cmdPromo = new SqlCommand(@"
                        SELECT TipoDescuento, ValorDescuento
                        FROM dbo.Promocion
                        WHERE IdPromocion = @IdPromocion
                          AND Estado = 1
                          AND @FechaEntrada BETWEEN FechaInicio AND FechaFin;", conn, transaction))
                    {
                        cmdPromo.Parameters.AddWithValue("@IdPromocion", idPromocion);
                        cmdPromo.Parameters.AddWithValue("@FechaEntrada", fechaEntrada.Date);

                        using (SqlDataReader dr = cmdPromo.ExecuteReader())
                        {
                            if (!dr.Read())
                                throw new Exception("La promoción no está activa para la fecha de la reserva.");

                            tipoDescuento = dr["TipoDescuento"].ToString();
                            valorDescuento = Convert.ToDecimal(dr["ValorDescuento"]);
                        }
                    }

                    decimal descuentoAplicado;

                    if (tipoDescuento == "P")
                    {
                        descuentoAplicado = Math.Round(subtotalReserva * (valorDescuento / 100m), 2);
                    }
                    else
                    {
                        descuentoAplicado = valorDescuento > subtotalReserva ? subtotalReserva : valorDescuento;
                    }

                    using (SqlCommand cmdInsert = new SqlCommand(@"
                        INSERT INTO dbo.ReservaPromocion
                        (
                            IdReserva,
                            IdPromocion,
                            DescuentoAplicado
                        )
                        VALUES
                        (
                            @IdReserva,
                            @IdPromocion,
                            @DescuentoAplicado
                        );", conn, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@IdReserva", idReserva);
                        cmdInsert.Parameters.AddWithValue("@IdPromocion", idPromocion);
                        cmdInsert.Parameters.AddWithValue("@DescuentoAplicado", descuentoAplicado);
                        cmdInsert.ExecuteNonQuery();
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

        public static void QuitarPromocionReserva(int idReserva, int idPromocion)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    DELETE FROM dbo.ReservaPromocion
                    WHERE IdReserva = @IdReserva
                      AND IdPromocion = @IdPromocion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    cmd.Parameters.AddWithValue("@IdPromocion", idPromocion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void RecalcularPromocionesReserva(int idReserva)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE rp
                    SET DescuentoAplicado =
                        CASE
                            WHEN p.TipoDescuento = 'P'
                                THEN ROUND(r.Subtotal * (p.ValorDescuento / 100.0), 2)
                            ELSE
                                CASE
                                    WHEN p.ValorDescuento > r.Subtotal THEN r.Subtotal
                                    ELSE p.ValorDescuento
                                END
                        END
                    FROM dbo.ReservaPromocion rp
                    INNER JOIN dbo.Promocion p ON p.IdPromocion = rp.IdPromocion
                    INNER JOIN dbo.Reserva r ON r.IdReserva = rp.IdReserva
                    WHERE rp.IdReserva = @IdReserva;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}