# Desarrollo — Listas, colas y pilas

## Diseño

Los ejercicios 1–13 trabajan con listas: búsqueda y agregados O(n), eliminación con reconexión de enlaces, transformación a arreglo/cadena y ordenamiento. Merge sort divide y combina listas y tiene tiempo O(n log n). La búsqueda por valor sigue siendo lineal.

Las colas preservan FIFO y las pilas LIFO. Se comparan representaciones con arreglos de capacidad fija y nodos dinámicos. Insertar 10 y 20 y retirar un elemento debe retirar 10 en una cola y 20 en una pila.

El registro de validación distingue demostraciones que solo se ejecutan como comprobación básica de aquellas que contrastan valores esperados. No se afirma cobertura de todos los límites o errores de entrada.

---

## Comparación de estructuras

| Estructura | Regla de acceso | Aspecto que se revisa |
|---|---|---|
| Lista | Recorrido de nodos relacionados por enlaces. | No perder el resto de la cadena al insertar o eliminar. |
| Cola | Primero en entrar, primero en salir (FIFO). | Conservar el orden de llegada y detectar cola vacía. |
| Pila | Último en entrar, primero en salir (LIFO). | Retirar desde la cima y detectar pila vacía. |

## Coste y representación

Buscar, contar o calcular extremos en una lista requiere un recorrido O(n). Merge sort divide la lista y combina partes ordenadas en O(n log n). La eliminación por valor incluye el coste de localizar el nodo.

Un arreglo obliga a respetar su capacidad; una versión enlazada exige gestionar nodos y liberarlos. El coste de insertar o retirar depende de la representación concreta: una cola que desplaza elementos de un arreglo no tiene el mismo coste que una cola con enlaces y extremos.

## Casos de revisión

Además del orden FIFO/LIFO, conviene distinguir estructura vacía, un solo elemento, eliminación en un extremo y eliminación interior. La batería publicada registra las demostraciones y los casos realmente ejecutados; estos criterios no se presentan como cobertura nueva de todos los límites.

---

[Actividad](README.md) · [Resultados comunes](../VALIDACION.md)
