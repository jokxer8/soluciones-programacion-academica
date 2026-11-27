# Repaso del control de Redes

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Equipos

- **¿Por qué?** Conectar dispositivos y conectar redes son tareas distintas.
- **¿Para qué?** Para elegir entre switch de capa 2 y router.
- **¿Cómo?** Identifica si el destino vive en la LAN local o en otra LAN.
- **¿Cuándo usarlo?** Al leer un esquema de red.

### Modelos

- **¿Por qué?** Cada nivel cumple una función diferente.
- **¿Para qué?** Para ordenar el diagnóstico.
- **¿Cómo?** Si no hay señal, empieza por el medio y la capa física.
- **¿Cuándo usarlo?** Cuando no sepas dónde empezar a revisar un fallo.

### IOS

- **¿Por qué?** Observar no equivale a cambiar el equipo.
- **¿Para qué?** Para entender qué permite el modo actual.
- **¿Cómo?** Identifica el indicador de modo antes de interpretar las opciones disponibles.
- **¿Cuándo usarlo?** Al consultar un equipo de laboratorio.

### Administración

- **¿Por qué?** Un equipo puede administrarse aunque no enrute.
- **¿Para qué?** Para ubicar la interfaz lógica de gestión.
- **¿Cómo?** Distingue la SVI de gestión del gateway de los hosts.
- **¿Cuándo usarlo?** Al leer el esquema de un switch de capa 2.

### Protección de acceso

- **¿Por qué?** Ofuscar una clave guardada y proteger el tránsito son cosas distintas.
- **¿Para qué?** Para reconocer qué protege cada medida.
- **¿Cómo?** Pregunta si el dato está almacenado o viaja por la red.
- **¿Cuándo usarlo?** Cuando compares controles de una configuración.

### Encapsulación

- **¿Por qué?** La entrega local cambia al pasar a otro enlace.
- **¿Para qué?** Para separar tramo Ethernet y destino IP.
- **¿Cómo?** Dibuja la trama de entrada y la nueva trama de salida del router.
- **¿Cuándo usarlo?** Al seguir la comunicación entre dos LAN.

### Direccionamiento

- **¿Por qué?** La IP por sí sola no delimita la subred.
- **¿Para qué?** Para distinguir los bits de red de los de host.
- **¿Cómo?** Lee el prefijo como la cantidad inicial de bits de red.
- **¿Cuándo usarlo?** Al comparar dos direcciones IPv4.

### Diagnóstico

- **¿Por qué?** Un ping no comprueba que una aplicación funcione.
- **¿Para qué?** Para no confundir conectividad con disponibilidad del servicio.
- **¿Cómo?** Separa observaciones de conectividad de la comprobación de HTTP.
- **¿Cuándo usarlo?** Cuando un host responde, pero su sitio no.

| Tema | Pregunta de práctica | Cómo revisar tu respuesta |
| --- | --- | --- |
| Equipos | ¿Qué equipo elige una ruta entre dos LAN? | El router; el switch de capa 2 reenvía tramas en su ámbito. |
| Modelos | ¿Dónde revisarías una falla de señal? | Empieza por el medio y la capa física. |
| IOS | ¿Qué diferencia hay entre EXEC y la configuración global? | Observar y diagnosticar no es lo mismo que cambiar la configuración. |
| Administración | ¿Para qué sirve una SVI de gestión en un switch? | Da una interfaz lógica de administración; no implica que enrute. |
| Seguridad | ¿Ofuscar una clave de consola protege una sesión remota en tránsito? | No; son controles distintos. |
| Encapsulación | ¿Cambia la trama al pasar por un router? | Sí; prepara una trama para el enlace siguiente. |
| Direccionamiento | ¿Qué parte de una IP te indica el prefijo? | Los bits que identifican la red. |
| Diagnóstico | ¿Un ping correcto demuestra que HTTP funciona? | No. Comprueba el servicio de aplicación por separado. |

Para ampliar: [Guía general de redes](guia-general-de-redes.pdf).
