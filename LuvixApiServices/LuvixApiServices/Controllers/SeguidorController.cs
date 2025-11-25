using LuvixApiServices.Context;
using LuvixApiServices.CustomProperties;
using LuvixApiServices.Models;
using LuvixApiServices.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LuvixApiServices.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SeguidorController : ControllerBase
    {

        private readonly AppDbContext _AppDbContext;
        private readonly Utilidades _utilidades;

        public SeguidorController(AppDbContext context, Utilidades utilidades)
        {
            _AppDbContext = context;
            _utilidades = utilidades;
        }

        [HttpPost("seguir/{idTienda}")]
        public async Task<IActionResult> SeguirTienda(int idTienda)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int idUsuario))
                return Unauthorized();

            var tiendaExiste = await _AppDbContext.Tiendas.AnyAsync(t => t.Id == idTienda);
            if (!tiendaExiste)
                return NotFound(new { mensaje = "La tienda no existe." });

            var yaSiguiendo = await _AppDbContext.SeguidoresUsuarioATienda
                .AnyAsync(s => s.IdUsuario == idUsuario && s.IdTienda == idTienda);

            if (yaSiguiendo)
                return BadRequest(new { mensaje = "Ya estás siguiendo esta tienda." });

            var seguidor = new SeguidorUsuarioATienda
            {
                IdUsuario = idUsuario,
                IdTienda = idTienda,
                FechaSeguimiento = DateTime.Now
            };

            _AppDbContext.SeguidoresUsuarioATienda.Add(seguidor);
            await _AppDbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Ahora sigues esta tienda." });
        }

        [HttpPost("quitar/{idTienda}")]
        public async Task<IActionResult> DejarDeSeguir(int idTienda)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int idUsuario))
                return Unauthorized();

            var seguidor = await _AppDbContext.SeguidoresUsuarioATienda
                .FirstOrDefaultAsync(s => s.IdUsuario == idUsuario && s.IdTienda == idTienda);

            if (seguidor == null)
                return BadRequest(new { mensaje = "No estás siguiendo esta tienda." });

            _AppDbContext.SeguidoresUsuarioATienda.Remove(seguidor);
            await _AppDbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Has dejado de seguir esta tienda." });
        }       
    }
}
