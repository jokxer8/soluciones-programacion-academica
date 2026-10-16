# Estructuras y memoria dinámica

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](.)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

---

## Qué trabajé y cómo leer los ejemplos

Trabajo con estructuras y punteros para representar datos relacionados. Un puntero guarda una dirección de memoria; una dirección escrita en hexadecimal identifica una ubicación durante una ejecución y no debe interpretarse como el resultado numérico del problema.

## Descripción de la actividad

Diez ejercicios de estructuras, punteros, rectángulos, complejos y listas.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

---

## Objetivo

Representar entidades mediante estructuras y trabajar con sus direcciones, relacionando almacenamiento automático, reserva dinámica y liberación de memoria con operaciones concretas.

---

## Conceptos aplicados

- Definición y acceso a miembros de struct.
- Punteros, paso por dirección y representación de cadenas.
- Reserva y liberación de memoria dinámica.
- Modelado de estudiantes, libros, tiempos, rectángulos y complejos.
- Construcción y recorrido de listas de nodos.

---

## Actividad desarrollada

Los diez programas avanzan desde registros y cálculos sobre estructuras hasta objetos reservados dinámicamente y recorridos de listas. Cada fuente mantiene su propio main: no se enlazan como si fueran un único programa.

## Archivos y soluciones

| Código | Ejercicio |
|---|---|
| [ejercicio-01.c](ejercicio-01.c) | Promedio de estudiantes |
| [ejercicio-02.c](ejercicio-02.c) | Suma de tiempos con acarreo |
| [ejercicio-03.c](ejercicio-03.c) | Libros de mayor y menor precio |
| [ejercicio-04.c](ejercicio-04.c) | Área de un rectángulo |
| [ejercicio-05.c](ejercicio-05.c) | Pertenencia de un punto |
| [ejercicio-06.c](ejercicio-06.c) | Rectángulo con memoria dinámica |
| [ejercicio-07.c](ejercicio-07.c) | Representación de complejos |
| [ejercicio-08.c](ejercicio-08.c) | Suma dinámica de complejos |
| [ejercicio-09.c](ejercicio-09.c) | Complejo convertido a cadena |
| [ejercicio-10.c](ejercicio-10.c) | Lista en orden directo e inverso |

Cada archivo tiene su propio `main`; se compila y ejecuta por separado. Los datos de ejemplo y límites del enunciado definen el alcance académico.

---

## Ejemplo de análisis y uso

| Programa | Datos de ejemplo | Resultado esperado |
|---|---|---|
| Suma de tiempos | `01:50:30` y `02:20:45` | `04:11:15`, aplicando los acarreos. |
| Área de rectángulo | Esquinas `(0,10)` y `(5,0)` | `50.00`. |
| Suma de complejos | `1 + j2` y `3 + j4` | `4.00 + j6.00`. |
| Recorrido inverso | Valores `5, 6, 7` | Orden directo `5, 6, 7`; inverso `7, 6, 5`. |

---

## Organización de esta carpeta

```text
estructuras-y-memoria/
├── README.md
├── desarrollo.md
├── ejercicio-01.c
├── ejercicio-02.c
├── ...
└── ejercicio-10.c
```

---

## Cómo revisar la actividad

Elegir una fuente y compilarla de forma independiente. En una consola de desarrollador de Visual Studio:

```text
cl /std:c17 /D_CRT_SECURE_NO_WARNINGS ejercicio-02.c /Fe:tiempos.exe
tiempos.exe
```

Introducir por separado horas, minutos y segundos de ambos tiempos. Los datos de la tabla permiten contrastar el resultado. La [validación común](../VALIDACION.md) registra los casos ejecutados.

---

## Relación con los archivos originales y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/hw03-v2.pdf). El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). Las correcciones se explican por separado de las entregas originales.

---

## Lenguaje y herramientas

C17, biblioteca estándar de C y herramientas C de Visual Studio en la validación registrada.

## Estado de la actividad

Actividad organizada y documentada como parte del portafolio académico. Los resultados verificados y los límites se detallan en los informes enlazados; la documentación posterior se distingue de la evidencia de entrega.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
