const formTienda = document.getElementById("formTienda");

if (formTienda) {
  formTienda.addEventListener("submit", (e) => {
    e.preventDefault();
    const nombre = document.getElementById("nombreTienda").value.trim();
    const descripcion = document.getElementById("descripcionTienda").value.trim();

    if (!nombre || !descripcion) return alert("Completa todos los campos.");

    localStorage.setItem("nombreTienda", nombre);
    localStorage.setItem("descripcionTienda", descripcion);

    window.location.href = "editorTienda.html";
  });
}


