// Selección de elementos del DOM
const imageUrlInput = document.getElementById('imageUrlInput');
const addImageBtn = document.getElementById('addImageBtn');
const deleteImageBtn = document.getElementById('deleteImageBtn');
const gallery = document.getElementById('gallery');
const imageCount = document.getElementById('imageCount');
const emptyState = document.getElementById('emptyState');

// Variable para guardar la imagen seleccionada
let selectedImage = null;

// URLs de imágenes por defecto (opcional)
const defaultImages = [
    'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=500',
    'https://images.unsplash.com/photo-1469474968028-56623f02e42e?w=500',
    'https://images.unsplash.com/photo-1501785888041-af3ef285b470?w=500'
];

// Inicializar la aplicación
function init() {
    // Cargar imágenes por defecto
    loadDefaultImages();
    
    // Event listeners
    addImageBtn.addEventListener('click', addImage);
    deleteImageBtn.addEventListener('click', deleteSelectedImage);
    
    // Permitir agregar imagen con Enter
    imageUrlInput.addEventListener('keydown', (e) => {
        if (e.key === 'Enter') {
            addImage();
        }
    });
    
    // Actualizar contador inicial
    updateImageCount();
}

// Cargar imágenes por defecto (opcional)
function loadDefaultImages() {
    defaultImages.forEach(url => {
        createImageElement(url);
    });
    updateImageCount();
}

// Función para agregar una imagen
function addImage() {
    const url = imageUrlInput.value.trim();
    
    // Validar que la URL no esté vacía
    if (!url) {
        alert('Por favor, ingresa una URL válida');
        return;
    }
    
    // Validar formato básico de URL
    if (!isValidUrl(url)) {
        alert('Por favor, ingresa una URL válida de imagen');
        return;
    }
    
    // Crear el elemento de imagen
    createImageElement(url);
    
    // Limpiar el input
    imageUrlInput.value = '';
    
    // Actualizar contador
    updateImageCount();
    
    // Focus en el input para agregar más imágenes
    imageUrlInput.focus();
}

// Validar URL
function isValidUrl(string) {
    try {
        new URL(string);
        return true;
    } catch (err) {
        return false;
    }
}

// Crear elemento de imagen en el DOM
function createImageElement(url) {
    // Crear contenedor de la imagen
    const galleryItem = document.createElement('div');
    galleryItem.className = 'gallery-item';
    
    // Crear elemento img
    const img = document.createElement('img');
    img.src = url;
    img.alt = 'Imagen de galería';
    
    // Manejar error de carga de imagen
    img.onerror = () => {
        galleryItem.remove();
        alert('No se pudo cargar la imagen. Verifica la URL.');
        updateImageCount();
    };
    
    // Event listener para seleccionar imagen
    galleryItem.addEventListener('click', () => {
        selectImage(galleryItem);
    });
    
    // Agregar imagen al contenedor
    galleryItem.appendChild(img);
    
    // Agregar contenedor a la galería
    gallery.appendChild(galleryItem);
    
    // Ocultar estado vacío
    emptyState.classList.remove('visible');
}

// Función para seleccionar una imagen
function selectImage(galleryItem) {
    // Si hay una imagen previamente seleccionada, deseleccionarla
    if (selectedImage) {
        selectedImage.classList.remove('selected');
    }
    
    // Si se selecciona la misma imagen, deseleccionarla
    if (selectedImage === galleryItem) {
        selectedImage = null;
        deleteImageBtn.disabled = true;
    } else {
        // Seleccionar la nueva imagen
        selectedImage = galleryItem;
        selectedImage.classList.add('selected');
        deleteImageBtn.disabled = false;
    }
}

// Función para eliminar la imagen seleccionada
function deleteSelectedImage() {
    if (!selectedImage) {
        return;
    }
    
    // Agregar animación de salida
    selectedImage.classList.add('removing');
    
    // Esperar a que termine la animación antes de eliminar
    setTimeout(() => {
        selectedImage.remove();
        selectedImage = null;
        deleteImageBtn.disabled = true;
        updateImageCount();
    }, 500);
}

// Actualizar contador de imágenes
function updateImageCount() {
    const count = gallery.querySelectorAll('.gallery-item').length;
    imageCount.textContent = count;
    
    // Mostrar/ocultar estado vacío
    if (count === 0) {
        emptyState.classList.add('visible');
    } else {
        emptyState.classList.remove('visible');
    }
}

// Event listener adicional para teclas del teclado
document.addEventListener('keydown', (e) => {
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
    }
});

// Inicializar la aplicación cuando el DOM esté listo
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init);
} else {
    init();
}