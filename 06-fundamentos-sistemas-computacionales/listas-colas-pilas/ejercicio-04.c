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

struct node *findMin(struct node *head)
{
    Nodo *menor;
    Nodo *actual;

    if (head == NULL)
    {
        return NULL;
    }

    menor = head;
    actual = head->siguiente;

    while (actual != NULL)
    {
        if (actual->dato < menor->dato)
        {
            menor = actual;
        }

        actual = actual->siguiente;
    }

    return menor;
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
    Nodo *menor;

    agregarAlFinal(&lista, 18);
    agregarAlFinal(&lista, 6);
    agregarAlFinal(&lista, 25);
    agregarAlFinal(&lista, 9);

    menor = findMin(lista);

    if (menor != NULL)
    {
        printf("El elemento menor es: %d\n", menor->dato);
    }

    liberarLista(lista);
    return 0;
}
