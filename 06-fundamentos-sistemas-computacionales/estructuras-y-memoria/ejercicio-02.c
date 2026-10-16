#include <stdio.h>

struct Tiempo {
    int horas;
    int minutos;
    int segundos;
};

struct Tiempo sumarTiempos(struct Tiempo t1, struct Tiempo t2) {
    struct Tiempo resultado;

    int totalSegundos;

    totalSegundos = t1.horas * 3600 + t1.minutos * 60 + t1.segundos;
    totalSegundos += t2.horas * 3600 + t2.minutos * 60 + t2.segundos;

    resultado.horas = totalSegundos / 3600;
    totalSegundos = totalSegundos % 3600;

    resultado.minutos = totalSegundos / 60;
    resultado.segundos = totalSegundos % 60;

    return resultado;
}

int main() {
    struct Tiempo t1, t2, suma;

    printf("Ingrese la primera hora\n");
    printf("Horas: ");
    scanf("%d", &t1.horas);
    printf("Minutos: ");
    scanf("%d", &t1.minutos);
    printf("Segundos: ");
    scanf("%d", &t1.segundos);

    printf("\nIngrese la segunda hora\n");
    printf("Horas: ");
    scanf("%d", &t2.horas);
    printf("Minutos: ");
    scanf("%d", &t2.minutos);
    printf("Segundos: ");
    scanf("%d", &t2.segundos);

    suma = sumarTiempos(t1, t2);

    printf("\nResultado: %02d:%02d:%02d\n",
           suma.horas, suma.minutos, suma.segundos);

    return 0;
}