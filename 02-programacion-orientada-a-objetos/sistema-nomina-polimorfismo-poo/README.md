# Sistema de Nómina con Polimorfismo - POO

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## Qué trabajé y cómo leer los ejemplos

Modelo distintas formas de calcular un pago mediante herencia y polimorfismo. Los importes del ejemplo son datos de práctica. En cada fórmula hay que distinguir cantidades monetarias, horas y tasas: una tasa de 0.10 equivale al 10 %, mientras que 10 como multiplicador tendría otro significado.

## Planteamiento

Este ejercicio consiste en crear un sistema de nómina utilizando programación orientada a objetos.

El programa permite calcular el salario de diferentes tipos de empleados, aplicando polimorfismo para tratar todos los empleados de forma general, aunque cada uno calcule su salario de manera diferente.

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

## Polimorfismo aplicado

El polimorfismo se evidencia cuando una lista de tipo `Empleado` almacena objetos de diferentes clases derivadas:

```text
EmpleadoFijo
EmpleadoPorHora
EmpleadoComision
```

Aunque todos se manejan como empleados, cada uno ejecuta su propia versión del método `CalcularSalario()`.

## Ejemplo de salida

```text
Sistema de Nómina

Empleado fijo:
Nombre: Ana Rodríguez
Salario calculado: 35000

Empleado por hora:
Nombre: Carlos Méndez
Salario calculado: 24000

Empleado por comisión:
Nombre: Laura Pérez
Salario calculado: 42000

Total general de nómina: 101000
```

## Análisis

Este ejercicio demuestra un diseño orientado a objetos más avanzado.

La clase abstracta `Empleado` define una estructura común para todos los empleados, pero delega el cálculo del salario a las clases hijas. Esto permite extender el sistema agregando nuevos tipos de empleados sin modificar la lógica principal de la nómina.

## Lenguaje utilizado

C#

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
