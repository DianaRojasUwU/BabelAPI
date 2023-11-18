using System.ComponentModel.DataAnnotations;

namespace BabelAPI.Models
{
    public class MRol
    {
        [Key]
        public int rolID { get; set; }
        public string rolNombre { get; set; }
    }

}
