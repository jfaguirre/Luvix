using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LuvixApiServices.Models;

namespace LuvixApiServices.CustomProperties
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;
       
        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hast = SHA256.Create())
            {
                byte[] bytes = sha256Hast.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string generarTokenJWT(Usuario modelo)
        {
            // Crear los claims
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.Id.ToString()),                
                new Claim(ClaimTypes.Email, modelo.Email),
                new Claim(ClaimTypes.Name, modelo.Nombre + " " + modelo.Apellido)
            };

            // Generar la llave privada
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Crear el token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
