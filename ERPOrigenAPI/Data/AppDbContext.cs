using ERPOrigenAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERPOrigenAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
    }
}
