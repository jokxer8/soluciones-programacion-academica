# Desarrollo - Listas Perezosas en SML

## 1. Definición de lista perezosa

La actividad utiliza el siguiente tipo de dato:

```sml
datatype 'a seq = Empty
                | Cons of 'a * (unit -> 'a seq);
```

Este tipo permite representar una secuencia que puede ser finita o infinita.

La clave está en que la cola de la secuencia no se calcula de inmediato, sino que se guarda como una función:

```sml
unit -> 'a seq
```

Esto permite simular evaluación perezosa en SML.

---

## 2. Generación de números naturales

La función `seqFrom` genera una secuencia infinita de números naturales a partir de un valor inicial.

```sml
fun seqFrom i = Cons(i, fn () => seqFrom(i + 1));
```

Por ejemplo:

```sml
seqFrom 1
```

representa la secuencia:

```text
1, 2, 3, 4, 5, ...
```

Esta secuencia no se construye completa en memoria. Solo se genera el siguiente elemento cuando una función como `takeSeq` lo solicita.

---

## 3. Función takeSeq

La función `takeSeq` extrae los primeros `n` elementos de una secuencia perezosa y los convierte en una lista normal de SML.

```sml
fun takeSeq (0, _) = []
  | takeSeq (_, Empty) = []
  | takeSeq (i, Cons(n, xt)) = n :: takeSeq(i - 1, xt());
```

Esta función es útil para observar una parte finita de una secuencia infinita.

---

## 4. Función dropSeq

La función `dropSeq` descarta los primeros `n` elementos de una secuencia y devuelve el resto.

```sml
fun dropSeq (0, xs) = xs
  | dropSeq (_, Empty) = Empty
  | dropSeq (i, Cons(_, xt)) = dropSeq(i - 1, xt());
```

Esto permite avanzar dentro de una secuencia sin convertirla completa en lista.

---

## 5. Función mapSeq

La función `mapSeq` aplica una función a cada elemento de una secuencia perezosa.

```sml
fun mapSeq f Empty = Empty
  | mapSeq f (Cons(v, xt)) = Cons(f v, fn () => mapSeq f (xt()));
```

La transformación conserva la evaluación diferida, porque el resto de la secuencia transformada también queda guardado como función.

---

## 6. Función filterSeq

La función `filterSeq` conserva solamente los elementos que cumplen una condición.

```sml
fun filterSeq p Empty = Empty
  | filterSeq p (Cons(n, xt)) =
        if p n
        then Cons(n, fn () => filterSeq p (xt()))
        else filterSeq p (xt());
```

Esta función permite filtrar una secuencia sin generar todos los valores desde el inicio.

---

## 7. Secuencia de múltiplos

La secuencia de múltiplos se construye aplicando `mapSeq` a una secuencia infinita de naturales.

```sml
fun multSeq k = mapSeq (fn n => n * k) (seqFrom 1);
```

Por ejemplo:

```sml
takeSeq (6, multSeq 5);
```

produce:

```text
[5, 10, 15, 20, 25, 30]
```

### Justificación

La solución usa `mapSeq` para transformar cada número natural `n` en `n * k`.

La evaluación diferida permite que la secuencia sea infinita sin calcular todos sus valores desde el principio.

Solo se generan los múltiplos que se solicitan mediante `takeSeq`.

---

## 8. Secuencia de factoriales

La secuencia de factoriales se construye utilizando un acumulador.

```sml
fun factSeqFrom (n, acc) =
    Cons(acc, fn () => factSeqFrom(n + 1, acc * (n + 1)));
```

La secuencia se inicia con:

```sml
val factorialSeq = factSeqFrom(1, 1);
```

Por ejemplo:

```sml
takeSeq (6, factorialSeq);
```

produce:

```text
[1, 2, 6, 24, 120, 720]
```

### Justificación

Cada nuevo factorial se calcula usando el resultado anterior.

Esto evita recalcular desde cero el factorial de cada número, como ocurriría en una implementación recursiva tradicional.

La evaluación diferida permite generar solamente los factoriales necesarios.

---

## 9. Secuencia de Fibonacci

La secuencia de Fibonacci se construye conservando dos valores consecutivos.

```sml
fun fibSeqFrom (a, b) =
    Cons(a, fn () => fibSeqFrom(b, a + b));
```

La secuencia se inicia con:

```sml
val fibonacciSeq = fibSeqFrom(1, 1);
```

Por ejemplo:

```sml
takeSeq (8, fibonacciSeq);
```

produce:

```text
[1, 1, 2, 3, 5, 8, 13, 21]
```

### Justificación

Cada nuevo término se obtiene sumando los dos valores anteriores.

Este enfoque es más eficiente que calcular cada término con la recursión clásica de Fibonacci, porque evita repetir llamadas innecesarias.

La evaluación diferida permite avanzar en la secuencia solo cuando se solicitan nuevos valores.

---

## 10. Rol de la evaluación diferida

La evaluación diferida permite representar secuencias potencialmente infinitas sin calcularlas completamente.

En SML, esto se simula guardando la cola de la secuencia como una función:

```sml
fn () => ...
```

Así, el siguiente fragmento de la secuencia solo se construye cuando se llama a esa función.

Esto permite trabajar con listas infinitas de forma controlada y eficiente.

---

## Conclusión

Esta actividad permite comprender cómo simular listas perezosas en un lenguaje que normalmente evalúa de manera estricta.

Las secuencias de múltiplos, factoriales y Fibonacci muestran cómo la programación funcional puede construir estructuras infinitas y calcular solamente los valores necesarios.

El uso de funciones como `mapSeq`, `filterSeq`, `takeSeq` y `dropSeq` evidencia la utilidad de la evaluación diferida para resolver problemas de manera clara y flexible.
