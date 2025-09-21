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
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BorrarUsuarioController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;
        public BorrarUsuarioController(AppDbContext AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }

        // DELETE: api/BorrarUsuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _AppDbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado." });
            }
            _AppDbContext.Usuarios.Remove(usuario);
            await _AppDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario eliminado satisfactoriamente." });
        }
    }
}
