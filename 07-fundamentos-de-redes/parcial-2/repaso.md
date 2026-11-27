# Repaso del Parcial 2

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Prefijo y rango

- **¿Por qué?** Una dirección se interpreta con su máscara.
- **¿Para qué?** Para identificar red, hosts y difusión.
- **¿Cómo?** Con `192.0.2.70/26`, ubica el bloque que comienza en `.64`.
- **¿Cuándo usarlo?** Antes de asignar IP a los equipos.

### Gateway y rutas

- **¿Por qué?** Un equipo remoto queda fuera de la LAN.
- **¿Para qué?** Para identificar la salida local y el retorno.
- **¿Cómo?** Ubica el gateway en la misma red y dibuja los saltos de ida y vuelta.
- **¿Cuándo usarlo?** Al conectar segmentos distintos.

### Comprobación

- **¿Por qué?** El plan de rutas no basta para demostrar que funcionan.
- **¿Para qué?** Para dejar una observación que otro pueda interpretar.
- **¿Cómo?** Registra origen, destino y respuesta por separado.
- **¿Cuándo usarlo?** Al leer o preparar evidencias de una práctica.

1. Para `192.0.2.70/26`, calcula red, máscara y difusión. **Comprobación:** `.64`, `255.255.255.192` y `.127`.
2. Identifica el gateway del host. Debe pertenecer a la red local y responder en la simulación.
3. Dos LAN pueden tener enlace local y aun así no comunicarse entre ellas. Dibuja siguiente salto de ida y de retorno antes de escribir rutas.
4. Para comprobar el resultado, anota origen, destino y respuestas. Si falla, contrasta interfaces, prefijos y trayecto; no des por hecho que es el gateway.

[Práctica resuelta](practica-resuelta.pdf) · [Guía de estudio](../material-de-estudio/guia-de-estudio.pdf).
