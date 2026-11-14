#include <iostream>
#include <string>
int main() {
    std::string palabra, texto;
    std::cout << "Ingrese una palabra: ";
    if (!std::getline(std::cin,palabra) || palabra.empty()) {
        std::cerr << "Error: la palabra no puede estar vacia.\n"; return 1;
    }
    std::cout << "Ingrese un texto: ";
    if (!std::getline(std::cin,texto)) { std::cerr << "Error: falta el texto.\n"; return 1; }
    std::size_t contador=0;
    for (std::size_t posicion=texto.find(palabra); posicion!=std::string::npos;
         posicion=texto.find(palabra,posicion+1)) ++contador;
    std::cout << "\nCoincidencias: " << contador << '\n';
}
