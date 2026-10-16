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

float findAverage(struct node *head)
{
    int suma = 0;
    int cantidad = 0;
    Nodo *actual = head;

    while (actual != NULL)
    {
        suma = suma + actual->dato;
        cantidad++;
        actual = actual->siguiente;
    }

    if (cantidad == 0)
    {
        return 0.0f;
    }

    return (float)suma / cantidad;
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

    agregarAlFinal(&lista, 10);
    agregarAlFinal(&lista, 20);
    agregarAlFinal(&lista, 30);
    agregarAlFinal(&lista, 40);

    printf("El promedio es: %.2f\n", findAverage(lista));

    liberarLista(lista);
    return 0;
}
