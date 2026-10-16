# Auditoría de publicación

Revisión de la carpeta «32. Funds de Sist. Computacionales», 15 de octubre de 2026 (hora de Santo Domingo).

## Trazabilidad y selección

Se clasificaron 121 archivos y se publicaron 88, preservando 43 fuentes C y 15 fuentes ASM. Los hashes de origen y publicación se registran en el [inventario](../archivos-originales/fundamentos-sistemas-computacionales/inventario.json). El código seleccionado se conserva salvo una rectificación de compilación en FASM, registrada en el inventario; las explicaciones, pruebas y rectificaciones se añaden en Markdown y `tests/`.

Los cuatro comprimidos de entregas se abrieron y compararon con las carpetas expandidas. Las asignaciones 3 y 4 y los cinco archivos del RAR final coinciden con sus equivalentes. El RAR híbrido contiene siete equivalentes y una nota de empaquetado adicional. No se ejecutó el EXE suministrado: las pruebas usan compilaciones nuevas de las fuentes.

## Corrección de código

En `04-cuenta-regresiva.asm` la última expresión era `coma_1 = $ -`, incompleta. Se completó como `coma_1 = $ - coma`, longitud de la cadena separadora. Tras el cambio, los doce programas FASM se ensamblan. El hash de origen permanece registrado y el hash público identifica la versión corregida.

## Ajustes documentales y privacidad

La matrícula manuscrita se retiró del PDF público del parcial, páginas 1 y 3. Ambas páginas se renderizaron y comprobaron visualmente después de la redacción; se conservaron nombre y respuestas. Se retiraron el identificador de una nota y el registro de asistencia de teoría. Se excluyeron las configuraciones locales con rutas personales.

La asignación 1 tiene enunciado, sin respuestas localizadas. Las rectificaciones del primer parcial están separadas del manuscrito. El resumen encontrado en la asignación 2 corresponde al proyecto híbrido C/NASM. La imagen de tamaños no describe todos los modelos de datos de 64 bits.

## Límites y créditos

No se redistribuyen los libros ni módulos Huawei como trabajo propio. La presencia de esos módulos no demuestra una certificación. El CSV de países es histórico y su procedencia/licencia no está indicada en los archivos recibidos. Las afirmaciones de sanitizadores de la auditoría original se conservan como históricas; consultar los [resultados actuales](VALIDACION.md).

[Materia](README.md)
