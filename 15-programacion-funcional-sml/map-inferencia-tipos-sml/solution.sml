(*
   Función map e inferencia de tipos en Standard ML

   Esta solución implementa una versión propia de la función map
   y realiza pruebas aplicando funciones a listas de enteros y cadenas.

   También evidencia el tipo general de map:

   ('a -> 'b) -> 'a list -> 'b list
*)

(* Definición de una función map propia *)
fun mapPropio f [] = []
  | mapPropio f (hd::tl) = (f hd) :: (mapPropio f tl);

(* Prueba 1: duplicar números enteros *)
val numerosDuplicados =
    mapPropio (fn n => 2 * n) [1, 2, 3, 4, 5, 6, 7, 8];

(*
   Resultado esperado:
   numerosDuplicados = [2, 4, 6, 8, 10, 12, 14, 16]
*)

(* Prueba 2: duplicar cadenas de texto *)
val cadenasDuplicadas =
    mapPropio (fn s => s ^ s) ["uno", "dos", "tres"];

(*
   Resultado esperado:
   cadenasDuplicadas = ["unouno", "dosdos", "trestres"]
*)

(* Prueba 3: determinar si los números son pares *)
val sonPares =
    mapPropio (fn n => n mod 2 = 0) [1, 2, 3, 4, 5, 6];

(*
   Resultado esperado:
   sonPares = [false, true, false, true, false, true]
*)

(* Prueba 4: calcular la longitud de cada palabra *)
val longitudes =
    mapPropio String.size ["SML", "funcional", "map"];

(*
   Resultado esperado:
   longitudes = [3, 9, 3]
*)
