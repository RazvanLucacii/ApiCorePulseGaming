using ApiCorePulseGaming.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCorePulseGaming.Data
{
    public class JuegosContext : DbContext
    {
        public JuegosContext(DbContextOptions<JuegosContext> options): base(options){}

        public DbSet<Juego> Juegos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Editor> Editores { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<DetallesPedido> DetallePedidos { get; set; }

        public DbSet<DetallePedidoView> DetallePedidoViews { get; set; }
    }
}
