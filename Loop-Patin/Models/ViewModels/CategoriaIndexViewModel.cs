using System.ComponentModel.DataAnnotations;

namespace Loop_Patin.Models.ViewModels
{
     public class CategoriaIndexViewModel
    {
        public int? Id { get; set; }
        // Formulario
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        // Listado
        public List<Categoria>? Categorias { get; set; }
    }

}
