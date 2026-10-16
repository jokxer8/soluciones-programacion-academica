using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using HotelReservas;

class FunctionalTests
{
    static List<string> results = new List<string>();
    static int failures;
    static int serial;
    static DateTime arrival = new DateTime(2035, 6, 10);
    static object Scalar(string sql)
    {
        using (var c = Database.GetConnection()) { c.Open(); using (var q = new SqlCommand(sql,c)) return q.ExecuteScalar(); }
    }
    static int Number(string sql) { return Convert.ToInt32(Scalar(sql)); }
    static void Exec(string sql) { Scalar(sql); }
    static void Assert(bool condition, string message) { if (!condition) throw new Exception(message); }
    static void Reject(Action action, string expected)
    {
        try { action(); } catch (Exception ex) {
            Assert(ex.Message.IndexOf(expected, StringComparison.OrdinalIgnoreCase) >= 0, "Error inesperado: " + ex.Message);
            return;
        }
        throw new Exception("Se acepto una operacion que debia rechazarse: " + expected);
    }
    static void Test(string name, Action action)
    {
        try { action(); results.Add("PASS\t"+name+"\tOK"); Console.WriteLine("PASS "+name); }
        catch (Exception ex) { failures++; var e=ex.GetBaseException(); results.Add("FAIL\t"+name+"\t"+e.Message.Replace('\r',' ').Replace('\n',' ')); Console.WriteLine("FAIL "+name+": "+e.Message); }
    }
    static int Room()
    {
        return Number("INSERT dbo.Habitacion (IdSucursal,IdTipoHabitacion,IdEstadoHabitacion,Numero,Piso,PrecioPorNoche,Descripcion) SELECT TOP 1 IdSucursal,IdTipoHabitacion,1,'QA"+(++serial)+"',1,1000,'Prueba automatica' FROM dbo.Habitacion; SELECT CONVERT(int,SCOPE_IDENTITY());");
    }
    static int Reserve(int room, int state)
    {
        Database.InsertarReserva(1,room,1,state,arrival,arrival.AddDays(2),1,"Prueba automatica");
        return Number("SELECT MAX(IdReserva) FROM dbo.Reserva WHERE IdHabitacion="+room);
    }
    static int State(string name) { return Number("SELECT IdEstadoReserva FROM dbo.EstadoReserva WHERE NombreEstado='"+name+"'"); }
    static string RoomState(int room) { return Convert.ToString(Scalar("SELECT e.NombreEstado FROM dbo.Habitacion h JOIN dbo.EstadoHabitacion e ON e.IdEstadoHabitacion=h.IdEstadoHabitacion WHERE h.IdHabitacion="+room)); }
    static void Update(int reservation,int room,int state) { Database.ActualizarReserva(reservation,1,room,1,state,arrival,arrival.AddDays(2),1,"Actualizada"); }
    [STAThread]
    static int Main(string[] args)
    {
        Test("Instalacion: restricciones y objetos", delegate {
            Assert(Number("SELECT COUNT(*) FROM sys.tables") == 21,"Se esperaban 21 tablas antes de inicializar modulos");
            Assert(Number("SELECT COUNT(*) FROM sys.foreign_keys WHERE is_disabled=1 OR is_not_trusted=1")==0,"Claves no confiables");
            Assert(Number("SELECT COUNT(*) FROM sys.check_constraints WHERE is_disabled=1 OR is_not_trusted=1")==0,"Restricciones no confiables");
            Assert(Number("SELECT COUNT(*) FROM sys.procedures WHERE name IN ('sp_InsertarReserva','sp_GenerarFactura','sp_ValidarLogin')")==3,"Faltan procedimientos");
            Assert(Number("SELECT COUNT(*) FROM sys.triggers WHERE parent_class=1")==2,"Faltan triggers");
        });
        Test("Inicializacion de notificaciones y valoraciones idempotente",delegate {
            NotificacionData.EnsureTableExists(); RatingData.EnsureTableExists();
            NotificacionData.EnsureTableExists(); RatingData.EnsureTableExists();
            Assert(Number("SELECT COUNT(*) FROM sys.tables")==23,"Faltan tablas de modulos");
        });
        foreach(var account in new[]{new[]{"admin","123456","ADMIN"},new[]{"recepcion2","4321","RECEPCION"},new[]{"empleado1","1234","EMPLEADO"},new[]{"soporte1","123456","SOPORTE"}}) {
            var a=account;
            Test("Login " + a[2],delegate { Assert(Database.ValidarLogin(a[0],a[1]),"Login rechazado"); Assert(SesionUsuario.Rol==a[2],"Rol incorrecto"); });
        }
        Test("Login invalido y vacio",delegate { SesionUsuario.Cerrar(); Assert(!Database.ValidarLogin("admin","incorrecta"),"Clave invalida aceptada"); Assert(!Database.ValidarLogin("",""),"Vacio aceptado"); });
        Test("Usuarios del seed tienen claves compatibles",delegate {
            Assert(Number("SELECT COUNT(*) FROM dbo.Usuarios WHERE LEN(Clave)<>64")==0,"Quedaron claves sin migrar");
            string user=Convert.ToString(Scalar("SELECT TOP 1 NombreUsuario FROM dbo.Usuarios WHERE IdEmpleado>4 ORDER BY IdUsuario"));
            Assert(!string.IsNullOrEmpty(user) && Database.ValidarLogin(user,"1234"),"No accede usuario ampliado");
        });
        Test("Login inactivo",delegate { Exec("UPDATE dbo.Usuarios SET Estado=0 WHERE NombreUsuario='admin'"); try { Assert(!Database.ValidarLogin("admin","123456"),"Inactivo aceptado"); } finally { Exec("UPDATE dbo.Usuarios SET Estado=1 WHERE NombreUsuario='admin'"); } });
        Test("Cierre de sesion",delegate { Database.ValidarLogin("admin","123456"); SesionUsuario.Cerrar(); Assert(SesionUsuario.IdUsuario==0 && SesionUsuario.Rol=="","Sesion no borrada"); });
        Test("Login fallido borra sesion anterior",delegate { Database.ValidarLogin("admin","123456"); Assert(!Database.ValidarLogin("admin","incorrecta"),"Clave aceptada"); Assert(SesionUsuario.IdUsuario==0,"Quedo una sesion privilegiada previa"); });
        Test("CRUD reserva, consulta y cancelacion",delegate {
            int h=Room(), r=Reserve(h,State("PENDIENTE"));
            Assert(Database.ObtenerReservas().Select("IdReserva="+r).Length==1,"Reserva no consultable");
            Update(r,h,State("CONFIRMADA")); Assert(RoomState(h)=="OCUPADA","Habitacion no ocupada");
            Update(r,h,State("CANCELADA")); Assert(RoomState(h)=="DISPONIBLE","Habitacion no liberada");
            Database.EliminarReserva(r); Assert(Number("SELECT COUNT(*) FROM dbo.Reserva WHERE IdReserva="+r)==0,"Reserva no eliminada");
        });
        Test("Fechas invalidas",delegate { Reject(delegate { Database.InsertarReserva(1,Room(),1,State("PENDIENTE"),arrival,arrival,1,""); },"fecha"); });
        Test("Referencias inexistentes",delegate { Reject(delegate { Database.InsertarReserva(2147483647,Room(),1,State("PENDIENTE"),arrival,arrival.AddDays(2),1,""); },"FOREIGN KEY"); });
        Test("Importes negativos rechazados",delegate { int h=Room(); Reject(delegate { Exec("UPDATE dbo.Habitacion SET PrecioPorNoche=-1 WHERE IdHabitacion="+h); },"CHECK"); });
        Test("Solapamiento al crear y modificar",delegate {
            int h=Room(); Reserve(h,State("CONFIRMADA"));
            Reject(delegate { Reserve(h,State("PENDIENTE")); },"rango de fechas");
            int other=Room(), r=Reserve(other,State("PENDIENTE"));
            Reject(delegate { Update(r,h,State("CONFIRMADA")); },"rango de fechas");
            Assert(Number("SELECT IdHabitacion FROM dbo.Reserva WHERE IdReserva="+r)==other,"Se modifico reserva rechazada");
        });
        Test("Cancelar no libera otra reserva confirmada",delegate {
            int h=Room(), r=Reserve(h,State("CONFIRMADA"));
            Database.InsertarReserva(1,h,1,State("CONFIRMADA"),arrival.AddDays(5),arrival.AddDays(7),1,"");
            Update(r,h,State("CANCELADA")); Assert(RoomState(h)=="OCUPADA","Se libero habitacion con otra reserva confirmada");
        });
        Test("Trasladar reserva conserva ocupacion anterior",delegate {
            int oldRoom=Room(), newRoom=Room(), r=Reserve(oldRoom,State("CONFIRMADA"));
            Database.InsertarReserva(1,oldRoom,1,State("CONFIRMADA"),arrival.AddDays(5),arrival.AddDays(7),1,"");
            Update(r,newRoom,State("CONFIRMADA"));
            Assert(RoomState(oldRoom)=="OCUPADA" && RoomState(newRoom)=="OCUPADA","Estado incorrecto al trasladar");
        });
        Test("Mantenimiento prevalece y se recalcula al terminar",delegate {
            int h=Room(); Reserve(h,State("CONFIRMADA"));
            int m=Number("INSERT dbo.Mantenimiento(IdHabitacion,IdEmpleado,Motivo,Costo,Estado) VALUES("+h+",1,'QA',10,'PENDIENTE'); SELECT CONVERT(int,SCOPE_IDENTITY());");
            Assert(RoomState(h)=="MANTENIMIENTO","No entro a mantenimiento");
            Exec("UPDATE dbo.Mantenimiento SET Estado='FINALIZADO' WHERE IdMantenimiento="+m);
            Assert(RoomState(h)=="OCUPADA","Terminar mantenimiento libero otra reserva");
        });
        Test("Check-in rechaza reserva pendiente",delegate { int r=Reserve(Room(),State("PENDIENTE")); Reject(delegate{CheckInOutData.RegistrarCheckIn(r,"");},"CONFIRMADA"); });
        Test("Check-out sin check-in rechazado",delegate { int r=Reserve(Room(),State("CONFIRMADA")); Reject(delegate{CheckInOutData.RegistrarCheckOut(r,"");},"No existe un check-in"); });
        Test("Check-in usa IdEmpleado y no IdUsuario",delegate {
            int employee=Number("INSERT dbo.Empleado(IdSucursal,Nombre,Apellido,Cedula,FechaIngreso,Cargo,Estado) SELECT TOP 1 IdSucursal,'Operador','QA','QA-EMP-UNICO',GETDATE(),'Recepcion',1 FROM dbo.Empleado; SELECT CONVERT(int,SCOPE_IDENTITY());");
            Exec("SET IDENTITY_INSERT dbo.Usuarios ON; INSERT dbo.Usuarios(IdUsuario,IdEmpleado,NombreUsuario,Clave,NombreCompleto,IdRol,Estado) VALUES(1000,"+employee+",'qaoperator',LOWER(CONVERT(varchar(64),HASHBYTES('SHA2_256','qatest'),2)),'Operador QA',1,1); SET IDENTITY_INSERT dbo.Usuarios OFF;");
            Assert(Database.ValidarLogin("qaoperator","qatest"),"No accedio operador QA");
            int r=Reserve(Room(),State("CONFIRMADA")); CheckInOutData.RegistrarCheckIn(r,"");
            Assert(Number("SELECT IdEmpleadoCheckIn FROM dbo.CheckInOut WHERE IdReserva="+r)==employee,"Se guardo identificador de usuario en lugar de empleado");
            CheckInOutData.RegistrarCheckOut(r,"");
            Assert(Number("SELECT IdEmpleadoCheckOut FROM dbo.CheckInOut WHERE IdReserva="+r)==employee,"Empleado de salida incorrecto");
            Database.ValidarLogin("admin","123456");
        });
        Test("Ciclo check-in/out, factura y pagos",delegate {
            Database.ValidarLogin("admin","123456");
            int h=Room(),r=Reserve(h,State("CONFIRMADA"));
            Exec("INSERT dbo.ReservaServicio(IdReserva,IdServicioAdicional,Cantidad,PrecioUnitario,Subtotal) VALUES("+r+",1,2,50,100); UPDATE dbo.Reserva SET Descuento=25 WHERE IdReserva="+r+"; INSERT dbo.ReservaPromocion(IdReserva,IdPromocion,DescuentoAplicado) VALUES("+r+",1,75)");
            CheckInOutData.RegistrarCheckIn(r,"Entrada QA");
            Reject(delegate{CheckInOutData.RegistrarCheckIn(r,"");},"ya tiene un check-in");
            CheckInOutData.RegistrarCheckOut(r,"Salida QA");
            Assert(Convert.ToString(Scalar("SELECT Estado FROM dbo.CheckInOut WHERE IdReserva="+r))=="CHECKED_OUT","Check-out no persistido");
            int f=Number("SELECT IdFactura FROM dbo.Factura WHERE IdReserva="+r);
            Assert(Convert.ToDecimal(Scalar("SELECT Total FROM dbo.Factura WHERE IdFactura="+f))==2360m,"Total esperado (2000+100-25-75)*1.18=2360");
            Reject(delegate{Database.GenerarFactura(r,18);},"Ya existe una factura");
            Assert(Number("SELECT COUNT(*) FROM dbo.Factura WHERE IdReserva="+r)==1,"Factura duplicada");
            Database.RegistrarPago(f,1,100,"QA",""); Assert(Database.ObtenerTotalPagadoFactura(f)==100m,"Pago parcial incorrecto");
            Reject(delegate{Database.RegistrarPago(f,1,3000,"","");},"excede");
            Reject(delegate{Database.RegistrarPago(f,1,-1,"","");},"mayor que cero");
            Database.RegistrarPago(f,1,2260,"QA","");
            Assert(Convert.ToString(Scalar("SELECT Estado FROM dbo.Factura WHERE IdFactura="+f))=="PAGADA","Factura sin saldar");
        });
        Test("Check-out finaliza y libera habitacion",delegate {
            Database.ValidarLogin("admin","123456"); int h=Room(),r=Reserve(h,State("CONFIRMADA"));
            CheckInOutData.RegistrarCheckIn(r,""); CheckInOutData.RegistrarCheckOut(r,"");
            Assert(Number("SELECT IdEstadoReserva FROM dbo.Reserva WHERE IdReserva="+r)==State("FINALIZADA"),"Reserva sigue confirmada tras check-out");
            Assert(RoomState(h)=="DISPONIBLE","Habitacion sigue ocupada tras check-out");
        });
        Test("Rollback ante fallo intermedio de facturacion",delegate {
            Database.ValidarLogin("admin","123456"); int r=Reserve(Room(),State("CONFIRMADA")); CheckInOutData.RegistrarCheckIn(r,"");
            Exec("CREATE TRIGGER dbo.QA_FalloFactura ON dbo.Factura AFTER INSERT AS THROW 51000,'Fallo QA deliberado',1;");
            try { Reject(delegate{CheckInOutData.RegistrarCheckOut(r,"");},"Fallo QA deliberado"); }
            finally { Exec("DROP TRIGGER dbo.QA_FalloFactura"); }
            Assert(Convert.ToString(Scalar("SELECT Estado FROM dbo.CheckInOut WHERE IdReserva="+r))=="CHECKED_IN","Check-out parcial persistido");
            Assert(Number("SELECT COUNT(*) FROM dbo.Factura WHERE IdReserva="+r)==0,"Factura parcial persistida");
        });
        Test("ITBIS configurable y redondeo",delegate {
            int h=Room(); Exec("UPDATE dbo.Habitacion SET PrecioPorNoche=1000.01 WHERE IdHabitacion="+h);
            int r=Reserve(h,State("CONFIRMADA")); Database.ActualizarConfiguracion("ITBIS_PORCENTAJE","17.50");
            try {
                CheckInOutData.RegistrarCheckIn(r,""); CheckInOutData.RegistrarCheckOut(r,"");
                Assert(Convert.ToDecimal(Scalar("SELECT Impuesto FROM dbo.Factura WHERE IdReserva="+r))==350.00m,"Impuesto mal redondeado");
                Assert(Convert.ToDecimal(Scalar("SELECT Total FROM dbo.Factura WHERE IdReserva="+r))==2350.02m,"Total incorrecto");
            } finally { Database.ActualizarConfiguracion("ITBIS_PORCENTAJE","18"); }
        });
        Test("Pago con referencia inexistente no altera saldo",delegate {
            int r=Reserve(Room(),State("CONFIRMADA")); Database.GenerarFactura(r,18);
            int f=Number("SELECT IdFactura FROM dbo.Factura WHERE IdReserva="+r);
            Reject(delegate {Database.RegistrarPago(f,2147483647,100,"","");},"FOREIGN KEY");
            Assert(Database.ObtenerTotalPagadoFactura(f)==0,"Pago parcial tras fallo");
            Reject(delegate {Database.RegistrarPago(f,1,0,"","");},"mayor que cero");
        });
        Test("Reportes: importes y filtros sin datos",delegate {
            var dt=ReportesData.ObtenerResumenOperativo(new DateTime(2035,1,1),new DateTime(2035,12,31));
            Assert(Convert.ToInt32(dt.Rows[0]["TotalReservas"])==Number("SELECT COUNT(*) FROM dbo.Reserva WHERE FechaReserva>='20350101' AND FechaReserva<'20360101'"),"Conteo distinto al SQL de control");
            var empty=ReportesData.ObtenerDetalleOperativo(new DateTime(1900,1,1),new DateTime(1900,1,2)); Assert(empty.Rows.Count==0,"Filtro vacio devuelve datos");
            var monthly=ReportesData.ObtenerFacturacionMensual(DateTime.Today.Year);
            decimal sum=monthly.Rows.Cast<DataRow>().Sum(row=>Convert.ToDecimal(row["Total"]));
            Assert(sum==Convert.ToDecimal(Scalar("SELECT ISNULL(SUM(Total),0) FROM dbo.Factura WHERE Estado<>'ANULADA' AND YEAR(FechaFactura)=YEAR(GETDATE())")),"Facturacion mensual difiere del control");
        });
        foreach(var method in typeof(ReportesData).GetMethods(BindingFlags.Public|BindingFlags.Static)) {
            var m=method;
            Test("Reporte SQL " + m.Name,delegate {
                object[] values=m.GetParameters().Select(p=>p.ParameterType==typeof(int)?(object)2026:(p.Name=="desde"?(object)new DateTime(2026,1,1):new DateTime(2026,12,31))).ToArray();
                Assert(m.Invoke(null,values) is DataTable,"Resultado no tabular");
            });
        }
        Test("Reportes sin historial anual",delegate {
            Exec("DELETE dbo.Pago; DELETE dbo.Factura; DELETE dbo.CheckInOut; DELETE dbo.ReservaServicio; DELETE dbo.ReservaPromocion; DELETE dbo.Rating; DELETE dbo.Reserva; DELETE dbo.Mantenimiento;");
            var years=ReportesData.ObtenerAniosDisponibles();
            Assert(years.Rows.Count==1 && Convert.ToInt32(years.Rows[0]["Anio"])==DateTime.Today.Year,"No devuelve anio actual sin datos");
        });
        Test("Inicio de formularios con SQL",delegate {
            using(var login=new LoginForm()) { var handle=login.Handle; }
            Database.ValidarLogin("admin","123456"); using(var main=new MainForm()) { var handle=main.Handle; }
        });
        Directory.CreateDirectory(args[0]);
        File.WriteAllLines(Path.Combine(args[0],"functional-results.tsv"),new[]{"Estado\tPrueba\tDetalle"}.Concat(results));
        File.WriteAllText(Path.Combine(args[0],"summary.json"),"{\"passed\":"+(results.Count-failures)+",\"failed\":"+failures+",\"total\":"+results.Count+"}");
        Console.WriteLine("RESULTADO: "+(results.Count-failures)+" correctas, "+failures+" fallidas.");
        return failures==0?0:1;
    }
}
