#include <stdio.h>
#include <stdlib.h>

typedef struct node
{
    char dato;
    struct node *siguiente;
} Nodo;

Nodo *crearNodo(char dato)
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

void agregarAlFinal(Nodo **cabeza, char dato)
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

char *listToString(struct node *head)
{
    int cantidad = contarNodos(head);
    char *cadena = malloc((cantidad + 1) * sizeof(char));
    int posicion = 0;
    Nodo *actual = head;

    if (cadena == NULL)
    {
        return NULL;
    }

    while (actual != NULL)
    {
        cadena[posicion] = actual->dato;
        posicion++;
        actual = actual->siguiente;
    }

    cadena[posicion] = '\0';
    return cadena;
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
    char *cadena;

    agregarAlFinal(&lista, 'H');
    agregarAlFinal(&lista, 'o');
    agregarAlFinal(&lista, 'l');
    agregarAlFinal(&lista, 'a');

    cadena = listToString(lista);

    if (cadena != NULL)
    {
        printf("Cadena formada: %s\n", cadena);
        free(cadena);
    }

    liberarLista(lista);
    return 0;
}
