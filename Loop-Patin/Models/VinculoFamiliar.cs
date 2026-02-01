namespace Loop_Patin.Models
{
   
    public class VinculoFamiliar
    {
        public int Id { get; set; }
        public string TutorId { get; set; }
        public Usuario Tutor { get; set; } // Propiedad de navegación
        public string PatinadoraId { get; set; }
        public Usuario Patinadora { get; set; } // Propiedad de navegación
    }

}
