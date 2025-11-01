namespace LuvixApiServices.Models.DTOs
{
    public class MostrarTiendas
    {
        public int Id { get; set; }        
        public string NombreCategoria { get; set; }
        public string nombreUsuario { get; set; }
        public string NombreTienda { get; set; }
        public string Descripcion { get; set; }        
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
