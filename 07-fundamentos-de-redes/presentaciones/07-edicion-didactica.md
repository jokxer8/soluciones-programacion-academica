# Capa de enlace de datos

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Tramas Ethernet

- **¿Por qué?** Cada salto local tiene origen y destino propios.
- **¿Para qué?** Para decidir qué MAC se usa en el primer tramo.
- **¿Cómo?** Marca el gateway como destino MAC cuando el destino IP esté en otra LAN.
- **¿Cuándo usarlo?** Al seguir tráfico que pasa por un router.

### Aprendizaje y salto siguiente

- **¿Por qué?** El switch ubica vecinos por su MAC; el router prepara una trama para el enlace siguiente.
- **¿Para qué?** Para distinguir entrega local de enrutamiento.
- **¿Cómo?** Sigue la trama hasta el router y dibuja otra para el tramo de salida.
- **¿Cuándo usarlo?** Cuando un paquete cruza varias redes.

## Ejercicio

Dibuja dos LAN y un router. Identifica la primera MAC de destino de una comunicación remota y la IP final.

## Comprobación

En la primera LAN la MAC de destino es la del gateway; la IP de destino sigue siendo la del equipo remoto si no hay NAT.

[Volver a presentaciones](README.md). Material didáctico redactado para el portafolio. No contiene las diapositivas de Cisco.
