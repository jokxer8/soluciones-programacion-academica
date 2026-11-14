// Elaboración nueva basada en el enunciado conservado del primer parcial.
// Los intervalos son semiabiertos para resolver el solapamiento en 70, 80 y 90.
#include <cmath>
#include <iomanip>
#include <iostream>
#include <limits>
#include <string>

int main() {
    std::string nombre;
    std::string clave;
    std::string seccion;
    std::string descripcion;
    double nota = 0.0;

    std::cout << "Nombre estudiante: ";
    if (!std::getline(std::cin, nombre) || nombre.empty()) {
        std::cerr << "error: falta el nombre del estudiante\n";
        return 1;
    }
    std::cout << "Clave asignatura: ";
    if (!std::getline(std::cin, clave) || clave.empty()) {
        std::cerr << "error: falta la clave de la asignatura\n";
        return 1;
    }
    std::cout << "No. Seccion: ";
    if (!std::getline(std::cin, seccion) || seccion.empty()) {
        std::cerr << "error: falta la seccion\n";
        return 1;
    }
    std::cout << "Descripcion asignatura: ";
    if (!std::getline(std::cin, descripcion) || descripcion.empty()) {
        std::cerr << "error: falta la descripcion de la asignatura\n";
        return 1;
    }
    std::cout << "Calificacion: ";
    if (!(std::cin >> nota) || !std::isfinite(nota) ||
        nota < 0.0 || nota > 100.0) {
        std::cerr << "error: la calificacion debe estar entre 0 y 100 unicamente\n";
        return 1;
    }
    std::cin >> std::ws;
    if (!std::cin.eof()) {
        std::cerr << "error: la calificacion debe ser numerica\n";
        return 1;
    }

    char letra;
    const char* mensaje;
    if (nota < 60.0) {
        letra = 'F';
        mensaje = "estudiante reprobado";
    } else if (nota < 70.0) {
        letra = 'D';
        mensaje = "estudiante aceptable";
    } else if (nota < 80.0) {
        letra = 'C';
        mensaje = "estudiante bueno";
    } else if (nota < 90.0) {
        letra = 'B';
        mensaje = "estudiante muy bueno";
    } else {
        letra = 'A';
        mensaje = "estudiante sobresaliente";
    }

    std::cout << "Nombre estudiante: " << nombre << '\n'
              << "Clave asignatura: " << clave << '\n'
              << "Seccion: " << seccion << '\n'
              << "Descripcion asignatura: " << descripcion << '\n'
              << "Calificacion: " << std::fixed << std::setprecision(2) << nota << '\n'
              << "Letra: " << letra << '\n'
              << "Mensaje: " << mensaje << '\n';
    return 0;
}
