
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/04-algoritmo-clasico-y-estructura-de-datos-practica-06.svg" alt="Práctica 06 — Caminos mínimos y fiabilidad (Practice 06 — Shortest Paths and Reliability)" width="100%"></h1>


</div>

[Materia 04](../README.md) · [Índice de prácticas](../MATERIALES.md) · [Práctica 05 ←](../practica-05/README.md) · [Práctica 07 →](../practica-07/README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Dibuja lugares y las conexiones entre ellos. El camino con menos tramos no siempre es el de menor costo. La práctica distingue cómo comparar rutas cuando la pregunta es distancia o cuando lo que importa es fiabilidad.

### Dijkstra

- **¿Por qué?** Tener menos tramos no garantiza menor costo.
- **¿Para qué?** Para hallar distancias mínimas desde un origen.
- **¿Cómo?** Empieza con el origen en cero y elige primero el nodo pendiente con menor distancia conocida; revisa sus vecinos.
- **¿Cuándo usarlo?** Cuando los pesos no son negativos.

### Bellman-Ford

- **¿Por qué?** Un tramo de peso negativo puede mejorar una ruta que parecía definitiva.
- **¿Para qué?** Para hallar distancias desde un origen y detectar ciclos negativos alcanzables.
- **¿Cómo?** Revisa repetidamente si cada arista mejora una distancia; una mejora adicional tras las rondas previstas señala un ciclo negativo alcanzable.
- **¿Cuándo usarlo?** Cuando el grafo admite pesos negativos.

### Floyd-Warshall

- **¿Por qué?** Una sola consulta desde un origen no responde qué pasa con todos los demás.
- **¿Para qué?** Para comparar rutas entre todos los pares de nodos.
- **¿Cómo?** Prueba cada nodo como punto intermedio y reemplaza una distancia si el desvío la mejora.
- **¿Cuándo usarlo?** Cuando necesitas una tabla de distancias entre todos los pares.

### Máxima fiabilidad

- **¿Por qué?** La distancia de una ruta no determina qué tan confiable es.
- **¿Para qué?** Para comparar caminos según la probabilidad de éxito.
- **¿Cómo?** Si el modelo supone tramos independientes, multiplica sus probabilidades y compara los productos.
- **¿Cuándo usarlo?** Cuando la pregunta pide fiabilidad, no el costo o la cantidad de tramos.

## Propósito

Elegir y aplicar un algoritmo de caminos según el problema y las características del grafo.

## Mandato

La actividad aborda caminos de coste mínimo con Dijkstra y Bellman-Ford, Floyd-Warshall y una ruta de máxima fiabilidad.

## Qué se trabaja

Comparación de algoritmos de caminos mínimos y tratamiento de fiabilidad de rutas.

## Qué aporta esta actividad

Elegir y aplicar un algoritmo de caminos según el problema y las características del grafo. El aprendizaje esperado es razonar sobre el método y justificar el resultado, no solo registrar una respuesta. Esta ficha enlaza el material disponible y señala sus límites para que se entienda qué permite comprobar.

## Entrega

[Abrir entrega PDF](entrega.pdf)

## Estado de revisión

Los grafos son parte esencial de algunos enunciados; no se reconstruyen aquí. La entrega está publicada, pero no se certifica su corrección.

La carpeta documenta el tema y la evidencia localizada. No se dispone aquí de una comprobación independiente de cada paso ni de la calificación de la actividad.

[Volver al índice de prácticas](../MATERIALES.md) · [Volver a la materia](../README.md)
