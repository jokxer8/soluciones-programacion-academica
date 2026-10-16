#include <stdio.h>

struct complejo {
    float real;
    float imaginaria;
};

void imprimirComplejo(struct complejo c) {
    if (c.imaginaria >= 0) {
        printf("%.2f + j%.2f\n", c.real, c.imaginaria);
    } else {
        printf("%.2f - j%.2f\n", c.real, -c.imaginaria);
    }
}

int main() {
    struct complejo c1;

    printf("Ingrese la parte real: ");
    scanf("%f", &c1.real);

    printf("Ingrese la parte imaginaria: ");
    scanf("%f", &c1.imaginaria);

    printf("\nNumero complejo: ");
    imprimirComplejo(c1);

    return 0;
}