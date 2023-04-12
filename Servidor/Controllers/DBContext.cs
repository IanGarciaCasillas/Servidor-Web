using Microsoft.EntityFrameworkCore;
using Servidor.Model;

namespace Servidor.Controllers
{
    public class DBContext : DbContext
    {
        public DbSet<Client> Client { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=testing;user=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        

    }
}
