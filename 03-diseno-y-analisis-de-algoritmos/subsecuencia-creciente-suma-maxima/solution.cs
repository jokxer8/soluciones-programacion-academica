using System;

class Program
{
    static void Main()
    {
        Console.Write("Cantidad de elementos: ");
        int cantidadElementos = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Ingrese los elementos separados por espacio:");
        string[] partes = Console.ReadLine()!.Split(' ');

        int[] numeros = new int[cantidadElementos];

        for (int i = 0; i < cantidadElementos; i++)
        {
            numeros[i] = int.Parse(partes[i]);
        }

        int[] mejorSuma = new int[cantidadElementos];
        int[] indiceAnterior = new int[cantidadElementos];

        for (int i = 0; i < cantidadElementos; i++)
        {
            mejorSuma[i] = numeros[i];
            indiceAnterior[i] = -1;
        }

        int mejorIndice = 0;

        for (int i = 0; i < cantidadElementos; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (numeros[j] < numeros[i] && mejorSuma[j] + numeros[i] > mejorSuma[i])
                {
                    mejorSuma[i] = mejorSuma[j] + numeros[i];
                    indiceAnterior[i] = j;
                }
            }

            if (mejorSuma[i] > mejorSuma[mejorIndice])
            {
                mejorIndice = i;
            }
        }

        int[] subsecuencia = new int[cantidadElementos];
        int cantidadSubsecuencia = 0;
        int actual = mejorIndice;

        while (actual != -1)
        {
            subsecuencia[cantidadSubsecuencia] = numeros[actual];
            cantidadSubsecuencia++;
            actual = indiceAnterior[actual];
        }

        Console.WriteLine("Suma máxima: " + mejorSuma[mejorIndice]);
        Console.Write("Subsecuencia: ");

        for (int i = cantidadSubsecuencia - 1; i >= 0; i--)
        {
            Console.Write(subsecuencia[i]);

            if (i > 0)
            {
                Console.Write(" ");
            }
        }

        Console.WriteLine();
    }
}
