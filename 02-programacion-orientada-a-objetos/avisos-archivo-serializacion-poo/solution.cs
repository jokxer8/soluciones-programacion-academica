using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        GestorAvisos gestor = new GestorAvisos();
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine();
            Console.WriteLine("Sistema de avisos");
            Console.WriteLine();
            Console.WriteLine("1. Registrar aviso");
            Console.WriteLine("2. Mostrar avisos");
            Console.WriteLine("3. Guardar reporte en archivo TXT");
            Console.WriteLine("4. Serializar avisos en JSON");
            Console.WriteLine("5. Cargar avisos desde JSON");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    RegistrarAvisoDesdeConsola(gestor);
                    break;

                case "2":
                    gestor.MostrarAvisos();
                    break;

                case "3":
                    gestor.GuardarReporteTexto("avisos.txt");
                    break;

                case "4":
                    gestor.SerializarAvisos("avisos.json");
                    break;

                case "5":
                    gestor.DeserializarAvisos("avisos.json");
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("Programa finalizado.");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    static void RegistrarAvisoDesdeConsola(GestorAvisos gestor)
    {
        Console.Write("Ingrese el título del aviso: ");
        string? titulo = Console.ReadLine();

        Console.Write("Ingrese el mensaje del aviso: ");
        string? mensaje = Console.ReadLine();

        try
        {
            Aviso aviso = new Aviso(titulo, mensaje);
            gestor.AgregarAviso(aviso);

            Console.WriteLine("Aviso registrado correctamente.");
        }
        catch (ArgumentException error)
        {
            Console.WriteLine("Error al registrar el aviso:");
            Console.WriteLine(error.Message);
        }
    }
}

class Aviso
{
    public string Titulo { get; set; }
    public string Mensaje { get; set; }
    public DateTime FechaCreacion { get; set; }

    public Aviso()
    {
        Titulo = "";
        Mensaje = "";
        FechaCreacion = DateTime.Now;
    }

    public Aviso(string? titulo, string? mensaje)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new ArgumentException("El título no puede estar vacío.");
        }

        if (string.IsNullOrWhiteSpace(mensaje))
        {
            throw new ArgumentException("El mensaje no puede estar vacío.");
        }

        Titulo = titulo;
        Mensaje = mensaje;
        FechaCreacion = DateTime.Now;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine("Título: " + Titulo);
        Console.WriteLine("Mensaje: " + Mensaje);
        Console.WriteLine("Fecha: " + FechaCreacion);
        Console.WriteLine();
    }
}

class GestorAvisos
{
    private List<Aviso> avisos;

    public GestorAvisos()
    {
        avisos = new List<Aviso>();
    }

    public void AgregarAviso(Aviso aviso)
    {
        avisos.Add(aviso);
    }

    public void MostrarAvisos()
    {
        if (avisos.Count == 0)
        {
            Console.WriteLine("No hay avisos registrados.");
            return;
        }

        Console.WriteLine("Avisos registrados:");
        Console.WriteLine();

        foreach (Aviso aviso in avisos)
        {
            aviso.MostrarInformacion();
        }
    }

    public void GuardarReporteTexto(string rutaArchivo)
    {
        using StreamWriter escritor = new StreamWriter(rutaArchivo, false);

        escritor.WriteLine("Reporte de avisos");
        escritor.WriteLine("=================");
        escritor.WriteLine();

        foreach (Aviso aviso in avisos)
        {
            escritor.WriteLine("Título: " + aviso.Titulo);
            escritor.WriteLine("Mensaje: " + aviso.Mensaje);
            escritor.WriteLine("Fecha: " + aviso.FechaCreacion);
            escritor.WriteLine();
        }

        Console.WriteLine("Reporte guardado correctamente en el archivo: " + rutaArchivo);
    }

    public void SerializarAvisos(string rutaArchivo)
    {
        JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(avisos, opciones);

        File.WriteAllText(rutaArchivo, json);

        Console.WriteLine("Avisos serializados correctamente en el archivo: " + rutaArchivo);
    }

    public void DeserializarAvisos(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo))
        {
            Console.WriteLine("No existe el archivo: " + rutaArchivo);
            return;
        }

        string json = File.ReadAllText(rutaArchivo);

        List<Aviso>? avisosCargados = JsonSerializer.Deserialize<List<Aviso>>(json);

        if (avisosCargados == null)
        {
            Console.WriteLine("No se pudieron cargar los avisos.");
            return;
        }

        avisos = avisosCargados;

        Console.WriteLine("Avisos cargados correctamente desde el archivo: " + rutaArchivo);
    }
}
