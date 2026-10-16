# Contraseñas — cierre de la tarea #8

La aplicación reemplaza el SHA256 rápido de la entrega académica por **PBKDF2-HMAC-SHA256**, con sal aleatoria de 16 bytes, resultado de 32 bytes y 600000 iteraciones por defecto. No se añaden dependencias: se utiliza `Rfc2898DeriveBytes` de .NET Framework 4.8.

## Formato y coste

```text
pbkdf2-sha256$v1$iteraciones$salBase64$claveDerivadaBase64
```

`HOTELRESERVAS_PASSWORD_ITERATIONS` permite un coste entre **600000 y 2000000**. Una configuración inválida se rechaza; no se reduce silenciosamente el coste. Cada registro conserva su algoritmo, versión, coste y sal. Una cuenta válida se actualiza al aumentar el coste configurado y no se degrada al reducirlo. Se rechazan versiones desconocidas, Base64 no canónico, tamaños incorrectos y costes fuera de los límites antes de ejecutar la derivación.

La comparación de claves derivadas recorre todos los bytes, sin detenerse en la primera diferencia. Los hashes no se cargan en el campo de contraseña ni se incluyen en la lista administrativa. Las claves nuevas conservan Unicode y espacios significativos; solo el nombre de usuario se recorta en el formulario de acceso.

## Instalación nueva

1. Ejecutar el esquema base en una instancia de práctica; el script base elimina y recrea HotelDB.
2. Ejecutar `HotelDB_Fase1_Migracion.sql` y, opcionalmente, `HotelDB_SeedData.sql` una sola vez.
3. Ejecutar [HotelDB_Fase2_Contrasenas.sql](../Base%20de%20Datos/HotelDB_Fase2_Contrasenas.sql).
4. Compilar y utilizar la nueva aplicación. La columna `Usuarios.Clave` admite 256 caracteres y el procedimiento de acceso entrega el registro para verificación criptográfica en C#.

## Migración de una base existente

Conservar una copia de seguridad y desplegar conjuntamente la aplicación y Fase2. **No volver a ejecutar el esquema base ni el seed** sobre una base existente. Fase2 se puede repetir y no modifica las contraseñas almacenadas.

El primer acceso correcto a una credencial SHA256 hexadecimal de 64 caracteres verifica el mecanismo anterior y deriva PBKDF2 a partir de la contraseña introducida. El acceso incorrecto o a una cuenta inactiva no cambia su credencial. Para esa migración se mantiene el recorte de espacios del login histórico; después se conserva la contraseña canónica antigua.

La actualización usa el identificador y una comparación binaria con la credencial leída. Si otra operación cambia la contraseña o desactiva la cuenta, no se sobrescribe ese cambio ni se abre la sesión con la lectura anterior. El rol y el estado se consultan de nuevo antes de iniciar sesión.

Las altas y los cambios de clave utilizan PBKDF2. Editar un usuario con la clave vacía conserva el valor existente; el formulario no carga ni vuelve a derivar un hash como si fuera una contraseña. No se admite texto plano como alternativa de autenticación.

No se puede recuperar una contraseña original a partir de su SHA256: las cuentas que no vuelvan a acceder permanecen heredadas hasta su restablecimiento por administración. No se inventa una conversión masiva de hashes. Para volver a una versión anterior se necesita una copia coherente de aplicación y base, no convertir PBKDF2 a SHA256.

## Pruebas y resultados

**79 pruebas correctas y 0 fallidas en SQL Server LocalDB 17.0.4025.3**, incluidas las 65 anteriores y 14 nuevas. [Resultados locales completos](evidencias/passwords-functional-results.tsv).

La ejecución de [GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38849154210) prueba la revisión `4cfeb99350658eeda821ef8d3a275daedf7cce63` en Windows y SQL Server LocalDB. Resultado remoto verificado: **79 correctas, 0 fallidas**. Las 14 comprobaciones nuevas también pasan en Actions.

Se verifican sal individual, clave incorrecta, vector independiente calculado con Python `hashlib`, Unicode/espacios, formatos corruptos, límites de coste, alta administrativa, edición sin cambiar clave, cambio por las dos rutas, ausencia de hashes en la lista, migración solo con acceso válido, cuenta inactiva, hash corrupto y aumento de coste sin degradación. Fase2 se ejecuta dos veces para comprobar que no altera credenciales al repetirse.

## Demostración y uso fuera del aula

Las cuentas publicadas siguen siendo **datos ficticios y conocidos**. El nuevo almacenamiento no convierte esas contraseñas públicas en secretos ni certifica un despliegue de producción. Fuera del aula se deben sustituir cuentas de demostración, aplicar política de contraseñas y restablecimiento, controlar intentos y utilizar permisos mínimos de Windows/SQL Server. La mejora #8 no añade MFA ni certifica concurrencia general, despliegue o revisión visual completa.

Referencias de diseño: [OWASP Password Storage Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html) y [Rfc2898DeriveBytes de Microsoft](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rfc2898derivebytes?view=netframework-4.8).

[Hotel](../README.md) · [Pruebas funcionales](PRUEBAS-FUNCIONALES.md) · [Tarea #8](https://github.com/jokxer8/soluciones-programacion-academica/issues/8)
