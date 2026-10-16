#include <stdio.h>
#include <stdlib.h>

struct complejo {
    float real;
    float imaginaria;
};

struct complejo *sumar_complejos(struct complejo c1, struct complejo c2) {
    struct complejo *resultado;

    resultado = (struct complejo *) malloc(sizeof(struct complejo));

    if (resultado == NULL) {
        return NULL;
    }

    resultado->real = c1.real + c2.real;
    resultado->imaginaria = c1.imaginaria + c2.imaginaria;

    return resultado;
}

void imprimirComplejo(struct complejo c) {
    if (c.imaginaria >= 0) {
        printf("%.2f + j%.2f\n", c.real, c.imaginaria);
    } else {
        printf("%.2f - j%.2f\n", c.real, -c.imaginaria);
    }
}

int main() {
    struct complejo c1, c2;
    struct complejo *suma;

    printf("Ingrese el primer numero complejo\n");
    printf("Parte real: ");
    scanf("%f", &c1.real);
    printf("Parte imaginaria: ");
    scanf("%f", &c1.imaginaria);

    printf("\nIngrese el segundo numero complejo\n");
    printf("Parte real: ");
    scanf("%f", &c2.real);
    printf("Parte imaginaria: ");
    scanf("%f", &c2.imaginaria);

    suma = sumar_complejos(c1, c2);

    if (suma == NULL) {
        printf("Error: no se pudo reservar memoria.\n");
        return 1;
    }

    printf("\nResultado de la suma: ");
    imprimirComplejo(*suma);

    free(suma);

    return 0;
}