using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext() 
        {
            //Hi !! I'm code firsts
            Database.EnsureCreated();
        }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost,1433; database=RepositoryDb; User ID=sa; password=19Mayis1919!;");
        }

        private DbSet<Product> Products { get; set; }
    }
}