# Pseudocódigo revisado del triángulo

[Actividad](README.md) · [Diagrama corregido](diagrama-corregido.rap) · [Pruebas](PRUEBAS-RAPTOR.md)

Esta versión describe el diagrama corregido. El [pseudocódigo de la entrega](../../archivos-originales/introduccion-programacion/triangulo/algoritmo.txt), basado en Newton, se conserva por separado.

```text
Leer a, b como valores numéricos
Si no (a > 0 y b > 0 y a/2 < a y b/2 < b):
    Mostrar error y terminar

Si a >= b:
    hipotenusa = a * sqrt(1 + (b/a)^2)
En otro caso:
    hipotenusa = b * sqrt(1 + (a/b)^2)

area = (a/2) * b
perimetro = a + b + hipotenusa

Si no (hipotenusa/2 < hipotenusa y perimetro/2 < perimetro
       y (area = 0 o area/2 < area)):
    Mostrar error y terminar

Mostrar hipotenusa, area, perimetro
```

Para un número positivo finito, dividir entre dos produce un valor menor. Un infinito positivo no cumple esa desigualdad. Uso estas comprobaciones en RAPTOR para detectar resultados que salen de rango; el C++ expresa esa intención con `std::isfinite`. Se admite el área cero que puede producir el subdesbordamiento de valores diminutos, igual que en la versión C++.

La raíz con escala calcula la hipotenusa sin elevar ambos catetos directamente al cuadrado. Esta expresión y `std::hypot` pueden redondear de manera ligeramente distinta: la comparación es numérica, no exige una representación textual idéntica.
