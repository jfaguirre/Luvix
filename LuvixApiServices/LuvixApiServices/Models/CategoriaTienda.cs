using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class CategoriaTienda
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        // Relacion con tiendas
        public ICollection<Tienda> Tiendas { get; set; } = new List<Tienda>();
        
        // Relación con subcategorías
        public ICollection<SubCategoria> Subcategorias { get; set; } = new List<SubCategoria>();

    }
}
