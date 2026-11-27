# Switch y dispositivo final

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Gestión del switch

- **¿Por qué?** Administrar el switch no lo convierte en router.
- **¿Para qué?** Para identificar su propia dirección de gestión.
- **¿Cómo?** Señala la interfaz lógica de gestión en el esquema y no la confundas con el gateway.
- **¿Cuándo usarlo?** Al revisar el acceso administrativo de un switch de capa 2.

### Dirección y salida del PC

- **¿Por qué?** Comunicarse con un vecino no exige lo mismo que llegar a otra red.
- **¿Para qué?** Para saber qué parámetros necesita el host.
- **¿Cómo?** Comprueba que IP y máscara coincidan con el segmento y localiza el gateway para salir.
- **¿Cuándo usarlo?** Cuando el equipo alcanza la LAN pero no un destino remoto.

## Ejercicio

En un laboratorio aislado compara la conectividad entre dos PCs locales con y sin gateway. Observa la tabla de MAC y el estado de la interfaz antes de guardar.

## Comprobación

El tráfico local puede funcionar sin gateway. El tráfico hacia otra red necesita una ruta, empezando por el gateway.

[Volver a presentaciones](README.md). Material didáctico redactado para el portafolio. No contiene las diapositivas de Cisco.
