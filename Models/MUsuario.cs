using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BabelAPI.Models
{
    public class MUsuario
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }

        // Relación con Roles
        [ForeignKey("Rol")]
        public int RolID { get; set; }
        public MRol Rol { get; set; }
    }

}
