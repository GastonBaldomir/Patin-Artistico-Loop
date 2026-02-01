namespace Loop_Patin.Models
{
    
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Ej: "Iniciación", "Intermedio", "Competición"
        public string? Descripcion { get; set; } //requisitos o nivel deseado de aptitudes para pertenecer a la categoría

        // Relación: Una categoría tiene muchas patinadoras
        public ICollection<Usuario>? Patinadoras { get; set; }
    }

}
