using Microsoft.EntityFrameworkCore;
using Pecas2.Models;

namespace Pecas2.Data
{
    public class PecasContext : DbContext
    {
        public PecasContext(DbContextOptions<PecasContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento 1:N entre Cliente e Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId);

            // Relacionamento N:N entre Pedido e Produto
            modelBuilder.Entity<ItemPedido>()
                .HasKey(pp => new { pp.PedidoId, pp.ProdutoId });

            modelBuilder.Entity<ItemPedido>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.ItemPedidos)
                .HasForeignKey(pp => pp.PedidoId);

            modelBuilder.Entity<ItemPedido>()
                .HasOne(pp => pp.Produto)
                .WithMany(p => p.ItemPedidos)
                .HasForeignKey(pp => pp.ProdutoId);
        }
    }

}

