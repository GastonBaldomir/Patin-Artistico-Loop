
using System.ComponentModel.DataAnnotations;

namespace Loop_Patin.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de cuenta")]
        public string RolSeleccionado { get; set; }

        public List<string> RolesDisponibles { get; set; } = new();
    }
}
