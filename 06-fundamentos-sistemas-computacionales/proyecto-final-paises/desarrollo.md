# Desarrollo — Gestión de países — Proyecto final

## Diseño y gestión de memoria

Un nodo cabecera representa la lista vacía sin almacenar un país. Cada nodo posee una copia de `country` y sus cadenas. Al insertar se realiza copia profunda; convertir a arreglo también crea copias independientes. Borrar o liberar debe destruir las cadenas, el país y el nodo, conservando la coherencia de enlaces anteriores/siguientes.

Las búsquedas y el conteo recorren la lista. El ordenamiento compara nombre, población, área o densidad y conserva los enlaces. El lector CSV trata comillas y campos con comas; el conjunto contiene 227 registros, incluidos nombres como `Congo, Dem. Rep.`. Una división ingenua por comas no es una validación válida del CSV.

Las comprobaciones posteriores verifican lista vacía, inserción en extremos, búsqueda, promedio, copia profunda, eliminación, cuatro criterios de ordenamiento, enlaces y carga CSV. Se utiliza el código real mediante inclusión con renombrado de `main`, sin duplicar la implementación.

La auditoría TXT original menciona sanitizadores. Es una declaración histórica de la entrega; la revisión publicada usa MSVC y no vuelve a certificar ASan/UBSan ni todas las condiciones de memoria posibles.


[Actividad](README.md) · [Resultados comunes](../VALIDACION.md)
