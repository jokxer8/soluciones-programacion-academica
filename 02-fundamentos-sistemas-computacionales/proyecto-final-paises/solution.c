#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <ctype.h>

#define TAM_LINEA 2048
#define TOTAL_CAMPOS 20

typedef struct {
    char *nombre_pais;                 // Campo original: country
    char *region;                      // Campo original: region
    unsigned int poblacion;            // Campo original: population
    float area;                        // Campo original: area
    float densidad_poblacion;          // Campo original: pop_density
    float proporcion_costa;            // Campo original: coastline
    float migracion_neta;              // Campo original: net_migration
    float mortalidad_infantil;         // Campo original: infant_mortality
    int PIB;                           // Campo original: GDP
    float alfabetizacion;              // Campo original: literacy
    float telefonos;                   // Campo original: phones
    float tierra_cultivable;           // Campo original: arable
    float cultivos;                    // Campo original: crops
    float otros_terrenos;              // Campo original: other
    float clima;                       // Campo original: climate
    float tasa_natalidad;              // Campo original: birthrate
    float tasa_mortalidad;             // Campo original: deathrate
    float agricultura;                 // Campo original: agriculture
    float industria;                   // Campo original: industry
    float servicios;                   // Campo original: service
} country;

typedef struct Node {
    country *pais;                     // Campo original: coutry
    struct Node *siguiente;             // Campo original: next
    struct Node *anterior;              // Campo original: prev
} Node;

typedef enum Fields {
    NOMBRE,                 // Valor original: NAME
    POBLACION,              // Valor original: POPULATION
    AREA,                   // Valor original: AREA
    DENSIDAD_POBLACION      // Valor original: POP_DENSITY
} Fields;

void insertAtEnd(Node *cabeza, country paisRecibido);
void insertAtFront(Node *cabeza, country paisRecibido);
country *findCountryByName(Node *cabeza, char *nombre);
unsigned int countNodes(Node *cabeza);
double findGDPAverage(Node *cabeza);
void deleteByName(Node *cabeza, char *nombre);
bool deleteFirstNode(Node *cabeza);
bool deleteLastNode(Node *cabeza);
country *listToArray(Node *cabeza);
void sortLinkedList(Node *cabeza, Fields campo);
void display(Node *cabeza);

char *duplicarCadena(const char *texto);
char *limpiarEspacios(char *texto);
int separarCampos(char *linea, char *campos[], int maximo);
float convertirFloat(const char *texto);
int convertirInt(const char *texto);
unsigned int convertirUnsigned(const char *texto);
bool copiarPais(country *destino, const country *origen);
Node *crearNodo(country paisRecibido);
void liberarDatosPais(country *pais);
void liberarLista(Node *cabeza);
bool cargarArchivoCSV(const char *nombreArchivo, Node *cabeza);
int compararSinMayusculas(const char *texto1, const char *texto2);
int compararPaises(const country *pais1, const country *pais2, Fields campo);
void liberarArregloPaises(country *arreglo, unsigned int cantidad);
void leerLinea(char *texto, int tamano);
country leerPaisTeclado(void);
void mostrarPais(const country *pais);
void mostrarMenu(void);

int main(void) {
    Node cabeza = {NULL, NULL, NULL};
    int opcion;
    char entrada[100];

    printf("BASE DE DATOS DE PAISES\n");
    printf("------------------------\n");

    if (!cargarArchivoCSV("countries_of_the_world.csv", &cabeza)) {
        printf("\nNo se pudo abrir el archivo countries_of_the_world.csv.\n");
        printf("Coloque el archivo CSV en la misma carpeta desde donde se ejecuta el programa.\n");
        return 1;
    }

    printf("Archivo cargado correctamente.\n");
    printf("Cantidad inicial de paises: %u\n", countNodes(&cabeza));

    do {
        mostrarMenu();
        printf("Seleccione una opcion: ");
        leerLinea(entrada, sizeof(entrada));
        opcion = atoi(entrada);

        switch (opcion) {
            case 1:
                display(&cabeza);
                break;

            case 2:
                printf("\nTotal de paises en la lista: %u\n", countNodes(&cabeza));
                break;

            case 3: {
                country *paisEncontrado;
                printf("\nEscriba el nombre del pais: ");
                leerLinea(entrada, sizeof(entrada));
                paisEncontrado = findCountryByName(&cabeza, entrada);

                if (paisEncontrado != NULL) {
                    mostrarPais(paisEncontrado);
                } else {
                    printf("No se encontro ese pais.\n");
                }
                break;
            }

            case 4:
                printf("\nPromedio del PIB (GDP): %.2f\n", findGDPAverage(&cabeza));
                break;

            case 5: {
                country nuevoPais = leerPaisTeclado();
                insertAtFront(&cabeza, nuevoPais);
                liberarDatosPais(&nuevoPais);
                printf("Pais insertado al principio.\n");
                break;
            }

            case 6: {
                country nuevoPais = leerPaisTeclado();
                insertAtEnd(&cabeza, nuevoPais);
                liberarDatosPais(&nuevoPais);
                printf("Pais insertado al final.\n");
                break;
            }

            case 7:
                printf("\nEscriba el pais que desea borrar: ");
                leerLinea(entrada, sizeof(entrada));
                deleteByName(&cabeza, entrada);
                break;

            case 8:
                if (deleteFirstNode(&cabeza)) {
                    printf("Se borro el primer pais de la lista.\n");
                } else {
                    printf("La lista esta vacia.\n");
                }
                break;

            case 9:
                if (deleteLastNode(&cabeza)) {
                    printf("Se borro el ultimo pais de la lista.\n");
                } else {
                    printf("La lista esta vacia.\n");
                }
                break;

            case 10: {
                unsigned int cantidad = countNodes(&cabeza);
                country *arreglo = listToArray(&cabeza);
                unsigned int limite;
                unsigned int i;

                if (arreglo == NULL) {
                    printf("No se pudo crear el arreglo o la lista esta vacia.\n");
                    break;
                }

                limite = cantidad < 10 ? cantidad : 10;
                printf("\nSe creo un arreglo con %u paises.\n", cantidad);
                printf("Se muestran los primeros %u:\n\n", limite);

                for (i = 0; i < limite; i++) {
                    printf("%2u. %-35s Poblacion: %10u  PIB: %6d\n",
                           i + 1,
                           arreglo[i].nombre_pais,
                           arreglo[i].poblacion,
                           arreglo[i].PIB);
                }

                liberarArregloPaises(arreglo, cantidad);
                break;
            }

            case 11:
                sortLinkedList(&cabeza, NOMBRE);
                printf("Lista ordenada por nombre.\n");
                break;

            case 12:
                sortLinkedList(&cabeza, POBLACION);
                printf("Lista ordenada por poblacion.\n");
                break;

            case 13:
                sortLinkedList(&cabeza, AREA);
                printf("Lista ordenada por area.\n");
                break;

            case 14:
                sortLinkedList(&cabeza, DENSIDAD_POBLACION);
                printf("Lista ordenada por densidad de poblacion.\n");
                break;

            case 0:
                printf("\nFinalizando el programa...\n");
                break;

            default:
                printf("Opcion no valida. Intente otra vez.\n");
        }

    } while (opcion != 0);

    liberarLista(&cabeza);
    printf("Memoria liberada correctamente.\n");

    return 0;
}

void insertAtEnd(Node *cabeza, country paisRecibido) {
    Node *nuevoNodo;
    Node *ultimoNodo;

    if (cabeza == NULL) {
        return;
    }

    nuevoNodo = crearNodo(paisRecibido);
    if (nuevoNodo == NULL) {
        printf("No hay memoria suficiente para insertar el pais.\n");
        return;
    }

    ultimoNodo = cabeza;
    while (ultimoNodo->siguiente != NULL) {
        ultimoNodo = ultimoNodo->siguiente;
    }

    nuevoNodo->anterior = ultimoNodo;
    ultimoNodo->siguiente = nuevoNodo;
}

void insertAtFront(Node *cabeza, country paisRecibido) {
    Node *nuevoNodo;

    if (cabeza == NULL) {
        return;
    }

    nuevoNodo = crearNodo(paisRecibido);
    if (nuevoNodo == NULL) {
        printf("No hay memoria suficiente para insertar el pais.\n");
        return;
    }

    nuevoNodo->siguiente = cabeza->siguiente;
    nuevoNodo->anterior = cabeza;

    if (cabeza->siguiente != NULL) {
        cabeza->siguiente->anterior = nuevoNodo;
    }

    cabeza->siguiente = nuevoNodo;
}

country *findCountryByName(Node *cabeza, char *nombre) {
    Node *nodoActual;

    if (cabeza == NULL || nombre == NULL) {
        return NULL;
    }

    nodoActual = cabeza->siguiente;

    while (nodoActual != NULL) {
        if (compararSinMayusculas(nodoActual->pais->nombre_pais, nombre) == 0) {
            return nodoActual->pais;
        }
        nodoActual = nodoActual->siguiente;
    }

    return NULL;
}

unsigned int countNodes(Node *cabeza) {
    unsigned int cantidad = 0;
    Node *nodoActual;

    if (cabeza == NULL) {
        return 0;
    }

    nodoActual = cabeza->siguiente;

    while (nodoActual != NULL) {
        cantidad++;
        nodoActual = nodoActual->siguiente;
    }

    return cantidad;
}

double findGDPAverage(Node *cabeza) {
    double suma = 0.0;
    unsigned int cantidad = 0;
    Node *nodoActual;

    if (cabeza == NULL) {
        return 0.0;
    }

    nodoActual = cabeza->siguiente;

    while (nodoActual != NULL) {
        suma += nodoActual->pais->PIB;
        cantidad++;
        nodoActual = nodoActual->siguiente;
    }

    if (cantidad == 0) {
        return 0.0;
    }

    return suma / cantidad;
}

void deleteByName(Node *cabeza, char *nombre) {
    Node *nodoActual;

    if (cabeza == NULL || nombre == NULL) {
        return;
    }

    nodoActual = cabeza->siguiente;

    while (nodoActual != NULL) {
        if (compararSinMayusculas(nodoActual->pais->nombre_pais, nombre) == 0) {
            nodoActual->anterior->siguiente = nodoActual->siguiente;

            if (nodoActual->siguiente != NULL) {
                nodoActual->siguiente->anterior = nodoActual->anterior;
            }

            liberarDatosPais(nodoActual->pais);
            free(nodoActual->pais);
            free(nodoActual);

            printf("El pais fue borrado de la lista.\n");
            return;
        }

        nodoActual = nodoActual->siguiente;
    }

    printf("No se encontro el pais indicado.\n");
}

bool deleteFirstNode(Node *cabeza) {
    Node *primerNodo;

    if (cabeza == NULL || cabeza->siguiente == NULL) {
        return false;
    }

    primerNodo = cabeza->siguiente;

    cabeza->siguiente = primerNodo->siguiente;

    if (cabeza->siguiente != NULL) {
        cabeza->siguiente->anterior = cabeza;
    }

    liberarDatosPais(primerNodo->pais);
    free(primerNodo->pais);
    free(primerNodo);

    return true;
}

bool deleteLastNode(Node *cabeza) {
    Node *ultimoNodo;

    if (cabeza == NULL || cabeza->siguiente == NULL) {
        return false;
    }

    ultimoNodo = cabeza->siguiente;

    while (ultimoNodo->siguiente != NULL) {
        ultimoNodo = ultimoNodo->siguiente;
    }

    ultimoNodo->anterior->siguiente = NULL;

    liberarDatosPais(ultimoNodo->pais);
    free(ultimoNodo->pais);
    free(ultimoNodo);

    return true;
}

country *listToArray(Node *cabeza) {
    unsigned int cantidad;
    unsigned int i = 0;
    country *arreglo;
    Node *nodoActual;

    if (cabeza == NULL) {
        return NULL;
    }

    cantidad = countNodes(cabeza);

    if (cantidad == 0) {
        return NULL;
    }

    arreglo = (country *)malloc(cantidad * sizeof(country));
    if (arreglo == NULL) {
        return NULL;
    }

    nodoActual = cabeza->siguiente;

    while (nodoActual != NULL) {
        if (!copiarPais(&arreglo[i], nodoActual->pais)) {
            liberarArregloPaises(arreglo, i);
            return NULL;
        }

        i++;
        nodoActual = nodoActual->siguiente;
    }

    return arreglo;
}

void sortLinkedList(Node *cabeza, Fields campo) {
    bool huboCambio;
    Node *nodoActual;
    country *paisTemporal;

    if (cabeza == NULL || cabeza->siguiente == NULL) {
        return;
    }

    do {
        huboCambio = false;
        nodoActual = cabeza->siguiente;

        while (nodoActual->siguiente != NULL) {
            if (compararPaises(nodoActual->pais, nodoActual->siguiente->pais, campo) > 0) {
                paisTemporal = nodoActual->pais;
                nodoActual->pais = nodoActual->siguiente->pais;
                nodoActual->siguiente->pais = paisTemporal;
                huboCambio = true;
            }

            nodoActual = nodoActual->siguiente;
        }

    } while (huboCambio);
}

void display(Node *cabeza) {
    Node *nodoActual;
    unsigned int numero = 1;

    if (cabeza == NULL || cabeza->siguiente == NULL) {
        printf("\nLa lista esta vacia.\n");
        return;
    }

    printf("\n%-4s %-35s %14s %10s\n", "No.", "Pais", "Poblacion", "PIB");
    printf("---------------------------------------------------------------------\n");

    nodoActual = cabeza->siguiente;

    while (nodoActual != NULL) {
        printf("%-4u %-35s %14u %10d\n",
               numero,
               nodoActual->pais->nombre_pais,
               nodoActual->pais->poblacion,
               nodoActual->pais->PIB);

        numero++;
        nodoActual = nodoActual->siguiente;
    }
}

char *duplicarCadena(const char *texto) {
    char *copia;
    size_t tamano;

    if (texto == NULL) {
        return NULL;
    }

    tamano = strlen(texto) + 1;
    copia = (char *)malloc(tamano);

    if (copia != NULL) {
        strcpy(copia, texto);
    }

    return copia;
}

char *limpiarEspacios(char *texto) {
    char *inicio;
    char *final;

    if (texto == NULL) {
        return texto;
    }

    inicio = texto;

    while (*inicio != '\0' && isspace((unsigned char)*inicio)) {
        inicio++;
    }

    if (*inicio == '\0') {
        return inicio;
    }

    final = inicio + strlen(inicio) - 1;

    while (final > inicio && isspace((unsigned char)*final)) {
        *final = '\0';
        final--;
    }

    return inicio;
}

int separarCampos(char *linea, char *campos[], int maximo) {
    int cantidad = 0;
    bool dentroDeComillas = false;
    char *lectura;
    char *escritura;

    if (linea == NULL || campos == NULL || maximo <= 0) {
        return 0;
    }

    lectura = linea;
    escritura = linea;
    campos[cantidad++] = escritura;

    while (*lectura != '\0') {
        if (*lectura == '"') {
            if (dentroDeComillas && lectura[1] == '"') {
                *escritura = '"';
                escritura++;
                lectura += 2;
            } else {
                dentroDeComillas = !dentroDeComillas;
                lectura++;
            }
        } else if (*lectura == ',' && !dentroDeComillas) {
            *escritura = '\0';
            escritura++;
            lectura++;

            if (cantidad >= maximo) {
                return cantidad + 1;
            }

            campos[cantidad++] = escritura;
        } else {
            *escritura = *lectura;
            escritura++;
            lectura++;
        }
    }

    *escritura = '\0';
    return cantidad;
}

float convertirFloat(const char *texto) {
    if (texto == NULL || texto[0] == '\0') {
        return 0.0f;
    }

    return strtof(texto, NULL);
}

int convertirInt(const char *texto) {
    if (texto == NULL || texto[0] == '\0') {
        return 0;
    }

    return (int)strtol(texto, NULL, 10);
}

unsigned int convertirUnsigned(const char *texto) {
    if (texto == NULL || texto[0] == '\0') {
        return 0;
    }

    return (unsigned int)strtoul(texto, NULL, 10);
}

bool copiarPais(country *destino, const country *origen) {
    if (destino == NULL || origen == NULL) {
        return false;
    }

    *destino = *origen;

    destino->nombre_pais = duplicarCadena(origen->nombre_pais);
    destino->region = duplicarCadena(origen->region);

    if (destino->nombre_pais == NULL || destino->region == NULL) {
        free(destino->nombre_pais);
        free(destino->region);
        destino->nombre_pais = NULL;
        destino->region = NULL;
        return false;
    }

    return true;
}

Node *crearNodo(country paisRecibido) {
    Node *nuevoNodo = (Node *)malloc(sizeof(Node));

    if (nuevoNodo == NULL) {
        return NULL;
    }

    nuevoNodo->pais = (country *)malloc(sizeof(country));

    if (nuevoNodo->pais == NULL) {
        free(nuevoNodo);
        return NULL;
    }

    if (!copiarPais(nuevoNodo->pais, &paisRecibido)) {
        free(nuevoNodo->pais);
        free(nuevoNodo);
        return NULL;
    }

    nuevoNodo->siguiente = NULL;
    nuevoNodo->anterior = NULL;

    return nuevoNodo;
}

void liberarDatosPais(country *pais) {
    if (pais == NULL) {
        return;
    }

    free(pais->nombre_pais);
    free(pais->region);
    pais->nombre_pais = NULL;
    pais->region = NULL;
}

void liberarLista(Node *cabeza) {
    Node *nodoActual;
    Node *nodoSiguiente;

    if (cabeza == NULL) {
        return;
    }

    nodoActual = cabeza->siguiente;

    while (nodoActual != NULL) {
        nodoSiguiente = nodoActual->siguiente;

        liberarDatosPais(nodoActual->pais);
        free(nodoActual->pais);
        free(nodoActual);

        nodoActual = nodoSiguiente;
    }

    cabeza->siguiente = NULL;
}

bool cargarArchivoCSV(const char *nombreArchivo, Node *cabeza) {
    FILE *archivo;
    char linea[TAM_LINEA];
    char *campos[TOTAL_CAMPOS];
    int cantidadCampos;
    int i;
    country pais;

    archivo = fopen(nombreArchivo, "r");

    if (archivo == NULL) {
        return false;
    }

    if (fgets(linea, sizeof(linea), archivo) == NULL) {
        fclose(archivo);
        return false;
    }

    while (fgets(linea, sizeof(linea), archivo) != NULL) {
        cantidadCampos = separarCampos(linea, campos, TOTAL_CAMPOS);

        if (cantidadCampos != TOTAL_CAMPOS) {
            continue;
        }

        for (i = 0; i < TOTAL_CAMPOS; i++) {
            campos[i] = limpiarEspacios(campos[i]);
        }

        pais.nombre_pais = campos[0];
        pais.region = campos[1];
        pais.poblacion = convertirUnsigned(campos[2]);
        pais.area = convertirFloat(campos[3]);
        pais.densidad_poblacion = convertirFloat(campos[4]);
        pais.proporcion_costa = convertirFloat(campos[5]);
        pais.migracion_neta = convertirFloat(campos[6]);
        pais.mortalidad_infantil = convertirFloat(campos[7]);
        pais.PIB = convertirInt(campos[8]);
        pais.alfabetizacion = convertirFloat(campos[9]);
        pais.telefonos = convertirFloat(campos[10]);
        pais.tierra_cultivable = convertirFloat(campos[11]);
        pais.cultivos = convertirFloat(campos[12]);
        pais.otros_terrenos = convertirFloat(campos[13]);
        pais.clima = convertirFloat(campos[14]);
        pais.tasa_natalidad = convertirFloat(campos[15]);
        pais.tasa_mortalidad = convertirFloat(campos[16]);
        pais.agricultura = convertirFloat(campos[17]);
        pais.industria = convertirFloat(campos[18]);
        pais.servicios = convertirFloat(campos[19]);

        insertAtEnd(cabeza, pais);
    }

    fclose(archivo);
    return true;
}

int compararSinMayusculas(const char *texto1, const char *texto2) {
    unsigned char letra1;
    unsigned char letra2;

    if (texto1 == NULL && texto2 == NULL) {
        return 0;
    }

    if (texto1 == NULL) {
        return -1;
    }

    if (texto2 == NULL) {
        return 1;
    }

    while (*texto1 != '\0' && *texto2 != '\0') {
        letra1 = (unsigned char)tolower((unsigned char)*texto1);
        letra2 = (unsigned char)tolower((unsigned char)*texto2);

        if (letra1 != letra2) {
            return letra1 - letra2;
        }

        texto1++;
        texto2++;
    }

    return (unsigned char)*texto1 - (unsigned char)*texto2;
}

int compararPaises(const country *pais1, const country *pais2, Fields campo) {
    if (campo == NOMBRE) {
        return compararSinMayusculas(pais1->nombre_pais, pais2->nombre_pais);
    }

    if (campo == POBLACION) {
        if (pais1->poblacion < pais2->poblacion) return -1;
        if (pais1->poblacion > pais2->poblacion) return 1;
        return 0;
    }

    if (campo == AREA) {
        if (pais1->area < pais2->area) return -1;
        if (pais1->area > pais2->area) return 1;
        return 0;
    }

    if (campo == DENSIDAD_POBLACION) {
        if (pais1->densidad_poblacion < pais2->densidad_poblacion) return -1;
        if (pais1->densidad_poblacion > pais2->densidad_poblacion) return 1;
        return 0;
    }

    return 0;
}

void liberarArregloPaises(country *arreglo, unsigned int cantidad) {
    unsigned int i;

    if (arreglo == NULL) {
        return;
    }

    for (i = 0; i < cantidad; i++) {
        liberarDatosPais(&arreglo[i]);
    }

    free(arreglo);
}

void leerLinea(char *texto, int tamano) {
    size_t largo;

    if (texto == NULL || tamano <= 0) {
        return;
    }

    if (fgets(texto, tamano, stdin) == NULL) {
        texto[0] = '\0';
        return;
    }

    largo = strlen(texto);

    if (largo > 0 && texto[largo - 1] == '\n') {
        texto[largo - 1] = '\0';
    }

    largo = strlen(texto);

    if (largo > 0 && texto[largo - 1] == '\r') {
        texto[largo - 1] = '\0';
    }
}

country leerPaisTeclado(void) {
    country pais = {0};
    char entrada[150];

    printf("\nDATOS DEL NUEVO PAIS\n");

    printf("Nombre: ");
    leerLinea(entrada, sizeof(entrada));
    pais.nombre_pais = duplicarCadena(entrada);

    printf("Region: ");
    leerLinea(entrada, sizeof(entrada));
    pais.region = duplicarCadena(entrada);

    printf("Poblacion: ");
    leerLinea(entrada, sizeof(entrada));
    pais.poblacion = convertirUnsigned(entrada);

    printf("Area: ");
    leerLinea(entrada, sizeof(entrada));
    pais.area = convertirFloat(entrada);

    printf("Densidad de poblacion: ");
    leerLinea(entrada, sizeof(entrada));
    pais.densidad_poblacion = convertirFloat(entrada);

    printf("Costa: ");
    leerLinea(entrada, sizeof(entrada));
    pais.proporcion_costa = convertirFloat(entrada);

    printf("Migracion neta: ");
    leerLinea(entrada, sizeof(entrada));
    pais.migracion_neta = convertirFloat(entrada);

    printf("Mortalidad infantil: ");
    leerLinea(entrada, sizeof(entrada));
    pais.mortalidad_infantil = convertirFloat(entrada);

    printf("PIB (GDP): ");
    leerLinea(entrada, sizeof(entrada));
    pais.PIB = convertirInt(entrada);

    printf("Alfabetizacion: ");
    leerLinea(entrada, sizeof(entrada));
    pais.alfabetizacion = convertirFloat(entrada);

    printf("Telefonos: ");
    leerLinea(entrada, sizeof(entrada));
    pais.telefonos = convertirFloat(entrada);

    printf("Tierra cultivable: ");
    leerLinea(entrada, sizeof(entrada));
    pais.tierra_cultivable = convertirFloat(entrada);

    printf("Cultivos: ");
    leerLinea(entrada, sizeof(entrada));
    pais.cultivos = convertirFloat(entrada);

    printf("Otros terrenos: ");
    leerLinea(entrada, sizeof(entrada));
    pais.otros_terrenos = convertirFloat(entrada);

    printf("Clima: ");
    leerLinea(entrada, sizeof(entrada));
    pais.clima = convertirFloat(entrada);

    printf("Tasa de nacimiento: ");
    leerLinea(entrada, sizeof(entrada));
    pais.tasa_natalidad = convertirFloat(entrada);

    printf("Tasa de mortalidad: ");
    leerLinea(entrada, sizeof(entrada));
    pais.tasa_mortalidad = convertirFloat(entrada);

    printf("Agricultura: ");
    leerLinea(entrada, sizeof(entrada));
    pais.agricultura = convertirFloat(entrada);

    printf("Industria: ");
    leerLinea(entrada, sizeof(entrada));
    pais.industria = convertirFloat(entrada);

    printf("Servicios: ");
    leerLinea(entrada, sizeof(entrada));
    pais.servicios = convertirFloat(entrada);

    return pais;
}

void mostrarPais(const country *pais) {
    if (pais == NULL) {
        return;
    }

    printf("\nDATOS DEL PAIS\n");
    printf("Nombre: %s\n", pais->nombre_pais);
    printf("Region: %s\n", pais->region);
    printf("Poblacion: %u\n", pais->poblacion);
    printf("Area: %.2f\n", pais->area);
    printf("Densidad de poblacion: %.2f\n", pais->densidad_poblacion);
    printf("Costa: %.2f\n", pais->proporcion_costa);
    printf("Migracion neta: %.2f\n", pais->migracion_neta);
    printf("Mortalidad infantil: %.2f\n", pais->mortalidad_infantil);
    printf("PIB (GDP): %d\n", pais->PIB);
    printf("Alfabetizacion: %.2f\n", pais->alfabetizacion);
    printf("Telefonos: %.2f\n", pais->telefonos);
    printf("Tierra cultivable: %.2f\n", pais->tierra_cultivable);
    printf("Cultivos: %.2f\n", pais->cultivos);
    printf("Otros terrenos: %.2f\n", pais->otros_terrenos);
    printf("Clima: %.2f\n", pais->clima);
    printf("Tasa de nacimiento: %.2f\n", pais->tasa_natalidad);
    printf("Tasa de mortalidad: %.2f\n", pais->tasa_mortalidad);
    printf("Agricultura: %.3f\n", pais->agricultura);
    printf("Industria: %.3f\n", pais->industria);
    printf("Servicios: %.3f\n", pais->servicios);
}

void mostrarMenu(void) {
    printf("\nMENU PRINCIPAL\n");
    printf("1. Mostrar todos los paises\n");
    printf("2. Contar los paises\n");
    printf("3. Buscar un pais por nombre\n");
    printf("4. Calcular el promedio del PIB (GDP)\n");
    printf("5. Insertar un pais al principio\n");
    printf("6. Insertar un pais al final\n");
    printf("7. Borrar un pais por nombre\n");
    printf("8. Borrar el primer pais\n");
    printf("9. Borrar el ultimo pais\n");
    printf("10. Convertir la lista en arreglo\n");
    printf("11. Ordenar por nombre\n");
    printf("12. Ordenar por poblacion\n");
    printf("13. Ordenar por area\n");
    printf("14. Ordenar por densidad de poblacion\n");
    printf("0. Salir\n");
}
