#include <stdio.h>

int main(void) {
    int a, b;

    printf("Digite el primer numero: ");
    scanf("%d", &a);

    printf("Digite el segundo numero: ");
    scanf("%d", &b);

    if ((a >= 100 && a <= 200) || (b >= 100 && b <= 200)) {
        printf("1\n");
    } else {
        printf("0\n");
    }

    return 0;
}
