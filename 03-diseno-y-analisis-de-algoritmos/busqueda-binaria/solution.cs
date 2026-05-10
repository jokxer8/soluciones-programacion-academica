using System;

class Program
{
    static void Main()
    {
        int[] numeros = { 2, 5, 8, 10, 15, 20, 25, 30, 40 };

        Console.WriteLine("Búsqueda Binaria");
        Console.WriteLine("Arreglo disponible:");

        foreach (int numero in numeros)
        {
            Console.Write(numero + " ");
        }

        Console.WriteLine();
        Console.Write("Ingrese el número que desea buscar: ");

        string? entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int numeroBuscado))
        {
            Console.WriteLine("Error: debe ingresar un número entero.");
            return;
        }

        int posicion = BuscarBinario(numeros, numeroBuscado);

        if (posicion != -1)
        {
            Console.WriteLine($"El número {numeroBuscado} fue encontrado en la posición {posicion}.");
        }
        else
        {
            Console.WriteLine("El número no fue encontrado.");
        }
    }

    static int BuscarBinario(int[] arreglo, int valorBuscado)
    {
        int izquierda = 0;
        int derecha = arreglo.Length - 1;

        while (izquierda <= derecha)
        {
            int centro = izquierda + (derecha - izquierda) / 2;

            if (arreglo[centro] == valorBuscado)
            {
                return centro;
            }

            if (valorBuscado < arreglo[centro])
            {
                derecha = centro - 1;
            }
            else
            {
                izquierda = centro + 1;
            }
        }

        return -1;
    }
}
