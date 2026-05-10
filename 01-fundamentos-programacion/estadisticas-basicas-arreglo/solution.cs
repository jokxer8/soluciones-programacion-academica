using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese la cantidad de números: ");
        int cantidad = int.Parse(Console.ReadLine()!);

        if (cantidad <= 0)
        {
            Console.WriteLine("La cantidad de números debe ser mayor que cero.");
            return;
        }

        int[] numeros = new int[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            Console.Write("Ingrese el número " + (i + 1) + ": ");
            numeros[i] = int.Parse(Console.ReadLine()!);
        }

        int suma = CalcularSuma(numeros);
        double promedio = CalcularPromedio(numeros);
        int mayor = EncontrarMayor(numeros);
        int menor = EncontrarMenor(numeros);

        Console.WriteLine();
        Console.WriteLine("Resultados");
        Console.WriteLine("----------");
        Console.WriteLine("Suma total: " + suma);
        Console.WriteLine("Promedio: " + promedio);
        Console.WriteLine("Número mayor: " + mayor);
        Console.WriteLine("Número menor: " + menor);
    }

    static int CalcularSuma(int[] numeros)
    {
        int suma = 0;

        for (int i = 0; i < numeros.Length; i++)
        {
            suma += numeros[i];
        }

        return suma;
    }

    static double CalcularPromedio(int[] numeros)
    {
        int suma = CalcularSuma(numeros);
        return (double)suma / numeros.Length;
    }

    static int EncontrarMayor(int[] numeros)
    {
        int mayor = numeros[0];

        for (int i = 1; i < numeros.Length; i++)
        {
            if (numeros[i] > mayor)
            {
                mayor = numeros[i];
            }
        }

        return mayor;
    }

    static int EncontrarMenor(int[] numeros)
    {
        int menor = numeros[0];

        for (int i = 1; i < numeros.Length; i++)
        {
            if (numeros[i] < menor)
            {
                menor = numeros[i];
            }
        }

        return menor;
    }
}
