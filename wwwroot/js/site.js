// Validador de RUT
function formatRut(input) {
    // Obtener el valor actual del RUT sin puntos ni guión
    let rut = input.value.replace(/[.-]/g, '');

    // Verificar si el RUT tiene al menos un dígito
    if (rut.length > 0) {
        // Formatear el RUT con puntos y guión
        rut = rut.replace(/^(\d{1,2})(\d{3})(\d{3})(\w{1})$/, '$1.$2.$3-$4');
        input.value = rut;
    }
}
// Función para validar el formato del correo electrónico


// Función para mostrar un mensaje de error
function mostrarError(elemento, mensaje) {
    var errorSpan = document.getElementById(elemento + "Error");
    errorSpan.textContent = mensaje;
}

// Función para limpiar el mensaje de error
function limpiarError(elemento) {
    var errorSpan = document.getElementById(elemento + "Error");
    errorSpan.textContent = "";
}

// Obtener el input del correo personal
var inputCorreoPersonal = document.getElementById("correoPersonal");

// Agregar evento de cambio al input del correo personal
inputCorreoPersonal.addEventListener("input", function () {
    var correo = inputCorreoPersonal.value.trim();

    if (correo === "") {
        limpiarError("correoPersonal");
    } else {
        if (validarCorreoElectronico(correo)) {
            limpiarError("correoPersonal");
        } else {
            mostrarError("correoPersonal", "El formato del correo electrónico no es válido.");
        }
    }
});
