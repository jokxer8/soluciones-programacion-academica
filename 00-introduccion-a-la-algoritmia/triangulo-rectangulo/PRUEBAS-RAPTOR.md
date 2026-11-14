# Triángulo: ejecución en RAPTOR y comparación de versiones

[Actividad](README.md) · [Desarrollo](desarrollo.md) · [Validación de la materia](../VALIDACION.md)

Revisión del **16 de octubre de 2026**, en Windows con RAPTOR Avalonia sobre .NET 8. Ejecuté el diagrama de la entrega y una versión corregida. Conservo los archivos históricos para que se pueda entender el error y comparar el trabajo con la revisión actual.

## El error de la entrega

Con catetos **3 y 4**, el diagrama original devuelve **hipotenusa 6, área 6 y perímetro 13**. La [captura de la consola](evidencias/raptor-original-3-4.png) registra esa ejecución y su finalización.

El ciclo incrementa `raiz` de uno en uno y sale cuando `raiz*raiz > cateto1*cateto1 + cateto2*cateto2`. Para 3 y 4, no sale en 5: sale en 6. El área se calcula correctamente, pero el error de la hipotenusa también altera el perímetro.

| Versión | Método | Resultado para 3 y 4 |
|---|---|---|
| Diagrama original, ejecutado en RAPTOR | Incrementos enteros; termina al superar el cuadrado buscado. | 6; 6; 13. |
| Pseudocódigo original, transcrito a Python para comprobarlo | Newton, con tolerancia de 0.00001 sobre el residuo del cuadrado. | Aproximadamente 5.000000000054; 6; 12.000000000054. |
| C++ histórico | Incrementos de 0.01; conserva la aproximación inferior. | Su resultado depende de la aproximación acumulada; no se confunde con la versión compilable actual. |
| `solution.cpp`, compilado y ejecutado | `std::hypot`, entradas positivas y finitas, comprobación de resultados. | 5.000000; 6.000000; 12.000000. |
| Diagrama corregido | Raíz con escala y comprobación del rango. | 5; 6; 12. |

La transcripción de Newton no es una ejecución de RAPTOR. Sus resultados para 3–4, 5–12 y 0.3–0.4, junto con las salidas reales del C++, están en el [registro de comparación](evidencias/comparacion-cpp-newton.json). La tolerancia del pseudocódigo se aplica a `raiz²−(a²+b²)`, no directamente al error de la hipotenusa. Tampoco contiene la validación de entradas del C++ actual.

## Qué corregí

En [diagrama-corregido.rap](diagrama-corregido.rap) sustituyo el incremento por `mayor * sqrt(1 + (menor/mayor)^2)`. Es la misma relación de Pitágoras, expresada para evitar calcular directamente cuadrados enormes o demasiado pequeños. El C++ ya utiliza `std::hypot`, por lo que no necesitó cambios.

Rechazo catetos numéricos no positivos y resultados fuera del rango representable. El área se calcula como `(a/2)*b` y el perímetro como `a+b+hipotenusa`, igual que en C++. El [pseudocódigo corregido](pseudocodigo-corregido.md) describe estas decisiones. Esta revisión es una incorporación al portafolio, no una sustitución de la entrega histórica.

## Casos ejecutados

La [batería RAPTOR](evidencias/pruebas-raptor.rap) asigna los datos de cada caso y utiliza el mismo cálculo y decisiones del diagrama interactivo. Se ejecutó dentro de RAPTOR; no es una simulación del diagrama en otro lenguaje. La [captura completa](evidencias/raptor-corregido-7-casos.png) muestra las siete salidas y `Run Complete`.

| Caso | Catetos | RAPTOR corregido: hipotenusa; área; perímetro | Comparación con C++ |
|---|---|---|---|
| T1 | 3; 4 | 5; 6; 12 | Coincide. |
| T2 | 5; 12 | 13; 30; 30 | Coincide. |
| T3 | 0.3; 0.4 | 0.5; 0.06; 1.2 | Coincide. |
| T4 | 0; 4 | Error | Ambos rechazan. |
| T5 | −3; 4 | Error | Ambos rechazan. |
| T6 | 1×10³⁰⁸; 1×10³⁰⁸ | Error | Ambos rechazan los resultados fuera de rango. |
| T7 | 1×10⁻³⁰⁰; 1×10⁻³⁰⁰ | Aproximadamente 1.414213562373095×10⁻³⁰⁰; 0; 3.414213562373095×10⁻³⁰⁰ | Ambos aceptan. C++ imprime ceros por su formato fijo de seis decimales; RAPTOR muestra notación científica. |

**Resultado: siete casos RAPTOR con el resultado esperado.** La batería general de los cinco programas C++ también se repitió: **32 correctos y 0 fallidos**. Los siete casos RAPTOR no son los mismos siete casos de la batería C++: aquí se añade el caso diminuto y el texto no numérico se mantiene como prueba propia de C++.

Además, abrí el diagrama corregido e introduje manualmente 3 y 4 en sus dos cuadros de entrada. La [captura de esa ejecución](evidencias/raptor-corregido-3-4.png) muestra hipotenusa 5, área 6 y perímetro 12.

En T7 el área cae por debajo del mínimo representable y queda en cero. No es un área geométricamente nula. `E-300` en la consola significa «multiplicado por 10⁻³⁰⁰». No comparo como cadenas salidas con formatos diferentes.

## Cómo abrir y repetir

1. Abrir `diagrama-corregido.rap` en RAPTOR Avalonia y pulsar **Run**. Introducir primero un cateto y después el otro.
2. Para repetir los siete casos sin escribir cada entrada, abrir `evidencias/pruebas-raptor.rap`, pulsar **Run** y comparar las líneas T1–T7 con la tabla.
3. Para revisar el original en la edición actual, abrir [diagrama-original.oldrap](diagrama-original.oldrap). Es una copia con los mismos bytes del [archivo conservado](../../archivos-originales/introduccion-programacion/triangulo/diagrama.rap), únicamente con la extensión de importación adecuada.

La [página oficial de RAPTOR](https://raptor.martincarlisle.com/) indica que la edición Avalonia importa los archivos binarios antiguos con extensión `.oldrap`; los `.rap` nuevos usan XML. El original no estaba dañado: la extensión de su formato antiguo impedía abrirlo directamente en la edición actual.

## Límites

Los casos de RAPTOR cubren entradas numéricas. No se ha validado aquí su comportamiento interactivo ante texto, cancelación ni todas las expresiones que permite su cuadro de entrada. La prueba C++ de texto inválido no se atribuye al diagrama. Estos resultados tampoco prueban todas las combinaciones posibles de números en coma flotante.

[Volver a la actividad](README.md) · [Pendientes](../../PENDIENTES.md)
