(*
   Serialización de árboles de expresión en Standard ML

   Esta solución define un tipo de dato recursivo para representar
   árboles binarios de expresión y produce recorridos en preorden,
   enorden y postorden.
*)

datatype tree = Leaf | Br of tree * string * tree;

(* Árbol correspondiente a la expresión:
   (x * 45) + z / (y + 5050)
*)
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

(* Une dos cadenas evitando espacios innecesarios *)
fun unir ("", s) = s
  | unir (s, "") = s
  | unir (s1, s2) = s1 ^ " " ^ s2;

(* Une tres cadenas usando la función unir *)
fun unir3 (a, b, c) = unir (unir (a, b), c);

(* Recorrido en preorden: raíz - izquierda - derecha *)
fun preorden Leaf = ""
  | preorden (Br(izq, dato, der)) =
        unir3 (dato, preorden izq, preorden der);

(* Recorrido en enorden: izquierda - raíz - derecha *)
fun enorden Leaf = ""
  | enorden (Br(Leaf, dato, Leaf)) = dato
  | enorden (Br(izq, dato, der)) =
        "(" ^ enorden izq ^ " " ^ dato ^ " " ^ enorden der ^ ")";

(* Recorrido en postorden: izquierda - derecha - raíz *)
fun postorden Leaf = ""
  | postorden (Br(izq, dato, der)) =
        unir3 (postorden izq, postorden der, dato);

(* Pruebas *)
val resultadoPreorden = preorden exprTree;
val resultadoEnorden = enorden exprTree;
val resultadoPostorden = postorden exprTree;

(*
   Resultados esperados:

   resultadoPreorden:
   "+ * x 45 / z + y 5050"

   resultadoEnorden:
   "((x * 45) + (z / (y + 5050)))"

   resultadoPostorden:
   "x 45 * z y 5050 + / +"
*)
