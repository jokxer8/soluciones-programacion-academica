#include <stdio.h>

int main(void) {
    int a, b, resultado;

    printf("Digite el primer valor: ");
    scanf("%d", &a);

    printf("Digite el segundo valor: ");
    scanf("%d", &b);

    resultado = a + b;

    if (a == b) {
        resultado = resultado * 2;
    }

    printf("Resultado: %d\n", resultado);

    return 0;
}
