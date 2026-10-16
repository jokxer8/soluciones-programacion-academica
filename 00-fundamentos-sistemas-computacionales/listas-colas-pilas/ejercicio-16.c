#include <stdio.h>
#include <stdbool.h>

#define MAXIMO 20

typedef struct
{
    int datos[MAXIMO];
    int cantidad;
} Cola;

void inicializarCola(Cola *cola)
{
    cola->cantidad = 0;
}

bool insertar(Cola *cola, int numero)
{
    if (cola->cantidad == MAXIMO)
    {
        return false;
    }

    cola->datos[cola->cantidad] = numero;
    cola->cantidad++;
    return true;
}

bool encontrarMaximo(Cola *cola, int *mayor)
{
    int i;

    if (cola->cantidad == 0)
    {
        return false;
    }

    *mayor = cola->datos[0];

    for (i = 1; i < cola->cantidad; i++)
    {
        if (cola->datos[i] > *mayor)
        {
            *mayor = cola->datos[i];
        }
    }

    return true;
}

int main(void)
{
    Cola cola;
    int cantidad;
    int numero;
    int mayor;
    int i;

    inicializarCola(&cola);

    do
    {
        printf("Cantidad de numeros (1 a %d): ", MAXIMO);
        scanf("%d", &cantidad);
    } while (cantidad < 1 || cantidad > MAXIMO);

    for (i = 0; i < cantidad; i++)
    {
        printf("Numero %d: ", i + 1);
        scanf("%d", &numero);
        insertar(&cola, numero);
    }

    if (encontrarMaximo(&cola, &mayor))
    {
        printf("El elemento maximo es: %d\n", mayor);
    }

    return 0;
}
