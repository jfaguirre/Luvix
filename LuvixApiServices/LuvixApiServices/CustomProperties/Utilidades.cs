using LuvixApiServices.Context;
using LuvixApiServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LuvixApiServices.CustomProperties
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context; // Necesario para cargar roles y permisos

        public Utilidades(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hast = SHA256.Create())
            {
                byte[] bytes = sha256Hast.ComputeHash(Encoding.UTF8.GetBytes(texto));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<string> generarTokenJWT(Usuario modelo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.Id.ToString()),
                new Claim(ClaimTypes.Email, modelo.Email),
                new Claim("nombre", modelo.Nombre),
                new Claim("apellido", modelo.Apellido)
                //new Claim(ClaimTypes.Name, $"{modelo.Nombre} {modelo.Apellido}")
            };

            // ⭐ Cargar todos los roles del usuario
            var rolesDelUsuario = await _context.UsuarioRoles
                .Where(ur => ur.UsuarioId == modelo.Id)
                .Include(ur => ur.Rol)
                    .ThenInclude(r => r.RolPermisos) // Incluir permisos del rol
                        .ThenInclude(rp => rp.Permiso)
                .Select(ur => ur.Rol)
                .ToListAsync();

            foreach (var rol in rolesDelUsuario)
            {
                // Agregar el rol como claim
                claims.Add(new Claim(ClaimTypes.Role, rol.Nombre));

                // Agregar cada permiso del rol como claim personalizado
                foreach (var permiso in rol.RolPermisos.Select(rp => rp.Permiso.Nombre))
                {
                    claims.Add(new Claim("Permiso", permiso)); // Tipo: "Permiso"
                }
            }

            // Generar clave simétrica
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Crear el token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Puedes ajustar el tiempo
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}