# Listas, colas y pilas

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](.)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

---

## Qué trabajé y cómo leer los ejemplos

Organizo datos mediante nodos enlazados. FIFO significa que sale primero el elemento que entró primero, como en una cola; LIFO significa que sale primero el último que entró, como en una pila. Los números de ejercicio sirven para localizar cada programa independiente.

## Descripción de la actividad

Diecinueve ejercicios de búsqueda, agregación, eliminación, ordenamiento, FIFO y LIFO.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

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
