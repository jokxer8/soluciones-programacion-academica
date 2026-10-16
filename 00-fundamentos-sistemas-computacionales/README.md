# Fundamentos de Sistema Computacionales

[Inicio](../README.md) · **Materia 00** · [Materia 01 →](../01-introduccion-programacion) · [Pendientes](../PENDIENTES.md)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%20%2F%20Ensamblador-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Nivel](https://img.shields.io/badge/Nivel-Fundamentos%20a%20Aplicaci%C3%B3n-green)
![Estado](https://img.shields.io/badge/Estado-Incorporado%20y%20documentado-brightgreen)

Trabajos de representación de datos, programación C, ensamblador y estructuras dinámicas realizados durante mi formación en Ciencias de la Computación. Los enunciados identifican la asignatura como «Fundamentos de Sistemas Computacionales», ICC-133; esta sección reúne mis trabajos de la asignatura.

---

## Objetivo de la materia

Presentar los trabajos que conectan la representación de información con la construcción de programas en C y ensamblador. La sección muestra el recorrido desde bits y tipos de datos hasta estructuras dinámicas, interoperabilidad y un proyecto final con archivos.

---

## Actividades incluidas

| Actividad | Descripción | Conceptos aplicados |
|---|---|---|
| [Representación de datos y fundamentos](fundamentos-representacion-datos) | Sistemas de numeración, complemento a dos, IEEE 754 y tamaños de tipos. | Bases numéricas, complemento a dos, IEEE 754, LP64/LLP64. |
| [Programación en C y ensamblador FASM](c-y-ensamblador) | Doce ejercicios equivalentes de entrada/salida, decisiones, ciclos, primos y patrones. | Registros, syscalls, decisiones, ciclos y comparación C/FASM. |
| [Estructuras y memoria dinámica](estructuras-y-memoria) | Diez ejercicios de estructuras, punteros, rectángulos, complejos y listas. | Struct, punteros, almacenamiento automático y memoria dinámica. |
| [Listas, colas y pilas](listas-colas-pilas) | Diecinueve ejercicios de búsqueda, agregación, eliminación, ordenamiento, FIFO y LIFO. | Nodos, recorridos, transformaciones, merge sort, FIFO y LIFO. |
| [Proyecto híbrido C y NASM](proyecto-hibrido-c-nasm) | Interoperabilidad C/ensamblador x64 para sumar, ordenar e intercambiar enteros. | ABI de Windows x64, paso de parámetros y enlazado C/NASM. |
| [Gestión de países — Proyecto final](proyecto-final-paises) | Lista doblemente enlazada con nodo cabecera, carga CSV, consultas, inserciones, eliminación y cuatro criterios de ordenamiento. | CSV, lista doble, copia profunda y gestión de memoria. |

---

## Organización actual

```text
00-fundamentos-sistemas-computacionales/
├── fundamentos-representacion-datos/
├── c-y-ensamblador/
│   ├── c/
│   └── fasm-linux/
├── estructuras-y-memoria/
├── listas-colas-pilas/
├── proyecto-hibrido-c-nasm/
├── proyecto-final-paises/
├── tests/
├── AUDITORIA.md
├── VALIDACION.md
├── PRUEBAS-LINUX.md
└── README.md
```

La colección conserva **43 fuentes C y 15 fuentes ASM**: 42 programas C independientes, un programa principal C/NASM, doce soluciones FASM y tres rutinas NASM. Los archivos de pruebas son ampliaciones de revisión y se distinguen de esas fuentes académicas.

---

## Habilidades evidenciadas

- Interpretación de sistemas de numeración, complemento a dos e IEEE 754.
- Uso de estructuras de control y generación de patrones en C y ensamblador.
- Modelado con estructuras, punteros y memoria dinámica.
- Implementación y comparación de listas, colas y pilas.
- Interoperabilidad entre C y rutinas NASM con la ABI de Windows x64.
- Lectura de CSV y mantenimiento de una lista doblemente enlazada.
- Compilación y comparación reproducible de programas en Windows y Linux.
- Documentación de decisiones, evidencias, rectificaciones y límites de validación.

---

## Enfoque académico

La asignatura ocupa la posición **00** porque introduce conceptos utilizados después en las demás materias. La representación de datos y el control de ejecución ayudan a comprender los programas de la [materia 02](../02-fundamentos-programacion); las estructuras dinámicas se relacionan con el estudio posterior de [algoritmos](../04-diseno-y-analisis-de-algoritmos). El proyecto de países organiza información en memoria y archivos, antes de abordar persistencia relacional en [Bases de Datos](../06-bases-de-datos).

---

## Organización de cada actividad

```text
nombre-de-la-actividad/
├── README.md
├── desarrollo.md
└── fuentes C / ASM, datos o enlaces a evidencia
```

Cada README presenta descripción, objetivo, conceptos, ejemplos, archivos y forma de revisión. El desarrollo explica el razonamiento técnico. Los trabajos teóricos se documentan con sus evidencias; no se añade código ficticio para completar la estructura.

---

## Relación con los archivos originales

Se revisaron los **121 archivos** de la carpeta académica. Se seleccionaron **88 archivos fuente/documentales** y se clasificaron 33 exclusiones: material editorial o de capacitación de terceros, comprimidos redundantes, un ejecutable y configuraciones locales. La documentación de revisión y las pruebas son adiciones posteriores y no se presentan como entregas originales.

Cada actividad sigue el patrón `README.md` + `desarrollo.md` + código o enlace a evidencia. Los documentos se conservan en [archivos originales](../archivos-originales/fundamentos-sistemas-computacionales), con [inventario completo](../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) y hashes SHA-256.

---

## Ejecución y resultados verificados

[![C y FASM en Linux](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/fundamentos-linux.yml/badge.svg?branch=main)](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/fundamentos-linux.yml)

**Linux verificado:** 132 comparaciones correctas y 0 fallidas entre los doce pares C/FASM, con pipe y terminal PTY. [Resultados, correcciones y reproducción](PRUEBAS-LINUX.md).

| Bloque | Entorno | Resultado registrado |
|---|---|---|
| Programas C independientes | Windows, MSVC y C17. | 42 compilaciones correctas. |
| Proyecto híbrido | Windows x64, C y NASM. | Enlazado y casos de suma, ordenamiento, intercambio y validación de entrada comprobados. |
| Doce pares C/FASM | Ubuntu x86-64, GCC y FASM. | 132 comparaciones correctas, 0 fallidas, con pipe y terminal PTY. |
| Proyecto final de países | C17 y CSV local. | Carga de 227 registros y controles de operaciones, copias y enlaces. |

Los programas C independientes se compilan por separado. El proyecto híbrido necesita Windows x64 y NASM; las otras doce fuentes ASM usan FASM y generan ELF64 para Linux. El proyecto de países utiliza un CSV local y no requiere una base de datos SQL.

[Resultados reproducibles y límites](VALIDACION.md) · [Auditoría de publicación](AUDITORIA.md).

---

## Estado de la sección

Las seis actividades están organizadas y documentadas. El alcance de las pruebas se registra por separado para cada tecnología y proyecto.

El estado de incorporación indica que la evidencia disponible está organizada. No afirma que se localizaron todas las respuestas ni equivale a una certificación de calificación: la asignación 1 solo contiene su enunciado y sus respuestas siguen sin localizarse.

---

## Lenguajes y herramientas

C17, ensamblador x86-64, MSVC, GCC, FASM, NASM, archivos CSV y GitHub Actions. FASM genera ELF64 para Linux; el proyecto NASM utiliza objetos y convención de llamadas de Windows x64.

---

## Autor

**Kelvin César Del Castillo Torres**. Se preservan los créditos y enunciados de origen. Libros y módulos de Huawei se describen en el inventario, sin redistribuirlos como trabajo propio o como certificados obtenidos.

[Volver al repositorio](../README.md)

---

## Recorrido de la materia

En la tabla de actividades se puede abrir cada trabajo y consultar su objetivo, explicación, solución y documentos de apoyo. Los ejemplos se leen junto con sus unidades y condiciones; los resultados ejecutados se identifican con su informe de pruebas.

[Cómo leer la notación y los estados](../GUIA-DE-LECTURA.md) · [Criterios de presentación](../GUIA-DE-ESTILO.md)
