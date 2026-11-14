using System;
using System.Collections.Generic;

class Program
{
    static int filas;
    static int columnas;
    static int[,] mapa;
    static int[,] etiquetas;

    static int[] cambioFila = { -1, 1, 0, 0 };
    static int[] cambioColumna = { 0, 0, -1, 1 };

    const int Infinito = 1000000000;

    static void Main()
    {
        string[] dimensiones = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        filas = int.Parse(dimensiones[0]);
        columnas = int.Parse(dimensiones[1]);

        mapa = new int[filas, columnas];
        etiquetas = new int[filas, columnas];

        for (int i = 0; i < filas; i++)
        {
            string[] partes = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < columnas; j++)
            {
                mapa[i, j] = int.Parse(partes[j]);
                etiquetas[i, j] = -1;
            }
        }

        int cantidadIslas = ContarYEtiquetarIslas();

        Console.WriteLine("Cantidad de islas: " + cantidadIslas);

        if (cantidadIslas <= 1)
        {
            Console.WriteLine("Mínimo de celdas de agua a convertir: 0");
            return;
        }

        int respuesta = CalcularMinimoParaConectarTodas(cantidadIslas);

        Console.WriteLine("Mínimo de celdas de agua a convertir: " + respuesta);
    }

    static int ContarYEtiquetarIslas()
    {
        int cantidadIslas = 0;

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if (mapa[i, j] == 1 && etiquetas[i, j] == -1)
                {
                    EtiquetarIsla(i, j, cantidadIslas);
                    cantidadIslas++;
                }
            }
        }

        return cantidadIslas;
    }

    static void EtiquetarIsla(int filaInicial, int columnaInicial, int idIsla)
    {
        Queue<(int Fila, int Columna)> cola = new Queue<(int, int)>();

        cola.Enqueue((filaInicial, columnaInicial));
        etiquetas[filaInicial, columnaInicial] = idIsla;

        while (cola.Count > 0)
        {
            var actual = cola.Dequeue();

            for (int k = 0; k < 4; k++)
            {
                int nuevaFila = actual.Fila + cambioFila[k];
                int nuevaColumna = actual.Columna + cambioColumna[k];

                if (!EstaDentroDelMapa(nuevaFila, nuevaColumna))
                {
                    continue;
                }

                if (mapa[nuevaFila, nuevaColumna] == 1 && etiquetas[nuevaFila, nuevaColumna] == -1)
                {
                    etiquetas[nuevaFila, nuevaColumna] = idIsla;
                    cola.Enqueue((nuevaFila, nuevaColumna));
                }
            }
        }
    }

    static int CalcularMinimoParaConectarTodas(int cantidadIslas)
    {
        int totalMascaras = 1 << cantidadIslas;

        int[,,] dp = new int[totalMascaras, filas, columnas];

        for (int mascara = 0; mascara < totalMascaras; mascara++)
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    dp[mascara, i, j] = Infinito;
                }
            }
        }

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if (mapa[i, j] == 1)
                {
                    int idIsla = etiquetas[i, j];
                    int mascara = 1 << idIsla;

                    dp[mascara, i, j] = 0;
                }
            }
        }

        for (int mascara = 1; mascara < totalMascaras; mascara++)
        {
            for (int subMascara = (mascara - 1) & mascara; subMascara > 0; subMascara = (subMascara - 1) & mascara)
            {
                int otraMascara = mascara ^ subMascara;

                if (otraMascara == 0)
                {
                    continue;
                }

                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        int costoCelda = mapa[i, j] == 0 ? 1 : 0;

                        int nuevoCosto = dp[subMascara, i, j] + dp[otraMascara, i, j] - costoCelda;

                        if (nuevoCosto < dp[mascara, i, j])
                        {
                            dp[mascara, i, j] = nuevoCosto;
                        }
                    }
                }
            }

            PropagarCostos(mascara, dp);
        }

        int mascaraTodasLasIslas = totalMascaras - 1;
        int respuesta = Infinito;

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                respuesta = Math.Min(respuesta, dp[mascaraTodasLasIslas, i, j]);
            }
        }

        return respuesta;
    }

    static void PropagarCostos(int mascara, int[,,] dp)
    {
        PriorityQueue<(int Fila, int Columna), int> cola = new PriorityQueue<(int, int), int>();

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if (dp[mascara, i, j] < Infinito)
                {
                    cola.Enqueue((i, j), dp[mascara, i, j]);
                }
            }
        }

        while (cola.TryDequeue(out var actual, out int costoActual))
        {
            if (costoActual != dp[mascara, actual.Fila, actual.Columna])
            {
                continue;
            }

            for (int k = 0; k < 4; k++)
            {
                int nuevaFila = actual.Fila + cambioFila[k];
                int nuevaColumna = actual.Columna + cambioColumna[k];

                if (!EstaDentroDelMapa(nuevaFila, nuevaColumna))
                {
                    continue;
                }

                int costoEntrar = mapa[nuevaFila, nuevaColumna] == 0 ? 1 : 0;
                int nuevoCosto = costoActual + costoEntrar;

                if (nuevoCosto < dp[mascara, nuevaFila, nuevaColumna])
                {
                    dp[mascara, nuevaFila, nuevaColumna] = nuevoCosto;
                    cola.Enqueue((nuevaFila, nuevaColumna), nuevoCosto);
                }
            }
        }
    }

    static bool EstaDentroDelMapa(int fila, int columna)
    {
        return fila >= 0 && fila < filas && columna >= 0 && columna < columnas;
    }
}
