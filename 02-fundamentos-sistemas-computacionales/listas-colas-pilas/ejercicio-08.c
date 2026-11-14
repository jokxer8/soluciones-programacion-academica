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

bool isPalindrome(struct node *head)
{
    int cantidad = contarNodos(head);
    int *arreglo;
    int izquierda = 0;
    int derecha = cantidad - 1;
    int posicion = 0;
    Nodo *actual = head;

    if (cantidad <= 1)
    {
        return true;
    }

    arreglo = malloc(cantidad * sizeof(int));

    if (arreglo == NULL)
    {
        return false;
    }

    while (actual != NULL)
    {
        arreglo[posicion] = actual->dato;
        posicion++;
        actual = actual->siguiente;
    }

    while (izquierda < derecha)
    {
        if (arreglo[izquierda] != arreglo[derecha])
        {
            free(arreglo);
            return false;
        }

        izquierda++;
        derecha--;
    }

    free(arreglo);
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

    agregarAlFinal(&lista, 1);
    agregarAlFinal(&lista, 2);
    agregarAlFinal(&lista, 3);
    agregarAlFinal(&lista, 2);
    agregarAlFinal(&lista, 1);

    if (isPalindrome(lista))
    {
        printf("La lista forma un palindromo.\n");
    }
    else
    {
        printf("La lista no forma un palindromo.\n");
    }

    liberarLista(lista);
    return 0;
}
