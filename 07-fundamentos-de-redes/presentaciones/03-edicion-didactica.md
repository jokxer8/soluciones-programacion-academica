# Protocolos y modelos

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Capas y funciones

- **¿Por qué?** Una conversación web depende de tareas distintas, desde el mensaje hasta la señal.
- **¿Para qué?** Para revisar dónde puede fallar sin cambiar todo a la vez.
- **¿Cómo?** Traza la consulta por aplicación, transporte, red, enlace y medio físico.
- **¿Cuándo usarlo?** Cuando encuentres un problema y no sepas qué nivel revisar primero.

### Encapsulación

- **¿Por qué?** Cada etapa necesita información de control propia.
- **¿Para qué?** Para distinguir el mensaje original de lo que permite transportarlo y entregarlo.
- **¿Cómo?** Dibuja los datos dentro del paquete y este dentro de la trama; separa la señal del contenido.
- **¿Cuándo usarlo?** Al seguir un envío desde la aplicación hasta la red.

## Ejercicio

Para una consulta web, traza aplicación, transporte, paquete IP, trama y señal. Si falta la señal, no empieces por el navegador.

## Comprobación

El modelo sirve para ordenar el diagnóstico; no hace que una aplicación pertenezca a la misma capa que un cable.

[Volver a presentaciones](README.md). Material didáctico redactado para el portafolio. No contiene las diapositivas de Cisco.
