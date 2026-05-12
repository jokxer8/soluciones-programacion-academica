# Desarrollo - Combinador Y y Fibonacci

## 1. Definición del combinador Y

El combinador Y permite expresar funciones recursivas mediante el concepto de punto fijo.

La definición utilizada en la actividad es:

```text
Y ≜ λf.λn.f(Y f)n
```

Esto significa que el combinador Y recibe una función `f` y devuelve una nueva función que puede aplicarse recursivamente.

---

## 2. Definición cuasi-recursiva de Fibonacci

La función de Fibonacci se expresa de forma cuasi-recursiva como:

```text
Fib = λg.λm. if (m = 1 or m = 2) then 1 else (g(m - 1) + g(m - 2))
```

En esta definición:

- `g` representa la función que permitirá realizar llamadas recursivas.
- `m` representa la posición de Fibonacci que se desea calcular.
- Si `m` es 1 o 2, el resultado es 1.
- En otro caso, se calcula la suma de los dos valores anteriores.

---

## 3. Reducción de la expresión

La expresión a reducir es:

```text
(Y Fib)4
```

---

## 4. Desarrollo paso a paso

Partimos de:

```text
(Y Fib)4
```

Sustituimos la definición de `Y`:

```text
(λf.λn.f(Y f)n Fib)4
```

Aplicamos reducción beta sustituyendo `f` por `Fib`:

```text
(λn.Fib(Y Fib)n)4
```

Sustituimos `n` por `4`:

```text
Fib(Y Fib)4
```

Sustituimos la definición de `Fib`:

```text
(λg.λm. if (m = 1 or m = 2) then 1 else (g(m - 1) + g(m - 2))) (Y Fib) 4
```

Sustituimos `g` por `(Y Fib)`:

```text
(λm. if (m = 1 or m = 2) then 1 else ((Y Fib)(m - 1) + (Y Fib)(m - 2))) 4
```

Sustituimos `m` por `4`:

```text
if (4 = 1 or 4 = 2) then 1 else ((Y Fib)(4 - 1) + (Y Fib)(4 - 2))
```

Evaluamos la condición:

```text
4 ≠ 1
4 ≠ 2
```

Por tanto:

```text
(Y Fib)3 + (Y Fib)2
```

---

## 5. Evaluación de los casos restantes

Sabemos que:

```text
(Y Fib)3 = (Y Fib)2 + (Y Fib)1
```

También:

```text
(Y Fib)2 = 1
(Y Fib)1 = 1
```

Entonces:

```text
(Y Fib)3 = 1 + 1 = 2
```

Ahora sustituimos en la expresión principal:

```text
(Y Fib)4 = (Y Fib)3 + (Y Fib)2
```

```text
(Y Fib)4 = 2 + 1
```

```text
(Y Fib)4 = 3
```

---

## 6. Resultado final

```text
(Y Fib)4 = 3
```

---

## 7. Interpretación

El resultado obtenido confirma que el combinador Y permite expresar la recursividad de la función Fibonacci.

Aunque la función `Fib` no se define directamente como recursiva, al aplicarle el combinador Y se obtiene una función capaz de llamarse a sí misma mediante el punto fijo.

---

## Conclusión

Esta actividad permite comprender cómo el cálculo lambda y la programación funcional representan la recursividad desde una perspectiva teórica.

El combinador Y demuestra que la recursividad puede construirse como una propiedad de las funciones, sin depender inicialmente de una declaración recursiva tradicional.
