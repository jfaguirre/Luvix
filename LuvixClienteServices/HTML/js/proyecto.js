// Abrir y cerrar modal Tienda
const modalTienda = document.getElementById("modalTienda");
const closeTienda = document.getElementById("closeTienda");

// Abrir y cerrar modal Servicio
const modalServicio = document.getElementById("modalServicio");
const closeServicio = document.getElementById("closeServicio");

// Botones de ejemplo (puedes asignarlos desde tu HTML con id="btnTienda" y id="btnServicio")
const btnTienda = document.getElementById("btnTienda");
const btnServicio = document.getElementById("btnServicio");

// Abrir modales
if (btnTienda) {
  btnTienda.addEventListener("click", () => {
    modalTienda.style.display = "flex";
    modalTienda.classList.add("show");
  });
}
if (btnServicio) {
  btnServicio.addEventListener("click", () => {
    modalServicio.style.display = "flex";
    modalServicio.classList.add("show");
  });
}

// Cerrar modales con (x)
closeTienda.addEventListener("click", () => {
  modalTienda.classList.remove("show");
  setTimeout(() => (modalTienda.style.display = "none"), 300);
});

closeServicio.addEventListener("click", () => {
  modalServicio.classList.remove("show");
  setTimeout(() => (modalServicio.style.display = "none"), 300);
});

// Cerrar si hacen clic fuera del modal
window.addEventListener("click", (e) => {
  if (e.target === modalTienda) {
    modalTienda.classList.remove("show");
    setTimeout(() => (modalTienda.style.display = "none"), 300);
  }
  if (e.target === modalServicio) {
    modalServicio.classList.remove("show");
    setTimeout(() => (modalServicio.style.display = "none"), 300);
  }
});
// Función para validar contraseñas seguras
function validarPassword(password) {
  const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/;
  return regex.test(password);
}

// Validar formulario de Tienda
document.getElementById("formTienda").addEventListener("submit", function(e) {
  const pass = document.getElementById("passwordTienda").value;
  const errorMsg = document.getElementById("errorTienda");
  if (!validarPassword(pass)) {
    e.preventDefault();
    errorMsg.textContent = "La contraseña debe tener al menos 8 caracteres, incluir mayúsculas, minúsculas, un número y un símbolo.";
  } else {
    errorMsg.textContent = "";
    alert("✅ Tienda creada exitosamente.");
     // Redirige después de la alerta
    window.location.href = "editorTienda.html";

  }


});

// Validar formulario de Servicio
document.getElementById("formServicio").addEventListener("submit", function(e) {
  const pass = document.getElementById("passwordServicio").value;
  const errorMsg = document.getElementById("errorServicio");
  if (!validarPassword(pass)) {
    e.preventDefault();
    errorMsg.textContent = "La contraseña debe tener al menos 8 caracteres, incluir mayúsculas, minúsculas, un número y un símbolo.";
  } else {
    errorMsg.textContent = "";
    alert("✅ Servicio registrado exitosamente.");
     // Redirige después de la alerta
    window.location.href = "editorTienda.html";
  }

});


  const navToggle = document.getElementById('navMenuToggle');
  const navSidebar = document.getElementById('navResponsiveSidebar');

  navToggle.addEventListener('click', () => {
    navSidebar.style.display = navSidebar.style.display === 'block' ? 'none' : 'block';
  });

  // Opcional: cerrar sidebar al hacer clic fuera
  window.addEventListener('click', (e) => {
    if (!navSidebar.contains(e.target) && !navToggle.contains(e.target)) {
      navSidebar.style.display = 'none';
    }
  });

// Funciones para abrir modales
function abrirModalTienda() {
  document.getElementById("modalTienda").style.display = "block";
}

function abrirModalServicio() {
  document.getElementById("modalServicio").style.display = "block";
}

// Botones de la barra superior
document.getElementById("btnTienda").addEventListener("click", abrirModalTienda);
document.getElementById("btnServicio").addEventListener("click", abrirModalServicio);

// Botones responsive
document.querySelectorAll(".btnTiendaResponsive").forEach(btn => {
  btn.addEventListener("click", abrirModalTienda);
});

document.querySelectorAll(".btnServicioResponsive").forEach(btn => {
  btn.addEventListener("click", abrirModalServicio);
});

// Botones cerrar
document.getElementById("cerrarModalTienda").addEventListener("click", () => {
  document.getElementById("modalTienda").style.display = "none";
});

document.getElementById("cerrarModalServicio").addEventListener("click", () => {
  document.getElementById("modalServicio").style.display = "none";
});

// Cerrar modal si se hace clic fuera del contenido
window.addEventListener("click", (e) => {
  if(e.target.classList.contains("modal")){
    e.target.style.display = "none";
  }
});





