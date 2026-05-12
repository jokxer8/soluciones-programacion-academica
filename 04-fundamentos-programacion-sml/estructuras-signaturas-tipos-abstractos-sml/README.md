# Estructuras, Signaturas y Tipos Abstractos en SML

## Descripción de la actividad

Esta actividad trabaja conceptos avanzados de Standard ML relacionados con signaturas, estructuras, tipos abstractos de datos, records, listas de asociación, excepciones y tipos algebraicos.

El ejercicio se divide en dos partes principales:

1. Implementar una signatura de estudiante usando dos estructuras diferentes.
2. Definir una estructura y signatura para un tipo abstracto de secuencia.

---

## Objetivo

Aplicar los mecanismos de modularidad de SML para construir tipos de datos abstractos mediante signaturas y estructuras.

La actividad busca demostrar cómo una misma interfaz puede implementarse con diferentes representaciones internas, manteniendo ocultos los detalles de implementación.

---

## Conceptos aplicados

- Standard ML.
- Programación funcional.
- Signaturas.
- Estructuras.
- Tipos abstractos de datos.
- Encapsulación.
- Listas de asociación.
- Records.
- Excepciones.
- Tipos algebraicos.
- Tipos polimórficos.
- Pruebas de funciones.
- Manejo de errores.

---

## Parte 1: Signatura de estudiante

La primera parte define una signatura llamada `ESTUDIANTE`.

Esta signatura permite crear y manipular datos de un estudiante mediante funciones `put` y `get`.

Las funciones `put` permiten agregar datos como:

- Nombre.
- Apellido.
- ID.
- Género.
- Año académico.
- Índice.

Las funciones `get` permiten recuperar esos valores.

Una regla importante del ejercicio es que cada función `put` solo puede ser llamada una vez para cada propiedad. Si se intenta colocar una propiedad repetida, el programa debe lanzar una excepción.

---

## Implementaciones realizadas

La signatura `ESTUDIANTE` se implementa de dos maneras:

### 1. Estructura con lista de asociación

Utiliza una lista de pares ordenados donde cada elemento contiene:

```text
nombre de la propiedad, valor de la propiedad
```

### 2. Estructura con record

Utiliza un record de SML con campos opcionales.

Cada campo inicia como `NONE` y, al asignarse un valor, pasa a ser `SOME valor`.

---

## Parte 2: Tipo abstracto de secuencia

La segunda parte define una signatura y estructura para representar una secuencia.

Una secuencia puede ser:

- Vacía.
- Una secuencia con un solo elemento.
- Una secuencia formada por la unión de dos secuencias.

La estructura implementa funciones para:

- Agregar un elemento al inicio.
- Agregar un elemento al final.
- Unir dos secuencias.
- Convertir una secuencia a lista.

---

## Corrección aplicada

En la versión profesionalizada se agregan casos de prueba con resultados esperados para mostrar la efectividad de las funciones.

Esto corrige la debilidad señalada en el documento original, donde faltaba mostrar claramente la ejecución y los resultados de los casos de prueba.

---

## Archivos de esta carpeta

```text
estructuras-signaturas-tipos-abstractos-sml/
├── README.md
├── desarrollo.md
├── solution.sml
└── actividad-estructuras-signaturas-tipos-abstractos.pdf
```

El archivo `README.md` presenta la actividad de forma general.

El archivo `desarrollo.md` contiene la explicación organizada de la solución y los casos de prueba.

El archivo `solution.sml` contiene la implementación limpia y corregida en Standard ML.

El archivo `actividad-estructuras-signaturas-tipos-abstractos.pdf` conserva la evidencia documental de la práctica original.

---

## Relación con los archivos originales

El documento original de la actividad se conserva como respaldo académico en:

```text
archivos-originales/programacion-funcional-sml/
```

Esta carpeta contiene una versión organizada, limpia y profesional de la actividad.

---

## Nota sobre datos personales

Para mantener una presentación profesional y proteger información personal, se recomienda que el PDF utilizado como evidencia no contenga datos sensibles como ID, matrícula, correo, calificaciones o información privada.

En caso de que el documento original contenga datos personales, se debe subir una versión limpia o censurada.

---

## Estado

Actividad organizada, corregida y documentada como parte del portafolio académico de Programación Funcional con SML.
