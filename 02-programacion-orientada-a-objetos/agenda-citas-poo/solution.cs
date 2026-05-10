using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        AgendaMedica agenda = new AgendaMedica();

        try
        {
            Cita cita1 = new Cita(
                DiaSemana.Lunes,
                9,
                30,
                "Consulta general"
            );

            Cita cita2 = new Cita(
                DiaSemana.Miercoles,
                14,
                0,
                "Revisión de resultados"
            );

            Cita cita3 = new Cita(
                DiaSemana.Viernes,
                16,
                45,
                "Seguimiento del paciente"
            );

            agenda.AgregarCita(cita1);
            agenda.AgregarCita(cita2);
            agenda.AgregarCita(cita3);

            Console.WriteLine("Agenda de citas");
            Console.WriteLine();

            agenda.MostrarCitas();
        }
        catch (ArgumentException error)
        {
            Console.WriteLine("Error al registrar la cita:");
            Console.WriteLine(error.Message);
        }
    }
}

enum DiaSemana
{
    Lunes,
    Martes,
    Miercoles,
    Jueves,
    Viernes,
    Sabado,
    Domingo
}

class Cita
{
    public DiaSemana Dia { get; private set; }
    public int Hora { get; private set; }
    public int Minuto { get; private set; }
    public string Descripcion { get; private set; }

    public Cita(DiaSemana dia, int hora, int minuto, string descripcion)
    {
        if (hora < 0 || hora > 23)
        {
            throw new ArgumentException("La hora debe estar entre 0 y 23.");
        }

        if (minuto < 0 || minuto > 59)
        {
            throw new ArgumentException("Los minutos deben estar entre 0 y 59.");
        }

        if (string.IsNullOrWhiteSpace(descripcion))
        {
            throw new ArgumentException("La descripción no puede estar vacía.");
        }

        Dia = dia;
        Hora = hora;
        Minuto = minuto;
        Descripcion = descripcion;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"{Dia} - {Hora:D2}:{Minuto:D2} - {Descripcion}");
    }
}

class AgendaMedica
{
    private List<Cita> citas;

    public AgendaMedica()
    {
        citas = new List<Cita>();
    }

    public void AgregarCita(Cita cita)
    {
        citas.Add(cita);
    }

    public void MostrarCitas()
    {
        if (citas.Count == 0)
        {
            Console.WriteLine("No hay citas registradas.");
            return;
        }

        Console.WriteLine("Citas registradas:");
        Console.WriteLine();

        foreach (Cita cita in citas)
        {
            cita.MostrarInformacion();
        }
    }
}
