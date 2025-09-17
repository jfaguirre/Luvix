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
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;
        private readonly Utilidades _utilidades;

        public AccesoController(AppDbContext AppDbContext, Utilidades utilidades)
        {
            _AppDbContext = AppDbContext;
            _utilidades = utilidades;
        }

        // POST: api/Acceso/Registrar
        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(CrearUsuarioDTO objeto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nombre = objeto.Nombre,
                    Apellido = objeto.Apellido,
                    Email = objeto.Email,
                    Password = _utilidades.encriptarSHA256(objeto.Password),
                    FechaRegistro = DateTime.Now,
                    Estado = "activo"
                };

                await _AppDbContext.Usuarios.AddAsync(usuario);
                await _AppDbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "El usuario se creo satisfactoriamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }            
        }

        // POST: api/Acceso/Registrar - Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUsuarioDTO objeto)
        {
            var usuarioEncontrado = await _AppDbContext.Usuarios
                .Where(u =>
                u.Email == objeto.Email &&
                u.Password == _utilidades.encriptarSHA256(objeto.Password)
                ).FirstOrDefaultAsync();
            if(usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = false, token = "" });               
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = true, token = _utilidades.generarTokenJWT(usuarioEncontrado) });
            }    
        }
    }
}
