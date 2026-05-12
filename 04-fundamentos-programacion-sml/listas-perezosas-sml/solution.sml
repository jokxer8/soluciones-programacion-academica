(*
   Listas perezosas en Standard ML

   Esta solución implementa secuencias perezosas para generar:

   1. Múltiplos de un número.
   2. Factoriales.
   3. Números de Fibonacci.

   También incluye funciones auxiliares para tomar elementos,
   descartar elementos, transformar secuencias y filtrar valores.
*)

datatype 'a seq =
    Empty
  | Cons of 'a * (unit -> 'a seq);

(*
   Genera una secuencia infinita desde un número inicial.
*)
fun seqFrom i =
    Cons(i, fn () => seqFrom(i + 1));

(*
   Secuencia infinita de números naturales comenzando en 0.
*)
val natNum =
    seqFrom 0;

(*
   Toma los primeros n elementos de una secuencia perezosa
   y los convierte en una lista normal.
*)
fun takeSeq (0, _) = []
  | takeSeq (_, Empty) = []
  | takeSeq (i, Cons(n, xt)) =
        n :: takeSeq(i - 1, xt());

(*
   Descarta los primeros n elementos de una secuencia perezosa.
*)
fun dropSeq (0, xs) = xs
  | dropSeq (_, Empty) = Empty
  | dropSeq (i, Cons(_, xt)) =
        dropSeq(i - 1, xt());

(*
   Aplica una función a cada elemento de una secuencia,
   conservando la evaluación perezosa.
*)
fun mapSeq f Empty = Empty
  | mapSeq f (Cons(v, xt)) =
        Cons(f v, fn () => mapSeq f (xt()));

(*
   Filtra una secuencia según una condición,
   conservando la evaluación perezosa.
*)
fun filterSeq p Empty = Empty
  | filterSeq p (Cons(n, xt)) =
        if p n
        then Cons(n, fn () => filterSeq p (xt()))
        else filterSeq p (xt());

(*
   Secuencia de múltiplos de un número.
   Ejemplo: multSeq 5 genera 5, 10, 15, 20, ...
*)
fun multSeq k =
    mapSeq (fn n => n * k) (seqFrom 1);

(*
   Secuencia de factoriales.

   Se usa un acumulador para evitar recalcular factoriales
   desde cero en cada posición.
*)
fun factSeqFrom (n, acc) =
    Cons(acc, fn () => factSeqFrom(n + 1, acc * (n + 1)));

val factorialSeq =
    factSeqFrom(1, 1);

(*
   Secuencia de Fibonacci.

   Se conservan dos valores consecutivos para generar
   el siguiente término de forma eficiente.
*)
fun fibSeqFrom (a, b) =
    Cons(a, fn () => fibSeqFrom(b, a + b));

val fibonacciSeq =
    fibSeqFrom(1, 1);

(* ========================================================= *)
(* Casos de prueba                                           *)
(* ========================================================= *)

val pruebaMultiplos =
    takeSeq(6, multSeq 5);

val pruebaFactoriales =
    takeSeq(6, factorialSeq);

val pruebaFibonacci =
    takeSeq(8, fibonacciSeq);

val primerosNaturales =
    takeSeq(10, seqFrom 1);

val despuesDeCinco =
    takeSeq(5, dropSeq(5, seqFrom 1));

val numerosPares =
    takeSeq(6, filterSeq (fn n => n mod 2 = 0) (seqFrom 1));

val multiplosDeTres =
    takeSeq(7, multSeq 3);

(*
   Resultados esperados:

   pruebaMultiplos =
   [5, 10, 15, 20, 25, 30]

   pruebaFactoriales =
   [1, 2, 6, 24, 120, 720]

   pruebaFibonacci =
   [1, 1, 2, 3, 5, 8, 13, 21]

   primerosNaturales =
   [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

   despuesDeCinco =
   [6, 7, 8, 9, 10]

   numerosPares =
   [2, 4, 6, 8, 10, 12]

   multiplosDeTres =
   [3, 6, 9, 12, 15, 18, 21]
*)
