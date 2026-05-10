using System;

class Program
{
    static void Main()
    {
        int[] numeros = { 8, 3, 1, 7, 0, 10, 2 };

        Console.WriteLine("Ordenamiento Rápido - QuickSort");
        Console.WriteLine("Arreglo original:");

        MostrarArreglo(numeros);

        QuickSort(numeros, 0, numeros.Length - 1);

        Console.WriteLine("Arreglo ordenado:");

        MostrarArreglo(numeros);
    }

    static void QuickSort(int[] arreglo, int izquierda, int derecha)
    {
        if (izquierda < derecha)
        {
            int indicePivote = Particionar(arreglo, izquierda, derecha);

            QuickSort(arreglo, izquierda, indicePivote - 1);
            QuickSort(arreglo, indicePivote + 1, derecha);
        }
    }

    static int Particionar(int[] arreglo, int izquierda, int derecha)
    {
        int pivote = arreglo[derecha];
        int indiceMenor = izquierda - 1;

        for (int i = izquierda; i < derecha; i++)
        {
            if (arreglo[i] <= pivote)
            {
                indiceMenor++;
                Intercambiar(arreglo, indiceMenor, i);
            }
        }

        Intercambiar(arreglo, indiceMenor + 1, derecha);

        return indiceMenor + 1;
    }

    static void Intercambiar(int[] arreglo, int posicionA, int posicionB)
    {
        int temporal = arreglo[posicionA];
        arreglo[posicionA] = arreglo[posicionB];
        arreglo[posicionB] = temporal;
    }

    static void MostrarArreglo(int[] arreglo)
    {
        foreach (int numero in arreglo)
        {
            Console.Write(numero + " ");
        }

        Console.WriteLine();
    }
}
