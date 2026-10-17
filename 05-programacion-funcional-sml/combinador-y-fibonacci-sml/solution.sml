(*
   Combinador Y y Fibonacci en Standard ML

   Esta solución implementa el combinador Y y una versión cuasi-recursiva
   de la función Fibonacci.

   La idea principal es usar el combinador Y para convertir una función
   que recibe como parámetro su propia referencia recursiva en una función
   recursiva funcional.
*)

(* Definición del combinador Y *)
val rec Y =
    fn f =>
        fn n =>
            f (Y f) n;

(* Definición cuasi-recursiva de Fibonacci *)
val fib =
    fn g =>
        fn m =>
            if (m = 1) orelse (m = 2)
            then 1
            else g (m - 1) + g (m - 2);

(* Aplicación del combinador Y para obtener Fibonacci recursivo *)
val fibonacci = Y fib;

(* Casos de prueba *)
val pruebaFib1 = fibonacci 1;
val pruebaFib2 = fibonacci 2;
val pruebaFib3 = fibonacci 3;
val pruebaFib4 = fibonacci 4;
val pruebaFib5 = fibonacci 5;
val pruebaFib6 = fibonacci 6;

(*
   Resultados esperados:

   pruebaFib1 = 1
   pruebaFib2 = 1
   pruebaFib3 = 2
   pruebaFib4 = 3
   pruebaFib5 = 5
   pruebaFib6 = 8
*)
