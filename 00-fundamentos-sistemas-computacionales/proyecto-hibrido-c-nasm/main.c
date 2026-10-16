#include <stdio.h>

#define MAX_SIZE 20

void swap(int* a, int* b);
int sum(int* arr, int size);
void bubble_sort(int* arr, int size);

void limpiar_entrada(void) {
    int c;

    while ((c = getchar()) != '\n' && c != EOF) {
    }
}

int leer_entero(const char* mensaje) {
    int valor;
    int resultado;

    while (1) {
        printf("%s", mensaje);
        resultado = scanf_s("%d", &valor);

        if (resultado == 1) {
            limpiar_entrada();
            return valor;
        }

        printf("Error: debe ingresar un numero entero.\n");
        limpiar_entrada();
    }
}

void imprimir_arreglo(int arr[], int size) {
    for (int i = 0; i < size; i++) {
        printf("numeros[%d] = %d\n", i, arr[i]);
    }
}

void pausar_programa(void) {
    printf("\nPresione Enter para salir...");
    getchar();
}

int main(void) {
    int numeros[MAX_SIZE];
    int size;

    do {
        size = leer_entero("Ingrese el tamano del arreglo: ");

        if (size < 1 || size > MAX_SIZE) {
            printf("Tamano invalido. Debe ingresar un valor entre 1 y %d.\n\n", MAX_SIZE);
        }

    } while (size < 1 || size > MAX_SIZE);

    printf("\nIngrese los valores del arreglo:\n");

    for (int i = 0; i < size; i++) {
        char mensaje[50];

        sprintf_s(mensaje, sizeof(mensaje), "numeros[%d] = ", i);
        numeros[i] = leer_entero(mensaje);
    }

    int total = sum(numeros, size);

    bubble_sort(numeros, size);

    printf("\nArreglo ordenado de forma ascendente:\n");
    imprimir_arreglo(numeros, size);

    printf("\nEl total es: %d\n", total);

    int x = leer_entero("\nIngrese el valor de x: ");
    int y = leer_entero("Ingrese el valor de y: ");

    printf("\nAntes del intercambio:\n");
    printf("x = %d, y = %d\n", x, y);

    swap(&x, &y);

    printf("\nDespues del intercambio:\n");
    printf("x = %d, y = %d\n", x, y);

    pausar_programa();

    return 0;
}