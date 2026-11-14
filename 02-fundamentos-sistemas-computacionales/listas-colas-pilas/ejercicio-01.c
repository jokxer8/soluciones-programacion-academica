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

struct node *findElement(struct node *head, int key)
{
    Nodo *actual = head;

    while (actual != NULL)
    {
        if (actual->dato == key)
        {
            return actual;
        }

        actual = actual->siguiente;
    }

    return NULL;
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
    Nodo *encontrado;
    int numeroBuscado = 30;

    agregarAlFinal(&lista, 10);
    agregarAlFinal(&lista, 20);
    agregarAlFinal(&lista, 30);
    agregarAlFinal(&lista, 40);

    encontrado = findElement(lista, numeroBuscado);

    if (encontrado != NULL)
    {
        printf("El elemento %d fue encontrado.\n", encontrado->dato);
    }
    else
    {
        printf("El elemento no fue encontrado.\n");
    }

    liberarLista(lista);
    return 0;
}
