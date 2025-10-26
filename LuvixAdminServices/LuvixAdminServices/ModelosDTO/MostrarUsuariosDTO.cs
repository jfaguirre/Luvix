using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MostrarUsuariosDTO.cs
namespace LuvixAdminServices.ModelosDTO
{
    public class MostrarUsuariosDTO // Cambiado de 'internal' a 'public'
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public class ApiResponse
    {
        public List<MostrarUsuariosDTO> value { get; set; }
    }
}
