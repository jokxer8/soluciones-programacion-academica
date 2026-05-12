(*
   Función map e inferencia de tipos en Standard ML

   Esta solución implementa una versión propia de map y realiza
   pruebas aplicando funciones a listas de enteros y cadenas.
*)

(* Definición de una función map propia *)
fun mapPropio f [] = []
  | mapPropio f (hd::tl) = (f hd) :: (mapPropio f tl);

(* Prueba 1: duplicar números *)
val numerosDuplicados = mapPropio (fn n => 2 * n) [1, 2, 3, 4, 5, 6, 7, 8];

(* Resultado esperado:
   [2, 4, 6, 8, 10, 12, 14, 16]
*)

(* Prueba 2: duplicar cadenas *)
val cadenasDuplicadas = mapPropio (fn s => s ^ s) ["uno", "dos", "tres"];

(* Resultado esperado:
   ["unouno", "dosdos", "trestres"]
*)

(* Prueba 3: convertir números a booleanos según si son pares *)
val sonPares = mapPropio (fn n => n mod 2 = 0) [1, 2, 3, 4, 5, 6];

(* Resultado esperado:
   [false, true, false, true, false, true]
*)
