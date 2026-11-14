#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

typedef struct nodoCola
{
    int dato;
    struct nodoCola *siguiente;
} NodoCola;

typedef struct
{
    NodoCola *frente;
    NodoCola *final;
} Cola;

void inicializarCola(Cola *cola)
{
    cola->frente = NULL;
    cola->final = NULL;
}

bool colaVacia(Cola *cola)
{
    return cola->frente == NULL;
}

bool insertar(Cola *cola, int numero)
{
    NodoCola *nuevo = (NodoCola *)malloc(sizeof(NodoCola));

    if (nuevo == NULL)
    {
        return false;
    }

    nuevo->dato = numero;
    nuevo->siguiente = NULL;

    if (colaVacia(cola))
    {
        cola->frente = nuevo;
        cola->final = nuevo;
    }
    else
    {
        cola->final->siguiente = nuevo;
        cola->final = nuevo;
    }

    return true;
}

bool sacar(Cola *cola, int *numero)
{
    NodoCola *nodoAEliminar;

    if (colaVacia(cola))
    {
        return false;
    }

    nodoAEliminar = cola->frente;
    *numero = nodoAEliminar->dato;
    cola->frente = nodoAEliminar->siguiente;

    if (cola->frente == NULL)
    {
        cola->final = NULL;
    }

    free(nodoAEliminar);
    return true;
}

void mostrarCola(Cola *cola)
{
    NodoCola *actual = cola->frente;

    if (colaVacia(cola))
    {
        printf("La cola esta vacia.\n");
        return;
    }

    printf("Cola: ");

    while (actual != NULL)
    {
        printf("%d ", actual->dato);
        actual = actual->siguiente;
    }

    printf("\n");
}

void liberarCola(Cola *cola)
{
    int numero;

    while (sacar(cola, &numero))
    {
        /* Se van eliminando todos los nodos. */
    }
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
        printf("4. Verificar si esta vacia\n");
        printf("0. Salir\n");
        printf("Opcion: ");
        scanf("%d", &opcion);

        if (opcion == 1)
        {
            printf("Numero: ");
            scanf("%d", &numero);

            if (!insertar(&cola, numero))
                printf("No se pudo reservar memoria.\n");
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
        else if (opcion == 4)
        {
            if (colaVacia(&cola))
                printf("La cola esta vacia.\n");
            else
                printf("La cola contiene elementos.\n");
        }
        else if (opcion != 0)
        {
            printf("Opcion no valida.\n");
        }

    } while (opcion != 0);

    liberarCola(&cola);
    return 0;
}
