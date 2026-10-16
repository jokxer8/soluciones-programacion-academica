# Desarrollo: Flotilla de Aeronaves: Modelo Entidad-Relación

Diseñar un modelo que conserve la operación actual de una flotilla y sus cambios a lo largo del tiempo.

## Problema y solución

AERONAVE se relaciona con MODELO, FABRICANTE, TALLER, MANTENIMIENTO y AEROPUERTO. El diseño evita tratar el aeropuerto base y el estado como atributos que se sobrescriben: BASE y REGISTRO_ESTADO_AERONAVE conservan periodos de vigencia.

## Transformación relacional

Las relaciones se convierten en claves foráneas o tablas asociativas según su cardinalidad. El modelo con datos permite revisar casos concretos; la normalización hasta 3FN separa información reutilizable y el diccionario explicita tipos, claves y reglas. Las versiones previas permanecen en los originales para mostrar la evolución.

## Autoría

Trabajo del Grupo 3: Kelvin Del Castillo Torres, Gabriel Galasso González y Camila Michelle Vásquez. La evidencia se presenta como colaboración académica.

## Relación con la actividad

El modelo organiza aeronaves, modelos, fabricantes, talleres, mantenimientos y aeropuertos. Las entidades BASE y REGISTRO_ESTADO_AERONAVE guardan periodos de vigencia para consultar tanto la situación actual como la histórica.

Si una aeronave cambia de aeropuerto base, se registra un nuevo periodo de asignación. El historial permite responder dónde estaba basada antes del cambio.

## Alcance de la revisión

Consultar [VALIDACION.md](../VALIDACION.md) para distinguir el análisis documental y sintáctico de los resultados de ejecución. Las copias públicas conservan las soluciones y los créditos; los identificadores institucionales detectados en texto fueron retirados.
