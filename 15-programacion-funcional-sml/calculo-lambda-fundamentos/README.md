
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/15-programacion-funcional-sml-calculo-lambda-fundamentos.svg" alt="Cálculo Lambda - Fundamentos de Programación Funcional (Lambda Calculus — Functional Programming Fundamentals)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Evidencia](actividad-calculo-lambda-fundamentos.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SML-purple)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Una función recibe algo y devuelve un resultado. En cálculo lambda practicas reemplazar una variable por el argumento: si la regla dice `x + 1` y das `4`, obtienes `5`. El orden de las sustituciones y los nombres de las variables importan.

### [Ejercicio 1.1 · Función y cuerpo](desarrollo.md#ejercicio-11)

- **¿Por qué?** Sin separar el parámetro del cuerpo no se ve qué letra queda ligada.
- **¿Para qué?** Para dibujar una abstracción sencilla.
- **¿Cómo?** En `λz.zx`, coloca `λz` arriba y la aplicación de `z` a `x` debajo.
- **¿Cuándo usarlo?** Al descomponer una función de un parámetro.

### [Ejercicio 1.2 · Dos funciones](desarrollo.md#ejercicio-12)

- **¿Por qué?** Una función también puede recibir otra función.
- **¿Para qué?** Para localizar qué parte actúa como argumento.
- **¿Cómo?** Dibuja una aplicación en la raíz y una función en cada rama.
- **¿Cuándo usarlo?** Al leer una expresión con dos funciones yuxtapuestas.

### [Ejercicio 1.3 · Funciones anidadas](desarrollo.md#ejercicio-13)

- **¿Por qué?** El anidamiento cambia dónde está cada parámetro.
- **¿Para qué?** Para separar las funciones exteriores de la aplicación interna.
- **¿Cómo?** Sigue `λx`, después `λy`, y dibuja la aplicación de la función interior a `u`.
- **¿Cuándo usarlo?** Cuando una expresión empiece con varios lambdas seguidos.

### [Ejercicio 2.1 · Evitar captura](desarrollo.md#ejercicio-21)

- **¿Por qué?** Un nombre ligado puede capturar una letra que entra libre.
- **¿Para qué?** Para conservar el significado del argumento al sustituir.
- **¿Cómo?** Renombra el `x` ligado de la expresión original antes de sustituir la `z` libre.
- **¿Cuándo usarlo?** Si la expresión que introduces trae letras libres con nombres ya ligados.

### [Ejercicio 2.2 · Distinguir dos z](desarrollo.md#ejercicio-22)

- **¿Por qué?** La misma letra puede aparecer como parámetro y como argumento libre.
- **¿Para qué?** Para cambiar solo la aparición correcta.
- **¿Cómo?** Sustituye la `z` que queda fuera de la función por `y`, sin tocar la `z` que liga `λz`.
- **¿Cuándo usarlo?** Cuando el nombre por sustituir aparece dentro y fuera de su función.

### [Ejercicio 2.3 · Renombrar primero](desarrollo.md#ejercicio-23)

- **¿Por qué?** Introducir `y` bajo `λy` cambiaría erróneamente su alcance.
- **¿Para qué?** Para que la `y` introducida conserve su condición de libre.
- **¿Cómo?** Convierte `λy.z` en `λa.z` antes de sustituir `z` por `y`.
- **¿Cuándo usarlo?** Cuando la letra que entra coincida con un parámetro del lugar de destino.

### [Ejercicio 3.1 · Encadenar reducciones](desarrollo.md#ejercicio-31)

- **¿Por qué?** Una aplicación simplificada puede dejar otra lista para reducir.
- **¿Para qué?** Para seguir el cálculo sin perder el orden de las aplicaciones.
- **¿Cómo?** Aplica la función exterior a `z` y luego reduce la aplicación que queda, hasta obtener `yzy`.
- **¿Cuándo usarlo?** Cuando un término tenga aplicaciones simplificables dentro de otras.

### [Ejercicio 3.2 · Preparar un argumento](desarrollo.md#ejercicio-32)

- **¿Por qué?** El argumento exterior puede contener sus propias aplicaciones.
- **¿Para qué?** Para comparar el resultado interno con el de la función completa.
- **¿Cómo?** Reduce primero el argumento hasta `rur` y sustituye después en la función exterior.
- **¿Cuándo usarlo?** Cuando convenga resolver un argumento por partes en el papel.

### [Ejercicio 3.3 · Reconocer el final](desarrollo.md#ejercicio-33)

- **¿Por qué?** Ver una función no significa que aún se pueda aplicar o reducir.
- **¿Para qué?** Para decidir cuándo llegó el término a forma normal.
- **¿Cómo?** Reduce `(λz.z)y` a `y`; la función restante actúa como argumento, no tiene por qué aplicarse.
- **¿Cuándo usarlo?** Al revisar si queda algún punto donde una función tenga un argumento que pueda sustituirse.

## Qué trabajé y cómo leer los ejemplos

Estudio funciones mediante variables, abstracción y aplicación. En λx.x, λ introduce una función y x es su parámetro. La aplicación (λx.x) 5 se reduce a 5 por sustitución; el símbolo λ no representa una constante numérica.

## Descripción de la actividad

Esta actividad trabaja conceptos fundamentales del cálculo lambda, una base teórica importante para comprender la programación funcional.

El ejercicio incluye la construcción de árboles de sintaxis, sustituciones y reducciones beta para obtener formas normales de expresiones lambda.

---

## Objetivo

Comprender la estructura y evaluación de expresiones lambda mediante:

- Representación de λ-términos.
- Construcción de árboles de sintaxis.
- Sustitución de variables.
- Aplicación de reducciones beta.
- Obtención de formas normales.

---

## Conceptos aplicados

- Cálculo lambda.
- Programación funcional.
- Abstracción lambda.
- Variables libres y ligadas.
- Aplicación de funciones.
- Sustitución.
- Reducción beta.
- Forma normal.
- Árboles de sintaxis.

---

## Importancia en Programación Funcional

El cálculo lambda permite entender cómo se representan y evalúan las funciones en los lenguajes funcionales.

Aunque esta actividad no contiene código directamente en SML, fortalece los fundamentos necesarios para trabajar con funciones, recursividad, patrones, listas y evaluación de expresiones en Standard ML.

---

## Contenido de la actividad

La práctica se divide en tres partes:

1. Dibujar árboles de sintaxis de λ-términos.
2. Realizar sustituciones en expresiones lambda.
3. Aplicar reducciones beta para obtener formas normales.

---

## Archivos de esta carpeta

```text
calculo-lambda-fundamentos/
├── README.md
├── desarrollo.md
└── actividad-calculo-lambda-fundamentos.pdf
```

El archivo `README.md` presenta la actividad de forma general.

El archivo `desarrollo.md` contiene una versión organizada y explicada del desarrollo de la actividad.

El archivo `actividad-calculo-lambda-fundamentos.pdf` conserva la evidencia documental de la práctica original.

---

## Relación con los archivos originales

El documento original de la actividad también se conserva como respaldo académico en:

```text
archivos-originales/programacion-funcional-sml/
```

Esta carpeta contiene una versión organizada y profesional del desarrollo, mientras que la carpeta de archivos originales conserva la evidencia académica inicial.

---

## Nota sobre datos personales

El PDF complementa la explicación y permite consultar el planteamiento académico de la actividad.

En caso de que el documento original contenga datos personales, se debe subir una versión limpia o censurada.

---

## Estado

Actividad organizada y documentada como parte del portafolio académico de Programación Funcional-SML.

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
