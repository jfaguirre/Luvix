using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class SubCategoria
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(255)]
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        // Clave foránea hacia CategoriaTienda
        public int CategoriaTiendaId { get; set; }
        public CategoriaTienda CategoriaTienda { get; set; } = null!;
    }
}
