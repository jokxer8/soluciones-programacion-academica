#include <stdio.h>
#include <stdlib.h>

struct punto {
    int x;
    int y;
};

struct rectangulo {
    struct punto arribaIzquierda;
    struct punto abajoDerecha;
};

struct rectangulo *crear_rectangulo(int x1, int y1, int x2, int y2) {
    struct rectangulo *r;

    r = (struct rectangulo *) malloc(sizeof(struct rectangulo));

    if (r == NULL) {
        return NULL;
    }

    r->arribaIzquierda.x = x1;
    r->arribaIzquierda.y = y1;

    r->abajoDerecha.x = x2;
    r->abajoDerecha.y = y2;

    return r;
}

int valorAbsoluto(int numero) {
    if (numero < 0) {
        return -numero;
    }

    return numero;
}

float area(struct rectangulo *rect) {
    int ancho;
    int alto;

    ancho = valorAbsoluto(rect->abajoDerecha.x - rect->arribaIzquierda.x);
    alto = valorAbsoluto(rect->arribaIzquierda.y - rect->abajoDerecha.y);

    return (float)(ancho * alto);
}

int main() {
    int x1, y1, x2, y2;

    struct rectangulo *r;

    printf("Ingrese x1 y y1 del punto arriba izquierda: ");
    scanf("%d %d", &x1, &y1);

    printf("Ingrese x2 y y2 del punto abajo derecha: ");
    scanf("%d %d", &x2, &y2);

    r = crear_rectangulo(x1, y1, x2, y2);

    if (r == NULL) {
        printf("Error: no se pudo reservar memoria.\n");
        return 1;
    }

    printf("\nRectangulo creado correctamente.\n");
    printf("Area del rectangulo: %.2f\n", area(r));

    free(r);

    return 0;
}