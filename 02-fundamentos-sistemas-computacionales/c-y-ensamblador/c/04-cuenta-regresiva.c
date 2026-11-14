#include <stdio.h>

int main(void) {
    int m, n, temp;

    printf("Digite un numero entero positivo: ");
    scanf("%d", &m);

    printf("Digite otro numero entero positivo: ");
    scanf("%d", &n);

    if (m < n) {
        temp = m;
        m = n;
        n = temp;
    }

    printf("Los numeros entre en ese rango son:\n");

    for (int i = m; i >= n; i--) {
        printf("%d", i);

        if (i > n) {
            printf(", ");
        }
    }

    printf("\n");

    return 0;
}
