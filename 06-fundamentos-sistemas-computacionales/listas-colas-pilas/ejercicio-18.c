#include <stdio.h>
#include <stdbool.h>

#define MAXIMO 10

typedef struct
{
    int datos[MAXIMO];
    int cima;
} Pila;

void inicializarPila(Pila *pila)
{
    pila->cima = -1;
}

bool isEmpty(Pila *pila)
{
    return pila->cima == -1;
}

bool isFull(Pila *pila)
{
    return pila->cima == MAXIMO - 1;
}

void push(Pila *pila, int numero)
{
    if (isFull(pila))
    {
        printf("La pila esta llena.\n");
        return;
    }

    pila->cima++;
    pila->datos[pila->cima] = numero;
}

int pop(Pila *pila)
{
    int numero = pila->datos[pila->cima];
    pila->cima--;
    return numero;
}

void mostrarPila(Pila *pila)
{
    int i;

    if (isEmpty(pila))
    {
        printf("La pila esta vacia.\n");
        return;
    }

    printf("Pila desde la cima: ");

    for (i = pila->cima; i >= 0; i--)
    {
        printf("%d ", pila->datos[i]);
    }

    printf("\n");
}

int main(void)
{
    Pila pila;
    int opcion;
    int numero;

    inicializarPila(&pila);

    do
    {
        printf("\n1. Push\n");
        printf("2. Pop\n");
        printf("3. Mostrar\n");
        printf("4. Verificar si esta vacia\n");
        printf("5. Verificar si esta llena\n");
        printf("0. Salir\n");
        printf("Opcion: ");
        scanf("%d", &opcion);

        if (opcion == 1)
        {
            printf("Numero: ");
            scanf("%d", &numero);
            push(&pila, numero);
        }
        else if (opcion == 2)
        {
            if (isEmpty(&pila))
                printf("La pila esta vacia.\n");
            else
                printf("Se saco el numero %d.\n", pop(&pila));
        }
        else if (opcion == 3)
        {
            mostrarPila(&pila);
        }
        else if (opcion == 4)
        {
            if (isEmpty(&pila))
                printf("La pila esta vacia.\n");
            else
                printf("La pila contiene elementos.\n");
        }
        else if (opcion == 5)
        {
            if (isFull(&pila))
                printf("La pila esta llena.\n");
            else
                printf("La pila todavia tiene espacio.\n");
        }
        else if (opcion != 0)
        {
            printf("Opcion no valida.\n");
        }

    } while (opcion != 0);

    return 0;
}
