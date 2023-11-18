using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BabelAPI.Models
{
    public class MLibro
    {
        [Key]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // Relación con Categorías
        [ForeignKey("Categoria")]
        public int CategoriaID { get; set; }
        public MCategoria Categoria1 { get; set; }
    }
}
