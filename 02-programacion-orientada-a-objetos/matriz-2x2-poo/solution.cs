using System;

class Program
{
    static void Main()
    {
        Matriz2x2 matriz = new Matriz2x2(4, 7, 2, 6);

        Console.WriteLine("Matriz original:");
        matriz.Mostrar();

        double determinante = matriz.CalcularDeterminante();

        Console.WriteLine();
        Console.WriteLine("Determinante: " + determinante);

        if (matriz.TieneInversa())
        {
            Console.WriteLine();
            Console.WriteLine("La matriz tiene inversa.");

            Matriz2x2 inversa = matriz.CalcularInversa();

            Console.WriteLine();
            Console.WriteLine("Matriz inversa:");
            inversa.Mostrar();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("La matriz no tiene inversa porque su determinante es cero.");
        }
    }
}

class Matriz2x2
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }
    public double D { get; set; }

    public Matriz2x2()
    {
        A = 0;
        B = 0;
        C = 0;
        D = 0;
    }

    public Matriz2x2(double a, double b, double c, double d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public double CalcularDeterminante()
    {
        return (A * D) - (B * C);
    }

    public bool TieneInversa()
    {
        return CalcularDeterminante() != 0;
    }

    public Matriz2x2 CalcularInversa()
    {
        double determinante = CalcularDeterminante();

        if (determinante == 0)
        {
            throw new InvalidOperationException("La matriz no tiene inversa porque su determinante es cero.");
        }

        double nuevoA = D / determinante;
        double nuevoB = -B / determinante;
        double nuevoC = -C / determinante;
        double nuevoD = A / determinante;

        return new Matriz2x2(nuevoA, nuevoB, nuevoC, nuevoD);
    }

    public void Mostrar()
    {
        Console.WriteLine("| " + A + "  " + B + " |");
        Console.WriteLine("| " + C + "  " + D + " |");
    }
}
