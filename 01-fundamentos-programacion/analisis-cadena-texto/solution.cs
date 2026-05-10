using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese una frase: ");
        string? texto = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(texto))
        {
            Console.WriteLine("Debe ingresar una frase válida.");
            return;
        }

        int cantidadCaracteres = texto.Length;
        int cantidadPalabras = ContarPalabras(texto);
        int cantidadVocales = ContarVocales(texto);
        int cantidadConsonantes = ContarConsonantes(texto);
        string textoInvertido = InvertirTexto(texto);

        Console.WriteLine();
        Console.WriteLine("Análisis del texto");
        Console.WriteLine("------------------");
        Console.WriteLine("Cantidad de caracteres: " + cantidadCaracteres);
        Console.WriteLine("Cantidad de palabras: " + cantidadPalabras);
        Console.WriteLine("Cantidad de vocales: " + cantidadVocales);
        Console.WriteLine("Cantidad de consonantes: " + cantidadConsonantes);
        Console.WriteLine("Texto invertido: " + textoInvertido);
    }

    static int ContarPalabras(string texto)
    {
        string[] palabras = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return palabras.Length;
    }

    static int ContarVocales(string texto)
    {
        int contador = 0;

        for (int i = 0; i < texto.Length; i++)
        {
            char caracter = char.ToLower(texto[i]);

            if (EsVocal(caracter))
            {
                contador++;
            }
        }

        return contador;
    }

    static int ContarConsonantes(string texto)
    {
        int contador = 0;

        for (int i = 0; i < texto.Length; i++)
        {
            char caracter = char.ToLower(texto[i]);

            if (char.IsLetter(caracter) && !EsVocal(caracter))
            {
                contador++;
            }
        }

        return contador;
    }

    static bool EsVocal(char caracter)
    {
        return caracter == 'a' ||
               caracter == 'e' ||
               caracter == 'i' ||
               caracter == 'o' ||
               caracter == 'u' ||
               caracter == 'á' ||
               caracter == 'é' ||
               caracter == 'í' ||
               caracter == 'ó' ||
               caracter == 'ú';
    }

    static string InvertirTexto(string texto)
    {
        string invertido = "";

        for (int i = texto.Length - 1; i >= 0; i--)
        {
            invertido += texto[i];
        }

        return invertido;
    }
}
