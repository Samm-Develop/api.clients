using api.clients.Models;
using Microsoft.EntityFrameworkCore;

namespace api.clients.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pais> Paises { get; set; }
    }
}