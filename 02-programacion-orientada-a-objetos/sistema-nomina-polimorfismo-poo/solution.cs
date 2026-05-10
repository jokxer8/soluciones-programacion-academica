using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Nomina nomina = new Nomina();

        Empleado empleadoFijo = new EmpleadoFijo(
            "Ana Rodríguez",
            "Contabilidad",
            35000
        );

        Empleado empleadoPorHora = new EmpleadoPorHora(
            "Carlos Méndez",
            "Soporte Técnico",
            160,
            150
        );

        Empleado empleadoComision = new EmpleadoComision(
            "Laura Pérez",
            "Ventas",
            25000,
            85000,
            0.20
        );

        nomina.AgregarEmpleado(empleadoFijo);
        nomina.AgregarEmpleado(empleadoPorHora);
        nomina.AgregarEmpleado(empleadoComision);

        Console.WriteLine("Sistema de Nómina");
        Console.WriteLine();

        nomina.MostrarReporte();
    }
}

abstract class Empleado
{
    public string Nombre { get; private set; }
    public string Departamento { get; private set; }

    public Empleado(string nombre, string departamento)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre del empleado no puede estar vacío.");
        }

        if (string.IsNullOrWhiteSpace(departamento))
        {
            throw new ArgumentException("El departamento no puede estar vacío.");
        }

        Nombre = nombre;
        Departamento = departamento;
    }

    public abstract decimal CalcularSalario();

    public virtual void MostrarInformacion()
    {
        Console.WriteLine("Nombre: " + Nombre);
        Console.WriteLine("Departamento: " + Departamento);
        Console.WriteLine("Salario calculado: " + CalcularSalario());
    }
}

class EmpleadoFijo : Empleado
{
    public decimal SalarioMensual { get; private set; }

    public EmpleadoFijo(string nombre, string departamento, decimal salarioMensual)
        : base(nombre, departamento)
    {
        if (salarioMensual <= 0)
        {
            throw new ArgumentException("El salario mensual debe ser mayor que cero.");
        }

        SalarioMensual = salarioMensual;
    }

    public override decimal CalcularSalario()
    {
        return SalarioMensual;
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine("Empleado fijo:");
        base.MostrarInformacion();
    }
}

class EmpleadoPorHora : Empleado
{
    public int HorasTrabajadas { get; private set; }
    public decimal PagoPorHora { get; private set; }

    public EmpleadoPorHora(string nombre, string departamento, int horasTrabajadas, decimal pagoPorHora)
        : base(nombre, departamento)
    {
        if (horasTrabajadas < 0)
        {
            throw new ArgumentException("Las horas trabajadas no pueden ser negativas.");
        }

        if (pagoPorHora <= 0)
        {
            throw new ArgumentException("El pago por hora debe ser mayor que cero.");
        }

        HorasTrabajadas = horasTrabajadas;
        PagoPorHora = pagoPorHora;
    }

    public override decimal CalcularSalario()
    {
        return HorasTrabajadas * PagoPorHora;
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine("Empleado por hora:");
        base.MostrarInformacion();
        Console.WriteLine("Horas trabajadas: " + HorasTrabajadas);
        Console.WriteLine("Pago por hora: " + PagoPorHora);
    }
}

class EmpleadoComision : Empleado
{
    public decimal SalarioBase { get; private set; }
    public decimal VentasRealizadas { get; private set; }
    public decimal PorcentajeComision { get; private set; }

    public EmpleadoComision(
        string nombre,
        string departamento,
        decimal salarioBase,
        decimal ventasRealizadas,
        decimal porcentajeComision
    )
        : base(nombre, departamento)
    {
        if (salarioBase < 0)
        {
            throw new ArgumentException("El salario base no puede ser negativo.");
        }

        if (ventasRealizadas < 0)
        {
            throw new ArgumentException("Las ventas realizadas no pueden ser negativas.");
        }

        if (porcentajeComision < 0 || porcentajeComision > 1)
        {
            throw new ArgumentException("El porcentaje de comisión debe estar entre 0 y 1.");
        }

        SalarioBase = salarioBase;
        VentasRealizadas = ventasRealizadas;
        PorcentajeComision = porcentajeComision;
    }

    public override decimal CalcularSalario()
    {
        return SalarioBase + (VentasRealizadas * PorcentajeComision);
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine("Empleado por comisión:");
        base.MostrarInformacion();
        Console.WriteLine("Salario base: " + SalarioBase);
        Console.WriteLine("Ventas realizadas: " + VentasRealizadas);
        Console.WriteLine("Porcentaje de comisión: " + PorcentajeComision);
    }
}

class Nomina
{
    private List<Empleado> empleados;

    public Nomina()
    {
        empleados = new List<Empleado>();
    }

    public void AgregarEmpleado(Empleado empleado)
    {
        empleados.Add(empleado);
    }

    public decimal CalcularTotalNomina()
    {
        decimal total = 0;

        foreach (Empleado empleado in empleados)
        {
            total += empleado.CalcularSalario();
        }

        return total;
    }

    public void MostrarReporte()
    {
        if (empleados.Count == 0)
        {
            Console.WriteLine("No hay empleados registrados.");
            return;
        }

        foreach (Empleado empleado in empleados)
        {
            empleado.MostrarInformacion();
            Console.WriteLine();
        }

        Console.WriteLine("Total general de nómina: " + CalcularTotalNomina());
    }
}
