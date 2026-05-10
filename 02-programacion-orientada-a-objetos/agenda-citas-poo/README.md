# Agenda de Citas - Programación Orientada a Objetos

## Descripción del problema

Este ejercicio consiste en crear un programa para registrar y mostrar citas dentro de una agenda.

Cada cita contiene información sobre el día de la semana, la hora, los minutos y una descripción. El programa valida que los datos ingresados sean correctos antes de crear una cita.

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

## Ejemplo de salida

```text
Agenda de citas

Citas registradas:

Lunes - 09:30 - Consulta general
Miércoles - 14:00 - Revisión de resultados
Viernes - 16:45 - Seguimiento del paciente
```

## Análisis

Este ejercicio permite practicar el diseño de clases con responsabilidades claras.

La clase `Cita` se encarga de representar y validar los datos de una cita, mientras que la clase `AgendaMedica` administra el conjunto de citas registradas.

## Lenguaje utilizado

C#
