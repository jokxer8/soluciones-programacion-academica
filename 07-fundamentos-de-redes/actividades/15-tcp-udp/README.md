# Exposición: TCP y UDP

[Fundamentos de Redes](../../README.md) · [Todas las actividades](../README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../../assets/readme-banners/en-palabras-simples.svg)

### TCP

- **¿Por qué?** Algunos datos necesitan llegar en el orden correcto como un flujo continuo.
- **¿Para qué?** Para transportar contenido como un archivo íntegro.
- **¿Cómo?** Relaciona conexión, números de secuencia, confirmaciones, retransmisión y control de flujo.
- **¿Cuándo usarlo?** Cuando la aplicación necesita entrega ordenada, como en muchas transferencias de archivos.

### UDP

- **¿Por qué?** Esperar retransmisiones puede resultar inútil cuando el dato pierde valor al llegar tarde.
- **¿Para qué?** Para enviar mensajes sin que el transporte garantice orden o entrega.
- **¿Cómo?** Identifica el puerto de destino y decide qué pérdidas tolera la aplicación o cómo las maneja.
- **¿Cuándo usarlo?** En aplicaciones que gestionan la pérdida o priorizan la oportunidad del dato, según su diseño.

## Qué se trabajó

Usa un archivo como ejemplo de datos que deben llegar completos y un flujo en tiempo real como ejemplo donde puede importar más llegar a tiempo. Distingue puertos, establecimiento de conexión, números de secuencia, confirmaciones y control de flujo.

## Evidencia y alcance

**Material localizado:** Guion, diapositivas y presentación interactiva local sobre transporte.

Es una explicación propia, no una copia de guiones, cuestionarios o recursos multimedia. [TCP](https://www.rfc-editor.org/rfc/rfc9293) · [UDP](https://www.rfc-editor.org/rfc/rfc768).

## Archivo de respaldo o estudio

[Presentación pública (PDF)](../../exposiciones/presentacion-tcp-y-udp.pdf). Adaptación de la exposición de Kelvin César Del Castillo Torres y Gabriel Galasso, con diagramas originales, sin matrículas ni banco de preguntas.

[Ver los archivos de esta actividad](../../exposiciones/README.md).
