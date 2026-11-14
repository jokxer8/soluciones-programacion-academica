#include <iostream>
#include <cmath>
#include <iomanip>
int main() {
    double a, b;
    std::cout << "Ingrese los dos catetos: ";
    if (!(std::cin >> a >> b) || !std::isfinite(a) || !std::isfinite(b) || a <= 0 || b <= 0) {
        std::cerr << "Error: los catetos deben ser positivos y finitos.\n"; return 1;
    }
    double c = std::hypot(a,b), area = (a/2)*b, perimetro = a+b+c;
    if (!std::isfinite(c) || !std::isfinite(area) || !std::isfinite(perimetro)) {
        std::cerr << "Error: resultado fuera de rango.\n"; return 1;
    }
    std::cout << std::fixed << std::setprecision(6)
              << "\nHipotenusa: " << c << "\nArea: " << area << "\nPerimetro: " << perimetro << '\n';
}
