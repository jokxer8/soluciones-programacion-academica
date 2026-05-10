using System;

class Program
{
    static void Main()
    {
        Bateria bateria = new Bateria(
            "EB-BA546ABY",
            5000,
            "Li-Ion"
        );

        Pantalla pantalla = new Pantalla(
            6.4,
            16000000
        );

        TelefonoMovil telefono = new TelefonoMovil(
            "Galaxy A54",
            "Samsung",
            35000,
            "Kelvin César Del Castillo",
            bateria,
            pantalla
        );

        telefono.MostrarInformacion();
    }
}

class TelefonoMovil
{
    public string Modelo { get; set; }
    public string Fabricante { get; set; }
    public decimal Precio { get; set; }
    public string Propietario { get; set; }
    public Bateria Bateria { get; set; }
    public Pantalla Pantalla { get; set; }

    public TelefonoMovil(
        string modelo,
        string fabricante,
        decimal precio,
        string propietario,
        Bateria bateria,
        Pantalla pantalla
    )
    {
        Modelo = modelo;
        Fabricante = fabricante;
        Precio = precio;
        Propietario = propietario;
        Bateria = bateria;
        Pantalla = pantalla;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine("Información del teléfono móvil");
        Console.WriteLine();

        Console.WriteLine("Modelo: " + Modelo);
        Console.WriteLine("Fabricante: " + Fabricante);
        Console.WriteLine("Precio: " + Precio);
        Console.WriteLine("Propietario: " + Propietario);
        Console.WriteLine();

        Console.WriteLine("Batería:");
        Bateria.MostrarInformacion();
        Console.WriteLine();

        Console.WriteLine("Pantalla:");
        Pantalla.MostrarInformacion();
    }
}

class Bateria
{
    public string Modelo { get; set; }
    public int CapacidadMah { get; set; }
    public string Tipo { get; set; }

    public Bateria(string modelo, int capacidadMah, string tipo)
    {
        Modelo = modelo;
        CapacidadMah = capacidadMah;
        Tipo = tipo;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine("Modelo: " + Modelo);
        Console.WriteLine("Capacidad: " + CapacidadMah + " mAh");
        Console.WriteLine("Tipo: " + Tipo);
    }
}

class Pantalla
{
    public double TamanoPulgadas { get; set; }
    public int CantidadColores { get; set; }

    public Pantalla(double tamanoPulgadas, int cantidadColores)
    {
        TamanoPulgadas = tamanoPulgadas;
        CantidadColores = cantidadColores;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine("Tamaño: " + TamanoPulgadas + " pulgadas");
        Console.WriteLine("Colores: " + CantidadColores);
    }
}
