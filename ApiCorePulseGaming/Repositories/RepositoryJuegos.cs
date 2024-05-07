using ApiCorePulseGaming.Data;
using ApiCorePulseGaming.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCorePulseGaming.Repositories
{
    public class RepositoryJuegos : IRepositoryJuegos
    {
        private JuegosContext context;

        public RepositoryJuegos(JuegosContext context)
        {
            this.context = context;
        }

        public async Task<List<Juego>> GetJuegosAsync()
        {
            return await this.context.Juegos.ToListAsync();
        }

        public async Task<List<Juego>> GetJuegosPrecioAsceAsync()
        {
            return await this.context.Juegos.OrderBy(z => z.PrecioJuego).ToListAsync();
        }

        public async Task<List<Juego>> GetJuegosPrecioDescAsync()
        {
            return await this.context.Juegos.OrderByDescending(z => z.PrecioJuego).ToListAsync();
        }

        public async Task<List<Juego>> GetJuegosGenerosAsync(int idgenero)
        {
            string sql = "SP_FILTRAR_JUEGOS_CATEGORIAS @idgenero";
            SqlParameter pamID = new SqlParameter("idgenero", idgenero);
            var consulta = this.context.Juegos.FromSqlRaw(sql, pamID);
            List<Juego> juegos = await consulta.ToListAsync();
            return juegos;
        }

        public async Task<List<Juego>> GetJuegosSessionAsync(List<int> juegos)
        {
            return await this.context.Juegos
                .Where(c => juegos.Contains(c.IdJuego))
                .ToListAsync();
        }

        public async Task<int> GetNumeroJuegosAsync()
        {
            return await this.context.Juegos.CountAsync();
        }

        public async Task<Juego> FindJuegoAsync(int IdJuego)
        {
            return await this.context.Juegos.FirstOrDefaultAsync(z => z.IdJuego == IdJuego);
        }

        public async Task RegistrarJuego(string nombre, int idGenero, string imagen, decimal precio, string descripcion, int idEditor)
        {
            string sql = "SP_INSERT_JUEGO @NombreJuego, @IDGenero, @Imagen, @Precio, @Descripcion, @IDEditor";
            SqlParameter pamNombre = new SqlParameter("NombreJuego", nombre);
            SqlParameter pamIDGenero = new SqlParameter("@IDGenero", idGenero);
            SqlParameter pamImagen = new SqlParameter("@Imagen", imagen);
            SqlParameter pamPrecio = new SqlParameter("@Precio", precio);
            SqlParameter pamDescripcion = new SqlParameter("@Descripcion", descripcion);
            SqlParameter pamIDEditor = new SqlParameter("@IDEditor", idEditor);
            this.context.Database.ExecuteSqlRaw(sql, pamNombre, pamIDGenero, pamImagen, pamPrecio, pamDescripcion, pamIDEditor);

        }

        public async Task ModificarJuegoAsync(int idJuego, string nombre, int idGenero, string imagen, decimal precio, string descripcion, int idEditor)
        {
            Juego juego = await this.FindJuegoAsync(idJuego);
            juego.NombreJuego = nombre;
            juego.IDGenero = idGenero;
            juego.ImagenJuego = imagen;
            juego.PrecioJuego = precio;
            juego.Descripcion = descripcion;
            juego.IdEditor = idEditor;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteJuegoAsync(int idjuego)
        {
            Juego juego = await this.FindJuegoAsync(idjuego);
            this.context.Juegos.Remove(juego);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<List<Editor>> GetEditoresAsync()
        {
            return await this.context.Editores.ToListAsync();
        }

        public async Task<Genero> FindGeneroAsync(int idGenero)
        {
            return await this.context.Generos.FirstOrDefaultAsync(z => z.IdGenero == idGenero);
        }

        public async Task<Editor> FindEditorAsync(int idEditor)
        {
            return await this.context.Editores.FirstOrDefaultAsync(z => z.IDEditor == idEditor);
        }

        public async Task CrearGeneroAsync(string nombre)
        {
            Genero genero = new Genero();
            genero.IdGenero = await context.Generos.Select(g => g.IdGenero).MaxAsync() + 1;
            genero.NombreGenero = nombre;
            this.context.Generos.Add(genero);
            await this.context.SaveChangesAsync();
        }

        public async Task CrearEditorAsync(string nombre)
        {
            Editor editor = new Editor();
            editor.IDEditor = await context.Editores.Select(g => g.IDEditor).MaxAsync() + 1;
            editor.NombreEditor = nombre;
            this.context.Editores.Add(editor);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteGeneroAsync(int idGenero)
        {
            Genero genero = await this.FindGeneroAsync(idGenero);
            this.context.Generos.Remove(genero);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteEditorAsync(int idEditor)
        {
            Editor editor = await this.FindEditorAsync(idEditor);
            this.context.Editores.Remove(editor);
            await this.context.SaveChangesAsync();
        }

        public async Task ModificarGeneroAsync(int idGenero, string nombre)
        {
            Genero genero = await this.FindGeneroAsync(idGenero);
            genero.NombreGenero = nombre;
            await this.context.SaveChangesAsync();
        }

        public async Task ModificarEditorAsync(int idEditor, string nombre)
        {
            Editor editor = await this.FindEditorAsync(idEditor);
            editor.NombreEditor = nombre;
            await this.context.SaveChangesAsync();
        }


        public async Task<List<Juego>> GetGrupoJuegosAsync(int posicion)
        {
            string sql = "SP_GRUPO_JUEGOS @posicion";
            SqlParameter pamPosicion = new SqlParameter("posicion", posicion);
            var consulta = this.context.Juegos.FromSqlRaw(sql, pamPosicion);
            return await consulta.ToListAsync();
        }

        public async Task<Pedido> CreatePedidoAsync(int idusuario, List<Juego> carrito)
        {
            var total = 0.0m;
            foreach (Juego juego in carrito)
            {
                total = juego.PrecioJuego + total;
            }
            Pedido pedido = new Pedido
            {
                IDPedido = await GetMaxIdPedidoAsync(),
                IDUsuario = idusuario,
                FechaPedido = DateTime.Now,
                Total = total
            };
            await this.context.Pedidos.AddAsync(pedido);
            await this.context.SaveChangesAsync();

            foreach (Juego p in carrito)
            {
                DetallesPedido detalle = new DetallesPedido
                {
                    IDDetallePedido = await GetMaxIdDetallePedidoAsync(),
                    IDPedido = pedido.IDPedido,
                    IDJuego = p.IdJuego,
                    Cantidad = 1,
                    PrecioUnitario = p.PrecioJuego
                };

                // Verificar si ya existe un DetallePedido con el mismo IdDetallePedido
                DetallesPedido existingDetalle = await this.context.DetallePedidos.FindAsync(detalle.IDDetallePedido);
                if (existingDetalle != null)
                {
                    // Actualizar el DetallePedido existente si es necesario
                    existingDetalle.IDPedido = detalle.IDPedido;
                    existingDetalle.IDJuego = detalle.IDJuego;
                    existingDetalle.Cantidad = detalle.Cantidad;
                    existingDetalle.PrecioUnitario = detalle.PrecioUnitario;
                }
                else
                {
                    // Agregar el nuevo DetallePedido al contexto si no existe
                    await this.context.AddAsync(detalle);
                    await this.context.SaveChangesAsync();

                }
            }

            await this.context.SaveChangesAsync();
            return pedido;
        }

        public async Task<List<DetallePedidoView>> GetProductosPedidoUsuarioAsync(int idUsuario)
        {
            return await context.DetallePedidoViews
                .Where(d => d.IdUsuario == idUsuario)
                .ToListAsync();
        }

        public async Task<int> GetMaxIdDetallePedidoAsync()
        {
            if (this.context.DetallePedidos.Count() == 0) return 1;
            return await this.context.DetallePedidos.MaxAsync(x => x.IDDetallePedido) + 1;
        }

        public async Task<int> GetMaxIdPedidoAsync()
        {
            if (this.context.Pedidos.Count() == 0) return 1;
            return await this.context.Pedidos.MaxAsync(x => x.IDPedido) + 1;
        }
    }
}
