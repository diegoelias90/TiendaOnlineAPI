using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnlineAPI.Models
{
    public class TiendaContext : DbContext // Hereda de DbContext para que sea válido como TContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options) { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoDetalle>().HasKey(pd => pd.PedidoDetalleId);
            base.OnModelCreating(modelBuilder);
        }

        public class Categoria
        {
            public int CategoriaId { get; set; }
            public string Nombre { get; set; }
        }

        public class Producto
        {
            public int ProductoId { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string UrlImagen { get; set; }
            public int CategoriaId { get; set; }
            public Categoria Categoria { get; set; }
        }

        public class Pedido
        {
            public int PedidoId { get; set; }
            public DateTime FechaPedido { get; set; }
            public string Cliente { get; set; }
            public ICollection<PedidoDetalle> Detalles { get; set; }
        }

        public class PedidoDetalle
        {
            [Key]
            public int PedidoDetalleId { get; set; }
            public int PedidoId { get; set; }
            public Pedido Pedido { get; set; }
            public int ProductoId { get; set; }
            public Producto Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
        }

    }
}