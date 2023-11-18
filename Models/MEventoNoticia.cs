using System.ComponentModel.DataAnnotations;

namespace BabelAPI.Models
{
    public class MEventoNoticia
    {
        [Key]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ubicacion { get; set; }
        public string Imagen { get; set; }
        public string Enlace { get; set; }
    }
}
