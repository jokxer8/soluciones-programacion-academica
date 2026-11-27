# Conmutación Ethernet

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Aprendizaje MAC

- **¿Por qué?** Al ver tráfico, el switch descubre de qué puerto viene cada origen.
- **¿Para qué?** Para enviar después al puerto que corresponde a un destino conocido.
- **¿Cómo?** Compara la tabla MAC antes y después de que A y B transmitan en la maqueta.
- **¿Cuándo usarlo?** Al revisar el comportamiento de una LAN Ethernet.

### Destino desconocido

- **¿Por qué?** Al principio la tabla puede no contener la MAC de destino.
- **¿Para qué?** Para entender por qué la primera trama puede salir por varios puertos de la VLAN.
- **¿Cómo?** Dibuja el envío inicial por puertos de esa VLAN y contrástalo con el envío a un puerto aprendido.
- **¿Cuándo usarlo?** Cuando observes diferencias entre el primer intercambio y los siguientes.

## Ejercicio

Conecta A y B a un switch aislado y compara la tabla MAC antes y después de que ambos transmitan.

## Comprobación

Cuando el destino se aprende, el switch normalmente reenvía por el puerto correspondiente, no por todos.

[Volver a presentaciones](README.md). Material didáctico redactado para el portafolio. No contiene las diapositivas de Cisco.
