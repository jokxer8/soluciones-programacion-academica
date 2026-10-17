# Triángulo rectángulo: hipotenusa, área y perímetro

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

![Área](https://img.shields.io/badge/Área-Algoritmia-green)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%2B%2B-blue)

## Qué trabajé

Trabajo realizado en el grupo 5. A partir de dos catetos calculo la hipotenusa, el área y el perímetro. Conservo el algoritmo, el diagrama de RAPTOR, la investigación y las versiones de la entrega para poder comparar sus enfoques.

---

## Objetivo

Traducir las relaciones geométricas a un algoritmo y un programa C++.

## Análisis y conceptos

Si a y b son catetos positivos, c = √(a²+b²), área = a·b/2 y perímetro = a+b+c. Los catetos, la hipotenusa y el perímetro usan la misma unidad de longitud; el área usa esa unidad al cuadrado. La versión compilable usa std::hypot y valida los datos.

## Entradas, proceso y salidas

| Elemento | Descripción |
|---|---|
| Entrada | Dos catetos a y b, números positivos y finitos. |
| Proceso | Calcular la hipotenusa, el área y el perímetro; comprobar que los resultados sean finitos. |
| Salida | Tres resultados con seis cifras después del punto decimal. |
| Unidad | Si los catetos están en cm, la hipotenusa y el perímetro están en cm y el área en cm². |

## Ejemplo y lectura de resultados

Con a=3 y b=4, la hipotenusa es 5, el área 6 y el perímetro 12. La entrega C++ incrementa una raíz de 0.01 en 0.01 y conserva el último valor inferior; su resultado puede quedar por debajo del correcto. El pseudocódigo describe una aproximación de Newton y el diagrama utiliza incrementos distintos. Son versiones diferentes: no deben presentarse como si implementaran exactamente el mismo cálculo.

---

## Ejemplo de consola

En este ejemplo incluyo la entrada junto al mensaje que la solicita:

```text
Ingrese los dos catetos: 3 4

Hipotenusa: 5.000000
Area: 6.000000
Perimetro: 12.000000
```

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Enunciado](../../archivos-originales/introduccion-programacion/triangulo/enunciado.txt) | Texto conservado |
| [Algoritmo](../../archivos-originales/introduccion-programacion/triangulo/algoritmo.txt) | Texto conservado |
| [Código original](../../archivos-originales/introduccion-programacion/triangulo/codigo-original.txt) | Texto conservado |
| [Algoritmo y DFD de entrega](../../archivos-originales/introduccion-programacion/triangulo/algoritmo-dfd-entrega.pdf) | Entrega y material de la actividad |
| [Diagrama editable RAPTOR](../../archivos-originales/introduccion-programacion/triangulo/diagrama.rap) | Entrega y material de la actividad |
| [Investigación grupal](../../archivos-originales/introduccion-programacion/triangulo/investigacion.pdf) | Entrega y material de la actividad |
| [Algoritmo previo](../../archivos-originales/introduccion-programacion/triangulo/algoritmo-borrador.pdf) | Entrega y material de la actividad |
| [Código previo](../../archivos-originales/introduccion-programacion/triangulo/codigo-borrador.pdf) | Entrega y material de la actividad |
| [DFD previo](../../archivos-originales/introduccion-programacion/triangulo/dfd-borrador.pdf) | Entrega y material de la actividad |

La versión [solution.cpp](solution.cpp) se prepara para esta edición del portafolio. Los textos y documentos enlazados conservan la entrega previa; las diferencias se explican en el [desarrollo](desarrollo.md).

La revisión añade el [diagrama corregido](diagrama-corregido.rap), su [pseudocódigo](pseudocodigo-corregido.md) y el [informe de ejecución en RAPTOR](PRUEBAS-RAPTOR.md). Para importar la entrega antigua en RAPTOR Avalonia incluyo una [copia `.oldrap`](diagrama-original.oldrap), con los mismos bytes del archivo conservado.

## Archivos de esta carpeta

```text
triangulo-rectangulo/
├── README.md
├── desarrollo.md
├── solution.cpp
├── diagrama-corregido.rap
├── diagrama-original.oldrap
├── pseudocodigo-corregido.md
├── PRUEBAS-RAPTOR.md
└── evidencias/
```

Los documentos de apoyo están enlazados en la tabla anterior; no se duplican dentro de la actividad. El [desarrollo](desarrollo.md) amplía el recorrido de los datos, las decisiones y los límites.

## Cómo revisar la actividad

Compilo el archivo independiente con un compilador C++17 y comparo sus resultados con los casos de [validación](../VALIDACION.md). Cada solución tiene su propio main; no se compilan juntas en un solo programa.

## Pruebas y estado

7 casos: tres pares válidos y cuatro entradas rechazadas. La versión compilable está verificada en Windows local y en [GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38982024711). Los datos y salidas de los casos se pueden consultar en el [registro de resultados](../tests/results-windows.json).

También ejecuté el diagrama original y la corrección en RAPTOR. El original devuelve 6 como hipotenusa para 3 y 4; la versión corregida devuelve 5, área 6 y perímetro 12. La batería numérica RAPTOR completó **siete casos con el resultado esperado**, con entradas y límites distintos de los siete casos C++. El [informe](PRUEBAS-RAPTOR.md) incluye las capturas, la comparación con Newton y C++, y los pasos para repetirla.

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
