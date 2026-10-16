# Desarrollo — Sistema de Avisos con Archivo y Serialización - POO

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

Esta guía reúne el análisis técnico ya documentado en la actividad. El README conserva el enunciado, los ejemplos y las explicaciones originales.

## Objetivo

Aplicar programación orientada a objetos junto con manejo de archivos y serialización de datos.

## Conceptos aplicados

- Clases
- Objetos
- Constructores
- Encapsulación
- Listas
- Menú interactivo
- Manejo de archivos
- Serialización JSON
- Deserialización JSON
- Validación de datos

## Estructura del programa

El programa contiene las siguientes clases:

### Clase `Aviso`

Representa un aviso individual.

Contiene:

- Título
- Mensaje
- Fecha de creación

### Clase `GestorAvisos`

Administra la lista de avisos registrados.

Contiene métodos para:

- Agregar avisos
- Mostrar avisos
- Guardar avisos en un archivo de texto
- Serializar avisos en un archivo JSON
- Deserializar avisos desde un archivo JSON

## Análisis

Este ejercicio permite comprender cómo una aplicación puede organizar información mediante objetos y conservar los datos usando archivos.

La clase `Aviso` representa la información principal, mientras que la clase `GestorAvisos` se encarga de administrar la colección de avisos y las operaciones de almacenamiento.
