# Desarrollo - Función Map e Inferencia de Tipos

## 1. Definición de la función map

La función `map` en SML aplica una función a cada elemento de una lista.

```sml
fun map f [] = []
  | map f (hd::tl) = (f hd) :: (map f tl);
```

La primera línea indica el caso base:

```sml
map f [] = []
```

Si la lista está vacía, el resultado también es una lista vacía.

La segunda línea indica el caso recursivo:

```sml
map f (hd::tl) = (f hd) :: (map f tl)
```

Si la lista tiene elementos, se aplica la función `f` al primer elemento `hd` y luego se continúa aplicando `map` al resto de la lista `tl`.

---

## 2. Llamada analizada

La llamada indicada en la actividad es:

```sml
map (fn s => s ^ s) ["uno", "dos", "tres"]
```

La función anónima:

```sml
fn s => s ^ s
```

recibe una cadena `s` y retorna la concatenación de esa cadena consigo misma.

Por ejemplo:

```text
"uno" ^ "uno" = "unouno"
```

---

## 3. Secuencia de reducciones

Partimos de:

```sml
map f ["uno", "dos", "tres"]
```

donde:

```sml
f = fn s => s ^ s
```

La lista puede representarse como:

```sml
"uno" :: ["dos", "tres"]
```

Aplicamos la definición recursiva de `map`:

```sml
map f ("uno" :: ["dos", "tres"])
=> (f "uno") :: (map f ["dos", "tres"])
```

Evaluamos `f "uno"`:

```sml
f "uno"
=> "uno" ^ "uno"
=> "unouno"
```

Entonces:

```sml
"unouno" :: (map f ["dos", "tres"])
```

Ahora reducimos:

```sml
map f ["dos", "tres"]
=> (f "dos") :: (map f ["tres"])
```

Evaluamos `f "dos"`:

```sml
f "dos"
=> "dos" ^ "dos"
=> "dosdos"
```

Entonces:

```sml
"dosdos" :: (map f ["tres"])
```

Ahora reducimos:

```sml
map f ["tres"]
=> (f "tres") :: (map f [])
```

Evaluamos `f "tres"`:

```sml
f "tres"
=> "tres" ^ "tres"
=> "trestres"
```

El caso base es:

```sml
map f [] = []
```

Por tanto:

```sml
map f ["tres"] => "trestres" :: []
```

Luego:

```sml
map f ["dos", "tres"] => "dosdos" :: ("trestres" :: [])
```

Finalmente:

```sml
map f ["uno", "dos", "tres"]
=> "unouno" :: ("dosdos" :: ("trestres" :: []))
```

Resultado final:

```sml
["unouno", "dosdos", "trestres"]
```

---

## 4. Inferencia de tipos de Hindley-Milner

Para determinar el tipo más general de `map`, se parte de tipos desconocidos.

Supongamos que:

```sml
f : 'a -> 'b
```

Esto significa que `f` recibe un valor de tipo `'a` y devuelve un valor de tipo `'b`.

La función `map` recibe una lista. En el patrón:

```sml
hd::tl
```

se deduce que:

```sml
hd : 'a
tl : 'a list
```

Como `f` se aplica sobre `hd`, entonces:

```sml
f hd : 'b
```

El operador `::` construye listas. Si el primer elemento es de tipo `'b`, entonces el resto de la lista también debe ser de tipo `'b list`.

Por eso:

```sml
map f tl : 'b list
```

Como `tl` es de tipo `'a list`, entonces:

```sml
map f : 'a list -> 'b list
```

Finalmente, como `map` recibe primero una función y luego una lista, su tipo general es:

```sml
map : ('a -> 'b) -> 'a list -> 'b list
```

---

## 5. Tipo más general

```sml
('a -> 'b) -> 'a list -> 'b list
```

---

## 6. Explicación del razonamiento

Para determinar el tipo de `map` con Hindley-Milner, se tratan los tipos como incógnitas al inicio.

Primero se asume que la función `f` recibe un valor de tipo `'a` y devuelve un valor de tipo `'b`.

Luego se analiza el patrón de la lista `hd::tl`, donde `hd` representa el primer elemento y `tl` representa el resto de la lista. Esto permite concluir que `hd` tiene tipo `'a` y `tl` tiene tipo `'a list`.

Después se observa la expresión `(f hd) :: (map f tl)`. Como `f hd` produce un valor de tipo `'b`, entonces el resultado de `map f tl` debe ser una lista de valores de tipo `'b`.

Por tanto, `map` transforma una lista de elementos de tipo `'a` en una lista de elementos de tipo `'b`, usando una función que convierte valores de `'a` en valores de `'b`.

---

## Conclusión

Esta actividad permite comprender cómo funciona una función de orden superior en programación funcional.

La función `map` es un ejemplo fundamental porque recibe otra función como argumento y la aplica a cada elemento de una lista.

Además, la inferencia de tipos de Hindley-Milner permite determinar automáticamente el tipo más general de la función sin necesidad de declararlo explícitamente.
