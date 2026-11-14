#include <stdio.h>
#include <stdint.h>

int main(void) {
    int32_t a, b;

    printf("Digite el primer numero entero de 32 bits: ");
    scanf("%d", &a);

    printf("Digite el segundo numero entero de 32 bits: ");
    scanf("%d", &b);

    printf("Suma: %d\n", a + b);
    printf("Resta: %d\n", a - b);
    printf("Producto: %lld\n", (long long)a * b);

    return 0;
}
