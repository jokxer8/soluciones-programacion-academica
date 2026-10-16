# Cómo leer los ejemplos y resultados

[Inicio](README.md) · [Presentación](GUIA-DE-ESTILO.md) · [Pendientes](PENDIENTES.md)

Reúno aquí la notación que aparece en distintas materias. Cada actividad explica sus datos y condiciones particulares.

## Números y representaciones

| Notación | Qué significa | Ejemplo |
|---|---|---|
| Decimal, base 10 | Usa los dígitos 0–9. | 25 = 2 × 10 + 5. |
| Binario, base 2 | Usa solo 0 y 1. | 11001 en base 2 representa 25 en decimal. |
| Octal, base 8 | Usa los dígitos 0–7. | 31 en base 8 representa 25 en decimal. |
| Hexadecimal, base 16 | Usa 0–9 y A–F; A vale 10 y F vale 15. El prefijo `0x` identifica esta base. | `0x19` representa 25 en decimal. |
| Bit y byte | Un bit tiene valor 0 o 1; un byte contiene 8 bits. | 4 bytes equivalen a 32 bits. |
| Patrón de bits | Su interpretación depende del tipo y del ancho. | Un patrón de 32 bits puede representar un entero o un número IEEE 754. |
| SHA-256 | Huella del contenido de un archivo, escrita con 64 caracteres hexadecimales. | Sirve para comparar versiones; no es una nota ni una contraseña. |

En los ejemplos, un porcentaje de 10 % se calcula multiplicando por 0.10. Los importes y medidas deben conservar la unidad del problema. Un identificador de registro no es una cantidad ni un dato que deba sumarse.

## Código, fórmulas y organización

- Los índices de arreglos de C y C# empiezan en 0. El índice 0 señala el primer elemento.
- En el análisis de algoritmos, `n` suele ser la cantidad de elementos. `O(n)` expresa cómo crece el trabajo; no indica segundos. Cada actividad define las variables adicionales.
- `01`, `02` y otros prefijos de carpetas o scripts sirven para ordenar el contenido. Los códigos ICC identifican asignaturas; los números de versión identifican herramientas o formatos.
- En SML, `'a` y `'b` son variables de tipo. `->` describe una función y `*` dentro de un tipo describe un producto o tupla.
- En modelos de datos, `1:N` significa uno a muchos y `N:M`, muchos a muchos. Son reglas de relación, no cantidades actuales de registros.

## Ejemplos, pruebas y estados

Un ejemplo permite seguir una solución con datos concretos. Una prueba ejecutada tiene un resultado registrado y debe consultarse con sus condiciones y herramientas. Un conjunto de pruebas correcto respalda los casos incluidos; no implica haber probado cualquier entrada posible.

El estado de organización de una materia indica si sus actividades están documentadas. Las mejoras de software se siguen por separado en [Pendientes](PENDIENTES.md) y, para el hotel, en su [tablero](https://github.com/users/jokxer8/projects/1). Una evidencia de un parcial corresponde a esa evaluación y no sustituye una certificación de nota final.
