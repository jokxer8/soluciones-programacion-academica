# Desarrollo — Agenda de Citas - Programación Orientada a Objetos

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

Esta guía reúne el análisis técnico ya documentado en la actividad. El README conserva el enunciado, los ejemplos y las explicaciones originales.

## Objetivo

Aplicar principios de programación orientada a objetos para representar una agenda de citas utilizando clases, objetos, constructores y validaciones.

## Conceptos aplicados

- Clases
- Objetos
- Constructores
- Propiedades
- Encapsulación
- Enumeraciones
- Listas
- Validaciones
- Excepciones
- Métodos

## Estructura del programa

El programa contiene las siguientes partes:

### Enumeración `DiaSemana`

Representa los días de la semana disponibles para una cita.

### Clase `Cita`

Representa una cita individual.

Contiene:

- Día de la semana
- Hora
- Minuto
- Descripción

### Clase `AgendaMedica`

Representa una agenda que puede almacenar varias citas.

Contiene métodos para:

- Agregar una cita
- Mostrar todas las citas registradas

## Validaciones realizadas

El constructor de la clase `Cita` valida que:

- La hora esté entre 0 y 23.
- Los minutos estén entre 0 y 59.
- La descripción no esté vacía.

Si algún dato no es válido, el programa lanza una excepción de tipo `ArgumentException`.

## Análisis

Este ejercicio permite practicar el diseño de clases con responsabilidades claras.

La clase `Cita` se encarga de representar y validar los datos de una cita, mientras que la clase `AgendaMedica` administra el conjunto de citas registradas.
