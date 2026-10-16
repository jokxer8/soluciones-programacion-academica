#include <stdio.h>

struct punto {
    int x;
    int y;
};

struct rectangulo {
    struct punto arribaIzquierda;
    struct punto abajoDerecha;
};

int menor(int a, int b) {
    if (a < b) {
        return a;
    }

    return b;
}

int mayor(int a, int b) {
    if (a > b) {
        return a;
    }

    return b;
}

int hay_intercepcion(struct rectangulo *r, struct punto *p) {
    int minX = menor(r->arribaIzquierda.x, r->abajoDerecha.x);
    int maxX = mayor(r->arribaIzquierda.x, r->abajoDerecha.x);

    int minY = menor(r->arribaIzquierda.y, r->abajoDerecha.y);
    int maxY = mayor(r->arribaIzquierda.y, r->abajoDerecha.y);

    if (p->x >= minX && p->x <= maxX &&
        p->y >= minY && p->y <= maxY) {
        return 1;
    }

    return 0;
}

int main() {
    struct rectangulo r;
    struct punto p;

    printf("Ingrese x1 y y1 del punto arriba izquierda: ");
    scanf("%d %d", &r.arribaIzquierda.x, &r.arribaIzquierda.y);

    printf("Ingrese x2 y y2 del punto abajo derecha: ");
    scanf("%d %d", &r.abajoDerecha.x, &r.abajoDerecha.y);

    printf("Ingrese x y y del punto a evaluar: ");
    scanf("%d %d", &p.x, &p.y);

    if (hay_intercepcion(&r, &p) == 1) {
        printf("\nEl punto esta dentro del rectangulo.\n");
    } else {
        printf("\nEl punto esta fuera del rectangulo.\n");
    }

    return 0;
}