from sqlalchemy import create_engine, Column, Integer, String, DateTime, Boolean, ForeignKey
from sqlalchemy.orm import declarative_base, sessionmaker
import urllib
import pyodbc

# Parámetros de conexión
server = 'localhost'
user = 'sa'
password = 'SQL#1234'
port = '1433'
database = 'piu'

conn_str = (
    f'DRIVER={{ODBC Driver 17 for SQL Server}};'
    f'SERVER={server},{port};'
    f'DATABASE={database};'
    f'UID={user};'
    f'PWD={password};'
)
# Construir la URL de SQLAlchemy con autenticación de Windows
try:
    conn = pyodbc.connect(conn_str)
    cursor = conn.cursor()

    # Ejemplo: realizar una consulta
    cursor.execute('SELECT @@version AS version')
    row = cursor.fetchone()
    print(f'SQL Server version: {row.version}')

    # Cerrar la conexión
    conn.close()

except pyodbc.Error as ex:
    print(f'Error de conexión: {ex}')

Base = declarative_base()

class Rol(Base):
    __tablename__ = 'rol'
    id = Column(Integer, primary_key=True)
    nombre_rol = Column(String(length=25))

class Funcionalidad(Base):
    __tablename__ = 'funcionalidad'
    id = Column(Integer, primary_key=True)
    descripcion = Column(String(length=100))

class FuncionalidadRol(Base):
    __tablename__ = 'funcionalidad_rol'
    rol_id = Column(Integer, ForeignKey('rol.id'), primary_key=True)
    funcionalidad_id = Column(Integer, ForeignKey('funcionalidad.id'), primary_key=True)

class Campus(Base):
    __tablename__ = 'campus'
    id = Column(Integer, primary_key=True)
    nombre_campus = Column(String(length=30))

class Escuela(Base):
    __tablename__ = 'escuela'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=30))

class Carrera(Base):
    __tablename__ = 'carrera'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=30))
    escuela_id = Column(Integer, ForeignKey('escuela.id'))

class Asignatura(Base):
    __tablename__ = 'asignatura'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=30))
    carrera_id = Column(Integer, ForeignKey('carrera.id'))
    docente_id = Column(Integer, ForeignKey('docente.id'))

class Docente(Base):
    __tablename__ = 'docente'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=30))
    apellido_paterno = Column(String(length=20))
    apellido_materno = Column(String(length=20))
    cargo = Column(String(length=30))
    correo = Column(String(length=30))
    celular = Column(String(length=15))

class AsignaturaCarrera(Base):
    __tablename__ = 'asignatura_carrera'
    id = Column(Integer, primary_key=True)
    carrera_id = Column(Integer, ForeignKey('carrera.id'))
    asignatura_id = Column(Integer, ForeignKey('asignatura.id'))

class Jornada(Base):
    __tablename__ = 'jornada'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=20))

class Genero(Base):
    __tablename__ = 'genero'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=20))

class Estudiante(Base):
    __tablename__ = 'estudiante'
    id = Column(Integer, primary_key=True)
    rut = Column(String(length=20))
    nombre = Column(String(length=30))
    apellido_paterno = Column(String(length=20))
    apellido_materno = Column(String(length=20))
    fecha_nacimiento = Column(DateTime)
    correo = Column(String(length=50))
    celular = Column(String(length=20))
    ingreso_piu = Column(Integer, ForeignKey('anio.id'))
    egreso_piu = Column(Integer, ForeignKey('anio.id'))
    carrera_id = Column(Integer, ForeignKey('carrera.id'))
    campus_id = Column(Integer, ForeignKey('campus.id'))
    jornada_id = Column(Integer, ForeignKey('jornada.id'))
    genero_id = Column(Integer, ForeignKey('genero.id'))
    asignatura_id = Column(Integer, ForeignKey('asignatura.id'))
    foto = Column(String(length=255))

class Anio(Base):
    __tablename__ = 'anio'
    id = Column(Integer, primary_key=True)
    nombre = Column(Integer)

class Documento(Base):
    __tablename__ = 'documento'
    id = Column(Integer, primary_key=True)
    estudiante_id = Column(Integer, ForeignKey('estudiante.id'))
    tipo_archivo = Column(String(length=15))
    archivo_adjunto = Column(String(length=255))

class Sesion(Base):
    __tablename__ = 'sesion'
    id = Column(Integer, primary_key=True)
    estudiante_id = Column(Integer, ForeignKey('estudiante.id'))
    fecha = Column(DateTime)
    via_contacto = Column(String(length=20))
    objetivo = Column(String(length=100))
    asistio = Column(Boolean)

class DetalleSesion(Base):
    __tablename__ = 'detalle_sesion'
    id = Column(Integer, primary_key=True)
    sesion_id = Column(Integer, ForeignKey('sesion.id'))
    etapa = Column(String(length=10))
    acciones = Column(String(length=100))
    observaciones = Column(String(length=100))

class AgendaSesion(Base):
    __tablename__ = 'agenda_sesion'
    id = Column(Integer, primary_key=True)
    estudiante_id = Column(Integer, ForeignKey('estudiante.id'))
    fecha = Column(DateTime)


engine = create_engine(f'mssql+pyodbc:///?odbc_connect={urllib.parse.quote_plus(conn_str)}', echo=True)

Base.metadata.create_all(engine)