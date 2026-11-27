# Anatomía visual de una red

[Fundamentos de Redes](../../README.md) · [Todas las actividades](../README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../../assets/readme-banners/en-palabras-simples.svg)

### Equipos, medios y alcance

- **¿Por qué?** Un equipo final y uno intermediario cumplen papeles diferentes.
- **¿Para qué?** Para leer un plano y reconocer qué está unido localmente.
- **¿Cómo?** Dibuja laptop → switch o punto de acceso → router → otra red; marca cable o Wi-Fi.
- **¿Cuándo usarlo?** Al inventariar una LAN o distinguirla de una WAN.

## Qué se trabajó

Dibuja un equipo, un switch y un router. Marca la conexión local y después señala dónde empieza otra red. Distingue LAN, WAN e Internet: la nube de Internet representa muchas redes interconectadas, no un dispositivo.

## Evidencia y alcance

**Material localizado:** Mapa conceptual original en XMind y dos exportaciones del mismo tema.

Se conserva una explicación y un esquema propio. La versión pública omite matrícula, referencias a redes personales y gráficos de origen no confirmado.

```mermaid
flowchart LR
  H["Host"] ---|"cable o Wi-Fi"| S["Switch o punto de acceso"]
  S --- R["Router"]
  R --- W["Otra red"]
```

## Archivo de respaldo o estudio

[Mapa de anatomía de una red (SVG)](../../tareas/01-anatomia-visual/mapa-anatomia-red.svg), una adaptación del mapa entregado por Kelvin. [Cuaderno de práctica](../../material-de-estudio/cuaderno-de-practica.pdf), lámina 1.

[Ver los archivos de esta actividad](../../tareas/README.md).
