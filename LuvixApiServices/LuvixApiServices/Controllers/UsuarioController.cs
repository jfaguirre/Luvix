using LuvixApiServices.Context;
using LuvixApiServices.CustomProperties;
using LuvixApiServices.Models;
using LuvixApiServices.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LuvixApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Admin")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;
        private readonly Utilidades _utilidades;

        public UsuarioController(AppDbContext AppDbContext, Utilidades utilidades)
        {
            _AppDbContext = AppDbContext;
            _utilidades = utilidades;
        }


        //GET: Mostrar todos los usuarios
        [HttpGet("lista-usuarios")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _AppDbContext.Usuarios
            .Select(u => new MostrarUsuariosDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro
                })
            .ToListAsync();

        return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }


        //GET: Mostrar usuario por Id
        [HttpGet("usuario-id/{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var usuario = await _AppDbContext.Usuarios.FindAsync(id);
            
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado." });
            }

            var dto = new MostrarUsuariosDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro,                
            };

            return StatusCode(StatusCodes.Status200OK, new { value = dto });
        }


        //PUT: Actualizar usuario
        [HttpPut("actualizar-usuario/{id:int}")]
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
                usuario.Password = _utilidades.encriptarSHA256(objeto.Password);
            }

            try
            {
                await _AppDbContext.Usuarios.AddAsync(usuario);
                await _AppDbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario actualizado satisfactoriamente." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _AppDbContext.Usuarios.AnyAsync(u => u.Id == id))
                {
                    return NotFound(new { mensaje = "Usuario no encontrado." });
                }
                throw;
            }
        }


        //DELETE: Aliminar usuario
        [HttpDelete("borrar-usuario/{id:int}")]
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


        //PUT: Subir foto
        [HttpPut("subir-foto")]
        public async Task<IActionResult> SubirFoto(IFormFile file)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = ObtenerIdDesdeToken(token); // Necesitamos esta función

            if (userId == 0 || file == null || file.Length == 0)
                return BadRequest(new { mensaje = "Datos inválidos." });

            // Carpeta donde se guardan las fotos
            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos-perfil");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Nombre único del archivo
            var extension = Path.GetExtension(file.FileName);
            var nombreArchivo = $"{userId}{extension}";
            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            // Si ya existe, elimínalo
            if (System.IO.File.Exists(rutaCompleta))
                System.IO.File.Delete(rutaCompleta);

            // Guarda el nuevo archivo
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Actualiza el campo FotoPerfil en la base de datos
            var usuario = await _AppDbContext.Usuarios.FindAsync(userId);
            usuario.FotoPerfil = $"/fotos-perfil/{nombreArchivo}";
            await _AppDbContext.SaveChangesAsync();     

            return Ok(new { fotoUrl = usuario.FotoPerfil });
        }

        // Método auxiliar para obtener el ID desde el token JWT
        private int ObtenerIdDesdeToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var idClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
                return idClaim != null ? int.Parse(idClaim.Value) : 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
