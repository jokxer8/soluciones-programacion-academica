# Cómo leer los ejemplos y resultados

[Inicio](README.md) · [Presentación](GUIA-DE-ESTILO.md) · [Pendientes](PENDIENTES.md)

Reúno aquí la notación que aparece en distintas materias. Cada actividad explica sus datos y condiciones particulares.

## En palabras simples

![En palabras simples · Guía para estudiantes](assets/readme-banners/en-palabras-simples.svg)

Usa esta guía si no sabes por dónde entrar. El README es la puerta de cada actividad; **En palabras simples** te da la idea y los otros enlaces te llevan al código, a la solución o al material original.

## Números y representaciones

- **¿Por qué?** Un mismo valor puede verse distinto según la base, y un mismo patrón de bits puede tener distintas interpretaciones.
- **¿Para qué?** Leer entradas, salidas y huellas de archivos sin confundir su formato con su significado.
- **¿Cómo?** Identifica la base, el ancho o el tipo antes de interpretar el dato. Por ejemplo, `11001` en binario y `25` en decimal representan el mismo valor.
- **¿Cuándo usarlo?** Cuando veas conversiones, números en memoria o comprobaciones de integridad.

| Tema | ¿Por qué? | ¿Para qué? | ¿Cómo? | ¿Cuándo usarlo? |
|---|---|---|---|---|
| Decimal (base 10) | Es la notación cotidiana. | Leer cantidades y resultados habituales. | En 25, el 2 vale dos decenas y el 5 vale cinco unidades. | Al leer conteos, importes o medidas normales. |
| Binario (base 2) | Expresa valores con los mismos dos símbolos de un bit: 0 y 1. | Leer representaciones binarias y máscaras. | Suma el valor de las posiciones con 1: `11001` vale 16 + 8 + 1 = 25. | Al interpretar bits, máscaras o conversiones de base. |
| Octal (base 8) | Agrupa tres bits por dígito. | Leer formatos que emplean los dígitos del 0 al 7. | En `31` octal, calcula 3 × 8 + 1 = 25. | Al estudiar conversiones o encontrar permisos numéricos en sistemas Unix. |
| Hexadecimal (base 16) | Agrupa cuatro bits por dígito. | Leer bytes y direcciones con menos caracteres que en binario. | En `0x19`, calcula 1 × 16 + 9 = 25. A–F representan 10–15. | Al ver bytes, direcciones de memoria o valores con prefijo `0x`. |
| Bit y byte | Son unidades distintas: un bit vale 0 o 1; un byte reúne 8 bits. | Comparar tamaños sin mezclar unidades. | Multiplica los bytes por 8: 4 bytes son 32 bits. | Al interpretar tamaños, anchos de datos o una tasa expresada en bits. |
| Patrón de bits | Los bits no indican por sí solos qué tipo representan. | Distinguir cómo se interpreta el mismo contenido. | Verifica el tipo y el ancho: 32 bits pueden representar un entero o un número IEEE 754. | Antes de leer bits de un archivo o de memoria como un valor concreto. |
| SHA-256 | Los nombres de dos archivos pueden coincidir aunque sus contenidos sean distintos. | Comprobar una copia contra una huella de referencia fiable. | Calcula la huella y compara los 64 caracteres hexadecimales con la referencia. | Al comprobar integridad. La huella no es una nota, contraseña ni por sí sola prueba de autoría. |

En los ejemplos, un porcentaje de 10 % se calcula multiplicando por 0.10. Los importes y medidas deben conservar la unidad del problema. Un identificador de registro no es una cantidad ni un dato que deba sumarse.

## Código, fórmulas y organización

- **¿Por qué?** Los símbolos y números de un ejemplo no siempre representan cantidades.
- **¿Para qué?** Distinguir una posición, una medida de complejidad, un tipo o un número de orden.
- **¿Cómo?** Lee la definición del ejercicio antes de operar: si `n` es la cantidad de elementos, `O(n)` describe cómo crece el trabajo al aumentar `n`.
- **¿Cuándo usarlo?** Cuando leas código, fórmulas, estructuras de datos o modelos.

- Los índices de arreglos de C y C# empiezan en 0. El índice 0 señala el primer elemento.
- En el análisis de algoritmos, `n` suele ser la cantidad de elementos. `O(n)` expresa cómo crece el trabajo; no indica segundos. Cada actividad define las variables adicionales.
- `01`, `02` y otros prefijos de carpetas o scripts sirven para ordenar el contenido. Los códigos ICC identifican asignaturas; los números de versión identifican herramientas o formatos.
- En SML, `'a` y `'b` son variables de tipo. `->` describe una función y `*` dentro de un tipo describe un producto o tupla.
- En modelos de datos, `1:N` significa uno a muchos y `N:M`, muchos a muchos. Son reglas de relación, no cantidades actuales de registros.

## Laboratorios de redes

- **¿Por qué?** Una ficha explica el ejercicio y el simulador muestra la topología concreta; cumplen funciones distintas.
- **¿Para qué?** Relacionar lo leído con los equipos, enlaces y resultados de la práctica.
- **¿Cómo?** Abre primero la ficha; luego identifica en el simulador los equipos y las conexiones que la ficha describe. Si buscas un resultado ya observado, revisa su evidencia.
- **¿Cuándo usarlo?** Cuando una actividad de Redes tenga instrucciones o un archivo de Packet Tracer.

En [07 · Fundamentos de Redes](07-fundamentos-de-redes/README.md), empieza por la ficha en palabras simples y abre después la entrega o el [simulador](07-fundamentos-de-redes/simuladores/README.md). Los registros de clase muestran qué se probó; que un archivo esté disponible para descargar no significa que se haya vuelto a ejecutar para publicarlo.

## Ejemplos, pruebas y estados

- **¿Por qué?** Ver un ejemplo, ejecutar una prueba y documentar una materia son trabajos distintos.
- **¿Para qué?** Saber qué está ilustrado, qué se verificó y dónde termina el alcance de la evidencia.
- **¿Cómo?** Busca entradas, condiciones y resultado registrado en las pruebas. Si solo hay un ejemplo, úsalo para seguir la idea sin tomarlo como prueba ejecutada.
- **¿Cuándo usarlo?** Al leer un resultado, comparar entregas o valorar el estado de una materia.

Un ejemplo permite seguir una solución con datos concretos. Una prueba ejecutada tiene un resultado registrado y debe consultarse con sus condiciones y herramientas. Un conjunto de pruebas correcto respalda los casos incluidos; no implica haber probado cualquier entrada posible.

El estado de organización de una materia indica si sus actividades están documentadas. Las mejoras de software se siguen por separado en [Pendientes](PENDIENTES.md) y, para el hotel, en su [tablero](https://github.com/users/jokxer8/projects/1). Una evidencia de un parcial corresponde a esa evaluación y no sustituye una certificación de nota final.
