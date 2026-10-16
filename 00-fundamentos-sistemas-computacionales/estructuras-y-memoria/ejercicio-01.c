#include <stdio.h>

#define MAX_NOMBRE 50

struct Estudiante {
    char *nombre;
    int edad;
    float calificacion;
};

int main() {
    struct Estudiante e1, e2;

    char nombre1[MAX_NOMBRE];
    char nombre2[MAX_NOMBRE];

    e1.nombre = nombre1;
    e2.nombre = nombre2;

    printf("Ingrese el nombre del estudiante 1: ");
    scanf(" %49[^\n]", e1.nombre);

    printf("Ingrese la edad del estudiante 1: ");
    scanf("%d", &e1.edad);

    printf("Ingrese la calificacion del estudiante 1: ");
    scanf("%f", &e1.calificacion);

    printf("\nIngrese el nombre del estudiante 2: ");
    scanf(" %49[^\n]", e2.nombre);

    printf("Ingrese la edad del estudiante 2: ");
    scanf("%d", &e2.edad);

    printf("Ingrese la calificacion del estudiante 2: ");
    scanf("%f", &e2.calificacion);

    float promedio = (e1.calificacion + e2.calificacion) / 2.0f;

    printf("\n--- Informacion de los estudiantes ---\n");

    printf("\nEstudiante 1\n");
    printf("Nombre: %s\n", e1.nombre);
    printf("Edad: %d\n", e1.edad);
    printf("Calificacion: %.2f\n", e1.calificacion);

    printf("\nEstudiante 2\n");
    printf("Nombre: %s\n", e2.nombre);
    printf("Edad: %d\n", e2.edad);
    printf("Calificacion: %.2f\n", e2.calificacion);

    printf("\nPromedio de calificaciones: %.2f\n", promedio);

    return 0;
}