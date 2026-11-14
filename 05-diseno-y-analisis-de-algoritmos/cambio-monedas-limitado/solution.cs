using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Cantidad de tipos de monedas: ");
        int cantidadTipos = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Ingrese los valores de las monedas separados por espacio:");
        string[] textoValores = Console.ReadLine()!.Split(' ');

        Console.WriteLine("Ingrese las cantidades disponibles separadas por espacio:");
        string[] textoCantidades = Console.ReadLine()!.Split(' ');

        Console.Write("Ingrese el valor objetivo X: ");
        int valorObjetivo = int.Parse(Console.ReadLine()!);

        int[] valores = new int[cantidadTipos];
        int[] cantidades = new int[cantidadTipos];

        for (int i = 0; i < cantidadTipos; i++)
        {
            valores[i] = int.Parse(textoValores[i]);
            cantidades[i] = int.Parse(textoCantidades[i]);
        }

        List<int> monedasDisponibles = new List<int>();

        for (int i = 0; i < cantidadTipos; i++)
        {
            for (int j = 0; j < cantidades[i]; j++)
            {
                monedasDisponibles.Add(valores[i]);
            }
        }

        int imposible = 1000000;
        int[] minimoMonedas = new int[valorObjetivo + 1];

        for (int i = 0; i <= valorObjetivo; i++)
        {
            minimoMonedas[i] = imposible;
        }

        minimoMonedas[0] = 0;

        foreach (int moneda in monedasDisponibles)
        {
            for (int suma = valorObjetivo; suma >= moneda; suma--)
            {
                if (minimoMonedas[suma - moneda] + 1 < minimoMonedas[suma])
                {
                    minimoMonedas[suma] = minimoMonedas[suma - moneda] + 1;
                }
            }
        }

        if (minimoMonedas[valorObjetivo] == imposible)
        {
            Console.WriteLine(-1);
        }
        else
        {
            Console.WriteLine("Mínimo número de monedas: " + minimoMonedas[valorObjetivo]);
        }
    }
}
