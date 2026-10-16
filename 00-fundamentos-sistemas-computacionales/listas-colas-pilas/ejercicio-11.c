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

int deleteDuplicatedNodes(struct node *head)
{
    int eliminados = 0;
    Nodo *actual = head;

    while (actual != NULL)
    {
        Nodo *anterior = actual;
        Nodo *comparar = actual->siguiente;

        while (comparar != NULL)
        {
            if (comparar->dato == actual->dato)
            {
                Nodo *duplicado = comparar;
                anterior->siguiente = comparar->siguiente;
                comparar = comparar->siguiente;
                free(duplicado);
                eliminados++;
            }
            else
            {
                anterior = comparar;
                comparar = comparar->siguiente;
            }
        }

        actual = actual->siguiente;
    }

    return eliminados;
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
    int eliminados;

    agregarAlFinal(&lista, 4);
    agregarAlFinal(&lista, 2);
    agregarAlFinal(&lista, 4);
    agregarAlFinal(&lista, 3);
    agregarAlFinal(&lista, 2);
    agregarAlFinal(&lista, 5);
    agregarAlFinal(&lista, 3);

    printf("Lista antes: ");
    mostrarLista(lista);

    eliminados = deleteDuplicatedNodes(lista);

    printf("Lista despues: ");
    mostrarLista(lista);
    printf("Duplicados eliminados: %d\n", eliminados);

    liberarLista(lista);
    return 0;
}
