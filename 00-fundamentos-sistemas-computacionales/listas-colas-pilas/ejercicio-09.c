#include <stdio.h>
#include <stdlib.h>

typedef struct node
{
    int dato;
    struct node *siguiente;
} Nodo;

Nodo *crearNodo(int dato)
{
    Nodo *nuevo = malloc(sizeof(Nodo));

    if (nuevo == NULL)
    {
        return NULL;
    }

    nuevo->dato = dato;
    nuevo->siguiente = NULL;
    return nuevo;
}

void agregarAlFinal(Nodo **cabeza, int dato)
{
    Nodo *nuevo = crearNodo(dato);
    Nodo *actual;

    if (nuevo == NULL)
    {
        printf("No se pudo reservar memoria.\n");
        return;
    }

    if (*cabeza == NULL)
    {
        *cabeza = nuevo;
        return;
    }

    actual = *cabeza;

    while (actual->siguiente != NULL)
    {
        actual = actual->siguiente;
    }

    actual->siguiente = nuevo;
}

int contarNodos(Nodo *cabeza)
{
    int cantidad = 0;
    Nodo *actual = cabeza;

    while (actual != NULL)
    {
        cantidad++;
        actual = actual->siguiente;
    }

    return cantidad;
}

int *listToArray(struct node *head)
{
    int cantidad = contarNodos(head);
    int *arreglo;
    int posicion = 0;
    Nodo *actual = head;

    if (cantidad == 0)
    {
        return NULL;
    }

    arreglo = malloc(cantidad * sizeof(int));

    if (arreglo == NULL)
    {
        return NULL;
    }

    while (actual != NULL)
    {
        arreglo[posicion] = actual->dato;
        posicion++;
        actual = actual->siguiente;
    }

    return arreglo;
}

void liberarLista(Nodo *cabeza)
{
    Nodo *actual = cabeza;

    while (actual != NULL)
    {
        Nodo *siguiente = actual->siguiente;
        free(actual);
        actual = siguiente;
    }
}

int main(void)
{
    Nodo *lista = NULL;
    int *arreglo;
    int cantidad;
    int i;

    agregarAlFinal(&lista, 7);
    agregarAlFinal(&lista, 14);
    agregarAlFinal(&lista, 21);
    agregarAlFinal(&lista, 28);

    cantidad = contarNodos(lista);
    arreglo = listToArray(lista);

    if (arreglo != NULL)
    {
        printf("Elementos del arreglo: ");

        for (i = 0; i < cantidad; i++)
        {
            printf("%d ", arreglo[i]);
        }

        printf("\n");
        free(arreglo);
    }

    liberarLista(lista);
    return 0;
}
