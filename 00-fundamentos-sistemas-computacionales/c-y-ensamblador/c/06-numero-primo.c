#include <stdio.h>

int main(void) {
    int n;
    int esPrimo = 1;

    printf("Digite un numero entero positivo: ");
    scanf("%d", &n);

    if (n < 2) {
        esPrimo = 0;
    } else {
        for (int i = 2; i * i <= n; i++) {
            if (n % i == 0) {
                esPrimo = 0;
                break;
            }
        }
    }

    if (esPrimo) {
        printf("%d es primo.\n", n);
    } else {
        printf("%d no es primo.\n", n);
    }

    return 0;
}
