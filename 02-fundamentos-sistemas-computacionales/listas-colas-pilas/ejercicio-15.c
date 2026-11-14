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

float calcularPromedio(Cola *cola)
{
    int suma = 0;
    int i;

    if (cola->cantidad == 0)
    {
        return 0.0f;
    }

    for (i = 0; i < cola->cantidad; i++)
    {
        suma = suma + cola->datos[i];
    }

    return (float)suma / cola->cantidad;
}

int main(void)
{
    Cola cola;
    int cantidad;
    int numero;
    int i;

    inicializarCola(&cola);

    do
    {
        printf("Cuantos numeros positivos desea guardar? (1 a %d): ", MAXIMO);
        scanf("%d", &cantidad);
    } while (cantidad < 1 || cantidad > MAXIMO);

    for (i = 0; i < cantidad; i++)
    {
        do
        {
            printf("Numero %d: ", i + 1);
            scanf("%d", &numero);

            if (numero <= 0)
                printf("Debe ingresar un numero positivo.\n");

        } while (numero <= 0);

        insertar(&cola, numero);
    }

    printf("Promedio de la cola: %.2f\n", calcularPromedio(&cola));

    return 0;
}
