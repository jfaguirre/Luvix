using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuvixApiServices.CustomProperties;
using LuvixApiServices.Models;
using LuvixApiServices.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using LuvixApiServices.Context;

namespace LuvixApiServices.Controllers
{
    // Actualizar usuario
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    
    public class ActualizarUsuarioController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;
        private readonly Utilidades _utilidades;
        public ActualizarUsuarioController(AppDbContext AppDbContext, Utilidades utilidades)
        {
            _AppDbContext = AppDbContext;
            _utilidades = utilidades;
        }

        // PUT: api/ActualizarUsuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, ActualizarUsuarioDTO objeto)
        {            
            var usuario = await _AppDbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado." });
            }
            usuario.Nombre = objeto.Nombre;
            usuario.Apellido = objeto.Apellido;            
            if (!string.IsNullOrEmpty(objeto.Password))
            {
                var utilidades = new Utilidades();
                usuario.Password = utilidades.encriptarSHA256(objeto.Password);
            }

            try
            {
                await _AppDbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario actualizado satisfactoriamente." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _AppDbContext.Usuarios.AnyAsync(u => u.Id == id))
                {
                    return NotFound(new { mensaje = "Usuario no encontrado." });
                }
                throw; // Deja que el middleware maneje la excepción
            }
        }

    }
}
