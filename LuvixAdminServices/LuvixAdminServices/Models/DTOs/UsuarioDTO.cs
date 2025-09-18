using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuvixAdminServices.Models.DTOs
{
    internal class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FotoPerfil { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } = "activo";
    }
}
