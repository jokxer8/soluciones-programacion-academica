# Desarrollo - Cálculo Lambda

## 1. Árboles de sintaxis de λ-términos

### Ejercicio 1.1

#### Expresión

```text
(λz.zx)
```

#### Análisis

Esta expresión representa una abstracción lambda.

La variable ligada es `z` y el cuerpo de la función es la aplicación `zx`.

#### Representación textual del árbol

```text
        λz
        |
       zx
      /  \
     z    x
```

#### Explicación

El nodo principal es una abstracción lambda.  
La variable `z` funciona como parámetro de la función.  
El cuerpo `zx` representa una aplicación donde `z` se aplica sobre `x`.

---

### Ejercicio 1.2

#### Expresión

```text
(λv.xv)(λw.λu.wuw)
```

#### Análisis

Esta expresión representa la aplicación de una función lambda a otra función lambda.

La primera parte es:

```text
(λv.xv)
```

La segunda parte es:

```text
(λw.λu.wuw)
```

#### Representación textual del árbol

```text
              Aplicación
             /          \
          λv              λw
          |               |
         xv               λu
        /  \              |
       x    v            wuw
                         /  \
                       wu    w
                      /  \
                     w    u
```

#### Explicación

La expresión completa es una aplicación.

A la izquierda se encuentra la función `(λv.xv)` y a la derecha se encuentra el argumento `(λw.λu.wuw)`.

El cuerpo `xv` representa la aplicación de `x` sobre `v`.

El cuerpo `wuw` se interpreta de forma asociativa hacia la izquierda como:

```text
((w u) w)
```

---

### Ejercicio 1.3

#### Expresión

```text
(λx.λy.(λz.xyz)u)
```

#### Análisis

Esta expresión contiene varias abstracciones lambda anidadas.

Primero se abstrae sobre `x`, luego sobre `y`, y dentro del cuerpo aparece la aplicación de `(λz.xyz)` sobre `u`.

#### Representación textual del árbol

```text
          λx
          |
          λy
          |
       Aplicación
        /      \
      λz        u
      |
     xyz
    /   \
   xy    z
  /  \
 x    y
```

#### Explicación

La expresión principal es una función que recibe `x`.

Dentro de ella hay otra función que recibe `y`.

Luego aparece una aplicación donde la función `(λz.xyz)` se aplica al argumento `u`.

La expresión `xyz` se interpreta de forma asociativa hacia la izquierda como:

```text
((x y) z)
```

---

## 2. Sustituciones

### Ejercicio 2.1

#### Expresión

```text
(λx.λu.x(uz))[(λz.xyz)/z]
```

#### Análisis

Se debe sustituir la variable libre `z` por la expresión:

```text
(λz.xyz)
```

Para evitar captura de variables, se puede renombrar la variable ligada `x` de la expresión original.

#### Resultado

```text
λa.λu.a(u(λz.xyz))
```

#### Explicación

La variable `z` que aparece dentro de `uz` es libre, por lo tanto puede ser sustituida.

La variable `x` fue renombrada como `a` para evitar confusión o captura de variables durante la sustitución.

---

### Ejercicio 2.2

#### Expresión

```text
x((λz.xz)z)[y/z]
```

#### Análisis

Se sustituye la variable libre `z` por `y`.

La `z` que está dentro de `(λz.xz)` está ligada por el lambda y no se sustituye.

#### Resultado

```text
x((λz.xz)y)
```

#### Explicación

Solo se sustituye la `z` libre que aparece como argumento fuera de la abstracción lambda.

---

### Ejercicio 2.3

#### Expresión

```text
((λz.xz)(λy.z))[y/z]
```

#### Análisis

Se debe sustituir la variable libre `z` por `y`.

En la expresión `(λy.z)`, la variable `z` es libre, pero si se sustituye directamente por `y`, podría quedar capturada por el `λy`.

Para evitar captura, se renombra primero la variable ligada `y`.

#### Paso con renombramiento

```text
((λz.xz)(λa.z))[y/z]
```

#### Resultado

```text
((λz.xz)(λa.y))
```

#### Explicación

El renombramiento de `λy` a `λa` permite realizar la sustitución sin alterar el significado lógico de la expresión.

---

## 3. Reducciones beta

### Ejercicio 3.1

#### Expresión

```text
(λx.(λz.yz)xy)z
```

#### Forma normal indicada

```text
yzy
```

#### Desarrollo

Primero se aplica la función al argumento `z`:

```text
(λx.(λz.yz)xy)z
→ (λz.yz)zy
```

Para evitar confusión con el nombre de la variable ligada, se puede renombrar:

```text
(λa.ya)zy
```

Aplicamos la reducción beta:

```text
(λa.ya)z → yz
```

Luego queda:

```text
yzy
```

#### Resultado

```text
yzy
```

---

### Ejercicio 3.2

#### Expresión

```text
(λx.xy)((λz.λv.vzv)ur)
```

#### Forma normal indicada

```text
(rur)y
```

#### Desarrollo

Primero se reduce la expresión interna:

```text
((λz.λv.vzv)u)r
```

Aplicamos la primera reducción beta:

```text
(λz.λv.vzv)u
→ λv.vuv
```

Ahora aplicamos el resultado a `r`:

```text
(λv.vuv)r
→ rur
```

La expresión principal queda:

```text
(λx.xy)(rur)
```

Aplicamos la reducción beta:

```text
(λx.xy)(rur)
→ (rur)y
```

#### Resultado

```text
(rur)y
```

---

### Ejercicio 3.3

#### Expresión

```text
x((λz.z)y)(λz.xz)
```

#### Forma normal indicada

```text
xy(λz.xz)
```

#### Desarrollo

Reducimos la expresión interna:

```text
(λz.z)y
→ y
```

Sustituimos el resultado en la expresión completa:

```text
x((λz.z)y)(λz.xz)
→ xy(λz.xz)
```

#### Resultado

```text
xy(λz.xz)
```

---

## Conclusión

Esta actividad permite comprender cómo se estructuran y evalúan expresiones dentro del cálculo lambda.

Los árboles de sintaxis ayudan a visualizar la estructura interna de los λ-términos, mientras que las sustituciones y reducciones beta permiten analizar cómo una expresión puede transformarse hasta llegar a su forma normal.

Estos conceptos son fundamentales para comprender la programación funcional y sirven como base teórica para lenguajes como Standard ML.
