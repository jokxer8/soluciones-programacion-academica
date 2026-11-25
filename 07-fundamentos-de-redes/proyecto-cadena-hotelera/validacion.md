# Validación del caso multisede

[Volver al proyecto](README.md) · [Fundamentos de Redes](../README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

No basta con que el mapa se vea conectado: hay que comprobar que una sucursal puede usar el sitio y que el recorrido cambia cuando se corta un enlace. Un cálculo de ruta esperada no sustituye una prueba de simulación.

## Dimensionamiento

| Sede | Terminales de referencia | Prefijo de su LAN |
|---|---:|---:|
| Reservas | 32 | /26 |
| Recepción | 20 | /27 |
| Operaciones | 14 | /27 |
| Atención | 10 | /28 |

El informe agrega crecimiento del 20 % y direcciones de infraestructura según corresponda. Sumando las cuatro LAN (64 + 32 + 32 + 16 direcciones) y los cinco enlaces /30 (20 direcciones), el diseño asigna **164 de 256** direcciones del bloque /24; **92** quedan fuera de las subredes asignadas. Esto no significa que haya 164 hosts útiles: cada una tiene sus reservas de red y difusión.

## Cinco pruebas de la entrega

| ID | Qué se comprobó | Resultado documentado | Límite |
|---|---|---|---|
| P01 | IP, máscara y acceso al gateway | El informe registra conformidad; muestra el ejemplo de Reservas con 4/4 respuestas | La figura citada representa una LAN, no todas las combinaciones |
| P02 | Conectividad inicial con rutas estáticas | Rutas estáticas en tránsito y recorrido al servidor por el camino previsto | Solo la fase inicial, no toda la red multisede |
| P03 | RIPv2 y acceso HTTP | El sitio abrió desde las tres sucursales; el informe muestra RIPv2 y rutas aprendidas | Las tablas calculadas se distinguen de capturas; no es una prueba de seguridad web |
| P04 | IPv6 local | 4/4 respuestas entre dos terminales de Reservas | No se enrutó IPv6 entre sedes |
| P05 | Recuperación al cortar E2 | Ruta habitual → ruta por Operaciones y Atención → ruta habitual tras restablecer | Un corte único simulado; no se promete un tiempo de recuperación |

Fuente: informe del proyecto, 22 de noviembre de 2026, apartados de diseño y validación. Esta revisión no ejecutó el archivo en Packet Tracer. [RFC 2453: RIPv2](https://www.rfc-editor.org/rfc/rfc2453).
