# Validación de Fundamentos de Sistema Computacionales

Revisión local en Windows x64, 15 de octubre de 2026. Los resultados se generan con el código publicado; no se atribuyen a la entrega histórica.

## Herramientas y reproducción

- Visual Studio, MSVC 14.51, compilación C17 con `/W4`.
- [NASM 2.16.03 para Windows](https://www.nasm.us/pub/nasm/releasebuilds/2.16.03/win64/nasm-2.16.03-win64.zip), SHA-256 `3ee4782247bcb874378d02f7eab4e294a84d3d15f3f6ee2de2f47a46aa7226e6`.
- [FASM 1.73.32 para Windows](https://flatassembler.net/fasmw17332.zip), SHA-256 `57cbffd990908b7526de48fb642937887bf9298f463c2f4039c8c19534406baa`.

Descargar de los sitios oficiales, comprobar los hashes y extraer fuera del repositorio. En PowerShell, desde la carpeta de esta materia:

```powershell
./tests/Run-Checks.ps1 -NasmPath 'C:/herramientas/nasm/nasm.exe' -FasmPath 'C:/herramientas/fasm/FASM.EXE'
```

El script localiza las herramientas C++ de Visual Studio, compila en una carpeta temporal, copia el CSV y las fuentes ASM necesarias, limita cada ejecución a cinco segundos y contrasta la salida con [casos reproducibles](tests/cases.json). No utiliza el EXE recibido con la entrega.

## Resultado de la ejecución final

| Comprobación | Resultado |
|---|---|
| Programas C independientes | 42/42 compilan |
| Proyecto híbrido | Compila y enlaza con las 3 rutinas NASM |
| Control auxiliar del proyecto final | Compila y pasa sus aserciones |
| Compilaciones C totales | 44/44, incluido el control auxiliar |
| Ejecuciones con los casos registrados | 47/47 correctas, incluidas comprobaciones básicas |
| Fuentes NASM Windows x64 | 3/3 ensamblan |
| Fuentes FASM ELF64 | 12/12 ensamblan después de la corrección |

[Registro estructurado de resultados](tests/results-windows.json). La compilación con `/W4` conserva advertencias en el registro temporal; no se afirma ausencia de advertencias. Estas pruebas se ejecutaron localmente, no en GitHub Actions.

## Revisión documental

Se comprobaron los 54 README del repositorio y sus 327 enlaces locales, sin rutas rotas. Los 121 archivos locales de origen mantienen sus hashes; las 88 publicaciones coinciden con los hashes públicos del inventario. Las diferencias deliberadas incluyen la redacción de datos personales y la corrección FASM.

## Alcance

Las comprobaciones incluyen operaciones y patrones en C, estructuras y memoria, demostraciones de listas, ordenamiento, FIFO/LIFO, promedio/máximo de colas, el menú de países y el proyecto híbrido. Las demostraciones de listas sin un patrón específico son comprobaciones básicas de ejecución, no pruebas completas de sus funciones.

El [control del proyecto final](tests/countries-check.c) comprueba lista vacía, inserción, búsqueda, promedio, independencia de la copia a arreglo, cuatro ordenamientos, enlaces anteriores/siguientes, eliminación y carga de 227 registros CSV, incluido un nombre con coma entre comillas.

## Corrección realizada

FASM detectó una expresión incompleta al final de `04-cuenta-regresiva.asm`: `coma_1 = $ -`. Se completó con el nombre de la cadena (`coma`). El inventario conserva el hash de origen y registra el de la versión corregida.

## Límites

El ensamblado FASM genera ELF64; no se ejecutó en Linux. Falta comprobar allí la entrada interactiva y la equivalencia de los doce pares C/ASM. Tampoco se ejecutaron ASan/UBSan en esta revisión, ni se certifica resistencia general a entradas inválidas, desbordamiento o falta de memoria. La auditoría TXT entregada se conserva como referencia histórica.

La asignación 1 solo aporta enunciado. Los libros y módulos Huawei no prueban autoría, aprobación ni certificación y no se redistribuyen. Los resultados del hotel pertenecen a otra materia y siguen documentados en su sección.

[Materia](README.md) · [Auditoría](AUDITORIA.md)
