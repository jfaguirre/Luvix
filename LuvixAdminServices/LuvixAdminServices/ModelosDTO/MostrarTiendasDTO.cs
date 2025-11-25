using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuvixAdminServices.ModelosDTO
{
    internal class MostrarTiendasDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; } 
        public int IdCategoria { get; set; } 
        public string subCategoria { get; set; } 
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }               
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
    }
}
