namespace LuvixApiServices.Models
{
    public class SubCategoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        // Clave foránea hacia CategoriaTienda
        public int CategoriaTiendaId { get; set; }
        public CategoriaTienda CategoriaTienda { get; set; } = null!;
    }
}
