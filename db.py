from operator import ge
from sqlalchemy import create_engine, Column, Integer, String, DateTime, Boolean, ForeignKey, SmallInteger
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
    nombre = Column(String(30), unique=True)
    correo = Column(String(30), unique=True)
    contrasena = Column(String(255))
    salt = Column(String(255))
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

class DocenteCarrera(Base):
	__tablename__ = 'docente_carrera'
	id = Column(Integer, primary_key=True)
	docente_id = Column(Integer, ForeignKey('docente.id'))
	carrera_id = Column(Integer, ForeignKey('carrera.id'))
	activo = Column(Boolean, default=True)

class Jornada(Base):
    __tablename__ = 'jornada'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=20))
    activo = Column(Boolean, default=True)

class Genero(Base):
    __tablename__ = 'genero'
    id = Column(Integer, primary_key=True)
    nombre = Column(String(length=50))
    activo = Column(Boolean, default=True)

class Estudiante(Base):
    __tablename__ = 'estudiante'
    id = Column(Integer, primary_key=True)
    rut = Column(String(length=20))
    nombre = Column(String(length=30))
    apellido_paterno = Column(String(length=20))
    apellido_materno = Column(String(length=20))
    fecha_nacimiento = Column(DateTime)
    correo_institucional = Column(String(length=50))
    correo_personal = Column(String(length=50))
    celular = Column(String(length=20))
    ingreso_piu = Column(SmallInteger)
    egreso_piu = Column(SmallInteger)
    carrera_id = Column(Integer, ForeignKey('carrera.id'))
    campus_id = Column(Integer, ForeignKey('campus.id'))
    jornada_id = Column(Integer, ForeignKey('jornada.id'))
    genero_id = Column(Integer, ForeignKey('genero.id'))
    foto = Column(String(length=255))
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

# Función para agregar un rol si no existe
rol_nombres = ['Administrador', 'Gestor', 'Lector']

for nombre_rol in rol_nombres:
    rol_existente = session.query(Rol).filter_by(nombre=nombre_rol).first()

    if not rol_existente:
        nueva_rol = Rol(nombre=nombre_rol)
        session.add(nueva_rol)
        session.commit()
        print(f"rol '{nombre_rol}' agregado.")
    else:
        print(f"rol '{nombre_rol}' ya existe.")

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

# Crear instancias de Docente
docentes_data = [
    {"nombre": "Claudio", "apellido_paterno": "Burgos", "apellido_materno": "Zamora"},
    {"nombre": "Claudio", "apellido_paterno": "Sánchez", "apellido_materno": "Toledo"},
    {"nombre": "Cristóbal", "apellido_paterno": "Tapia", "apellido_materno": "Almonacid"},
    {"nombre": "Drago", "apellido_paterno": "Radovic", "apellido_materno": "López"},
    {"nombre": "Felipe", "apellido_paterno": "Gómez", "apellido_materno": "Fernández"},
    {"nombre": "Hernando", "apellido_paterno": "González", "apellido_materno": "Muñoz"},
    {"nombre": "Luis", "apellido_paterno": "Baeza", "apellido_materno": "Salvo"},
    {"nombre": "Óscar", "apellido_paterno": "Soto", "apellido_materno": "Díaz"},
    {"nombre": "Rodolfo", "apellido_paterno": "Olguín", "apellido_materno": "Castillo"},
    {"nombre": "Rosa", "apellido_paterno": "Rodríguez", "apellido_materno": "Valenzuela"},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de contador auditor
for docente_info in docentes_data:
    docente_existente = session.query(Docente).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")

        # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()[0]

    # Asignar la carrera de contador auditor
    carrera_contador_auditor = session.query(Carrera).filter_by(nombre='Contador Auditor').first()
    if carrera_contador_auditor:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_contador_auditor.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Contador Auditor.")
    else:
        print("Error: Carrera de Contador Auditor no encontrada.")

# Crear instancias de Docente para Ingeniería Civil en Minas
docentes_ingenieria_minas_data = [
    {"nombre": "Adolfo", "apellido_paterno": "Balboa", "apellido_materno": "Monroy"},
    {"nombre": "Alejandro", "apellido_paterno": "Álvarez", "apellido_materno": "Toro"},
    {"nombre": "César", "apellido_paterno": "Olmos", "apellido_materno": "Maturana"},
    {"nombre": "Claudio", "apellido_paterno": "Corvalán", "apellido_materno": "Robert"},
    {"nombre": "Daniela", "apellido_paterno": "González", "apellido_materno": ""},
    {"nombre": "Guillermo", "apellido_paterno": "Olivares", "apellido_materno": "Quintanilla"},
    {"nombre": "Guillermo", "apellido_paterno": "Uribe", "apellido_materno": "Pérez"},
    {"nombre": "Luis", "apellido_paterno": "Chirino", "apellido_materno": "Gálvez"},
    {"nombre": "Luis", "apellido_paterno": "Lagno", "apellido_materno": "Bravo"},
    {"nombre": "Macarena", "apellido_paterno": "Ruiz", "apellido_materno": "Ugalde"},
    {"nombre": "Moisés", "apellido_paterno": "Álvarez", "apellido_materno": "Becerra"},
    {"nombre": "Sergio", "apellido_paterno": "Navarrete", "apellido_materno": "Letelier"},
    {"nombre": "Teresa", "apellido_paterno": "Cautivo", "apellido_materno": "Serrano"},
    {"nombre": "Yeimy", "apellido_paterno": "Vivar", "apellido_materno": "Lobos"},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de Ingeniería Civil en Minas
for docente_info in docentes_ingenieria_minas_data:
    docente_existente = session.query(Docente).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")

        # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()[0]

    # Asignar la carrera de Ingeniería Civil en Minas
    carrera_ingenieria_minas = session.query(Carrera).filter_by(nombre='Ingeniería Civil en Minas').first()
    if carrera_ingenieria_minas:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_ingenieria_minas.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Ingeniería Civil en Minas.")
    else:
        print("Error: Carrera de Ingeniería Civil en Minas no encontrada.")

# Crear instancias de Docente para Ingeniería Civil Industrial
docentes_ingenieria_industrial_data = [
    {"nombre": "David", "apellido_paterno": "Larrondo", "apellido_materno": "Narbona"},
    {"nombre": "Drago", "apellido_paterno": "Radovic", "apellido_materno": "López"},
    {"nombre": "Eduardo", "apellido_paterno": "Jones", "apellido_materno": "Chávez"},
    {"nombre": "Elías", "apellido_paterno": "Bracho", "apellido_materno": "Cordero"},
    {"nombre": "Gloria", "apellido_paterno": "Elgueta", "apellido_materno": "Villegas"},
    {"nombre": "Gloria", "apellido_paterno": "Joya", "apellido_materno": "García"},
    {"nombre": "Iván", "apellido_paterno": "Ayala", "apellido_materno": "Ayala"},
    {"nombre": "Jenny", "apellido_paterno": "Márquez", "apellido_materno": "Astorga"},
    {"nombre": "Katherine", "apellido_paterno": "López", "apellido_materno": "Arias"},
    {"nombre": "Nerio", "apellido_paterno": "Villasmil", "apellido_materno": "Pirela"},
    {"nombre": "Rodrigo", "apellido_paterno": "Martínez", "apellido_materno": "Campos"},
    {"nombre": "Rodrigo", "apellido_paterno": "Silva", "apellido_materno": "Haun"},
    {"nombre": "Víctor", "apellido_paterno": "López", "apellido_materno": "Casanova"},
    {"nombre": "Ximena", "apellido_paterno": "Petit-Breuilh", "apellido_materno": ""},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de Ingeniería Civil Industrial
for docente_info in docentes_ingenieria_industrial_data:
    docente_existente = session.query(Docente).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")

        # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()[0]

    # Asignar la carrera de Ingeniería Civil Industrial
    carrera_ingenieria_industrial = session.query(Carrera).filter_by(nombre='Ingeniería Civil Industrial').first()
    if carrera_ingenieria_industrial:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_ingenieria_industrial.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Ingeniería Civil Industrial.")
    else:
        print("Error: Carrera de Ingeniería Civil Industrial no encontrada.")

# Crear instancias de Docente para Ingeniería Civil Informática
docentes_ingenieria_informatica_data = [
    {"nombre": "Alejandro", "apellido_paterno": "Pares", "apellido_materno": "Villarroel"},
    {"nombre": "Claudia", "apellido_paterno": "Jiménez", "apellido_materno": "Quintana"},
    {"nombre": "David", "apellido_paterno": "Larrondo", "apellido_materno": "Narbona"},
    {"nombre": "Eduardo", "apellido_paterno": "Jones", "apellido_materno": "Chávez"},
    {"nombre": "Gonzalo", "apellido_paterno": "Cifuentes", "apellido_materno": "Neira"},
    {"nombre": "Graciela", "apellido_paterno": "Mardones", "apellido_materno": "Vera"},
    {"nombre": "José", "apellido_paterno": "Ruiz", "apellido_materno": "Cornejo"},
    {"nombre": "Lorena", "apellido_paterno": "Pérez", "apellido_materno": "Villegas"},
    {"nombre": "Manuel", "apellido_paterno": "García", "apellido_materno": "Hernández"},
    {"nombre": "Pablo", "apellido_paterno": "Ormeño", "apellido_materno": "Arriagada"},
    {"nombre": "Roberto", "apellido_paterno": "Loayza", "apellido_materno": "Alegría"},
    {"nombre": "Sergio", "apellido_paterno": "Fuentes", "apellido_materno": "León"},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de Ingeniería Civil Informática
for docente_info in docentes_ingenieria_informatica_data:
    docente_existente = session.query(Docente).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")

        # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()[0]

    # Asignar la carrera de Ingeniería Civil Informática
    carrera_ingenieria_informatica = session.query(Carrera).filter_by(nombre='Ingeniería Civil Informática').first()
    if carrera_ingenieria_informatica:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_ingenieria_informatica.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Ingeniería Civil Informática.")
    else:
        print("Error: Carrera de Ingeniería Civil Informática no encontrada.")

# Crear instancias de Docente para Ingeniería Comercial
docentes_ingenieria_comercial_data = [
    {"nombre": "Betsabe", "apellido_paterno": "Arellano", "apellido_materno": "Araya"},
    {"nombre": "Drago", "apellido_paterno": "Radovic", "apellido_materno": "López"},
    {"nombre": "Eric", "apellido_paterno": "Salinas", "apellido_materno": "Mayne"},
    {"nombre": "Galo", "apellido_paterno": "Herrera", "apellido_materno": "Baquedano"},
    {"nombre": "Gerardo", "apellido_paterno": "Castillejo", "apellido_materno": ""},
    {"nombre": "Hernando", "apellido_paterno": "González", "apellido_materno": "Muñoz"},
    {"nombre": "Katherine", "apellido_paterno": "López", "apellido_materno": "Arias"},
    {"nombre": "Marcelo", "apellido_paterno": "León", "apellido_materno": "Vargas"},
    {"nombre": "Michael", "apellido_paterno": "Ulriksen", "apellido_materno": "Gutiérrez"},
    {"nombre": "Óscar", "apellido_paterno": "Soto", "apellido_materno": "Díaz"},
    {"nombre": "Roberto", "apellido_paterno": "Paiva", "apellido_materno": ""},
    {"nombre": "Rodolfo", "apellido_paterno": "Olguín", "apellido_materno": "Castillo"},
    {"nombre": "Rosa", "apellido_paterno": "Rodríguez", "apellido_materno": "Valenzuela"},
    {"nombre": "Tania", "apellido_paterno": "Zumaeta", "apellido_materno": "Quiroz"},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de Ingeniería Comercial
for docente_info in docentes_ingenieria_comercial_data:
    docente_existente = session.query(Docente).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")

    # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()[0]

    # Asignar la carrera de Ingeniería Comercial
    carrera_ingenieria_comercial = session.query(Carrera).filter_by(nombre='Ingeniería Comercial').first()
    if carrera_ingenieria_comercial:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_ingenieria_comercial.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Ingeniería Comercial.")
    else:
        print("Error: Carrera de Ingeniería Comercial no encontrada.")

# Crear instancias de Docente para Ingeniería en Construcción
docentes_ingenieria_construccion_data = [
    {"nombre": "Adolfo", "apellido_paterno": "Balboa", "apellido_materno": "Monroy"},
    {"nombre": "Antonio", "apellido_paterno": "Maltrana", "apellido_materno": "Fuenzalida"},
    {"nombre": "Billy", "apellido_paterno": "Vílchez", "apellido_materno": "Branez"},
    {"nombre": "Edison", "apellido_paterno": "Paz", "apellido_materno": "Aguirre"},
    {"nombre": "Erick", "apellido_paterno": "Reiser", "apellido_materno": "Sgombich"},
    {"nombre": "Esteban", "apellido_paterno": "González", "apellido_materno": "Rauter"},
    {"nombre": "Gerardo", "apellido_paterno": "Soto", "apellido_materno": "Díaz"},
    {"nombre": "Guillermo", "apellido_paterno": "Brante", "apellido_materno": "Lara"},
    {"nombre": "Isaac", "apellido_paterno": "Flores", "apellido_materno": "Gutiérrez"},
    {"nombre": "Javiera", "apellido_paterno": "Pérez", "apellido_materno": "Clavería"},
    {"nombre": "Juan Carlos", "apellido_paterno": "Páez", "apellido_materno": "Olivares"},
    {"nombre": "Livio", "apellido_paterno": "Garrido", "apellido_materno": "Carvajal"},
    {"nombre": "Luis", "apellido_paterno": "Fredes", "apellido_materno": "Barrera"},
    {"nombre": "Manuel", "apellido_paterno": "Muñoz", "apellido_materno": "Guzmán"},
    {"nombre": "Marcelo", "apellido_paterno": "Meneses", "apellido_materno": "Aravena"},
    {"nombre": "Miguel", "apellido_paterno": "Vera", "apellido_materno": "Sánchez"},
    {"nombre": "Patricio", "apellido_paterno": "Cáceres", "apellido_materno": "Cifuentes"},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de Ingeniería en Construcción
for docente_info in docentes_ingenieria_construccion_data:
    docente_existente = session.query(Docente).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")
     
    # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]).first()[0]

    # Asignar la carrera de Ingeniería en Construcción
    carrera_ingenieria_construccion = session.query(Carrera).filter_by(nombre='Ingeniería en Construcción').first()
    if carrera_ingenieria_construccion:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_ingenieria_construccion.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Ingeniería en Construcción.")
    else:
        print("Error: Carrera de Ingeniería en Construcción no encontrada.")

# Crear instancias de Docente para Ingeniería en Gestión de Negocios Internacionales
docentes_negocios_internacionales_data = [
    {"nombre": "David", "apellido_paterno": "Baeza", "apellido_materno": "Castro"},
    {"nombre": "Drago", "apellido_paterno": "Radovic", "apellido_materno": "López"},
    {"nombre": "Katherine", "apellido_paterno": "López", "apellido_materno": "Arias"},
    {"nombre": "Rodolfo", "apellido_paterno": "Olguín", "apellido_materno": "Castillo"},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de Ingeniería en Gestión de Negocios Internacionales
for docente_info in docentes_negocios_internacionales_data:
    docente_existente = session.query(Docente).filter_by(
        nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]
    ).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")

    # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(
        nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]
    ).first()[0]

    # Asignar la carrera de Ingeniería en Gestión de Negocios Internacionales
    carrera_negocios_internacionales = session.query(Carrera).filter_by(nombre='Ingeniería en Gestión de Negocios Internacionales').first()
    if carrera_negocios_internacionales:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_negocios_internacionales.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Ingeniería en Gestión de Negocios Internacionales.")
    else:
        print("Error: Carrera de Ingeniería en Gestión de Negocios Internacionales no encontrada.")

# Crear instancias de Docente para Ingeniería en Prevención de Riesgos y Gestión Ambiental
docentes_prevencion_riesgos_data = [
    {"nombre": "Boris Gary", "apellido_paterno": "Zambra", "apellido_materno": ""},
    {"nombre": "Carlos", "apellido_paterno": "Núñez", "apellido_materno": "Uribe"},
    {"nombre": "Claudio", "apellido_paterno": "Salinas", "apellido_materno": "Romero"},
    {"nombre": "Gabriela", "apellido_paterno": "Urrejola", "apellido_materno": "Contreras"},
    {"nombre": "Gonzalo", "apellido_paterno": "Cordero", "apellido_materno": "Pizarro"},
    {"nombre": "Héctor", "apellido_paterno": "Silva", "apellido_materno": "Bobadilla"},
    {"nombre": "Jenny", "apellido_paterno": "Márquez", "apellido_materno": "Astorga"},
    {"nombre": "Jonathan", "apellido_paterno": "Pizarro", "apellido_materno": "Vidal"},
    {"nombre": "Nerio", "apellido_paterno": "Villasmil", "apellido_materno": "Pirela"},
    {"nombre": "Pilar", "apellido_paterno": "Durán", "apellido_materno": ""},
    {"nombre": "Rodrigo", "apellido_paterno": "Silva", "apellido_materno": "Haun"},
    {"nombre": "Rosa", "apellido_paterno": "Gamboa", "apellido_materno": "Mardones"},
    {"nombre": "Sandra", "apellido_paterno": "Sepúlveda", "apellido_materno": "Tello"},
    {"nombre": "Wladimir", "apellido_paterno": "Orellana", "apellido_materno": "Peña"},
]

# Agregar instancias de Docente a la sesión y asignar la carrera de Ingeniería en Prevención de Riesgos y Gestión Ambiental
for docente_info in docentes_prevencion_riesgos_data:
    docente_existente = session.query(Docente).filter_by(
        nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]
    ).first()

    if not docente_existente:
        nuevo_docente = Docente(**docente_info)
        session.add(nuevo_docente)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' agregado.")

        # Obtener el ID del docente recién creado
    id_docente = session.query(Docente.id).filter_by(
        nombre=docente_info["nombre"], apellido_paterno=docente_info["apellido_paterno"], apellido_materno=docente_info["apellido_materno"]
    ).first()[0]

    # Asignar la carrera de Ingeniería en Prevención de Riesgos y Gestión Ambiental
    carrera_prevencion_riesgos = session.query(Carrera).filter_by(nombre='Ingeniería en Prevención de Riesgos y Gestión Ambiental').first()
    if carrera_prevencion_riesgos:
        nuevo_docente_carrera = DocenteCarrera(docente_id=id_docente, carrera_id=carrera_prevencion_riesgos.id)
        session.add(nuevo_docente_carrera)
        session.commit()
        print(f"Docente '{docente_info['nombre']} {docente_info['apellido_paterno']}' asignado a la carrera de Ingeniería en Prevención de Riesgos y Gestión Ambiental.")
    else:
        print("Error: Carrera de Ingeniería en Prevención de Riesgos y Gestión Ambiental no encontrada.")
        
# Crear instancias de Estudiante con datos ficticios
estudiantes_data = [
    {"nombre": "Juan", "apellido_paterno": "Pérez", "apellido_materno": "Gómez", "foto": "img/Estudiante/FotoPerfil/1.jpg", 
     "fecha_nacimiento": "2001-01-20 00:00:00.000", "correo_personal": "juanito.perez89@gmail.com", "rut": "20.390.281-K",
     "celular": "+56 9 4923 2513", "ingreso_piu": "2020", "carrera_id": "1", "campus_id": "1", "jornada_id": "1"},
    {"nombre": "Pedro", "apellido_paterno": "Martínez", "apellido_materno": "Rojas", "foto": "img/Estudiante/FotoPerfil/3.jpg",
     "fecha_nacimiento": "2002-05-10 00:00:00.000", "correo_personal": "pedrito2002@hotmail.com", "rut": "21.456.789-3",
     "celular": "+56 9 5432 1234", "ingreso_piu": "2021", "carrera_id": "15", "campus_id": "2", "jornada_id": "2"},
    {"nombre": "Ana", "apellido_paterno": "Fernández", "apellido_materno": "Díaz", "foto": "img/Estudiante/FotoPerfil/4.jpg",
     "fecha_nacimiento": "2003-09-25 00:00:00.000", "correo_personal": "anafernandez03@gmail.com", "rut": "22.987.654-1",
     "celular": "+56 9 6789 5678", "ingreso_piu": "2019", "carrera_id": "8", "campus_id": "3", "jornada_id": "1"},
    {"nombre": "Carlos", "apellido_paterno": "Sánchez", "apellido_materno": "Alvarez", "foto": "img/Estudiante/FotoPerfil/5.jpg",
     "fecha_nacimiento": "2001-12-05 00:00:00.000", "correo_personal": "carlos.sanchez01@yahoo.com", "rut": "18.345.678-4",
     "celular": "+56 9 7654 4321", "ingreso_piu": "2018", "carrera_id": "22", "campus_id": "1", "jornada_id": "2"},
    {"nombre": "Laura", "apellido_paterno": "Ramírez", "apellido_materno": "Vargas", "foto": "img/Estudiante/FotoPerfil/6.jpg",
     "fecha_nacimiento": "2002-07-30 00:00:00.000", "correo_personal": "laurita_vargas@hotmail.com", "rut": "23.234.567-5",
     "celular": "+56 9 8765 4321", "ingreso_piu": "2022", "carrera_id": "35", "campus_id": "3", "jornada_id": "1"},
    {"nombre": "José", "apellido_paterno": "López", "apellido_materno": "Santana", "foto": "img/Estudiante/FotoPerfil/7.jpg",
     "fecha_nacimiento": "2001-02-18 00:00:00.000", "correo_personal": "jose.lopez01@gmail.com", "rut": "20.111.222-9",
     "celular": "+56 9 9876 5432", "ingreso_piu": "2023", "carrera_id": "19", "campus_id": "2", "jornada_id": "1"},
    {"nombre": "Carmen", "apellido_paterno": "García", "apellido_materno": "Fernández", "foto": "img/Estudiante/FotoPerfil/8.jpg",
     "fecha_nacimiento": "2000-11-12 00:00:00.000", "correo_personal": "carmengf2000@gmail.com", "rut": "19.876.543-1",
     "celular": "+56 9 1234 5678", "ingreso_piu": "2017", "carrera_id": "12", "campus_id": "1", "jornada_id": "2"},
    {"nombre": "Antonio", "apellido_paterno": "Dominguez", "apellido_materno": "Pérez", "foto": "img/Estudiante/FotoPerfil/9.jpg",
     "fecha_nacimiento": "2003-04-03 00:00:00.000", "correo_personal": "adominguez03@hotmail.com", "rut": "22.333.444-5",
     "celular": "+56 9 2345 6789", "ingreso_piu": "2020", "carrera_id": "25", "campus_id": "2", "jornada_id": "1"},
    {"nombre": "Isabel", "apellido_paterno": "Ortega", "apellido_materno": "Martínez", "foto": "img/Estudiante/FotoPerfil/10.jpg",
     "fecha_nacimiento": "2002-08-28 00:00:00.000", "correo_personal": "isa.ortega02@yahoo.com", "rut": "20.000.111-K",
     "celular": "+56 9 3456 7890", "ingreso_piu": "2016", "carrera_id": "30", "campus_id": "3", "jornada_id": "2"}
]
# Agregar instancias de Estudiante a la sesión
for estudiante_info in estudiantes_data:
    nuevo_estudiante = Estudiante(**estudiante_info)
    session.add(nuevo_estudiante)
    session.commit()
    print(f"Estudiante '{estudiante_info['nombre']} {estudiante_info['apellido_paterno']}' agregado.")

# Función para agregar una identidad de género si no existe
generos = ['Masculino', 
            'Femenino', 
            'No binario', 
            'Agénero',
            'Bigénero',
            'Pangénero',
            'Género fluido',
            'Trigénero',
            'Cisgénero',
            'Intergénero',
            'Poligénero',
            'Intersexual',
            'Transgénero',
            'Transexual',
            'Persona de sexo no ajustado o non-conforming',
            'Neutrois',
            'Berdache o Dos espíritus']

for genero in generos:
    rol_existente = session.query(Genero).filter_by(nombre=genero).first()

    if not rol_existente:
        nuevo_genero = Genero(nombre=genero)
        session.add(nuevo_genero)
        session.commit()
        print(f"Género '{genero}' agregado.")
    else:
        print(f"Género '{genero}' ya existe.")
        
# Cierra la sesión
session.close()