# Cambio de Monedas con Cantidad Limitada

## Descripción del problema

Este ejercicio consiste en determinar el mínimo número de monedas necesarias para formar exactamente un valor objetivo X.

A diferencia del problema clásico de cambio de monedas, en este caso cada tipo de moneda tiene una cantidad limitada disponible.

## Objetivo

Aplicar programación dinámica para resolver un problema de optimización con restricciones de cantidad.

## Entrada

El programa solicita la cantidad de tipos de monedas, los valores de cada moneda, la cantidad disponible de cada una y el valor objetivo X.

Ejemplo:

```text
Cantidad de tipos de monedas: 3
Ingrese los valores de las monedas separados por espacio:
1 3 4
Ingrese las cantidades disponibles separadas por espacio:
2 1 1
Ingrese el valor objetivo X: 6
```

## Salida

El programa muestra el mínimo número de monedas necesarias para formar exactamente el valor objetivo.

Ejemplo:

```text
Mínimo número de monedas: 3
```

Si no es posible formar el valor objetivo, muestra:

```text
-1
```

## Conceptos aplicados

- Programación dinámica
- Arreglos
- Listas
- Optimización
- Cantidad limitada de recursos
- Problema de cambio de monedas
- Análisis de complejidad

## Análisis del algoritmo

El algoritmo convierte las monedas disponibles en una lista individual, respetando la cantidad limitada de cada tipo de moneda.

Luego aplica programación dinámica para calcular el mínimo número de monedas necesario para alcanzar cada suma desde 0 hasta X.

El recorrido de las sumas se realiza de derecha a izquierda para evitar reutilizar una misma moneda más de una vez.

## Complejidad

Sea M la cantidad total de monedas disponibles después de expandir las cantidades.

- Tiempo: O(M × X)
- Espacio: O(X)

## Lenguaje utilizado

C#
