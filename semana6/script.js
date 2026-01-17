// Obtener referencias a los elementos del formulario
const form = document.getElementById('registroForm');
const nombre = document.getElementById('nombre');
const email = document.getElementById('email');
const password = document.getElementById('password');
const confirmPassword = document.getElementById('confirmPassword');
const edad = document.getElementById('edad');
const submitBtn = document.getElementById('submitBtn');
const strengthBar = document.getElementById('strengthBar');

// Objeto para rastrear el estado de validación de cada campo
const validations = {
    nombre: false,
    email: false,
    password: false,
    confirmPassword: false,
    edad: false
};

// Función para validar el nombre (mínimo nombre y apellido)
function validateNombre() {
    const value = nombre.value.trim();
    // Validar que tenga al menos dos palabras (nombre y apellido)
    const palabras = value.split(/\s+/).filter(word => word.length > 0);
    const isValid = palabras.length >= 2 && value.length >= 3;
    
    updateFieldStatus(nombre, 'nombre', isValid);
    validations.nombre = isValid;
    checkFormValidity();
}

// Función para validar el email
function validateEmail() {
    const value = email.value.trim();
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isValid = emailRegex.test(value);
    
    updateFieldStatus(email, 'email', isValid);
    validations.email = isValid;
    checkFormValidity();
}

// Función para validar la contraseña (letras mayúsculas, minúsculas, números y carácter especial)
function validatePassword() {
    const value = password.value;
    const hasMinLength = value.length >= 8;
    const hasNumber = /\d/.test(value);
    const hasSpecialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(value);
    const hasUpperCase = /[A-Z]/.test(value);
    const hasLowerCase = /[a-z]/.test(value);
    const isValid = hasMinLength && hasNumber && hasSpecialChar && hasUpperCase && hasLowerCase;
    
    // Calcular fuerza de la contraseña
    let strength = 0;
    if (value.length >= 8) strength++;
    if (hasNumber) strength++;
    if (hasSpecialChar) strength++;
    if (hasUpperCase) strength++;
    if (value.length >= 12) strength++;
    
    // Actualizar barra de fuerza
    strengthBar.className = 'password-strength-bar';
    if (strength <= 2) {
        strengthBar.classList.add('strength-weak');
    } else if (strength === 3 || strength === 4) {
        strengthBar.classList.add('strength-medium');
    } else if (strength >= 5) {
        strengthBar.classList.add('strength-strong');
    } else {
        strengthBar.style.width = '0';
    }
    
    updateFieldStatus(password, 'password', isValid);
    validations.password = isValid;
    
    // Revalidar confirmación si ya tiene valor
    if (confirmPassword.value) {
        validateConfirmPassword();
    }
    checkFormValidity();
}

// Función para validar la confirmación de contraseña
function validateConfirmPassword() {
    const isValid = confirmPassword.value === password.value && password.value !== '';
    
    updateFieldStatus(confirmPassword, 'confirmPassword', isValid);
    validations.confirmPassword = isValid;
    checkFormValidity();
}

// Función para validar la edad
function validateEdad() {
    const value = parseInt(edad.value);
    const isValid = !isNaN(value) && value >= 18;
    
    updateFieldStatus(edad, 'edad', isValid);
    validations.edad = isValid;
    checkFormValidity();
}

// Función para actualizar el estado visual de un campo
function updateFieldStatus(field, fieldName, isValid) {
    const errorMsg = document.getElementById(fieldName + 'Error');
    const successMsg = document.getElementById(fieldName + 'Success');
    
    // Si el campo está vacío, no mostrar ningún estado
    if (field.value === '') {
        field.classList.remove('valid', 'invalid');
        errorMsg.classList.remove('show');
        successMsg.classList.remove('show');
        return;
    }
    
    // Actualizar clases y mensajes según validación
    if (isValid) {
        field.classList.add('valid');
        field.classList.remove('invalid');
        errorMsg.classList.remove('show');
        successMsg.classList.add('show');
    } else {
        field.classList.add('invalid');
        field.classList.remove('valid');
        errorMsg.classList.add('show');
        successMsg.classList.remove('show');
    }
}

// Función para verificar si todo el formulario es válido
function checkFormValidity() {
    const allValid = Object.values(validations).every(v => v === true);
    submitBtn.disabled = !allValid;
}

// Event Listeners para validación en tiempo real
nombre.addEventListener('input', validateNombre);
email.addEventListener('input', validateEmail);
password.addEventListener('input', validatePassword);
confirmPassword.addEventListener('input', validateConfirmPassword);
edad.addEventListener('input', validateEdad);

// Event Listener para el envío del formulario
form.addEventListener('submit', function(e) {
    e.preventDefault();
    document.getElementById('successModal').classList.add('show');
});

// Event Listener para el botón de reinicio
document.getElementById('resetBtn').addEventListener('click', function() {
    // Resetear validaciones
    Object.keys(validations).forEach(key => validations[key] = false);
    submitBtn.disabled = true;
    strengthBar.style.width = '0';
    
    // Limpiar clases de validación
    document.querySelectorAll('input').forEach(input => {
        input.classList.remove('valid', 'invalid');
    });
    
    // Ocultar mensajes
    document.querySelectorAll('.error-message, .success-message').forEach(msg => {
        msg.classList.remove('show');
    });
});

// Función para cerrar el modal de éxito
function closeModal() {
    document.getElementById('successModal').classList.remove('show');
    form.reset();
    document.getElementById('resetBtn').click();
}