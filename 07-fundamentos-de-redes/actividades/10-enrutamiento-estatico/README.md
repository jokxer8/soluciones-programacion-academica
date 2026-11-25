# Enrutamiento estático: cuatro LAN

[Fundamentos de Redes](../../README.md) · [Todas las actividades](../README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../../assets/readme-banners/en-palabras-simples.svg)

Una ruta estática indica manualmente a qué vecino enviar el tráfico para llegar a una red que no está conectada directamente.

## Qué se trabajó

Dibuja cuatro LAN repartidas entre tres routers. Para cada LAN remota identifica el próximo salto y verifica que la respuesta también pueda volver. Si un primer ping falla, repite la prueba y registra ambas observaciones.

## Evidencia y alcance

**Material localizado:** Práctica con archivo .pkt y registros CLI de equipos simulados.

Las transcripciones muestran seis pares con 4/4 respuestas en su última prueba. Tres primeras pruebas desde el mismo equipo tuvieron 3/4; la nota de trabajo lo atribuye a resolución inicial de ARP. No se midió rendimiento real.

## Lecturas finales de los registros existentes

| Origen → destino | Respuestas | Observación |
|---|---:|---|
| PC0 → PC1 | 4/4 | Registro final |
| PC0 → PC2 | 4/4 | Registro final |
| PC1 → PC2 | 4/4 | Registro final |
| PC3 → PC0 | 4/4 | Repetición tras 3/4 inicial |
| PC3 → PC1 | 4/4 | Repetición tras 3/4 inicial |
| PC3 → PC2 | 4/4 | Repetición tras 3/4 inicial |

Una solicitud con respuesta verifica el camino de ida y retorno de ese intercambio. La tabla no representa doce pruebas direccionales independientes ni pruebas de carga.

## Archivo de respaldo o estudio

[Registro original de PC0](../../tareas/10-enrutamiento-estatico/evidencia-cli/pc0_ping.txt) · [PC1](../../tareas/10-enrutamiento-estatico/evidencia-cli/pc1_ping.txt) · [PC3](../../tareas/10-enrutamiento-estatico/evidencia-cli/pc3_ping.txt). Las tres transcripciones conservan los reintentos. Para reproducir el método con otra maqueta, usa la lámina 5 del [cuaderno](../../material-de-estudio/cuaderno-de-practica.pdf).

[Ver los archivos de esta actividad](../../tareas/10-enrutamiento-estatico/evidencia-cli/).

[Descargar el simulador de la entrega](../../tareas/10-enrutamiento-estatico/enrutamiento-estatico.pkt).
