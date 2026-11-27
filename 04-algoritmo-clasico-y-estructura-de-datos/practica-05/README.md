
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/04-algoritmo-clasico-y-estructura-de-datos-practica-05.svg" alt="Práctica 05 — Árboles de búsqueda y balanceados (Practice 05 — Search and Balanced Trees)" width="100%"></h1>


</div>

[Materia 04](../README.md) · [Índice de prácticas](../MATERIALES.md) · [Práctica 04 ←](../practica-04/README.md) · [Práctica 06 →](../practica-06/README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

En un árbol de búsqueda, cada número te indica si debes mirar a la izquierda o a la derecha. Si el árbol queda inclinado hacia un lado, buscar se parece a recorrer una lista. Equilibrarlo ayuda a conservar caminos cortos.

### Árbol binario de búsqueda y recorridos

- **¿Por qué?** El almacén de claves tiene una regla de orden que evita revisar todo.
- **¿Para qué?** Para buscar claves y leer los nodos en un orden determinado.
- **¿Cómo?** Compara la clave en cada nodo; para listar en orden creciente, visita izquierda, nodo y derecha.
- **¿Cuándo usarlo?** Cuando necesitas consultas por clave y recorridos ordenados.

### Balanceo AVL

- **¿Por qué?** Insertar valores ordenados puede inclinar un árbol y alargar búsquedas.
- **¿Para qué?** Para mantener acotada la diferencia de alturas entre subárboles.
- **¿Cómo?** Tras insertar, identifica el primer desequilibrio y aplica la rotación simple o doble que conserve el orden.
- **¿Cuándo usarlo?** Cuando necesitas mantener búsquedas rápidas con altura estrictamente controlada.

### Árbol rojo-negro

- **¿Por qué?** También se puede limitar la altura sin exigir el mismo balance que AVL.
- **¿Para qué?** Para mantener búsquedas e inserciones eficientes mediante reglas de color.
- **¿Cómo?** Después de un cambio, comprueba las reglas de color y corrige con recoloreos o rotaciones según el caso.
- **¿Cuándo usarlo?** Al estudiar estructuras ordenadas que necesitan soportar actualizaciones.

## Propósito

Relacionar el orden de claves con los recorridos del árbol y explicar cómo el balanceo mantiene operaciones eficientes.

## Mandato

La actividad aborda construcción de árboles binarios de búsqueda (BST) desde recorridos, inserciones, balanceo, rotaciones AVL y propiedades de árboles rojo-negro.

## Qué se trabaja

Construcción de BST, recorridos, inserciones y rebalanceo AVL/rojo-negro.

## Qué aporta esta actividad

Relacionar el orden de claves con los recorridos del árbol y explicar cómo el balanceo mantiene operaciones eficientes. El aprendizaje esperado es razonar sobre el método y justificar el resultado, no solo registrar una respuesta. Esta ficha enlaza el material disponible y señala sus límites para que se entienda qué permite comprobar.

## Entrega

[Abrir entrega PDF](entrega.pdf)

## Estado de revisión

El mapa registra que algunas preguntas usan figuras. La ficha describe el alcance, no reconstruye cada ejercicio ni valida las respuestas del PDF.

La carpeta documenta el tema y la evidencia localizada. No se dispone aquí de una comprobación independiente de cada paso ni de la calificación de la actividad.

[Volver al índice de prácticas](../MATERIALES.md) · [Volver a la materia](../README.md)
