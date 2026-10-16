#include <stdio.h>

struct punto {
    int x;
    int y;
};

struct rectangulo {
    struct punto arribaIzquierda;
    struct punto abajoDerecha;
};

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
    struct rectangulo r;

    printf("Ingrese x1 y y1 del punto arriba izquierda: ");
    scanf("%d %d", &r.arribaIzquierda.x, &r.arribaIzquierda.y);

    printf("Ingrese x2 y y2 del punto abajo derecha: ");
    scanf("%d %d", &r.abajoDerecha.x, &r.abajoDerecha.y);

    printf("\nEl area del rectangulo es: %.2f\n", area(&r));

    return 0;
}