# PIU
# Instalación
Para instalar es necesario SQL Server, una vez instalado se debe configurar este, se debe crear una base de datos llamada "piu" para continuar con la instalación.

Luego se debe ejecutar el script en Python db.py para la creación de tablas, la relación entre estas y la insersión inicial de datos.
`python db.py`
# Scaffolding
`
Scaffold-DbContext "SERVER=localhost,1433;DATABASE=piu;UID=sa;PWD=SQL#1234;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
`
# Actualizar Scaffold
`
Scaffold-DbContext "SERVER=localhost,1433;DATABASE=piu;UID=sa;PWD=SQL#1234;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
`
