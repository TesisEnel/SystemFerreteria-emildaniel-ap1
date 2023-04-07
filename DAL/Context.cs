using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class Context: IdentityDbContext
{
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<EntradaInventario> entradaInventario { get; set; }
        public DbSet<Producto> producto { get; set; }
        public DbSet<Venta> venta { get; set; }
        public Context (DbContextOptions<Context> options): base(options){}
}