using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class Rol
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Nombre { get; set; } = string.Empty;

        // Colección de usuarios con este rol
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();

        // Permisos del rol
        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
