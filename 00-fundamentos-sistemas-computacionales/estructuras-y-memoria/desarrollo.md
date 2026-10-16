# Desarrollo — Estructuras y memoria dinámica

## Diseño

Las estructuras agrupan datos relacionados. Los primeros ejercicios usan almacenamiento automático; los de rectángulos y complejos reservan memoria y la liberan después del cálculo. El último conecta nodos y recorre los valores en ambos sentidos mediante funciones separadas.

Caso reproducible: en el ejercicio 2, `01:50:30 + 02:20:45 = 04:11:15`. En el 4, las esquinas `(0,10)` y `(5,0)` delimitan un área de 50. En el 8, `(1 + j2) + (3 + j4) = 4 + j6`.

La compilación común usa C17. Las pruebas incluyen casos de operación; no constituyen una auditoría exhaustiva de entradas inválidas ni de agotamiento de memoria.

---

## Progresión de los ejercicios

| Bloque | Foco del razonamiento |
|---|---|
| 01–03 | Agrupar atributos y recorrer registros para obtener promedios o extremos. |
| 04–06 | Separar la representación de un rectángulo de las operaciones geométricas y su forma de reserva. |
| 07–09 | Modelar un número complejo, operar sobre sus componentes y construir su representación textual. |
| 10 | Relacionar nodos y comparar el recorrido directo con el inverso. |

## Memoria y comprobaciones

Una estructura no reserva automáticamente el contenido al que apuntan sus miembros: hay que distinguir el registro del almacenamiento de sus cadenas. Para la memoria dinámica se comprueba la reserva antes de usar el resultado y se identifica dónde termina su vida útil.

Los ejemplos de tiempos y geometría verifican el cálculo, mientras que los de complejos y listas ayudan a revisar el paso por dirección y los recorridos. La revisión registrada no certifica todos los escenarios de falta de memoria o entradas inválidas.

---

[Actividad](README.md) · [Resultados comunes](../VALIDACION.md)
