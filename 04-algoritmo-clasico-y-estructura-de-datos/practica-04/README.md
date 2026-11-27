
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/04-algoritmo-clasico-y-estructura-de-datos-practica-04.svg" alt="Práctica 04 — Resolución de recurrencias (Practice 04 — Solving Recurrence Relations)" width="100%"></h1>


</div>

[Materia 04](../README.md) · [Índice de prácticas](../MATERIALES.md) · [Práctica 03 ←](../practica-03/README.md) · [Práctica 05 →](../practica-05/README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Una recurrencia describe el trabajo del problema grande a partir del trabajo de casos pequeños. Puedes dibujar las llamadas como un árbol: cada nivel muestra problemas pendientes y cuánto trabajo se hace antes de pasar al siguiente.

### Árbol de recursión

- **¿Por qué?** El costo total puede repartirse en varios niveles.
- **¿Para qué?** Para ver qué nivel o conjunto de niveles aporta más trabajo.
- **¿Cómo?** Dibuja cuántas llamadas hay en cada nivel y suma el costo por nivel.
- **¿Cuándo usarlo?** Cuando quieras entender visualmente una recurrencia.

### Sustitución

- **¿Por qué?** Una intuición a partir del árbol no reemplaza una demostración.
- **¿Para qué?** Para comprobar una cota candidata.
- **¿Cómo?** Propón una cota, sustitúyela en los subproblemas y verifica que también se cumple para el problema mayor y los casos base.
- **¿Cuándo usarlo?** Cuando ya tienes una cota probable y quieres justificarla.

### Teorema maestro

- **¿Por qué?** Varias recurrencias de división siguen un patrón común.
- **¿Para qué?** Para clasificar el crecimiento sin dibujar todos los niveles.
- **¿Cómo?** Identifica cuántos subproblemas hay, cuánto reduce cada uno y cuánto cuesta el trabajo externo; compara con el caso aplicable.
- **¿Cuándo usarlo?** Cuando la recurrencia cumple la forma y las condiciones del teorema.

## Propósito

Resolver recurrencias por más de un método y contrastar el resultado con el caso aplicable del teorema maestro.

## Mandato

La actividad incluye problemas de recurrencias mediante árbol de recursión y sustitución, además de ejercicios del teorema maestro.

## Qué se trabaja

Árbol de recursión, demostración por sustitución y aplicación del teorema maestro.

## Qué aporta esta actividad

Resolver recurrencias por más de un método y contrastar el resultado con el caso aplicable del teorema maestro. El aprendizaje esperado es razonar sobre el método y justificar el resultado, no solo registrar una respuesta. Esta ficha enlaza el material disponible y señala sus límites para que se entienda qué permite comprobar.

## Entrega

[Abrir entrega PDF](entrega.pdf)

## Estado de revisión

El PDF se presenta como entrega disponible, sin validación independiente de sus respuestas.

La carpeta documenta el tema y la evidencia localizada. No se dispone aquí de una comprobación independiente de cada paso ni de la calificación de la actividad.

[Volver al índice de prácticas](../MATERIALES.md) · [Volver a la materia](../README.md)
