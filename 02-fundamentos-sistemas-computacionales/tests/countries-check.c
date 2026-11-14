/* Verificacion posterior de publicacion; no forma parte de la entrega original. */
#define main academic_main
#include "../proyecto-final-paises/solution.c"
#undef main
#include <assert.h>
#include <math.h>

static void links(Node *head) {
    Node *previous = head;
    for (Node *n = head->siguiente; n; n = n->siguiente) {
        assert(n->anterior == previous);
        assert(n->pais && n->pais->nombre_pais);
        previous = n;
    }
}

int main(void) {
    Node head = {0};
    assert(countNodes(&head) == 0);
    assert(findGDPAverage(&head) == 0);
    assert(!deleteFirstNode(&head) && !deleteLastNode(&head));
    country a = {0}, b = {0}, c = {0};
    a.nombre_pais = "Alpha"; a.region = "Test"; a.PIB = 100; a.poblacion = 30; a.area = 2; a.densidad_poblacion = 9;
    b.nombre_pais = "Beta"; b.region = "Test"; b.PIB = 200; b.poblacion = 10; b.area = 3; b.densidad_poblacion = 8;
    c.nombre_pais = "Gamma"; c.region = "Test"; c.PIB = 300; c.poblacion = 20; c.area = 1; c.densidad_poblacion = 7;
    insertAtEnd(&head, b); insertAtFront(&head, a); insertAtEnd(&head, c);
    links(&head);
    assert(countNodes(&head) == 3 && findGDPAverage(&head) == 200);
    assert(findCountryByName(&head, "aLPHA") != NULL);
    assert(findCountryByName(&head, "missing") == NULL);
    country *copy = listToArray(&head);
    assert(copy && strcmp(copy[0].nombre_pais, "Alpha") == 0);
    copy[0].nombre_pais[0] = 'X';
    assert(strcmp(head.siguiente->pais->nombre_pais, "Alpha") == 0);
    liberarArregloPaises(copy, 3);
    for (int field = NOMBRE; field <= DENSIDAD_POBLACION; field++) {
        sortLinkedList(&head, (Fields)field); links(&head);
        for (Node *n = head.siguiente; n && n->siguiente; n = n->siguiente)
            assert(compararPaises(n->pais, n->siguiente->pais, (Fields)field) <= 0);
    }
    deleteByName(&head, "Beta"); links(&head);
    assert(countNodes(&head) == 2 && !findCountryByName(&head, "Beta"));
    assert(deleteFirstNode(&head)); assert(deleteLastNode(&head));
    assert(countNodes(&head) == 0 && head.siguiente == NULL);
    assert(cargarArchivoCSV("countries_of_the_world.csv", &head));
    assert(countNodes(&head) == 227); links(&head);
    assert(findCountryByName(&head, "Afghanistan") != NULL);
    assert(findCountryByName(&head, "Congo, Dem. Rep.") != NULL);
    liberarLista(&head);
    assert(head.siguiente == NULL);
    puts("COUNTRIES CHECKS PASS");
    return 0;
}
