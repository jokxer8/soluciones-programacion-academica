# Desarrollo — Sistema de Nómina con Polimorfismo - POO

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## Objetivo

Aplicar conceptos avanzados de programación orientada a objetos mediante el uso de clases abstractas, herencia, sobrescritura de métodos y polimorfismo.

## Conceptos aplicados

- Programación orientada a objetos
- Abstracción
- Encapsulación
- Herencia
- Polimorfismo
- Clases abstractas
- Métodos abstractos
- Sobrescritura de métodos
- Listas de objetos
- Validación de datos
- Separación de responsabilidades

## Estructura del programa

El programa contiene una clase abstracta principal llamada `Empleado`.

A partir de ella se derivan diferentes tipos de empleados:

### Clase `EmpleadoFijo`

Representa un empleado que recibe un salario fijo mensual.

### Clase `EmpleadoPorHora`

Representa un empleado cuyo salario depende de las horas trabajadas y el pago por hora.

### Clase `EmpleadoComision`

Representa un empleado que recibe un salario base más una comisión por ventas.

### Clase `Nomina`

Administra una lista de empleados y calcula el total general a pagar.

## Análisis

Este ejercicio demuestra un diseño orientado a objetos más avanzado.

La clase abstracta `Empleado` define una estructura común para todos los empleados, pero delega el cálculo del salario a las clases hijas. Esto permite extender el sistema agregando nuevos tipos de empleados sin modificar la lógica principal de la nómina.
