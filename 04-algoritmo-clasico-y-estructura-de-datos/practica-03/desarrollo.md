# Práctica 03 — Desarrollo complementario

[← Ficha de la práctica](README.md) · [Índice de prácticas](../MATERIALES.md) · [Práctica 04 →](../practica-04/README.md)

> **Alcance:** esta resolución didáctica fue elaborada para explicar el tema a partir del mandato resumido en el inventario del repositorio. No es una entrega original del estudiante ni una transcripción del enunciado de la maestra. El enunciado y la entrega originales no se localizaron.

## Objetivos

- Definir el tamaño de entrada (n).
- Elegir la operación básica que permite contar el trabajo.
- Escribir una recurrencia con su caso base.
- Resolver la recurrencia y expresar el orden asintótico.
- Contar también el espacio usado por las llamadas recursivas.
- Separar los casos cuando la ejecución puede terminar en distintos momentos.

## Método de análisis

Para cada algoritmo se toma la misma ruta:

1. Definir qué representa (n).
2. Identificar cuántas llamadas recursivas se hacen y qué tamaño tienen.
3. Contar el trabajo adicional realizado fuera de esas llamadas.
4. Plantear la recurrencia y el caso base.
5. Resolver el tiempo y estimar la profundidad de la pila.

La recurrencia modela el coste temporal. La profundidad máxima de las llamadas estima el espacio auxiliar de la pila; no incluye el espacio de los datos de entrada.

## Ejemplo 1: suma recursiva de un arreglo

Supóngase un arreglo de (n) números. La función suma el último elemento con la suma recursiva de los primeros (n-1). El caso base para un arreglo vacío devuelve cero.

[
S(0)=c_0,qquad S(n)=S(n-1)+cquad(n>0)
]

En cada nivel se procesa un elemento y se hace una suma, así que el trabajo adicional es constante. Al expandir:

[
S(n)=S(n-2)+2c=S(n-3)+3c=dots=S(0)+nc
]

Por tanto, el tiempo es (Theta(n)). La profundidad llega a (n) llamadas, así que la pila usa (Theta(n)) espacio. Una operación básica razonable es la suma; si se cuenta cada visita, también se puede usar el número de elementos procesados, siempre que se mantenga el mismo criterio durante el análisis.

**Conclusión:** la reducción de uno en uno produce (n) niveles de recursión.

## Ejemplo 2: búsqueda binaria recursiva

La búsqueda binaria requiere que el arreglo esté ordenado. En cada llamada compara el objetivo con el elemento central y continúa solo en una mitad. Si el intervalo queda vacío, el elemento no está.

En el peor caso:

[
B(1)=c_0,qquad B(n)=B(lfloor n/2floor)+cquad(n>1)
]

La operación básica es la comparación del objetivo con el elemento central. Después de (k) reducciones, el intervalo tiene aproximadamente (n/2^k) elementos. Se llega a un intervalo de tamaño uno cuando (n/2^kle 1), de modo que (k) crece como (log_2 n).

El tiempo del peor caso es (Theta(log n)), y la pila también ocupa (Theta(log n)) espacio. En el mejor caso, el objetivo coincide con el elemento central en la primera comparación: el tiempo es (Theta(1)).

**Conclusión:** reducir el problema por un factor constante produce una cantidad logarítmica de niveles.

## Ejemplo 3: Fibonacci recursivo ingenuo

Considérese la versión directa que calcula (F(n)) llamando a (F(n-1)) y (F(n-2)), con (F(0)=0) y (F(1)=1).

[
T(0)=c_0,quad T(1)=c_1,qquad
T(n)=T(n-1)+T(n-2)+cquad(n>1)
]

Aquí cada llamada genera dos subproblemas. La misma subsolución se vuelve a calcular muchas veces. La cantidad de llamadas crece exponencialmente: el tiempo es (Theta(arphi^n)), donde (arphi=(1+sqrt5)/2). Aunque el árbol de llamadas tiene crecimiento exponencial, la profundidad máxima de la pila es (n), por lo que el espacio auxiliar es (Theta(n)).

**Conclusión:** el número de llamadas por nivel también importa; no basta con observar que cada llamada reduce (n). La memoización o la programación dinámica evitan repetir subproblemas, tema que puede estudiarse por separado.

## Comparación

| Algoritmo | Tamaños recursivos | Tiempo | Espacio de pila |
|---|---|---:|---:|
| Suma recursiva | (n-1) | (Theta(n)) | (Theta(n)) |
| Búsqueda binaria, peor caso | (lfloor n/2floor) | (Theta(log n)) | (Theta(log n)) |
| Fibonacci ingenuo | (n-1) y (n-2) | (Theta(arphi^n)) | (Theta(n)) |

Estas cotas describen los algoritmos bajo los modelos y supuestos indicados. Los costes constantes dependen del modelo de operaciones; las cotas asintóticas describen cómo crece el trabajo al aumentar (n).

## Cierre

El análisis recursivo convierte la estructura del algoritmo en una ecuación de coste. Para justificar una complejidad se deben declarar el tamaño de entrada, el caso base, las llamadas recursivas, el trabajo adicional y la profundidad de la pila. Los tres ejemplos muestran por qué una recurrencia lineal, una reducción a la mitad y dos llamadas por nivel producen comportamientos distintos.

## Evidencia y límites

- El mandato disponible es una síntesis del mapa e inventario académico; no se dispone de la consigna original literal.
- Este desarrollo es una explicación complementaria creada para hacer legible el tema; no debe confundirse con la entrega que el estudiante no presentó.
- No se afirma una calificación, recepción o validación por parte de la docente.
