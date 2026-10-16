#include <iostream>
int main() {
    int n;
    std::cout << "Ingrese el numero de pisos (1 a 60): ";
    if (!(std::cin >> n) || n < 1 || n > 60) {
        std::cerr << "Error: se requiere un numero de pisos entre 1 y 60.\n"; return 1;
    }
    std::cout << '\n';
    for (int piso=1; piso<=n; ++piso) {
        for (int espacio=0; espacio<n-piso; ++espacio) std::cout << ' ';
        for (int estrella=0; estrella<2*piso-1; ++estrella) std::cout << '*';
        std::cout << '\n';
    }
}
