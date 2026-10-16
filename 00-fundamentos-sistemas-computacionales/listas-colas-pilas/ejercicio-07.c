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

bool deleteNode(struct node **head_ref, int data)
{
    Nodo *actual;
    Nodo *anterior = NULL;

    if (head_ref == NULL || *head_ref == NULL)
    {
        return false;
    }

    actual = *head_ref;

    while (actual != NULL && actual->dato != data)
    {
        anterior = actual;
        actual = actual->siguiente;
    }

    if (actual == NULL)
    {
        return false;
    }

    if (anterior == NULL)
    {
        *head_ref = actual->siguiente;
    }
    else
    {
        anterior->siguiente = actual->siguiente;
    }

    free(actual);
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
    int numeroAEliminar = 16;

    agregarAlFinal(&lista, 8);
    agregarAlFinal(&lista, 12);
    agregarAlFinal(&lista, 16);
    agregarAlFinal(&lista, 20);

    printf("Lista antes de eliminar: ");
    mostrarLista(lista);

    if (deleteNode(&lista, numeroAEliminar))
    {
        printf("El nodo con el numero %d fue eliminado.\n", numeroAEliminar);
    }
    else
    {
        printf("El numero no fue encontrado.\n");
    }

    printf("Lista despues de eliminar: ");
    mostrarLista(lista);

    liberarLista(lista);
    return 0;
}
