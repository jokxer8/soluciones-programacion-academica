# Parcial 2: direccionamiento y rutas

[Fundamentos de Redes](../../README.md) · [Todas las actividades](../README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../../assets/readme-banners/en-palabras-simples.svg)

### Prefijos y tipos de dirección

- **¿Por qué?** Que una IPv4 termine en cero no decide si identifica a una red.
- **¿Para qué?** Para separar la dirección de red de las que pueden usar los hosts.
- **¿Cómo?** Compara `10.0.2.0/24` con esa misma dirección dentro de `10.0.0.0/16`: la máscara cambia la interpretación.
- **¿Cuándo usarlo?** Siempre que clasifiques una dirección o asignes un rango.

### Rutas y validación

- **¿Por qué?** Un rango correcto en papel no demuestra comunicación.
- **¿Para qué?** Para separar el diseño de una verificación en simulación.
- **¿Cómo?** Traza la ruta de ida y retorno y distingue el cálculo de cualquier prueba efectivamente registrada.
- **¿Cuándo usarlo?** Al revisar una práctica con varias LAN.

## Qué se trabajó

En un ejemplo nuevo, 10.0.2.0/24 identifica la red, mientras 10.0.2.0 dentro de 10.0.0.0/16 es una dirección de host (ejemplo aritmético con direcciones privadas). Separa la validación de rangos en papel de una prueba de conectividad en el simulador.

## Evidencia y alcance

**Material localizado:** Simulador local, práctica resuelta de preparación, consigna y cuestionario de respuestas.

El PDF de preparación verifica el modelo de direccionamiento, no documenta pings reales. Existe un .pkt independiente en la carpeta, sin vínculo verificado con esa práctica. No se incluyen cuestionarios ni claves.

## Archivo de respaldo o estudio

[Guía de estudio](../../material-de-estudio/guia-de-estudio.pdf), láminas 5–7, y [cuaderno](../../material-de-estudio/cuaderno-de-practica.pdf), lámina 6. Son casos de repaso nuevos, sin contenido de banco de evaluación.

[Ver los archivos de esta actividad](../../parcial-2/README.md).

[Descargar el simulador de la entrega](../../parcial-2/segundo-parcial-rutas.pkt).
