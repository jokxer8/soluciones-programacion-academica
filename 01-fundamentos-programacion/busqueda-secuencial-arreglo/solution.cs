using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Búsqueda Secuencial en un Arreglo");
        Console.WriteLine();

        int cantidad = LeerEnteroPositivo("Ingrese la cantidad de elementos: ");

        int[] numeros = new int[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            numeros[i] = LeerEntero("Ingrese el número " + (i + 1) + ": ");
        }

        Console.WriteLine();
        MostrarArreglo(numeros);

        Console.WriteLine();

        int numeroBuscado = LeerEntero("Ingrese el número que desea buscar: ");

        int posicion = BuscarSecuencial(numeros, numeroBuscado);

        Console.WriteLine();

        if (posicion != -1)
        {
            Console.WriteLine("El número " + numeroBuscado + " fue encontrado en la posición " + posicion + ".");
        }
        else
        {
            Console.WriteLine("El número no fue encontrado.");
        }
    }

    static int LeerEntero(string mensaje)
    {
        int numero;

        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out numero))
            {
                return numero;
            }

            Console.WriteLine("Entrada inválida. Debe ingresar un número entero.");
        }
    }

    static int LeerEnteroPositivo(string mensaje)
    {
        int numero;

        while (true)
        {
            numero = LeerEntero(mensaje);

            if (numero > 0)
            {
                return numero;
            }

            Console.WriteLine("El número debe ser mayor que cero.");
        }
    }

    static int BuscarSecuencial(int[] arreglo, int valorBuscado)
    {
        for (int i = 0; i < arreglo.Length; i++)
        {
            if (arreglo[i] == valorBuscado)
            {
                return i;
            }
        }

        return -1;
    }

    static void MostrarArreglo(int[] arreglo)
    {
        Console.WriteLine("Elementos del arreglo:");

        for (int i = 0; i < arreglo.Length; i++)
        {
            Console.Write(arreglo[i]);

            if (i < arreglo.Length - 1)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine();
    }
}
