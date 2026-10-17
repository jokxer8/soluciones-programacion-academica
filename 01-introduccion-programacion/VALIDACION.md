# Validación de Introducción a la Programación

[Materia](README.md) · [Casos y resultados](tests/results-windows.json) · [Pruebas reproducibles](tests/run-tests.py)

## Resultado local

Los cinco programas `solution.cpp` se compilaron individualmente en Windows x64 con MSVC, C++17 y las opciones `/W4 /WX`: **32 casos correctos y 0 fallidos**. `/W4` activa un nivel alto de advertencias y `/WX` las trata como errores. Se verifican estas versiones compilables, no una ejecución histórica de las entregas.

| Programa | Casos | Qué se comprueba |
|---|---|---|
| Triángulo | 7 | Catetos 3–4, 5–12 y decimales; rechazo de cero, negativos, texto y resultados fuera de rango. |
| Pirámide | 7 | 1, 3 y 60 pisos; espacios y asteriscos exactos; rechazo de −1, 0, 61 y texto. |
| Coincidencias | 6 | Superposición, mayúsculas, ausencia de coincidencias, texto vacío, búsqueda vacía y falta de texto. |
| Secuencial 3.9 | 5 | Suma normal, negativos, límite del entero y rechazo de desbordamiento y texto. |
| Fibonacci | 7 | 1, 2, 7 y 93 términos; rechazo de 0, 94 y texto. |

## Diferencias respecto de las entregas

- **Triángulo:** el C++ entregado aproxima por incrementos de 0.01; el pseudocódigo usa Newton y el diagrama otra progresión. La versión compilable utiliza `std::hypot`, valida catetos y comprueba resultados finitos. Las distintas versiones se conservan y se describen en su actividad.
- **Pirámide:** se mantiene la fórmula del código entregado, `n−piso` espacios y `2×piso−1` asteriscos. Se añade la comprobación de lectura. Se aclaran los errores de transcripción del pseudocódigo y el rango inclusivo 1–60.
- **Coincidencias:** se conserva la búsqueda parcial, sensible a mayúsculas y con superposición. Se normaliza el espacio no separable que impedía compilar la última línea del texto entregado y se usa `getline` para leer ambas cadenas. Una búsqueda vacía se rechaza.
- **Secuencial:** se conserva `B=A+3`; se amplía el tipo entero y se comprueba el rango antes de sumar.
- **Fibonacci:** se completa el fragmento con cabeceras y entrada validada. Se imprime exactamente la cantidad solicitada, incluido un solo término, y se limita el rango representable.

## Reproducción

En Windows, con Python 3 y las herramientas C++ de Visual Studio instaladas:

```powershell
./tests/Run-Tests.ps1
```

El script compila en un directorio temporal y escribe el informe en `tests/results-windows.json`. La batería verifica códigos de salida, resultados numéricos y el patrón de espacios de la pirámide. La [automatización de Windows](../.github/workflows/introduccion-cpp.yml) permite repetirla en GitHub Actions.

## Alcance

El diagrama del triángulo se ejecutó en RAPTOR Avalonia: se confirmó el error del original con catetos 3 y 4 y se comprobó la corrección con siete casos numéricos. El [informe específico](triangulo-rectangulo/PRUEBAS-RAPTOR.md) distingue la ejecución interactiva, la batería RAPTOR y la transcripción del pseudocódigo de Newton. Estas pruebas locales no forman parte del workflow C++. Los demás diagramas PNG se revisaron visualmente. Los PDF de lectura e investigación se revisaron y se retiraron los identificadores de las copias públicas, manteniendo el contenido y los créditos. El primer parcial solo dispone de enunciado: sus ejemplos explican la lectura de condiciones, no una solución entregada o probada.

Las pruebas cubren los casos indicados. No incluyen todas las representaciones posibles de entradas inválidas, codificaciones de texto ni valores extremos de punto flotante.

## Confirmación en GitHub Actions

La [ejecución de Windows del commit 419155e](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38982024711) compiló los cinco programas y confirmó **32 casos correctos y 0 fallidos**. La [compilación y validación del hotel](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38982024703), ya ubicado en la materia 06, también terminó correctamente con **79 pruebas SQL correctas y 0 fallidas**.
