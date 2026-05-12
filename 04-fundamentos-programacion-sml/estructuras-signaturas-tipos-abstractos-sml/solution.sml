(*
   Estructuras, signaturas y tipos abstractos en Standard ML

   Esta solución implementa:

   1. Una signatura ESTUDIANTE usando una lista de asociación.
   2. La misma signatura usando un record.
   3. Una signatura y estructura para secuencias.
   4. Casos de prueba con resultados esperados.

   La versión incluye pruebas visibles para demostrar la efectividad
   de las funciones implementadas.
*)

signature ESTUDIANTE = sig
    type dataEstudiante
    exception StudentError

    val empty : dataEstudiante

    val putNombre : string * dataEstudiante -> dataEstudiante
    val putApellido : string * dataEstudiante -> dataEstudiante
    val putID : string * dataEstudiante -> dataEstudiante
    val putGenero : string * dataEstudiante -> dataEstudiante
    val putAgnoAcademico : int * dataEstudiante -> dataEstudiante
    val putIndice : real * dataEstudiante -> dataEstudiante

    val getNombre : dataEstudiante -> string
    val getApellido : dataEstudiante -> string
    val getID : dataEstudiante -> string
    val getGenero : dataEstudiante -> char
    val getAgnoAcademico : dataEstudiante -> int
    val getIndice : dataEstudiante -> real
end;

(* ========================================================= *)
(* Implementación 1: Lista de asociación                     *)
(* ========================================================= *)

structure EstudianteLista :> ESTUDIANTE = struct
    datatype valor =
        VString of string
      | VChar of char
      | VInt of int
      | VReal of real

    type dataEstudiante = (string * valor) list

    exception StudentError

    val empty : dataEstudiante = []

    fun existe _ [] = false
      | existe clave ((k, _) :: xs) =
            (clave = k) orelse existe clave xs

    fun insertar (clave, valor, datos) =
        if existe clave datos
        then raise StudentError
        else (clave, valor) :: datos

    fun buscar _ [] = raise StudentError
      | buscar clave ((k, v) :: xs) =
            if clave = k
            then v
            else buscar clave xs

    fun convertirGenero texto =
        if String.size texto = 1
        then String.sub (texto, 0)
        else raise StudentError

    fun putNombre (s, d) =
        insertar ("nombre", VString s, d)

    fun putApellido (s, d) =
        insertar ("apellido", VString s, d)

    fun putID (s, d) =
        insertar ("id", VString s, d)

    fun putGenero (s, d) =
        insertar ("genero", VChar (convertirGenero s), d)

    fun putAgnoAcademico (n, d) =
        insertar ("agnoAcademico", VInt n, d)

    fun putIndice (r, d) =
        insertar ("indice", VReal r, d)

    fun getNombre d =
        case buscar "nombre" d of
            VString s => s
          | _ => raise StudentError

    fun getApellido d =
        case buscar "apellido" d of
            VString s => s
          | _ => raise StudentError

    fun getID d =
        case buscar "id" d of
            VString s => s
          | _ => raise StudentError

    fun getGenero d =
        case buscar "genero" d of
            VChar c => c
          | _ => raise StudentError

    fun getAgnoAcademico d =
        case buscar "agnoAcademico" d of
            VInt n => n
          | _ => raise StudentError

    fun getIndice d =
        case buscar "indice" d of
            VReal r => r
          | _ => raise StudentError
end;

(* Pruebas de EstudianteLista *)

val estudianteLista0 = EstudianteLista.empty;

val estudianteLista1 =
    EstudianteLista.putNombre ("Ana", estudianteLista0);

val estudianteLista2 =
    EstudianteLista.putApellido ("Rodriguez", estudianteLista1);

val estudianteLista3 =
    EstudianteLista.putID ("A001", estudianteLista2);

val estudianteLista4 =
    EstudianteLista.putGenero ("F", estudianteLista3);

val estudianteLista5 =
    EstudianteLista.putAgnoAcademico (4, estudianteLista4);

val estudianteListaCompleto =
    EstudianteLista.putIndice (3.85, estudianteLista5);

val pruebaListaNombre =
    EstudianteLista.getNombre estudianteListaCompleto;

val pruebaListaApellido =
    EstudianteLista.getApellido estudianteListaCompleto;

val pruebaListaID =
    EstudianteLista.getID estudianteListaCompleto;

val pruebaListaGenero =
    EstudianteLista.getGenero estudianteListaCompleto;

val pruebaListaAgno =
    EstudianteLista.getAgnoAcademico estudianteListaCompleto;

val pruebaListaIndice =
    EstudianteLista.getIndice estudianteListaCompleto;

val pruebaListaNombreRepetido =
    (EstudianteLista.putNombre ("Otro", estudianteListaCompleto); "fallo")
    handle EstudianteLista.StudentError => "ok";

val pruebaListaGeneroInvalido =
    (EstudianteLista.putGenero ("Femenino", EstudianteLista.empty); "fallo")
    handle EstudianteLista.StudentError => "ok";

(*
   Resultados esperados:

   pruebaListaNombre = "Ana"
   pruebaListaApellido = "Rodriguez"
   pruebaListaID = "A001"
   pruebaListaGenero = #"F"
   pruebaListaAgno = 4
   pruebaListaIndice = 3.85
   pruebaListaNombreRepetido = "ok"
   pruebaListaGeneroInvalido = "ok"
*)

(* ========================================================= *)
(* Implementación 2: Record                                 *)
(* ========================================================= *)

structure EstudianteRegistro :> ESTUDIANTE = struct
    type dataEstudiante =
    {
        nombre : string option,
        apellido : string option,
        id : string option,
        genero : char option,
        agnoAcademico : int option,
        indice : real option
    }

    exception StudentError

    val empty : dataEstudiante =
    {
        nombre = NONE,
        apellido = NONE,
        id = NONE,
        genero = NONE,
        agnoAcademico = NONE,
        indice = NONE
    }

    fun convertirGenero texto =
        if String.size texto = 1
        then String.sub (texto, 0)
        else raise StudentError

    fun putNombre
        (s, {nombre, apellido, id, genero, agnoAcademico, indice}) =
        case nombre of
            NONE =>
                {
                    nombre = SOME s,
                    apellido = apellido,
                    id = id,
                    genero = genero,
                    agnoAcademico = agnoAcademico,
                    indice = indice
                }
          | SOME _ => raise StudentError

    fun putApellido
        (s, {nombre, apellido, id, genero, agnoAcademico, indice}) =
        case apellido of
            NONE =>
                {
                    nombre = nombre,
                    apellido = SOME s,
                    id = id,
                    genero = genero,
                    agnoAcademico = agnoAcademico,
                    indice = indice
                }
          | SOME _ => raise StudentError

    fun putID
        (s, {nombre, apellido, id, genero, agnoAcademico, indice}) =
        case id of
            NONE =>
                {
                    nombre = nombre,
                    apellido = apellido,
                    id = SOME s,
                    genero = genero,
                    agnoAcademico = agnoAcademico,
                    indice = indice
                }
          | SOME _ => raise StudentError

    fun putGenero
        (s, {nombre, apellido, id, genero, agnoAcademico, indice}) =
        case genero of
            NONE =>
                {
                    nombre = nombre,
                    apellido = apellido,
                    id = id,
                    genero = SOME (convertirGenero s),
                    agnoAcademico = agnoAcademico,
                    indice = indice
                }
          | SOME _ => raise StudentError

    fun putAgnoAcademico
        (n, {nombre, apellido, id, genero, agnoAcademico, indice}) =
        case agnoAcademico of
            NONE =>
                {
                    nombre = nombre,
                    apellido = apellido,
                    id = id,
                    genero = genero,
                    agnoAcademico = SOME n,
                    indice = indice
                }
          | SOME _ => raise StudentError

    fun putIndice
        (r, {nombre, apellido, id, genero, agnoAcademico, indice}) =
        case indice of
            NONE =>
                {
                    nombre = nombre,
                    apellido = apellido,
                    id = id,
                    genero = genero,
                    agnoAcademico = agnoAcademico,
                    indice = SOME r
                }
          | SOME _ => raise StudentError

    fun getNombre
        {nombre, apellido, id, genero, agnoAcademico, indice} =
        case nombre of
            SOME s => s
          | NONE => raise StudentError

    fun getApellido
        {nombre, apellido, id, genero, agnoAcademico, indice} =
        case apellido of
            SOME s => s
          | NONE => raise StudentError

    fun getID
        {nombre, apellido, id, genero, agnoAcademico, indice} =
        case id of
            SOME s => s
          | NONE => raise StudentError

    fun getGenero
        {nombre, apellido, id, genero, agnoAcademico, indice} =
        case genero of
            SOME c => c
          | NONE => raise StudentError

    fun getAgnoAcademico
        {nombre, apellido, id, genero, agnoAcademico, indice} =
        case agnoAcademico of
            SOME n => n
          | NONE => raise StudentError

    fun getIndice
        {nombre, apellido, id, genero, agnoAcademico, indice} =
        case indice of
            SOME r => r
          | NONE => raise StudentError
end;

(* Pruebas de EstudianteRegistro *)

val estudianteRegistro0 = EstudianteRegistro.empty;

val estudianteRegistro1 =
    EstudianteRegistro.putNombre ("Luis", estudianteRegistro0);

val estudianteRegistro2 =
    EstudianteRegistro.putApellido ("Mendez", estudianteRegistro1);

val estudianteRegistro3 =
    EstudianteRegistro.putID ("B002", estudianteRegistro2);

val estudianteRegistro4 =
    EstudianteRegistro.putGenero ("M", estudianteRegistro3);

val estudianteRegistro5 =
    EstudianteRegistro.putAgnoAcademico (3, estudianteRegistro4);

val estudianteRegistroCompleto =
    EstudianteRegistro.putIndice (3.45, estudianteRegistro5);

val pruebaRegistroNombre =
    EstudianteRegistro.getNombre estudianteRegistroCompleto;

val pruebaRegistroApellido =
    EstudianteRegistro.getApellido estudianteRegistroCompleto;

val pruebaRegistroID =
    EstudianteRegistro.getID estudianteRegistroCompleto;

val pruebaRegistroGenero =
    EstudianteRegistro.getGenero estudianteRegistroCompleto;

val pruebaRegistroAgno =
    EstudianteRegistro.getAgnoAcademico estudianteRegistroCompleto;

val pruebaRegistroIndice =
    EstudianteRegistro.getIndice estudianteRegistroCompleto;

val pruebaRegistroIDRepetido =
    (EstudianteRegistro.putID ("B003", estudianteRegistroCompleto); "fallo")
    handle EstudianteRegistro.StudentError => "ok";

val pruebaRegistroCampoVacio =
    (EstudianteRegistro.getNombre EstudianteRegistro.empty; "fallo")
    handle EstudianteRegistro.StudentError => "ok";

(*
   Resultados esperados:

   pruebaRegistroNombre = "Luis"
   pruebaRegistroApellido = "Mendez"
   pruebaRegistroID = "B002"
   pruebaRegistroGenero = #"M"
   pruebaRegistroAgno = 3
   pruebaRegistroIndice = 3.45
   pruebaRegistroIDRepetido = "ok"
   pruebaRegistroCampoVacio = "ok"
*)

(* ========================================================= *)
(* Signatura y estructura de Secuencia                      *)
(* ========================================================= *)

signature SECUENCIA = sig
    type 'a sec

    val empty : 'a sec
    val addFront : 'a * 'a sec -> 'a sec
    val addBack : 'a * 'a sec -> 'a sec
    val seqAppend : 'a sec * 'a sec -> 'a sec
    val secToList : 'a sec -> 'a list
end;

structure Secuencia :> SECUENCIA = struct
    datatype 'a secuencia =
        Vacia
      | Uno of 'a
      | Juntar of 'a secuencia * 'a secuencia

    type 'a sec = 'a secuencia

    val empty = Vacia

    fun addFront (x, Vacia) = Uno x
      | addFront (x, s) = Juntar (Uno x, s)

    fun addBack (x, Vacia) = Uno x
      | addBack (x, s) = Juntar (s, Uno x)

    fun seqAppend (Vacia, s) = s
      | seqAppend (s, Vacia) = s
      | seqAppend (s1, s2) = Juntar (s1, s2)

    fun secToList Vacia = []
      | secToList (Uno x) = [x]
      | secToList (Juntar (s1, s2)) =
            secToList s1 @ secToList s2
end;

(* Pruebas de Secuencia con cadenas *)

val secuenciaVacia = Secuencia.empty;

val secuenciaA =
    Secuencia.addFront ("A", secuenciaVacia);

val secuenciaConFrente =
    Secuencia.addFront ("B", secuenciaA);

val secuenciaConFinal =
    Secuencia.addBack ("C", secuenciaConFrente);

val secuenciaD =
    Secuencia.addBack ("D", Secuencia.empty);

val secuenciaUnida =
    Secuencia.seqAppend (secuenciaConFinal, secuenciaD);

val pruebaSecuenciaVacia =
    Secuencia.secToList secuenciaVacia;

val pruebaSecuenciaFrente =
    Secuencia.secToList secuenciaConFrente;

val pruebaSecuenciaFinal =
    Secuencia.secToList secuenciaConFinal;

val pruebaSecuenciaUnida =
    Secuencia.secToList secuenciaUnida;

(* Pruebas de Secuencia con números *)

val secuenciaNumeros0 = Secuencia.empty;

val secuenciaNumeros1 =
    Secuencia.addFront (47, secuenciaNumeros0);

val secuenciaNumeros2 =
    Secuencia.addBack (103, secuenciaNumeros1);

val secuenciaNumeros3 =
    Secuencia.addFront (8, secuenciaNumeros2);

val secuenciaNumeros4 =
    Secuencia.addBack (256, secuenciaNumeros3);

val pruebaSecuenciaNumeros =
    Secuencia.secToList secuenciaNumeros4;

(*
   Resultados esperados:

   pruebaSecuenciaVacia = []
   pruebaSecuenciaFrente = ["B", "A"]
   pruebaSecuenciaFinal = ["B", "A", "C"]
   pruebaSecuenciaUnida = ["B", "A", "C", "D"]
   pruebaSecuenciaNumeros = [8, 47, 103, 256]
*)
