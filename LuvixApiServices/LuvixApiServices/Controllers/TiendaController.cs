
using LuvixApiServices.Context;
using LuvixApiServices.CustomProperties;
using LuvixApiServices.Models;
using LuvixApiServices.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TiendaController : ControllerBase
{
    private readonly AppDbContext _AppDbContext;
    private readonly Utilidades _utilidades;

    public TiendaController(AppDbContext context, Utilidades utilidades)
    {
        _AppDbContext = context;
        _utilidades = utilidades;
    }

    // POST
    [HttpPost("crear-tienda")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CrearTienda([FromBody] CrearTiendaDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { mensaje = "Datos inválidos.", errores = ModelState });
        }

        // Obtener el ID del usuario desde el token JWT
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized(new { mensaje = "No se pudo obtener el usuario del token." });
        }

        if (!int.TryParse(userIdClaim.Value, out int idUsuario))
        {
            return Unauthorized(new { mensaje = "ID de usuario inválido." });
        }

        // Verificar que la categoría exista
        var categoriaExiste = await _AppDbContext.CategoriasTienda.AnyAsync(c => c.Id == dto.IdCategoria);
        if (!categoriaExiste)
        {
            return BadRequest(new { mensaje = "La categoría especificada no existe." });
        }

        // Verificar que el usuario no tenga ya una tienda
        var tieneTienda = await _AppDbContext.Tiendas.AnyAsync(t => t.IdUsuario == idUsuario);
        if (tieneTienda)
        {
            return Conflict(new { mensaje = "El usuario ya tiene una tienda registrada." });
        }

        try
        {
            var tienda = new Tienda
            {
                IdUsuario = idUsuario,
                IdCategoria = dto.IdCategoria,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,                
                FechaCreacion = DateTime.Now,
                Estado = "activo"
            };

            _AppDbContext.Tiendas.Add(tienda);
            await _AppDbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, new
            {
                mensaje = "Tienda creada exitosamente.",
                tiendaId = tienda.Id
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                mensaje = "Ocurrió un error al crear la tienda.",
                detalle = ex.Message
            });
        }
    }
    
    // GET
    [HttpGet("lista-tiendas")]
    public async Task<IActionResult> ListaTiendas()
    {
        var lista = await _AppDbContext.Tiendas
            .Include(t => t.Usuario)           
            .Include(t => t.Categoria)         
            .Select(t => new MostrarTiendas
            {
                Id = t.Id,
                NombreCategoria = t.Categoria.Nombre,           
                nombreUsuario = t.Usuario.Nombre + " " + t.Usuario.Apellido,
                NombreTienda = t.Nombre,
                Descripcion = t.Descripcion ?? "",              
                Estado = t.Estado,
                FechaCreacion = t.FechaCreacion
            })
            .ToListAsync();

        return Ok(new { value = lista });
    }

    // GET
    [HttpGet("lista-categorias")]
    public async Task<IActionResult> ListaCategorias()
    {
        var categorias = await _AppDbContext.CategoriasTienda
            .Select(c => new CategoriaTiendaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre
            })
            .ToListAsync();

        return Ok(new { value = categorias });
    }

    // GET: api/Tienda/tiendas-publicas
    [HttpGet("tiendas-publicas")]
    public async Task<IActionResult> ListaTiendasPublicas()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        int? usuarioId = null;

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int id))
        {
            usuarioId = id;
        }

        var tiendas = await _AppDbContext.Tiendas
            .Select(t => new MostrarTiendaPublicaDTO
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Logo = t.Logo, 
                EstaSiguiendo = usuarioId.HasValue &&
                               t.SeguidoresUsuarios.Any(s => s.IdUsuario == usuarioId.Value)
            })
            .ToListAsync();

        return Ok(new { value = tiendas });
    }

    // GET: api/Tienda/tiendas-seguidas
    [HttpGet("tiendas-seguidas")]
    [Authorize]
    public async Task<IActionResult> ObtenerTiendasSeguidas()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int idUsuario))
            return Unauthorized();

        var tiendasSeguidas = await _AppDbContext.SeguidoresUsuarioATienda
            .Where(s => s.IdUsuario == idUsuario)
            .Select(s => new TiendaSeguidaDTO
            {
                Id = s.IdTienda,
                Nombre = s.Tienda.Nombre,
                Descripcion = s.Tienda.Descripcion,
                Logo = s.Tienda.Logo
            })
            .ToListAsync();

        return Ok(new { value = tiendasSeguidas });
    }

}