# Proyectos y contribuciones

🖥️ **08 · Sistemas Operativos** · [Inicio del portafolio](../../README.md) · [Índice de materia](../README.md)

[← Sistemas Operativos](../README.md) · [Competencias](../competencias/README.md) · [Evidencias](../evidencias/README.md)

Esta sección registra los trabajos colaborativos desarrollados en ISC-364 y distingue los resultados del equipo de la contribución personal documentada. Los resúmenes describen el alcance técnico y los límites de la evidencia disponible; no implican calificaciones ni atribuyen a una sola persona el trabajo grupal.

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

**Shell en Windows:** la exposición muestra cómo una instrucción escrita y un script permiten automatizar tareas; el aporte individual se detalla abajo. **NEXO Linux:** el equipo probó una modificación del núcleo de Linux en una máquina virtual. Piensa en el núcleo como la parte que conecta el software con los recursos del equipo. La ficha distingue lo comprobado en la demostración de sus límites.

## 1. Shell, intérpretes de mandatos y scripts en Windows

**Tipo:** exposición colaborativa  
**Tema:** historia del Shell, intérpretes de mandatos, comandos internos y externos, scripts y usos de la consola para crear, consultar y diagnosticar.

**Contribución de Kelvin:** introducción, explicación del concepto de Shell y cierre de la exposición, según la distribución de temas del equipo.

**Trabajo realizado:** preparación de una exposición con apoyos visuales y ejemplos sobre la evolución de los intérpretes, los mandatos, los objetos de Windows, los scripts y tareas de diagnóstico. La presentación relaciona la interfaz de comandos con la automatización y la revisión básica del sistema.

**Aprendizajes documentados:** explicar la función del Shell, organizar contenido técnico para una presentación grupal y conectar comandos con tareas concretas de administración.

## 2. NEXO Linux: kernel personalizado y llamada al sistema

**Tipo:** proyecto grupal con demostración técnica  
**Objetivo:** preparar un entorno Linux virtualizado, compilar e instalar un kernel personalizado y comprobar una modificación a nivel del kernel.

**Procedimiento documentado:**

1. Registrar el kernel activo, el espacio disponible y el estado de Secure Boot.
2. Preparar las herramientas de compilación y el código fuente de Linux 7.3.4.
3. Reutilizar la configuración del kernel del entorno y definir la versión local `7.3.4-nexo`.
4. Crear y registrar la llamada al sistema personalizada `nexo_saludo`.
5. Compilar el kernel y sus módulos, instalarlos y arrancar el sistema con la nueva versión.
6. Invocar la llamada con el identificador 473 y comprobar el mensaje del proyecto en el registro del kernel.

**Resultado que respalda la demostración:** el material registra el arranque del kernel `7.3.4-nexo` y la ejecución comprobada de la llamada personalizada. El último intento de reinicio quedó bloqueado por la sesión gráfica; ese detalle se conserva como límite de la demostración, sin ocultar las comprobaciones anteriores.

**Contribución de Kelvin:** preparación de la máquina virtual y revisión inicial del entorno, según el guion del equipo.

**Competencias reflejadas:** virtualización de Linux, preparación de dependencias, compilación de un kernel, registro de una syscall, verificación desde espacio de usuario y consulta de registros del sistema.

**Evidencias y publicación:** el proyecto cuenta con un trabajo escrito grupal y una demostración. Esta carpeta documenta el procedimiento y los resultados, pero no distribuye el informe grupal porque contiene datos identificativos de otros integrantes. La publicación conserva la contribución individual separada del resultado del equipo.

## Relación con las actividades

- [Virtualización](../actividades/tarea-02/README.md): preparación del entorno de trabajo.
- [Shell y scripts](../actividades/tarea-04/README.md): exposición y automatización en Windows.
- [Linux](../actividades/tarea-11/README.md): fundamentos, comandos y relación con el proyecto NEXO.
- [Competencias documentadas](../competencias/README.md): correspondencia entre actividades, proyectos y aprendizajes.

[Volver a Sistemas Operativos](../README.md)
