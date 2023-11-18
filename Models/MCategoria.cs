using System.ComponentModel.DataAnnotations;

namespace BabelAPI.Models
{
    public class MCategoria
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
    }
}
