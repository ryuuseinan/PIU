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
    nombre = Column(String(length=25))
    activo = Column(Boolean, default=True)

class Funcionalidad(Base):
    __tablename__ = 'funcionalidad'
    id = Column(Integer, primary_key=True)
    descripcion = Column(String(length=100))

class FuncionalidadRol(Base):
    __tablename__ = 'funcionalidad_rol'
    rol_id = Column(Integer, ForeignKey('rol.id'), primary_key=True)
    funcionalidad_id = Column(Integer, ForeignKey('funcionalidad.id'), primary_key=True)

class Usuario(Base):
    __tablename__ = 'usuario'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(30), unique=True, nullable=False)
    correo = Column(String(30), unique=True, nullable=False)
    contrasena = Column(String(255), nullable=False)
    rol_id = Column(Integer, ForeignKey('rol.id'))
    activo = Column(Boolean, default=True)

class Persona(Base):
    __tablename__ = 'persona'
    id = Column(Integer, primary_key=True)
    usuario_id = Column(Integer, ForeignKey('usuario.id'))
    # Usar una base de datos en utf8mb4_general_ci para no tener problemas con el rut.
    rut = Column(String(12), unique=True, nullable=False) 
    nombre = Column(String(30), nullable=False)
    apellido_paterno = Column(String(30), nullable=False)
    apellido_materno = Column(String(30), nullable=False)
    celular = Column(String(12), unique=True, nullable=False)
    activo = Column(Boolean, default=True, nullable=False)

class Campus(Base):
    __tablename__ = 'campus'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=30))
    activo = Column(Boolean, default=True)

class Escuela(Base):
    __tablename__ = 'escuela'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=50))
    activo = Column(Boolean, default=True)

class Carrera(Base):
    __tablename__ = 'carrera'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=100))
    escuela_id = Column(Integer, ForeignKey('escuela.id'))
    activo = Column(Boolean, default=True)

class Asignatura(Base):
    __tablename__ = 'asignatura'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=30))
    carrera_id = Column(Integer, ForeignKey('carrera.id'))
    docente_id = Column(Integer, ForeignKey('docente.id'))
    activo = Column(Boolean, default=True)

class Docente(Base):
    __tablename__ = 'docente'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=30))
    apellido_paterno = Column(String(length=20))
    apellido_materno = Column(String(length=20))
    cargo = Column(String(length=30))
    correo = Column(String(length=30))
    celular = Column(String(length=15))
    activo = Column(Boolean, default=True)

class AsignaturaCarrera(Base):
    __tablename__ = 'asignatura_carrera'
    id = Column(Integer, primary_key=True)
    carrera_id = Column(Integer, ForeignKey('carrera.id'))
    asignatura_id = Column(Integer, ForeignKey('asignatura.id'))

class Jornada(Base):
    __tablename__ = 'jornada'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=20))
    activo = Column(Boolean, default=True)

class Genero(Base):
    __tablename__ = 'genero'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=20))
    activo = Column(Boolean, default=True)

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
    activo = Column(Boolean, default=True)

class Anio(Base):
    __tablename__ = 'anio'
    id = Column(Integer, primary_key=True)
    nombre = Column(Integer)
    activo = Column(Boolean, default=True)

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
Base.metadata.drop_all(engine)
Base.metadata.create_all(engine)

Session = sessionmaker(bind=engine)
session = Session()

# Función para agregar un campus si no existe
campus_nombres = ['Recreo', 'Rodelillo', 'Miraflores']

for nombre_campus in campus_nombres:
    campus_existente = session.query(Campus).filter_by(nombre=nombre_campus).first()
    if not campus_existente:
        nuevo_campus = Campus(nombre=nombre_campus)
        session.add(nuevo_campus)
        session.commit()
        print(f"Campus '{nombre_campus}' agregado.")
    else:
        print(f"Campus '{nombre_campus}' ya existe.")

# Función para agregar una jornada si no existe
jornada_nombres = ['Vespertino', 'Diurno']

for nombre_jornada in jornada_nombres:
    jornada_existente = session.query(Jornada).filter_by(nombre=nombre_jornada).first()

    if not jornada_existente:
        nueva_jornada = Jornada(nombre=nombre_jornada)
        session.add(nueva_jornada)
        session.commit()
        print(f"Jornada '{nombre_jornada}' agregado.")
    else:
        print(f"Jornada '{nombre_jornada}' ya existe.")

# Datos de escuelas y carreras
escuelas_carreras = {
    'Escuela de Arquitectura, Comunicaciones y Diseño': [
        'Animación Digital', 
        'Arquitectura', 
        'Cine y Comunicación Audiovisual', 
        'Diseño', 
        'Periodismo'
    ],
    'Escuela de Ciencias': [
        'Bioquímica'
    ],
    'Escuela de Ciencias Agrícolas y Veterinarias': [
        'Agronomía',
        'Ingeniería en Medio Ambiente y Recursos Naturales', 
        'Medicina Veterinaria'
    ],
    'Escuela de Ciencias de la Salud': [
        'Enfermería', 
        'Fonoaudiología', 
        'Kinesiología', 
        'Nutrición y Dietética', 
        'Obstetricia', 
        'Odontología',
        'Química y Farmacia', 
        'Tecnología Médica', 
        'Terapia Ocupacional'
    ],
    'Escuela de Ciencias Jurídicas y Sociales': [
        'Administración Pública', 
        'Licenciatura en Administración Pública', 
        'Derecho',
        'Psicología', 
        'Trabajo Social',
        'Licenciatura en Trabajo Social'
    ],
    'Escuela de Educación': [
        'Educación Parvularia', 
        'Entrenador Deportivo', 
        'Licenciatura en Educación',
        'Pedagogía en Educación Diferencial',
        'Pedagogía en Educación Física', 
        'Programa de Formación Pedagógica',
        'Psicopedagogía'
    ],
    'Escuela de Ingeniería y Negocios': [
        'Contador Auditor',
        'Ingeniería Civil en Minas', 
        'Ingeniería Civil Industrial', 
        'Ingeniería Civil Informática',
        'Ingeniería Comercial',
        'Ingeniería en Construcción', 
        'Ingeniería en Gestión de Negocios Internacionales',
        'Ingeniería en Logística',
        'Ingeniería en Prevención de Riesgos y Gestión Ambiental',
    ]
}

# Agregar escuelas y carreras a la base de datos
for nombre_escuela, carreras in escuelas_carreras.items():
    escuela_existente = session.query(Escuela).filter_by(nombre=nombre_escuela).first()

    if not escuela_existente:
        nueva_escuela = Escuela(nombre=nombre_escuela)
        session.add(nueva_escuela)
        session.commit()
        print(f"Escuela '{nombre_escuela}' agregada.")
    else:
        print(f"Escuela '{nombre_escuela}' ya existe.")

    # Obtener el ID de la escuela recién creada o existente
    id_escuela = session.query(Escuela.id).filter_by(nombre=nombre_escuela).first()[0]

    # Agregar las carreras asociadas a la escuela
    for nombre_carrera in carreras:
        carrera_existente = session.query(Carrera).filter_by(nombre=nombre_carrera).first()

        if not carrera_existente:
            nueva_carrera = Carrera(nombre=nombre_carrera, escuela_id=id_escuela)
            session.add(nueva_carrera)
            session.commit()
            print(f"Carrera '{nombre_carrera}' agregada.")
        else:
            print(f"Carrera '{nombre_carrera}' ya existe.")

# Cierra la sesión
session.close()