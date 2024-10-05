using Microsoft.EntityFrameworkCore;
using PeopleCatalog.Domain.Entities;


namespace PeopleCatalog.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definición de las tablas
        public DbSet<Person> People { get; set; }

        // Método para configurar las tablas y los SPs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla People
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Person>().HasKey(p => p.Id); 


            // Relación con Address (objeto de valor)
            modelBuilder.Entity<Person>().OwnsOne(p => p.Address);

            // Definición de la Vista PersonSummary
            modelBuilder.Entity<PersonSummary>().HasNoKey().ToView("PersonSummary");
        }
    }

    // DTO para la vista PersonSummary
    public class PersonSummary
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
