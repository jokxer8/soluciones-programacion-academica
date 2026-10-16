#include <stdio.h>
#include <stdbool.h>

#define MAXIMO 10

typedef struct
{
    int datos[MAXIMO];
    int frente;
    int final;
    int cantidad;
} Cola;

void inicializarCola(Cola *cola)
{
    cola->frente = 0;
    cola->final = 0;
    cola->cantidad = 0;
}

bool colaVacia(Cola *cola)
{
    return cola->cantidad == 0;
}

bool colaLlena(Cola *cola)
{
    return cola->cantidad == MAXIMO;
}

bool insertar(Cola *cola, int numero)
{
    if (colaLlena(cola))
    {
        return false;
    }

    cola->datos[cola->final] = numero;
    cola->final = (cola->final + 1) % MAXIMO;
    cola->cantidad++;

    return true;
}

bool sacar(Cola *cola, int *numero)
{
    if (colaVacia(cola))
    {
        return false;
    }

    *numero = cola->datos[cola->frente];
    cola->frente = (cola->frente + 1) % MAXIMO;
    cola->cantidad--;

    return true;
}

void mostrarCola(Cola *cola)
{
    int i;
    int posicion;

    if (colaVacia(cola))
    {
        printf("La cola esta vacia.\n");
        return;
    }

    printf("Elementos de la cola: ");

    for (i = 0; i < cola->cantidad; i++)
    {
        posicion = (cola->frente + i) % MAXIMO;
        printf("%d ", cola->datos[posicion]);
    }

    printf("\n");
}

int main(void)
{
    Cola cola;
    int opcion;
    int numero;

    inicializarCola(&cola);

    do
    {
        printf("\n1. Insertar\n");
        printf("2. Sacar\n");
        printf("3. Mostrar\n");
        printf("0. Salir\n");
        printf("Opcion: ");
        scanf("%d", &opcion);

        if (opcion == 1)
        {
            printf("Numero: ");
            scanf("%d", &numero);

            if (!insertar(&cola, numero))
                printf("La cola esta llena.\n");
        }
        else if (opcion == 2)
        {
            if (sacar(&cola, &numero))
                printf("Se saco el numero %d.\n", numero);
            else
                printf("La cola esta vacia.\n");
        }
        else if (opcion == 3)
        {
            mostrarCola(&cola);
        }
        else if (opcion != 0)
        {
            printf("Opcion no valida.\n");
        }

    } while (opcion != 0);

    return 0;
}
