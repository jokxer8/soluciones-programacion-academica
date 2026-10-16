#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

typedef struct nodoPila
{
    int dato;
    struct nodoPila *siguiente;
} NodoPila;

typedef struct
{
    NodoPila *cima;
} Pila;

void inicializarPila(Pila *pila)
{
    pila->cima = NULL;
}

bool isEmpty(Pila *pila)
{
    return pila->cima == NULL;
}

/* En una pila enlazada no existe un limite fijo.
   Solo se considera llena si malloc no consigue memoria. */
bool isFull(Pila *pila)
{
    NodoPila *prueba;
    (void)pila;

    prueba = (NodoPila *)malloc(sizeof(NodoPila));

    if (prueba == NULL)
    {
        return true;
    }

    free(prueba);
    return false;
}

void push(Pila *pila, int numero)
{
    NodoPila *nuevo = (NodoPila *)malloc(sizeof(NodoPila));

    if (nuevo == NULL)
    {
        printf("No se pudo reservar memoria.\n");
        return;
    }

    nuevo->dato = numero;
    nuevo->siguiente = pila->cima;
    pila->cima = nuevo;
}

int pop(Pila *pila)
{
    NodoPila *nodoAEliminar = pila->cima;
    int numero = nodoAEliminar->dato;

    pila->cima = nodoAEliminar->siguiente;
    free(nodoAEliminar);

    return numero;
}

void mostrarPila(Pila *pila)
{
    NodoPila *actual = pila->cima;

    if (isEmpty(pila))
    {
        printf("La pila esta vacia.\n");
        return;
    }

    printf("Pila desde la cima: ");

    while (actual != NULL)
    {
        printf("%d ", actual->dato);
        actual = actual->siguiente;
    }

    printf("\n");
}

void liberarPila(Pila *pila)
{
    while (!isEmpty(pila))
    {
        pop(pila);
    }
}

int main(void)
{
    Pila pila;
    int opcion;
    int numero;

    inicializarPila(&pila);

    do
    {
        printf("\n1. Push\n");
        printf("2. Pop\n");
        printf("3. Mostrar\n");
        printf("4. Verificar si esta vacia\n");
        printf("5. Verificar si esta llena\n");
        printf("0. Salir\n");
        printf("Opcion: ");
        scanf("%d", &opcion);

        if (opcion == 1)
        {
            printf("Numero: ");
            scanf("%d", &numero);
            push(&pila, numero);
        }
        else if (opcion == 2)
        {
            if (isEmpty(&pila))
                printf("La pila esta vacia.\n");
            else
                printf("Se saco el numero %d.\n", pop(&pila));
        }
        else if (opcion == 3)
        {
            mostrarPila(&pila);
        }
        else if (opcion == 4)
        {
            if (isEmpty(&pila))
                printf("La pila esta vacia.\n");
            else
                printf("La pila contiene elementos.\n");
        }
        else if (opcion == 5)
        {
            if (isFull(&pila))
                printf("No hay memoria disponible.\n");
            else
                printf("Todavia se pueden agregar nodos.\n");
        }
        else if (opcion != 0)
        {
            printf("Opcion no valida.\n");
        }

    } while (opcion != 0);

    liberarPila(&pila);
    return 0;
}
