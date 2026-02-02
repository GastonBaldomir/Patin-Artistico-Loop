using Microsoft.AspNetCore.Identity;
using System.Collections.Generic; // Necesario para ICollection
using Loop_Patin.Models;
using System.ComponentModel.DataAnnotations;
public class Usuario : IdentityUser
{
    // Datos personales
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }
    public string ? Telefono { get; set; }
    public string? Dni { get; set; }
    public string? Direccion { get; set; }
    public bool? Estado { get; set; } = true; // activo/inactivo    
    public string? Descripcion { get; set; } //presentacion del usuario - ingreso personal

    // Datos específicos del club/deporte
    public int? CategoriaId { get; set; } 
    public Categoria? Categoria { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public string? FotoUrl  { get; set; }

   
    
}
