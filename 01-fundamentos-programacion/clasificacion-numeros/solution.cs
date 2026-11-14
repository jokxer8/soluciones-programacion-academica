using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese la cantidad de números: ");
        int cantidad = int.Parse(Console.ReadLine()!);

        if (cantidad <= 0)
        {
            Console.WriteLine("La cantidad debe ser mayor que cero.");
            return;
        }

        int positivos = 0;
        int negativos = 0;
        int ceros = 0;
        int pares = 0;
        int impares = 0;
        int sumaTotal = 0;

        for (int i = 1; i <= cantidad; i++)
        {
            Console.Write("Ingrese el número " + i + ": ");
            int numero = int.Parse(Console.ReadLine()!);

            sumaTotal += numero;

            if (numero > 0)
            {
                positivos++;
            }
            else if (numero < 0)
            {
                negativos++;
            }
            else
            {
                ceros++;
            }

            if (numero % 2 == 0)
            {
                pares++;
            }
            else
            {
                impares++;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Resultados:");
        Console.WriteLine("Positivos: " + positivos);
        Console.WriteLine("Negativos: " + negativos);
        Console.WriteLine("Ceros: " + ceros);
        Console.WriteLine("Pares: " + pares);
        Console.WriteLine("Impares: " + impares);
        Console.WriteLine("Suma total: " + sumaTotal);
    }
}
