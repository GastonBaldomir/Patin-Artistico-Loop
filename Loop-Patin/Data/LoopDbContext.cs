using Loop_Patin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loop_Patin.Data
{
    public class LoopDbContext : IdentityDbContext<Usuario>
    {
        public LoopDbContext(DbContextOptions<LoopDbContext> options)
            : base(options)
        {
        }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<VinculoFamiliar> VinculosFamiliares { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 1. Configura Identity (¡NO QUITAR!)
            base.OnModelCreating(builder);

            // 2. Configuración extra para Noticias
            builder.Entity<Noticia>(entity => {
                entity.Property(n => n.Titulo).IsRequired().HasMaxLength(150);
                entity.Property(n => n.Contenido).IsRequired();
            });

            // 3. Configuración para Vínculos Familiares (Padres e Hijas)
            builder.Entity<VinculoFamiliar>(entity =>
            {
                // Define la relación para el Tutor (Padre/Madre)
                entity.HasOne(vf => vf.Tutor)
                    .WithMany() // Muchos vínculos apuntan al mismo Tutor
                    .HasForeignKey(vf => vf.TutorId)
                    .OnDelete(DeleteBehavior.Restrict); // Evita el borrado en cascada

                // Define la relación para la Patinadora (Hija/Hijo)
                entity.HasOne(vf => vf.Patinadora)
                    .WithMany() // Muchos vínculos apuntan a la misma Patinadora
                    .HasForeignKey(vf => vf.PatinadoraId)
                    .OnDelete(DeleteBehavior.Restrict); // Evita el borrado en cascada
            });

        }



    }
}
