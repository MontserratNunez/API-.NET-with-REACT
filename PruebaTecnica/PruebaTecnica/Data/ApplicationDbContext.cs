using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Entities;

namespace PruebaTecnica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Contribuyente> Contribuyentes { get; set; }
        public DbSet<ComprobanteFiscal> ComprobantesFiscales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComprobanteFiscal>()
                .HasKey(c => new { c.RncCedula, c.NCF });
        }
    }
}
