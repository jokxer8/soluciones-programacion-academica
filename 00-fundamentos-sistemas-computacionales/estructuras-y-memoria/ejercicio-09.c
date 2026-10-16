#include <stdio.h>
#include <stdlib.h>

struct complejo {
    float real;
    float imaginaria;
};

char *complejo_str(struct complejo c1) {
    char *texto;

    texto = (char *) malloc(80 * sizeof(char));

    if (texto == NULL) {
        return NULL;
    }

    if (c1.imaginaria >= 0) {
        sprintf(texto, "%.2f + j%.2f", c1.real, c1.imaginaria);
    } else {
        sprintf(texto, "%.2f - j%.2f", c1.real, -c1.imaginaria);
    }

    return texto;
}

int main() {
    struct complejo c1;
    char *str;

    c1.real = -4.37f;
    c1.imaginaria = 9.5f;

    str = complejo_str(c1);

    if (str == NULL) {
        printf("Error: no se pudo reservar memoria.\n");
        return 1;
    }

    printf("[%s]\n", str);

    free(str);

    return 0;
}