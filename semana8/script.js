// Datos ficticios para la tabla de productos (actualizados para tienda tecnológica)
const productos = [
    { id: 1, nombre: "Laptop Gaming RTX 4080", categoria: "Computadoras", precio: "$2,499", disponible: "En stock", valoracion: "★★★★★" },
    { id: 2, nombre: "iPhone 15 Pro Max 1TB", categoria: "Celulares", precio: "$1,599", disponible: "En stock", valoracion: "★★★★★" },
    { id: 3, nombre: "Silla Gamer Pro Elite", categoria: "Mobiliario", precio: "$449", disponible: "Últimas unidades", valoracion: "★★★★☆" },
    { id: 4, nombre: "Mouse RGB 16,000 DPI", categoria: "Periféricos", precio: "$89", disponible: "En stock", valoracion: "★★★★☆" },
    { id: 5, nombre: "Teclado Mecánico RGB", categoria: "Periféricos", precio: "$129", disponible: "En stock", valoracion: "★★★★★" },
    { id: 6, nombre: "Tarjeta Gráfica RTX 4090", categoria: "Componentes", precio: "$1,799", disponible: "Agotado", valoracion: "★★★★★" },
    { id: 7, nombre: "Auriculares 7.1 Surround", categoria: "Audio", precio: "$199", disponible: "En stock", valoracion: "★★★★☆" },
    { id: 8, nombre: "Tablet Samsung S9 Ultra", categoria: "Tablets", precio: "$999", disponible: "En stock", valoracion: "★★★★☆" },
    { id: 9, nombre: "Cámara Web 4K Pro", categoria: "Video", precio: "$149", disponible: "En stock", valoracion: "★★★☆☆" },
    { id: 10, nombre: "Monitor Curvo 49\"", categoria: "Monitores", precio: "$899", disponible: "En stock", valoracion: "★★★★★" },
    { id: 11, nombre: "MacBook Pro M3 Max", categoria: "Computadoras", precio: "$3,299", disponible: "En stock", valoracion: "★★★★★" },
    { id: 12, nombre: "Smartwatch Ultra", categoria: "Wearables", precio: "$399", disponible: "Últimas unidades", valoracion: "★★★★☆" }
];

// Función para crear efecto de partículas Dragon Ball
function crearParticulas() {
    const particulasContainer = document.createElement('div');
    particulasContainer.className = 'particulas';
    document.body.appendChild(particulasContainer);
    
    const colores = [
        'var(--dbz-azul-electrico)',
        'var(--dbz-rosa)',
        'var(--dbz-oro-ssj)',
        'var(--dbz-azul-cielo)',
        'var(--dbz-naranja-fuego)'
    ];
    
    // Crear 40 partículas
    for (let i = 0; i < 40; i++) {
        const particula = document.createElement('div');
        particula.className = 'particula';
        
        // Tamaño aleatorio entre 2px y 8px
        const tamano = Math.random() * 6 + 2;
        particula.style.width = `${tamano}px`;
        particula.style.height = `${tamano}px`;
        
        // Posición aleatoria
        particula.style.left = `${Math.random() * 100}vw`;
        
        // Color aleatorio
        particula.style.background = colores[Math.floor(Math.random() * colores.length)];
        
        // Duración y delay de animación aleatorios
        const duracion = Math.random() * 20 + 10;
        const delay = Math.random() * 5;
        particula.style.animationDuration = `${duracion}s`;
        particula.style.animationDelay = `${delay}s`;
        
        // Opacidad aleatoria
        particula.style.opacity = Math.random() * 0.5 + 0.2;
        
        particulasContainer.appendChild(particula);
    }
}

// Función para cargar los productos en la tabla
function cargarProductos() {
    const tbody = document.getElementById('tablaProductos');
    tbody.innerHTML = ''; // Limpiar tabla
    
    productos.forEach(producto => {
        const fila = document.createElement('tr');
        
        // Determinar clase según disponibilidad
        if (producto.disponible === "Agotado") {
            fila.classList.add('table-danger');
        } else if (producto.disponible === "Últimas unidades") {
            fila.classList.add('table-warning');
        }
        
        // Añadir efecto hover dinámico
        fila.style.cursor = 'pointer';
        fila.addEventListener('click', function() {
            mostrarDetalleProducto(producto);
        });
        
        fila.innerHTML = `
            <th scope="row">${producto.id}</th>
            <td><strong>${producto.nombre}</strong></td>
            <td><span class="badge bg-secondary">${producto.categoria}</span></td>
            <td><strong class="text-primary">${producto.precio}</strong></td>
            <td>
                <span class="badge ${producto.disponible === 'En stock' ? 'bg-success' : producto.disponible === 'Agotado' ? 'bg-danger' : 'bg-warning'}">
                    ${producto.disponible === 'En stock' ? '✅ ' : producto.disponible === 'Agotado' ? '❌ ' : '⚠️ '}
                    ${producto.disponible}
                </span>
            </td>
            <td><span class="text-warning">${producto.valoracion}</span></td>
        `;
        
        tbody.appendChild(fila);
    });
}

// Función para mostrar detalle del producto
function mostrarDetalleProducto(producto) {
    const modalHTML = `
        <div class="modal fade" id="productoModal" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header" style="background: linear-gradient(90deg, var(--dbz-azul-oscuro), var(--dbz-violeta)); color: white;">
                        <h5 class="modal-title"><i class="bi bi-pc-display-horizontal me-2"></i>${producto.nombre}</h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <img src="https://images.unsplash.com/photo-1496181133206-80ce9b88a853?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80" 
                                     class="img-fluid rounded mb-3" alt="${producto.nombre}">
                            </div>
                            <div class="col-md-6">
                                <h6>Detalles del Producto</h6>
                                <p><strong>Categoría:</strong> ${producto.categoria}</p>
                                <p><strong>Precio:</strong> <span class="text-success fs-5">${producto.precio}</span></p>
                                <p><strong>Disponibilidad:</strong> ${producto.disponible}</p>
                                <p><strong>Valoración:</strong> ${producto.valoracion}</p>
                                <p><strong>ID:</strong> ${producto.id}</p>
                            </div>
                        </div>
                        <div class="mt-3">
                            <button class="btn btn-primary w-100">
                                <i class="bi bi-cart-plus me-2"></i>Añadir al Carrito
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;
    
    // Agregar el modal al body
    const modalContainer = document.createElement('div');
    modalContainer.innerHTML = modalHTML;
    document.body.appendChild(modalContainer);
    
    // Mostrar el modal
    const productoModal = new bootstrap.Modal(document.getElementById('productoModal'));
    productoModal.show();
    
    // Remover el modal del DOM después de cerrarse
    document.getElementById('productoModal').addEventListener('hidden.bs.modal', function() {
        document.body.removeChild(modalContainer);
    });
}

// Función para mostrar alerta personalizada (ofertas)
function mostrarAlertaPersonalizada() {
    const ofertas = [
        {
            titulo: "🔥 OFERTA ESPECIAL GAMING",
            mensaje: "¡Descuento del 30% en todos los teclados y mouse RGB! Solo por esta semana.",
            icono: "bi-controller"
        },
        {
            titulo: "🚀 NUEVOS PRODUCTOS",
            mensaje: "Ya disponibles las RTX 4090 y laptops gaming con i9 de 14va generación.",
            icono: "bi-rocket-takeoff"
        },
        {
            titulo: "🎮 TORNEO GAMING",
            mensaje: "Participa en nuestro torneo y gana una silla gamer profesional. Inscríbete gratis.",
            icono: "bi-trophy"
        },
        {
            titulo: "⚡ ENVÍO EXPRESS",
            mensaje: "¡Envío gratis en 24 horas para compras superiores a $500! Aprovecha ahora.",
            icono: "bi-lightning-charge"
        }
    ];
    
    // Seleccionar una oferta aleatoria
    const oferta = ofertas[Math.floor(Math.random() * ofertas.length)];
    
    const modalHTML = `
        <div class="modal fade" id="alertaModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content border-0 shadow-lg">
                    <div class="modal-header" style="background: linear-gradient(90deg, var(--dbz-naranja-fuego), var(--dbz-oro-ssj)); color: #000;">
                        <h5 class="modal-title fw-bold"><i class="bi ${oferta.icono} me-2"></i>${oferta.titulo}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <div class="text-center mb-3">
                            <i class="bi bi-gift" style="font-size: 3rem; color: var(--dbz-rosa);"></i>
                        </div>
                        <p class="fs-5 text-center">${oferta.mensaje}</p>
                        <div class="alert alert-info mt-3">
                            <i class="bi bi-info-circle me-2"></i>
                            Esta oferta es válida hasta agotar existencias.
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" onclick="mostrarNotificacion('¡Te has suscrito a las ofertas!', 'success')">
                            <i class="bi bi-bell me-2"></i>Recibir Ofertas
                        </button>
                    </div>
                </div>
            </div>
        </div>
    `;
    
    // Agregar el modal al body
    const modalContainer = document.createElement('div');
    modalContainer.innerHTML = modalHTML;
    document.body.appendChild(modalContainer);
    
    // Mostrar el modal
    const alertaModal = new bootstrap.Modal(document.getElementById('alertaModal'));
    alertaModal.show();
    
    // Remover el modal del DOM después de cerrarse
    document.getElementById('alertaModal').addEventListener('hidden.bs.modal', function() {
        document.body.removeChild(modalContainer);
    });
}

// Función para validar el formulario
function validarFormulario(event) {
    event.preventDefault();
    
    // Obtener elementos del formulario
    const nombre = document.getElementById('nombre');
    const email = document.getElementById('email');
    const mensaje = document.getElementById('mensaje');
    const formulario = document.getElementById('formularioContacto');
    
    let esValido = true;
    
    // Validar nombre
    if (nombre.value.trim() === '') {
        nombre.classList.add('is-invalid');
        esValido = false;
    } else {
        nombre.classList.remove('is-invalid');
        nombre.classList.add('is-valid');
    }
    
    // Validar email
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email.value.trim())) {
        email.classList.add('is-invalid');
        esValido = false;
    } else {
        email.classList.remove('is-invalid');
        email.classList.add('is-valid');
    }
    
    // Validar mensaje
    if (mensaje.value.trim() === '') {
        mensaje.classList.add('is-invalid');
        esValido = false;
    } else {
        mensaje.classList.remove('is-invalid');
        mensaje.classList.add('is-valid');
    }
    
    // Si el formulario es válido, mostrar mensaje de éxito
    if (esValido) {
        const mensajeExito = document.getElementById('mensajeExito');
        mensajeExito.classList.remove('d-none');
        
        // Simular envío del formulario
        setTimeout(() => {
            mensajeExito.classList.add('d-none');
            formulario.reset();
            
            // Limpiar clases de validación
            nombre.classList.remove('is-valid');
            email.classList.remove('is-valid');
            mensaje.classList.remove('is-valid');
            
            // Mostrar notificación de éxito
            mostrarNotificacion('¡Consulta enviada con éxito! Te contactaremos en 24h.', 'success');
        }, 3000);
    }
    
    return esValido;
}

// Función para mostrar notificaciones
function mostrarNotificacion(mensaje, tipo = 'info') {
    // Crear elemento de notificación
    const notificacion = document.createElement('div');
    notificacion.className = `alert alert-${tipo} alert-dismissible fade show position-fixed`;
    notificacion.style.cssText = 'top: 20px; right: 20px; z-index: 1050; min-width: 300px; max-width: 400px; box-shadow: 0 5px 15px rgba(0,0,0,0.2);';
    
    // Icono según tipo
    const iconos = {
        'success': 'bi-check-circle',
        'info': 'bi-info-circle',
        'warning': 'bi-exclamation-triangle',
        'danger': 'bi-x-circle'
    };
    
    notificacion.innerHTML = `
        <div class="d-flex align-items-center">
            <i class="bi ${iconos[tipo] || 'bi-info-circle'} me-3 fs-4"></i>
            <div>
                <strong>${tipo === 'success' ? 'Éxito!' : tipo === 'warning' ? 'Advertencia' : tipo === 'danger' ? 'Error' : 'Información'}</strong>
                <div>${mensaje}</div>
            </div>
        </div>
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;
    
    // Agregar al body
    document.body.appendChild(notificacion);
    
    // Remover automáticamente después de 5 segundos
    setTimeout(() => {
        if (notificacion.parentNode) {
            notificacion.classList.remove('show');
            setTimeout(() => {
                if (notificacion.parentNode) {
                    notificacion.remove();
                }
            }, 300);
        }
    }, 5000);
}

// Función para inicializar la aplicación
function inicializarApp() {
    // Crear efecto de partículas Dragon Ball
    crearParticulas();
    
    // Cargar productos en la tabla
    cargarProductos();
    
    // Configurar el botón de alerta
    const alertaBtn = document.getElementById('alertaBtn');
    alertaBtn.addEventListener('click', mostrarAlertaPersonalizada);
    
    // Configurar el formulario
    const formulario = document.getElementById('formularioContacto');
    formulario.addEventListener('submit', validarFormulario);
    
    // Agregar validación en tiempo real para el formulario
    const campos = ['nombre', 'email', 'mensaje'];
    campos.forEach(campoId => {
        const campo = document.getElementById(campoId);
        campo.addEventListener('input', function() {
            if (this.classList.contains('is-invalid')) {
                this.classList.remove('is-invalid');
            }
        });
    });
    
    // Configurar smooth scrolling para los enlaces del navbar
    document.querySelectorAll('a[href^="#"]').forEach(enlace => {
        enlace.addEventListener('click', function(e) {
            if (this.getAttribute('href') !== '#') {
                e.preventDefault();
                
                const objetivo = document.querySelector(this.getAttribute('href'));
                if (objetivo) {
                    window.scrollTo({
                        top: objetivo.offsetTop - 70,
                        behavior: 'smooth'
                    });
                    
                    // Cerrar navbar en móvil
                    const navbarCollapse = document.getElementById('navbarNav');
                    if (navbarCollapse.classList.contains('show')) {
                        new bootstrap.Collapse(navbarCollapse).hide();
                    }
                }
            }
        });
    });
    
    // Mostrar mensaje de bienvenida
    setTimeout(() => {
        mostrarNotificacion('🎮 ¡Bienvenido a TechPro Gaming! Descubre nuestra tecnología de alto rendimiento.', 'info');
    }, 1500);
    
    // Efecto hover para enlaces del footer
    document.querySelectorAll('footer a').forEach(enlace => {
        enlace.addEventListener('mouseenter', function() {
            this.style.color = 'var(--dbz-oro-ssj)';
            this.style.transition = 'color 0.3s ease';
        });
        enlace.addEventListener('mouseleave', function() {
            this.style.color = '';
        });
    });
}

// Inicializar la aplicación cuando el DOM esté completamente cargado
document.addEventListener('DOMContentLoaded', inicializarApp);

// Función global para uso en botones del modal
window.mostrarNotificacion = mostrarNotificacion;