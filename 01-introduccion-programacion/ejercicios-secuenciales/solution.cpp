#include <iostream>
#include <limits>
int main() {
    long long a;
    std::cout << "Ingrese A: ";
    if (!(std::cin >> a) || a > std::numeric_limits<long long>::max()-3) {
        std::cerr << "Error: entero invalido o resultado fuera de rango.\n"; return 1;
    }
    long long b=a+3;
    std::cout << "\nA: " << a << "\nB: " << b << '\n';
}
