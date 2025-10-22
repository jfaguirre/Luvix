using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class Permiso
    {     
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(255)]
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
