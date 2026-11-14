// Elaboración nueva basada en el enunciado conservado del primer parcial.
// No corresponde a una solución original entregada.
#include <cmath>
#include <iomanip>
#include <iostream>

int main() {
    int ciudad = 0;
    double ventas = 0.0;

    std::cout << "Numero de ciudad (1 Santiago, 2 Salcedo, 3 La Vega): ";
    if (!(std::cin >> ciudad)) {
        std::cerr << "error\n";
        return 1;
    }

    std::cout << "Volumen de ventas: ";
    if (!(std::cin >> ventas) || !std::isfinite(ventas) || ventas < 0.0) {
        std::cerr << "error\n";
        return 1;
    }
    std::cin >> std::ws;
    if (!std::cin.eof()) {
        std::cerr << "error\n";
        return 1;
    }

    double tasa = 0.0;
    if (ciudad == 1) {
        tasa = ventas <= 500.0 ? 0.05 : ventas <= 1000.0 ? 0.07
             : ventas <= 10000.0 ? 0.08 : 0.12;
    } else if (ciudad == 2) {
        tasa = ventas <= 500.0 ? 0.045 : ventas <= 1000.0 ? 0.075
             : ventas <= 10000.0 ? 0.10 : 0.13;
    } else if (ciudad == 3) {
        tasa = ventas <= 500.0 ? 0.055 : ventas <= 1000.0 ? 0.08
             : ventas <= 10000.0 ? 0.12 : 0.145;
    } else {
        std::cerr << "error\n";
        return 1;
    }

    const double comision = ventas * tasa;
    if (!std::isfinite(comision)) {
        std::cerr << "error\n";
        return 1;
    }

    std::cout << "Monto de la comision a pagar: "
              << std::fixed << std::setprecision(2) << comision << '\n';
    return 0;
}
