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
        public int IdUsuario { get; set; } // <--- Quiero que aparezaca el nombre de usuario en lugar del IdUsuario
        public int IdCategoria { get; set; } // <--- Quiero que aparezaca el nombre de la categoria en lugar del IdCategoria
        public string subCategoria { get; set; } // <--- Este no es parte directamente de Tiendas, pertenece a Categoria
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }               
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
    }
}
