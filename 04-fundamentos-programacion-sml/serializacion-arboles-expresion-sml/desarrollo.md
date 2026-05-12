# Desarrollo - Serialización de Árboles de Expresión

## 1. Tipo de dato recursivo

La actividad parte del siguiente tipo de dato recursivo:

```sml
datatype tree = Leaf | Br of tree * string * tree;
```

Este tipo permite representar árboles binarios.

Cada nodo `Br` contiene tres elementos:

```text
subárbol izquierdo, dato, subárbol derecho
```

---

## 2. Expresión aritmética representada

La expresión trabajada es:

```text
(x * 45) + z / (y + 5050)
```

Para representarla como árbol, los operadores se colocan como nodos internos y los operandos como hojas.

La raíz principal del árbol es el operador `+`, porque la expresión completa suma dos partes:

```text
(x * 45)
```

y

```text
z / (y + 5050)
```

---

## 3. Representación del árbol

La expresión puede modelarse en SML de la siguiente forma:

```sml
val exprTree =
    Br(
        Br(
            Br(Leaf, "x", Leaf),
            "*",
            Br(Leaf, "45", Leaf)
        ),
        "+",
        Br(
            Br(Leaf, "z", Leaf),
            "/",
            Br(
                Br(Leaf, "y", Leaf),
                "+",
                Br(Leaf, "5050", Leaf)
            )
        )
    );
```

---

## 4. Serialización en preorden

El recorrido en preorden visita primero la raíz, luego el subárbol izquierdo y finalmente el subárbol derecho.

```text
raíz - izquierda - derecha
```

Para el árbol trabajado, el resultado esperado es:

```text
+ * x 45 / z + y 5050
```

---

## 5. Serialización en enorden

El recorrido en enorden visita primero el subárbol izquierdo, luego la raíz y finalmente el subárbol derecho.

```text
izquierda - raíz - derecha
```

Para conservar correctamente la estructura de la expresión, se utilizan paréntesis.

Resultado esperado:

```text
((x * 45) + (z / (y + 5050)))
```

---

## 6. Serialización en postorden

El recorrido en postorden visita primero el subárbol izquierdo, luego el subárbol derecho y finalmente la raíz.

```text
izquierda - derecha - raíz
```

Para el árbol trabajado, el resultado esperado es:

```text
x 45 * z y 5050 + / +
```

---

## 7. Importancia de la actividad

Esta actividad permite comprender cómo una estructura matemática puede representarse mediante un tipo de dato recursivo.

También muestra cómo la recursividad y el pattern matching permiten recorrer árboles de diferentes maneras sin necesidad de estructuras imperativas.

---

## Conclusión

La serialización de árboles de expresión permite transformar una estructura jerárquica en una representación textual.

Los recorridos preorden, enorden y postorden son fundamentales para entender cómo se procesan árboles en programación funcional, compiladores, análisis sintáctico y estructuras de datos.
