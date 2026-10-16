#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

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

void mostrarLista(Nodo *cabeza)
{
    Nodo *actual = cabeza;

    while (actual != NULL)
    {
        printf("%d", actual->dato);

        if (actual->siguiente != NULL)
        {
            printf(" -> ");
        }

        actual = actual->siguiente;
    }

    printf("\n");
}

bool deleteFirstNode(struct node **head_ref)
{
    Nodo *nodoAEliminar;

    if (head_ref == NULL || *head_ref == NULL)
    {
        return false;
    }

    nodoAEliminar = *head_ref;
    *head_ref = (*head_ref)->siguiente;
    free(nodoAEliminar);

    return true;
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

    agregarAlFinal(&lista, 5);
    agregarAlFinal(&lista, 10);
    agregarAlFinal(&lista, 15);

    printf("Lista antes de eliminar: ");
    mostrarLista(lista);

    if (deleteFirstNode(&lista))
    {
        printf("El primer nodo fue eliminado.\n");
    }
    else
    {
        printf("La lista esta vacia.\n");
    }

    printf("Lista despues de eliminar: ");
    mostrarLista(lista);

    liberarLista(lista);
    return 0;
}
