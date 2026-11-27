
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/04-algoritmo-clasico-y-estructura-de-datos-proyecto-final.svg" alt="Proyecto final (Final Project)" width="100%"></h1>


</div>

[Materia 04](../README.md) · [Índice de materiales](../MATERIALES.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

**Árboles:** reconstruye la forma del árbol a partir del orden en que se visitan sus nodos. **Hash:** reorganiza claves sin perder las búsquedas después de borrar. **Caminos mínimos:** compara rutas entre puntos. **Expansión mínima:** conecta todos los puntos con el menor costo total. El PDF se publica como evidencia, no como verificación independiente de cada resultado.

### Reconstrucción de árboles

- **¿Por qué?** Un recorrido registra el orden de visitas, no dibuja por sí solo la estructura.
- **¿Para qué?** Para reconstruir el árbol respetando las propiedades disponibles.
- **¿Cómo?** Ubica la raíz según el tipo de recorrido y separa las claves que pertenecen a cada subárbol según el enunciado.
- **¿Cuándo usarlo?** Cuando los recorridos o la condición de árbol de búsqueda dan datos suficientes.

### Rehashing y eliminación

- **¿Por qué?** Borrar sin respetar la estrategia puede cortar una búsqueda por sondeo.
- **¿Para qué?** Para mantener localizables las claves que siguen activas.
- **¿Cómo?** Verifica que la búsqueda atraviese marcas de borrado si el método las usa; al reconstruir, reinserta solo claves activas.
- **¿Cuándo usarlo?** Al borrar o cambiar el tamaño de una tabla con colisiones.

### Caminos mínimos

- **¿Por qué?** La ruta más barata depende de los pesos y del origen.
- **¿Para qué?** Para elegir el recorrido de menor costo entre puntos.
- **¿Cómo?** Usa Dijkstra con pesos no negativos o Bellman-Ford si hay pesos negativos y verifica qué rutas son válidas.
- **¿Cuándo usarlo?** Cuando interesa el costo de trasladarse entre puntos, no instalar toda la red.

### Expansión mínima

- **¿Por qué?** Construir todos los enlaces posibles puede ser innecesario.
- **¿Para qué?** Para conectar toda la red con costo total mínimo.
- **¿Cómo?** Usa Prim o Kruskal y confirma que no quedan nodos aislados ni ciclos.
- **¿Cuándo usarlo?** Cuando el objetivo es diseñar una red completa económica.

## Propósito

El trabajo reúne reconstrucción de BST desde recorridos, rehashing con eliminación, caminos mínimos con Dijkstra y Bellman-Ford, y árboles de expansión mínima con Prim y Kruskal.

## Qué se desarrolla

El trabajo reúne reconstrucción de BST desde recorridos, rehashing con eliminación, caminos mínimos con Dijkstra y Bellman-Ford, y árboles de expansión mínima con Prim y Kruskal. El trabajo reúne varias unidades de la asignatura en una misma entrega y permite aplicar los conceptos en problemas relacionados.

## Entrega

[Abrir entrega PDF](entrega.pdf)

## Alcance de revisión

El PDF está disponible. No se declara una revisión independiente de sus desarrollos ni una validación de los grafos. La ficha deja explícito el tema y el estado de la evidencia para que el documento pueda leerse con contexto.

[Volver al índice de materiales](../MATERIALES.md) · [Volver a la materia](../README.md)
