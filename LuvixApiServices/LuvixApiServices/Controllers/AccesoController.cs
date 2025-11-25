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

        // POST: api/acceso/registrar
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("registrarse")]
        
        public async Task<IActionResult> Registrarse([FromBody] CrearUsuarioDTO objeto)
        {

            // Validar modelo DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = "Datos inválidos.", errores = ModelState });
            }

            // Verificar si el email ya existe
            var existeEmail = await _AppDbContext.Usuarios.AnyAsync(u => u.Email == objeto.Email);
            if (existeEmail)
            {
                return BadRequest(new { mensaje = "El correo electrónico ya está registrado." });
            }

            try
            {
                var usuario = new Usuario
                {
                    Nombre = objeto.Nombre,
                    Apellido = objeto.Apellido,
                    Genero = objeto.Genero,
                    Email = objeto.Email,
                    Password = _utilidades.encriptarSHA256(objeto.Password),
                    FotoPerfil = null,  
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

        // POST: api/acceso/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDTO objeto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = "Credenciales inválidas." });
            }

            var usuarioEncontrado = await _AppDbContext.Usuarios
                .Where(u =>
                u.Email == objeto.Email &&
                u.Password == _utilidades.encriptarSHA256(objeto.Password)
                ).FirstOrDefaultAsync();
            if(usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "El correo o password ingresado son incorrectos.", token = "" });               
            }
            
            var token = await _utilidades.generarTokenJWT(usuarioEncontrado);

            return Ok(new { mensaje = true, token = token });
        }
    }
}
