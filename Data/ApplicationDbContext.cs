using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Čtenářský_deník.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<KnihaImage>() // když se smaže kniha, smažou se i její obrázky
                .HasOne(i => i.Kniha)
                .WithMany(k => k.Images)
                .HasForeignKey(i => i.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Autor> Autori => Set<Autor>(); // tabulka s autory
        public DbSet<Kniha> Knihy => Set<Kniha>(); // tabulka s knihami

        public DbSet<ObdobiMaturita> ObdobiMaturita { get; set; } // tabulka s maturtinímu období

        public DbSet<KnihaImage> KnihaImages { get; set; } // tabulka pro cesty odkazující na obrázky pro knihy

    }
}
