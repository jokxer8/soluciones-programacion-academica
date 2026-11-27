
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/02-fundamentos-sistemas-computacionales-listas-colas-pilas.svg" alt="Listas, colas y pilas (Linked Lists, Queues, and Stacks)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](.)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

---

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Un nodo guarda un dato y el enlace al siguiente. Una cola funciona como una fila de espera: sale quien llegó primero. Una pila funciona como platos apilados: sale primero lo último que pusiste. Los ejercicios muestran qué cambia al buscar, ordenar o retirar datos.

### Ejercicios de esta carpeta, uno por uno

- **Búsqueda de un nodo:** Revisa los datos de cada nodo hasta encontrar el valor buscado. ([ejercicio-01.c](ejercicio-01.c))
- **Conteo de nodos:** Sigue los enlaces y suma 1 cada vez que visites un nodo. ([ejercicio-02.c](ejercicio-02.c))
- **Máximo:** Conserva el mayor valor que hayas encontrado durante el recorrido. ([ejercicio-03.c](ejercicio-03.c))
- **Mínimo:** Conserva el menor valor que hayas encontrado durante el recorrido. ([ejercicio-04.c](ejercicio-04.c))
- **Promedio:** Suma los valores y divide el total entre la cantidad de nodos. ([ejercicio-05.c](ejercicio-05.c))
- **Eliminar primer nodo:** Mueve el inicio al segundo nodo para retirar el primero. ([ejercicio-06.c](ejercicio-06.c))
- **Eliminar por valor:** Busca el valor y enlaza con cuidado el nodo anterior con el siguiente. ([ejercicio-07.c](ejercicio-07.c))
- **Palíndromo:** Compara la secuencia con su versión al revés para ver si se lee igual. ([ejercicio-08.c](ejercicio-08.c))
- **Conversión a arreglo:** Copia los valores a posiciones consecutivas de un arreglo. ([ejercicio-09.c](ejercicio-09.c))
- **Conversión a cadena:** Pasa los datos de la lista a un texto que se pueda mostrar o conservar. ([ejercicio-10.c](ejercicio-10.c))
- **Eliminar duplicados:** Identifica valores repetidos y conserva una sola copia. ([ejercicio-11.c](ejercicio-11.c))
- **Merge sort:** Divide la lista y vuelve a unir sus partes en orden. ([ejercicio-12.c](ejercicio-12.c))
- **Unir listas:** Recorre las dos listas para reunir sus valores en una misma estructura. ([ejercicio-13.c](ejercicio-13.c))
- **Cola con arreglo:** Usa posiciones de un arreglo para mantener el orden de llegada. ([ejercicio-14.c](ejercicio-14.c))
- **Promedio de cola:** Suma los valores de la cola y divídelos entre cuántos hay. ([ejercicio-15.c](ejercicio-15.c))
- **Máximo de cola:** Revisa los valores de la cola para encontrar el mayor. ([ejercicio-16.c](ejercicio-16.c))
- **Cola enlazada:** Une nodos para que se añada al final y se retire del principio. ([ejercicio-17.c](ejercicio-17.c))
- **Pila con arreglo:** Guarda elementos en un arreglo y retira primero el último que agregaste. ([ejercicio-18.c](ejercicio-18.c))
- **Pila enlazada:** Guarda el tope en un nodo; al retirarlo, el siguiente pasa a ser el nuevo tope. ([ejercicio-19.c](ejercicio-19.c))

### Las cuatro preguntas por tema

| Tema | ¿Por qué? | ¿Para qué? | ¿Cómo? | ¿Cuándo usarlo? |
|---|---|---|---|---|
| Búsqueda de un nodo | Un enlace lleva al siguiente nodo, no directamente al que buscas. | Localizar un valor en una lista. | Compara el dato de cada nodo y avanza hasta encontrarlo o llegar al final. | Al consultar una lista sin índice directo. |
| Conteo de nodos | En una lista enlazada el tamaño no se deduce del puntero inicial. | Calcular cuántos elementos hay. | Empieza en cero y suma uno por cada nodo visitado. | Cuando no mantienes un contador de tamaño aparte. |
| Máximo | Sin orden previo el valor mayor puede estar en cualquier nodo. | Encontrar el extremo superior. | Toma el primer dato como referencia y reemplázalo si encuentras uno mayor. | Al resumir una lista no vacía, incluso si todos los valores son negativos. |
| Mínimo | El elemento más pequeño no tiene por qué estar al inicio. | Encontrar el extremo inferior. | Toma el primer dato y actualiza la referencia si ves uno menor. | Al buscar el menor valor de una lista no vacía. |
| Promedio | La suma sola no considera el tamaño de la lista. | Obtener una media de los valores. | Acumula suma y cantidad; divide solo si la cantidad es positiva. | Al agregar valores numéricos de una lista no vacía. |
| Eliminar primer nodo | El primer elemento tiene un enlace especial: la referencia de inicio. | Retirar el nodo sin perder acceso al resto. | Si hay primer nodo, conserva su sucesor, mueve el inicio y libera el retirado. | Cuando quieres retirar la cabeza de una lista. |
| Eliminar por valor | Borrar un nodo intermedio puede romper la cadena si no conectas los vecinos. | Retirar el valor solicitado sin perder los demás. | Busca el nodo y conecta su anterior con su sucesor; si es el primero, actualiza el inicio. | Cuando debes borrar por contenido, no por posición conocida. |
| Palíndromo | El orden de los valores importa tanto como sus valores. | Saber si se leen igual desde ambos extremos. | Contrasta una secuencia como `1,2,1` con su versión invertida. | Al comprobar simetría en una secuencia. |
| Conversión a arreglo | Un arreglo da acceso por índice y una lista se sigue por enlaces. | Pasar los datos a posiciones consecutivas. | Recorre la lista y copia cada valor a la siguiente posición. | Cuando necesitas entregar los datos a una función que espera un arreglo. |
| Conversión a cadena | Los nodos enlazados no son una representación lista para lectura. | Mostrar o guardar los datos como texto. | Recorre los nodos y aplica un formato con un separador claro. | Cuando el resultado lo va a leer una persona o un sistema de texto. |
| Eliminar duplicados | Un dato repetido no siempre representa un elemento distinto. | Conservar una aparición por valor según la regla del ejercicio. | Compara los valores y retira las repeticiones sin perder enlaces. | Cuando el objetivo es una lista de valores únicos. |
| Merge sort | Ordenar una lista completa resulta más simple si divides el trabajo. | Ordenar enlazando partes ya ordenadas. | Parte la lista, ordena las partes y mezcla tomando primero el menor frente. | Cuando necesitas ordenar una lista enlazada por comparación. |
| Unir listas | Dos cadenas de nodos no se convierten solas en una. | Reunir los datos de dos secuencias. | Recorre ambas y reproduce el orden de unión definido en el ejercicio. | Cuando necesitas presentar como conjunto datos de dos listas. |
| Cola con arreglo | El turno de llegada debe preservarse aunque se retiren elementos. | Practicar la regla primero en entrar, primero en salir. | Inserta `A` y luego `B`: al retirar, debe salir `A`; sigue los índices. | Cuando puedes reservar un arreglo para la cola. |
| Promedio de cola | La posición en la cola no cambia la media de sus valores. | Resumir todos los datos pendientes con un número. | Suma los valores de la cola y divide entre su cantidad, si no está vacía. | Al calcular la media de los elementos que esperan turno. |
| Máximo de cola | El valor mayor no necesariamente tiene el primer turno. | Encontrar el mayor dato de la fila. | Recorre sus valores y actualiza el máximo cuando veas uno superior. | Cuando debes consultar un extremo sin confundirlo con el orden de salida. |
| Cola enlazada | Referenciar ambos extremos facilita agregar y retirar. | Representar la regla de llegada mediante nodos. | Enlaza el elemento nuevo al final y cambia el frente al retirar. | Cuando el tamaño cambia y prefieres nodos a posiciones de un arreglo. |
| Pila con arreglo | El elemento recién añadido debe ser el primero en salir. | Practicar operaciones sobre un tope indexado. | Añade `A`, luego `B`; al retirar, sale `B` y el tope retrocede. | Al modelar un historial de deshacer con capacidad reservada. |
| Pila enlazada | Cada elemento necesita apuntar al que queda debajo. | Representar una pila con nodos. | Añade un nodo que apunte al tope anterior; al retirarlo, su sucesor es el nuevo tope. | Cuando la cantidad de elementos de la pila varía durante la ejecución. |

## Qué trabajé y cómo leer los ejemplos

Organizo datos mediante nodos enlazados. FIFO significa que sale primero el elemento que entró primero, como en una cola; LIFO significa que sale primero el último que entró, como en una pila. Los números de ejercicio sirven para localizar cada programa independiente.

## Descripción de la actividad

Diecinueve ejercicios de búsqueda, agregación, eliminación, ordenamiento, FIFO y LIFO.

Materia: [Fundamentos de Sistemas Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

---

## Objetivo

Implementar y comparar estructuras lineales, entendiendo cómo su representación y sus reglas de acceso determinan las operaciones, los recorridos y la gestión de memoria.

---

## Conceptos aplicados

- Nodos, enlaces y recorridos de listas.
- Búsqueda, agregación, eliminación y eliminación de duplicados.
- Conversión entre lista, arreglo y cadena.
- Ordenamiento por merge sort y combinación de listas.
- Colas FIFO y pilas LIFO con arreglos y nodos.

---

## Actividad desarrollada

Los ejercicios 01–13 amplían gradualmente una lista: localizar datos, calcular agregados, eliminar, transformar y ordenar. Los ejercicios 14–19 comparan colas y pilas con almacenamiento contiguo y enlazado. La tabla conserva el acceso individual a las diecinueve fuentes.

## Archivos y soluciones

| Código | Ejercicio |
|---|---|
| [ejercicio-01.c](ejercicio-01.c) | Búsqueda de un nodo |
| [ejercicio-02.c](ejercicio-02.c) | Conteo de nodos |
| [ejercicio-03.c](ejercicio-03.c) | Máximo |
| [ejercicio-04.c](ejercicio-04.c) | Mínimo |
| [ejercicio-05.c](ejercicio-05.c) | Promedio |
| [ejercicio-06.c](ejercicio-06.c) | Eliminar primer nodo |
| [ejercicio-07.c](ejercicio-07.c) | Eliminar por valor |
| [ejercicio-08.c](ejercicio-08.c) | Palíndromo |
| [ejercicio-09.c](ejercicio-09.c) | Conversión a arreglo |
| [ejercicio-10.c](ejercicio-10.c) | Conversión a cadena |
| [ejercicio-11.c](ejercicio-11.c) | Eliminar duplicados |
| [ejercicio-12.c](ejercicio-12.c) | Merge sort |
| [ejercicio-13.c](ejercicio-13.c) | Unir listas |
| [ejercicio-14.c](ejercicio-14.c) | Cola con arreglo |
| [ejercicio-15.c](ejercicio-15.c) | Promedio de cola |
| [ejercicio-16.c](ejercicio-16.c) | Máximo de cola |
| [ejercicio-17.c](ejercicio-17.c) | Cola enlazada |
| [ejercicio-18.c](ejercicio-18.c) | Pila con arreglo |
| [ejercicio-19.c](ejercicio-19.c) | Pila enlazada |

Cada archivo tiene su propio `main`; se compila y ejecuta por separado. Los datos de ejemplo y límites del enunciado definen el alcance académico.

---

## Ejemplo de análisis y uso

| Estructura u operación | Secuencia de prueba | Resultado esperado |
|---|---|---|
| Cola FIFO | Insertar `10`, insertar `20`, retirar | Sale `10`; permanece `20`. |
| Pila LIFO | Apilar `10`, apilar `20`, retirar | Sale `20`; permanece `10`. |
| Promedio de cola | `10, 20, 30` | `20.00`. |
| Ordenamiento de lista | `7 → 3 → 9 → 1 → 5` | `1 → 3 → 5 → 7 → 9`. |

Las secuencias permiten observar la política de acceso, no solamente que el programa termine sin errores.

---

## Organización de esta carpeta

```text
listas-colas-pilas/
├── README.md
├── desarrollo.md
├── ejercicio-01.c
├── ejercicio-02.c
├── ...
└── ejercicio-19.c
```

---

## Cómo revisar la actividad

Compilar un solo archivo a la vez con C17. Para las estructuras con menú, repetir la secuencia de inserción y extracción de la tabla. Contrastar el orden de salida y el contenido restante. Los ejercicios demostrativos sin entrada muestran sus datos de ejemplo al ejecutarse; el informe distingue esas ejecuciones básicas de los casos con valores esperados específicos.

---

## Relación con los archivos originales y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/hw04-v2.pdf). El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). Las correcciones se explican por separado de las entregas originales.

---

## Lenguaje y herramientas

C17, biblioteca estándar y herramientas C de Visual Studio en la batería Windows registrada.

## Estado de la actividad

Actividad organizada y documentada como parte del portafolio académico. Los resultados verificados y los límites se detallan en los informes enlazados; la documentación posterior se distingue de la evidencia de entrega.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
