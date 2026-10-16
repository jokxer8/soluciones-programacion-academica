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

void mezclar(int arreglo[], int auxiliar[], int inicio, int medio, int final)
{
    int izquierda = inicio;
    int derecha = medio + 1;
    int posicion = inicio;
    int i;

    while (izquierda <= medio && derecha <= final)
    {
        if (arreglo[izquierda] <= arreglo[derecha])
        {
            auxiliar[posicion] = arreglo[izquierda];
            izquierda++;
        }
        else
        {
            auxiliar[posicion] = arreglo[derecha];
            derecha++;
        }

        posicion++;
    }

    while (izquierda <= medio)
    {
        auxiliar[posicion] = arreglo[izquierda];
        izquierda++;
        posicion++;
    }

    while (derecha <= final)
    {
        auxiliar[posicion] = arreglo[derecha];
        derecha++;
        posicion++;
    }

    for (i = inicio; i <= final; i++)
    {
        arreglo[i] = auxiliar[i];
    }
}

void ordenarMergeSort(int arreglo[], int auxiliar[], int inicio, int final)
{
    int medio;

    if (inicio >= final)
    {
        return;
    }

    medio = (inicio + final) / 2;

    ordenarMergeSort(arreglo, auxiliar, inicio, medio);
    ordenarMergeSort(arreglo, auxiliar, medio + 1, final);
    mezclar(arreglo, auxiliar, inicio, medio, final);
}

void sortLinkedList(struct node *head)
{
    int cantidad = contarNodos(head);
    int *arreglo;
    int *auxiliar;
    int posicion = 0;
    Nodo *actual = head;

    if (cantidad <= 1)
    {
        return;
    }

    arreglo = malloc(cantidad * sizeof(int));
    auxiliar = malloc(cantidad * sizeof(int));

    if (arreglo == NULL || auxiliar == NULL)
    {
        free(arreglo);
        free(auxiliar);
        return;
    }

    while (actual != NULL)
    {
        arreglo[posicion] = actual->dato;
        posicion++;
        actual = actual->siguiente;
    }

    ordenarMergeSort(arreglo, auxiliar, 0, cantidad - 1);

    actual = head;
    posicion = 0;

    while (actual != NULL)
    {
        actual->dato = arreglo[posicion];
        posicion++;
        actual = actual->siguiente;
    }

    free(arreglo);
    free(auxiliar);
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

    agregarAlFinal(&lista, 7);
    agregarAlFinal(&lista, 3);
    agregarAlFinal(&lista, 9);
    agregarAlFinal(&lista, 1);
    agregarAlFinal(&lista, 5);

    printf("Lista antes de ordenar: ");
    mostrarLista(lista);

    sortLinkedList(lista);

    printf("Lista ordenada: ");
    mostrarLista(lista);

    liberarLista(lista);
    return 0;
}
