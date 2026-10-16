# Desarrollo — Fundamentos de algoritmia y lectura inicial

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md)

## Punto de partida

Reúno mi reporte de lectura y los conceptos que utilicé al comenzar: lenguajes, representación de soluciones, compilación y pruebas. El documento conserva la redacción de la entrega; esta guía aclara los términos para leerlo con contexto.

## Razonamiento

Un algoritmo describe pasos finitos y ordenados. El programa expresa esos pasos en un lenguaje. En C++, compilar traduce el código a un ejecutable; probar consiste en ejecutar casos y contrastar su resultado. La STL reúne componentes de biblioteca como contenedores, iteradores y algoritmos; no es otro lenguaje.

## Comprobación manual

Para calcular un índice ponderado, primero multiplico cada cantidad de créditos por los puntos de su letra. Con 5 créditos y A (4 puntos), 4 y C (2), 4 y B (3), y 3 y F (0), obtengo 40 puntos entre 16 créditos: 2.5. Es un ejemplo de cálculo, no mi índice académico ni una afirmación sobre la normativa vigente.

## Alcance de la revisión

Se revisa la explicación y su correspondencia con los documentos. No se registra una ejecución de software para esta actividad.

[Documentos de la actividad](README.md#documentos-y-versiones) · [Volver a la materia](../README.md)

---

## De un problema a una solución

| Etapa | Pregunta que me hago | Resultado de la etapa |
|---|---|---|
| Análisis | ¿Qué recibo y qué necesito obtener? | Entradas, salidas y condiciones. |
| Diseño | ¿Qué pasos conectan esas entradas con la salida? | Algoritmo o diagrama. |
| Codificación | ¿Cómo expreso esos pasos en C++? | Código fuente. |
| Compilación | ¿El código cumple las reglas del lenguaje? | Ejecutable o errores de compilación. |
| Prueba | ¿La salida coincide con lo esperado? | Casos y resultados comparados. |

Compilar correctamente no demuestra por sí solo que una fórmula o una condición sea correcta. En las actividades del triángulo y la pirámide comparo precisamente el algoritmo, el dibujo y el programa.

## Conceptos que distingo al leer el reporte

Un lenguaje de programación permite expresar instrucciones. Un compilador traduce un programa; una biblioteca ofrece componentes reutilizables. Un algoritmo describe una solución y puede representarse antes de elegir un lenguaje.

En C++, STL se refiere a componentes de biblioteca como contenedores, iteradores y algoritmos. Los contenedores administran sus propios elementos; esto no elimina la necesidad de entender la vida útil de los objetos ni convierte cualquier recurso externo en memoria gestionada automáticamente.

El reporte incluye un recorrido histórico y explicaciones iniciales. Lo conservo como entrega de lectura; no lo utilizo como una referencia técnica definitiva para cada afirmación histórica o clasificación.

## Ejemplo de entrada, proceso y salida

Para practicar el análisis, uso un índice ponderado con datos ilustrativos:

| Créditos | Letra | Puntos por crédito | Producto |
|---|---|---|---|
| 5 | A | 4 | 20 |
| 4 | C | 2 | 8 |
| 4 | B | 3 | 12 |
| 3 | F | 0 | 0 |
| **16** | | | **40** |

La salida es 40/16=2.5. El denominador es la suma de créditos, no la cantidad de asignaturas. Este ejemplo explica una media ponderada; no presenta mis calificaciones ni pretende actualizar el reglamento académico de la institución.

## Relación con los demás trabajos

En [asignaciones secuenciales](../ejercicios-secuenciales/README.md) sigo cómo cambia una variable. En [triángulos](../triangulo-rectangulo/README.md) traduzco fórmulas; en [pirámides](../piramide-asteriscos/README.md), ciclos; y en [texto](../busqueda-coincidencias-texto/README.md), una búsqueda con una condición de terminación.

[Volver a la actividad](README.md) · [Validación de la materia](../VALIDACION.md)
