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
    public class MostrarUsuariosController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;   
        public MostrarUsuariosController(AppDbContext AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }

        // GET: api/Usuarios
        [HttpGet]
        [Route("lista-usuarios")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _AppDbContext.Usuarios.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }
    }
}
