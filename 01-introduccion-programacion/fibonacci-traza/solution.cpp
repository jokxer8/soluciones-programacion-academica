#include <iostream>
int main() {
    int n;
    std::cout << "Ingrese la cantidad de terminos (1 a 93): ";
    if (!(std::cin >> n) || n<1 || n>93) {
        std::cerr << "Error: se requiere una cantidad entre 1 y 93.\n"; return 1;
    }
    unsigned long long anterior=0, actual=1;
    std::cout << '\n';
    for(int i=0;i<n;++i) {
        if(i) std::cout << ' ';
        std::cout << anterior;
        if (i+1<n) {
        unsigned long long siguiente=anterior+actual;
        anterior=actual; actual=siguiente;
        }
    }
    std::cout << '\n';
}
