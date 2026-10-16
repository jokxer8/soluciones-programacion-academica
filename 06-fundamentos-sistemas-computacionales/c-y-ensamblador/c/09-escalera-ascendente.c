#include <stdio.h>

int main(void) {
    int n;

    printf("Digite un # entre 1 - 20: ");
    scanf("%d", &n);

    if (n < 1 || n > 20) {
        printf("Numero fuera de rango.\n");
        return 0;
    }

    for (int fila = 1; fila <= n; fila++) {
        for (int estrella = 1; estrella <= fila; estrella++) {
            printf("*");
        }

        printf("\n");
    }

    return 0;
}
