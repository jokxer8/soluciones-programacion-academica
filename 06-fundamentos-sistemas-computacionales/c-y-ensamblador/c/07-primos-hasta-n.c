#include <stdio.h>

int esPrimo(int numero) {
    if (numero < 2) {
        return 0;
    }

    for (int i = 2; i * i <= numero; i++) {
        if (numero % i == 0) {
            return 0;
        }
    }

    return 1;
}

int main(void) {
    int n;
    int primero = 1;

    printf("Digite un numero entero positivo: ");
    scanf("%d", &n);

    printf("Los numeros primos entre 1 y %d son:\n", n);

    for (int i = 1; i <= n; i++) {
        if (esPrimo(i)) {
            if (!primero) {
                printf(", ");
            }

            printf("%d", i);
            primero = 0;
        }
    }

    printf("\n");

    return 0;
}
