# PIU - Tutorial de Instalación

## Instalación de SQL Server con Docker

1. **Descargar la imagen de SQL Server:**
   Abre tu terminal y ejecuta el siguiente comando para descargar la imagen de SQL Server desde Docker Hub.

```
docker pull mcr.microsoft.com/mssql/server
```
Una vez descargada la imagen, ejecuta el siguiente comando para iniciar un contenedor de SQL Server. Asegúrate de configurar la contraseña para el usuario "sa".
```
docker run --name "sqlserver-local" -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SQL#1234" -p 1433:1433 -d mcr.microsoft.com/mssql/server
```
Verificar la instalación:
Puedes verificar que el contenedor está en ejecución usando el siguiente comando:
```
docker ps -a
```

# PIU - Tutorial de Instalación

## Instalación de SQL Server con Docker

1. **Descargar la imagen de SQL Server:**
   Abre tu terminal y ejecuta el siguiente comando para descargar la imagen de SQL Server desde Docker Hub.

   ```bash
   docker pull mcr.microsoft.com/mssql/server
Iniciar un contenedor de SQL Server:
Una vez descargada la imagen, ejecuta el siguiente comando para iniciar un contenedor de SQL Server. Asegúrate de configurar la contraseña para el usuario "sa".

bash
Copy code
docker run --name "sqlserver-local" -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SQL#1234" -p 1433:1433 -d mcr.microsoft.com/mssql/server
Verificar la instalación:
Puedes verificar que el contenedor está en ejecución usando el siguiente comando:

 ```
docker ps -a
 ```
Configuración de la Base de Datos PIU
Crear base de datos "piu":
Después de iniciar el contenedor, conecta tu herramienta de administración de SQL Server (por ejemplo, SQL Server Management Studio) y crea una nueva base de datos llamada "piu".

Ejecutar script Python para crear tablas y datos iniciales:
Utiliza el siguiente comando para ejecutar el script Python db.py que creará las tablas, establecerá relaciones y realizará la inserción inicial de datos.
```
python db.py
```

Uso de Scaffold para Entity Framework
Scaffolding inicial:
Si necesitas generar clases de modelo basadas en la estructura de la base de datos, utiliza el siguiente comando:

```
Scaffold-DbContext "SERVER=localhost,1433;DATABASE=piu;UID=sa;PWD=SQL#1234;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```
Actualizar Scaffold:
Si la estructura de la base de datos cambia y necesitas actualizar las clases de modelo, ejecuta el siguiente comando:

```
Scaffold-DbContext "SERVER=localhost,1433;DATABASE=piu;UID=sa;PWD=SQL#1234;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
```
Con estos pasos, has instalado SQL Server utilizando Docker y configurado la base de datos "piu" con el usuario "sa" y la contraseña "SQL#1234". Puedes ajustar la configuración según tus necesidades.
