# DHCPv4

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Concesión DHCP

- **¿Por qué?** Configurar cada cliente a mano aumenta trabajo y errores.
- **¿Para qué?** Para entregar IP y, según el caso, máscara, gateway y DNS automáticamente.
- **¿Cómo?** Relaciona descubrimiento, oferta, solicitud y confirmación con la concesión inicial.
- **¿Cuándo usarlo?** Cuando equipos de una LAN de práctica se conectan y desconectan.

### Rango dinámico e infraestructura

- **¿Por qué?** Si el conjunto dinámico incluye una IP de infraestructura fija, dos equipos podrían usarla.
- **¿Para qué?** Para separar direcciones previsibles de las prestadas a clientes.
- **¿Cómo?** En el ejemplo, reserva `.1` y `.2` fuera del intervalo `.10` a `.20`.
- **¿Cuándo usarlo?** Antes de dimensionar el rango para clientes.

## Ejercicio

Usa 192.0.2.0/27 como red de documentación; reserva .1 y .2 y propone un intervalo de .10 a .20. Anota configuración recibida, gateway y resultado de una prueba local.

## Comprobación

El intervalo tiene 11 direcciones. Recibir una IP no prueba que el servicio remoto o DNS funcione.

[Volver a presentaciones](README.md). Material didáctico redactado para el portafolio. No contiene las diapositivas de Cisco.
