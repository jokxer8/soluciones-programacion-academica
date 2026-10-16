#include <stdio.h>

#define MAX_NODOS 100

struct Nodo {
    int dato;
    struct Nodo *siguiente;
};

void imprimirLista(struct Nodo *cabeza) {
    struct Nodo *actual = cabeza;

    while (actual != NULL) {
        printf("Datos = %d\n", actual->dato);
        actual = actual->siguiente;
    }
}

void printReversed(struct Nodo *cabeza) {
    if (cabeza == NULL) {
        return;
    }

    printReversed(cabeza->siguiente);

    printf("Datos = %d\n", cabeza->dato);
}

int main() {
    struct Nodo nodos[MAX_NODOS];
    struct Nodo *cabeza;

    int n;
    int i;

    do {
        printf("Introduzca el numero de nodos (1-%d): ", MAX_NODOS);
        scanf("%d", &n);

        if (n < 1 || n > MAX_NODOS) {
            printf("Cantidad invalida. Intente nuevamente.\n");
        }

    } while (n < 1 || n > MAX_NODOS);

    for (i = 0; i < n; i++) {
        printf("Datos de entrada para el nodo %d: ", i + 1);
        scanf("%d", &nodos[i].dato);

        if (i == n - 1) {
            nodos[i].siguiente = NULL;
        } else {
            nodos[i].siguiente = &nodos[i + 1];
        }
    }

    cabeza = &nodos[0];

    printf("\nLos datos ingresados en la lista son:\n");
    imprimirLista(cabeza);

    printf("\nLa lista al reves es:\n");
    printReversed(cabeza);

    return 0;
}