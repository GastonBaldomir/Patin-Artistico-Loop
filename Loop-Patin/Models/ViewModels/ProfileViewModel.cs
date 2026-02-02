using System.ComponentModel.DataAnnotations;

namespace Loop_Patin.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellidos { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        public string? Telefono { get; set; }
        public string? Dni { get; set; }
        public string? Descripcion { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        
        public DateTime? FechaRegistro { get; set; }
    }
}
