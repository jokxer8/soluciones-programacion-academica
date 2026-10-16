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

struct node *mergeLIsts(struct node *head1, struct node *head2)
{
    Nodo *nuevaLista = NULL;
    Nodo *actual = head1;

    while (actual != NULL)
    {
        agregarAlFinal(&nuevaLista, actual->dato);
        actual = actual->siguiente;
    }

    actual = head2;

    while (actual != NULL)
    {
        agregarAlFinal(&nuevaLista, actual->dato);
        actual = actual->siguiente;
    }

    liberarLista(head1);
    liberarLista(head2);

    return nuevaLista;
}

int main(void)
{
    Nodo *lista1 = NULL;
    Nodo *lista2 = NULL;
    Nodo *listaUnida;

    agregarAlFinal(&lista1, 1);
    agregarAlFinal(&lista1, 3);
    agregarAlFinal(&lista1, 5);

    agregarAlFinal(&lista2, 2);
    agregarAlFinal(&lista2, 4);
    agregarAlFinal(&lista2, 6);

    printf("Primera lista: ");
    mostrarLista(lista1);

    printf("Segunda lista: ");
    mostrarLista(lista2);

    listaUnida = mergeLIsts(lista1, lista2);

    lista1 = NULL;
    lista2 = NULL;

    printf("Nueva lista unida: ");
    mostrarLista(listaUnida);

    liberarLista(listaUnida);
    return 0;
}
