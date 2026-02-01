using System.ComponentModel.DataAnnotations;

namespace Loop_Patin.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        
        public string Contenido { get; set; }
        public string? ImagenUrl { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
        // Relación con el usuario que publicó la noticia
        public string? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
