using System.ComponentModel.DataAnnotations;

namespace BabelAPI.Models
{
    public class MRol
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
    }

}
