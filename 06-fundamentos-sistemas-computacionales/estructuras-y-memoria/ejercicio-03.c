#include <stdio.h>

#define MAX_TEXTO 80
#define TOTAL_LIBROS 3

struct Libro {
    char *titulo;
    char *autor;
    float precio;
};

void mostrarLibro(struct Libro libro) {
    printf("Titulo: %s\n", libro.titulo);
    printf("Autor: %s\n", libro.autor);
    printf("Precio: %.2f\n", libro.precio);
}

int main() {
    struct Libro libros[TOTAL_LIBROS];

    char titulos[TOTAL_LIBROS][MAX_TEXTO];
    char autores[TOTAL_LIBROS][MAX_TEXTO];

    int i;
    int posicionMayor = 0;
    int posicionMenor = 0;

    for (i = 0; i < TOTAL_LIBROS; i++) {
        libros[i].titulo = titulos[i];
        libros[i].autor = autores[i];

        printf("\nIngrese los datos del libro %d\n", i + 1);

        printf("Titulo: ");
        scanf(" %79[^\n]", libros[i].titulo);

        printf("Autor: ");
        scanf(" %79[^\n]", libros[i].autor);

        printf("Precio: ");
        scanf("%f", &libros[i].precio);
    }

    for (i = 1; i < TOTAL_LIBROS; i++) {
        if (libros[i].precio > libros[posicionMayor].precio) {
            posicionMayor = i;
        }

        if (libros[i].precio < libros[posicionMenor].precio) {
            posicionMenor = i;
        }
    }

    printf("\n--- Libro mas caro ---\n");
    mostrarLibro(libros[posicionMayor]);

    printf("\n--- Libro de menor precio ---\n");
    mostrarLibro(libros[posicionMenor]);

    return 0;
}