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

struct node *findMax(struct node *head)
{
    Nodo *mayor;
    Nodo *actual;

    if (head == NULL)
    {
        return NULL;
    }

    mayor = head;
    actual = head->siguiente;

    while (actual != NULL)
    {
        if (actual->dato > mayor->dato)
        {
            mayor = actual;
        }

        actual = actual->siguiente;
    }

    return mayor;
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
    Nodo *mayor;

    agregarAlFinal(&lista, 12);
    agregarAlFinal(&lista, 45);
    agregarAlFinal(&lista, 8);
    agregarAlFinal(&lista, 30);

    mayor = findMax(lista);

    if (mayor != NULL)
    {
        printf("El elemento mayor es: %d\n", mayor->dato);
    }

    liberarLista(lista);
    return 0;
}
