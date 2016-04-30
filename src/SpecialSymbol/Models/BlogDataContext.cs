using Microsoft.Data.Entity;
using System.Linq;

namespace SpecialSymbol.Models
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogDataContext()
        {
            Database.EnsureCreated();            
        }

        public IQueryable<ArchivedPostsSummary> GetArchivedPosts()
        {
            return Posts
                .GroupBy(x => new { x.PostedDate.Year, x.PostedDate.Month })
                .Select(group => new ArchivedPostsSummary()
                {
                    Count = group.Count(),
                    Year = group.Key.Year,
                    Month = group.Key.Month
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ForSqlServerUseIdentityColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = "Data Source=localhost;Initial Catalog=AspNetBlog;Integrated Security=False;User Id=sa;Password=sql;MultipleActiveResultSets=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
