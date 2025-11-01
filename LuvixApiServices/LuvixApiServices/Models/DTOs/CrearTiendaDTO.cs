using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models.DTOs
{
    public class CrearTiendaDTO
    {
        public int IdCategoria { get; set; }        
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
