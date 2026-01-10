// Selección de elementos del DOM
const imageUrlInput = document.getElementById('imageUrlInput');
const addImageBtn = document.getElementById('addImageBtn');
const deleteImageBtn = document.getElementById('deleteImageBtn');
const gallery = document.getElementById('gallery');
const imageCount = document.getElementById('imageCount');
const emptyState = document.getElementById('emptyState');

// Variable para guardar la imagen seleccionada
let selectedImage = null;

console.log('🚀 Script cargado correctamente');

// Función para validar URL
function isValidUrl(string) {
    try {
        const url = new URL(string);
        return url.protocol === 'http:' || url.protocol === 'https:';
    } catch (err) {
        return false;
    }
}

// Función para agregar una imagen
function addImage() {
    console.log('🔘 Botón "Agregar Imagen" presionado');
    
    const url = imageUrlInput.value.trim();
    console.log('📝 URL ingresada:', url);
    
    // Validar que la URL no esté vacía
    if (!url) {
        alert('❌ Por favor, ingresa una URL');
        imageUrlInput.focus();
        return;
    }
    
    // Validar formato básico de URL
    if (!isValidUrl(url)) {
        alert('❌ Por favor, ingresa una URL válida (debe comenzar con http:// o https://)');
        imageUrlInput.focus();
        return;
    }
    
    console.log('✅ URL válida, creando imagen...');
    
    // Crear el elemento de imagen
    createImageElement(url);
    
    // Limpiar el input
    imageUrlInput.value = '';
    
    // Actualizar contador
    updateImageCount();
    
    // Focus en el input para agregar más imágenes
    imageUrlInput.focus();
    
    console.log('✅ Imagen agregada exitosamente');
}

// Crear elemento de imagen en el DOM
function createImageElement(url) {
    console.log('📸 Creando elemento de imagen con URL:', url);
    
    // Crear contenedor de la imagen
    const galleryItem = document.createElement('div');
    galleryItem.className = 'gallery-item';
    
    // Crear elemento img
    const img = document.createElement('img');
    img.src = url;
    img.alt = 'Imagen de galería';
    
    console.log('🖼️ Elemento <img> creado con src:', img.src);
    
    // Manejar carga exitosa
    img.onload = function() {
        console.log('✅ Imagen cargada correctamente:', url);
    };
    
    // Manejar error de carga de imagen
    img.onerror = function() {
        console.error('❌ Error al cargar imagen:', url);
        galleryItem.remove();
        alert('❌ No se pudo cargar la imagen. Verifica que la URL sea correcta y la imagen sea accesible.');
        updateImageCount();
    };
    
    // Event listener para seleccionar imagen
    galleryItem.addEventListener('click', function() {
        selectImage(galleryItem);
    });
    
    // Agregar imagen al contenedor
    galleryItem.appendChild(img);
    
    // Agregar contenedor a la galería
    gallery.appendChild(galleryItem);
    
    console.log('📦 Elemento agregado al DOM. Total de imágenes:', gallery.children.length);
    
    // Ocultar estado vacío
    emptyState.classList.remove('visible');
}

// Función para seleccionar una imagen
function selectImage(galleryItem) {
    console.log('🖱️ Imagen clickeada');
    
    // Si hay una imagen previamente seleccionada, deseleccionarla
    if (selectedImage) {
        selectedImage.classList.remove('selected');
    }
    
    // Si se selecciona la misma imagen, deseleccionarla
    if (selectedImage === galleryItem) {
        selectedImage = null;
        deleteImageBtn.disabled = true;
        console.log('❌ Imagen deseleccionada');
    } else {
        // Seleccionar la nueva imagen
        selectedImage = galleryItem;
        selectedImage.classList.add('selected');
        deleteImageBtn.disabled = false;
        console.log('✅ Imagen seleccionada');
    }
}

// Función para eliminar la imagen seleccionada
function deleteSelectedImage() {
    if (!selectedImage) {
        console.log('⚠️ No hay imagen seleccionada para eliminar');
        return;
    }
    
    console.log('🗑️ Eliminando imagen seleccionada');
    
    // Agregar animación de salida
    selectedImage.classList.add('removing');
    
    // Esperar a que termine la animación antes de eliminar
    setTimeout(function() {
        selectedImage.remove();
        selectedImage = null;
        deleteImageBtn.disabled = true;
        updateImageCount();
        console.log('✅ Imagen eliminada');
    }, 500);
}

// Actualizar contador de imágenes
function updateImageCount() {
    const count = gallery.querySelectorAll('.gallery-item').length;
    imageCount.textContent = count;
    console.log('📊 Contador actualizado:', count, 'imágenes');
    
    // Mostrar/ocultar estado vacío
    if (count === 0) {
        emptyState.classList.add('visible');
    } else {
        emptyState.classList.remove('visible');
    }
}

// Event listeners
addImageBtn.addEventListener('click', addImage);
deleteImageBtn.addEventListener('click', deleteSelectedImage);

// Permitir agregar imagen con Enter
imageUrlInput.addEventListener('keydown', function(e) {
    if (e.key === 'Enter') {
        e.preventDefault();
        console.log('⌨️ Tecla Enter presionada');
        addImage();
    }
});

// Event listener adicional para teclas del teclado
document.addEventListener('keydown', function(e) {
    // Eliminar con tecla Delete o Backspace
    if ((e.key === 'Delete' || e.key === 'Backspace') && selectedImage) {
        // Prevenir que borre si está escribiendo en el input
        if (document.activeElement !== imageUrlInput) {
            e.preventDefault();
            deleteSelectedImage();
        }
    }
    
    // Escape para deseleccionar
    if (e.key === 'Escape' && selectedImage) {
        selectedImage.classList.remove('selected');
        selectedImage = null;
        deleteImageBtn.disabled = true;
        console.log('⌨️ Escape presionado - imagen deseleccionada');
    }
});

// Inicializar la aplicación
function init() {
    console.log('🎬 Inicializando galería...');
    updateImageCount();
    console.log('✅ Galería lista para usar');
}

// Inicializar cuando el DOM esté listo
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init);
} else {
    init();
}

console.log('✅ Script completamente cargado');