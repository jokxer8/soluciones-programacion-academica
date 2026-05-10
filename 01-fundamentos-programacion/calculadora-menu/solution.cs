using System;

class Program
{
    static void Main()
    {
        bool continuar = true;

        while (continuar)
        {
            MostrarMenu();

            Console.Write("Opción: ");
            string? opcion = Console.ReadLine();

            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    RealizarOperacion("suma");
                    break;

                case "2":
                    RealizarOperacion("resta");
                    break;

                case "3":
                    RealizarOperacion("multiplicacion");
                    break;

                case "4":
                    RealizarOperacion("division");
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("Programa finalizado.");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("Calculadora con Menú Interactivo");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("1. Sumar");
        Console.WriteLine("2. Restar");
        Console.WriteLine("3. Multiplicar");
        Console.WriteLine("4. Dividir");
        Console.WriteLine("0. Salir");
    }

    static void RealizarOperacion(string tipoOperacion)
    {
        Console.Write("Ingrese el primer número: ");
        double numero1 = double.Parse(Console.ReadLine()!);

        Console.Write("Ingrese el segundo número: ");
        double numero2 = double.Parse(Console.ReadLine()!);

        double resultado;

        switch (tipoOperacion)
        {
            case "suma":
                resultado = Sumar(numero1, numero2);
                Console.WriteLine("Resultado: " + resultado);
                break;

            case "resta":
                resultado = Restar(numero1, numero2);
                Console.WriteLine("Resultado: " + resultado);
                break;

            case "multiplicacion":
                resultado = Multiplicar(numero1, numero2);
                Console.WriteLine("Resultado: " + resultado);
                break;

            case "division":
                if (numero2 == 0)
                {
                    Console.WriteLine("Error: no se puede dividir entre cero.");
                    return;
                }

                resultado = Dividir(numero1, numero2);
                Console.WriteLine("Resultado: " + resultado);
                break;
        }
    }

    static double Sumar(double numero1, double numero2)
    {
        return numero1 + numero2;
    }

    static double Restar(double numero1, double numero2)
    {
        return numero1 - numero2;
    }

    static double Multiplicar(double numero1, double numero2)
    {
        return numero1 * numero2;
    }

    static double Dividir(double numero1, double numero2)
    {
        return numero1 / numero2;
    }
}
