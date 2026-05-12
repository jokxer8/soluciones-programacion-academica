# Desarrollo - Estructuras, Signaturas y Tipos Abstractos en SML

## 1. Signatura ESTUDIANTE

La actividad inicia con la definición de una signatura llamada `ESTUDIANTE`.

Esta signatura describe las operaciones disponibles para trabajar con datos de un estudiante, sin revelar cómo se almacenan internamente.

La signatura incluye:

- Un tipo abstracto `dataEstudiante`.
- Una excepción `StudentError`.
- Un valor inicial `empty`.
- Funciones `put` para agregar información.
- Funciones `get` para recuperar información.

---

## 2. Primera implementación: lista de asociación

La primera implementación utiliza una lista de asociación.

Cada elemento de la lista tiene la forma:

```text
clave, valor
```

Por ejemplo:

```text
"nombre", "Ana"
"id", "A001"
"indice", 3.85
```

Como los valores pueden ser de distintos tipos, se define un tipo auxiliar llamado `valor`.

Este tipo permite representar valores `string`, `char`, `int` y `real`.

---

## 3. Manejo de errores

La excepción `StudentError` se utiliza en tres situaciones principales:

1. Cuando se intenta consultar una propiedad que no existe.
2. Cuando se intenta insertar dos veces la misma propiedad.
3. Cuando se intenta asignar un género con más de un carácter.

Esta regla responde al mandato de la actividad, donde se indica que las funciones `put` solo pueden ser llamadas una vez por propiedad.

---

## 4. Segunda implementación: record

La segunda implementación utiliza un record de SML.

Cada campo del record se representa como un valor opcional:

```sml
string option
char option
int option
real option
```

Esto permite distinguir entre:

- Un campo todavía no asignado: `NONE`.
- Un campo ya asignado: `SOME valor`.

Con esta estrategia, se puede validar si un campo ya fue colocado antes de permitir una nueva asignación.

---

## 5. Tipo abstracto de secuencia

La segunda parte de la actividad define una secuencia mediante un tipo algebraico.

Una secuencia puede ser:

```text
Vacía
Un elemento
Una secuencia seguida de otra secuencia
```

En SML esto se representa mediante constructores.

La estructura permite agregar elementos al inicio, al final, unir secuencias y convertirlas a listas.

---

## 6. Casos de prueba corregidos

En la versión profesional se agregan pruebas con resultados esperados para demostrar la efectividad de cada función.

Esto mejora el desarrollo original, ya que ahora no solo se presenta el código, sino también el comportamiento esperado.

---

## 7. Resultados esperados de pruebas

### Pruebas de EstudianteLista

| Prueba | Resultado esperado |
|---|---|
| `EstudianteLista.getNombre estudianteListaCompleto` | `"Ana"` |
| `EstudianteLista.getApellido estudianteListaCompleto` | `"Rodriguez"` |
| `EstudianteLista.getID estudianteListaCompleto` | `"A001"` |
| `EstudianteLista.getGenero estudianteListaCompleto` | `#"F"` |
| `EstudianteLista.getAgnoAcademico estudianteListaCompleto` | `4` |
| `EstudianteLista.getIndice estudianteListaCompleto` | `3.85` |
| Repetir `putNombre` | `"ok"` al capturar `StudentError` |
| Colocar género inválido | `"ok"` al capturar `StudentError` |

---

### Pruebas de EstudianteRegistro

| Prueba | Resultado esperado |
|---|---|
| `EstudianteRegistro.getNombre estudianteRegistroCompleto` | `"Luis"` |
| `EstudianteRegistro.getApellido estudianteRegistroCompleto` | `"Mendez"` |
| `EstudianteRegistro.getID estudianteRegistroCompleto` | `"B002"` |
| `EstudianteRegistro.getGenero estudianteRegistroCompleto` | `#"M"` |
| `EstudianteRegistro.getAgnoAcademico estudianteRegistroCompleto` | `3` |
| `EstudianteRegistro.getIndice estudianteRegistroCompleto` | `3.45` |
| Repetir `putID` | `"ok"` al capturar `StudentError` |
| Consultar campo vacío | `"ok"` al capturar `StudentError` |

---

### Pruebas de Secuencia

| Prueba | Resultado esperado |
|---|---|
| `Secuencia.secToList secuenciaVacia` | `[]` |
| `Secuencia.secToList secuenciaConFrente` | `["B", "A"]` |
| `Secuencia.secToList secuenciaConFinal` | `["B", "A", "C"]` |
| `Secuencia.secToList secuenciaUnida` | `["B", "A", "C", "D"]` |
| `Secuencia.secToList secuenciaNumeros` | `[8, 47, 103, 256]` |

---

## 8. Conclusión

Esta actividad demuestra cómo SML permite separar la interfaz de la implementación mediante signaturas y estructuras.

También evidencia el uso de tipos abstractos de datos, manejo de errores, records, listas de asociación y tipos algebraicos.

La versión corregida agrega resultados esperados en los casos de prueba, fortaleciendo la claridad y verificabilidad del ejercicio.
