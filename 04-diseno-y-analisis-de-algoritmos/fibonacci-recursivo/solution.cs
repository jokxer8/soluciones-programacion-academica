using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Cálculo de Fibonacci usando recursividad");
        Console.Write("Ingrese la posición de Fibonacci: ");

        string? entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int posicion) || posicion < 0)
        {
            Console.WriteLine("Error: debe ingresar un número entero mayor o igual a cero.");
            return;
        }

        long[] memoria = new long[posicion + 1];

        for (int i = 0; i < memoria.Length; i++)
        {
            memoria[i] = -1;
        }

        long resultado = CalcularFibonacci(posicion, memoria);

        Console.WriteLine($"El número Fibonacci en la posición {posicion} es: {resultado}");
    }

    static long CalcularFibonacci(int n, long[] memoria)
    {
        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        if (memoria[n] != -1)
        {
            return memoria[n];
        }

        memoria[n] = CalcularFibonacci(n - 1, memoria) + CalcularFibonacci(n - 2, memoria);

        return memoria[n];
    }
}
