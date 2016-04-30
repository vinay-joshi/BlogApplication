using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace SpecialSymbol.Models.Idenitty
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext()
        {
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId"); ;
        //    builder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
        //    builder.Entity<IdentityRole>().ToTable("Roles");
        //}
    }
}
