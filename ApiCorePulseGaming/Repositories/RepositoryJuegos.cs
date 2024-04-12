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
            string sql = "SP_TODOS_JUEGOS";
            var consulta = this.context.Juegos.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public List<Juego> GetJuegosPrecioAsce()
        {
            return this.context.Juegos.OrderBy(z => z.PrecioJuego).ToList();
        }

        public List<Juego> GetJuegosPrecioDesc()
        {
            return this.context.Juegos.OrderByDescending(z => z.PrecioJuego).ToList();
        }

        public async Task<List<Juego>> GetJuegosGenerosAsync(int idgenero)
        {
            string sql = "SP_FILTRAR_JUEGOS_CATEGORIAS @idgenero";
            SqlParameter pamID = new SqlParameter("idgenero", idgenero);
            var consulta = this.context.Juegos.FromSqlRaw(sql, pamID);
            List<Juego> juegos = await consulta.ToListAsync();
            return juegos;
        }

        public async Task<Juego> FindJuegoAsync(int IdJuego)
        {
            return await this.context.Juegos.FirstOrDefaultAsync(z => z.IdJuego == IdJuego);
        }

        public async Task RegistrarJuego(string nombre, int idGenero, string imagen, double precio, string descripcion, int idEditor)
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

        public void ModificarJuego(int idJuego, string nombre, int idGenero, string imagen, double precio, string descripcion, int idEditor)
        {
            string sql = "SP_MODIFICAR_JUEGO @IDJuego, @NombreJuego, @IDGenero, @ImagenJuego, @PrecioJuego, @Descripcion, @IDEditor";
            SqlParameter pamIdJuego = new SqlParameter("IDJuego", idJuego);
            SqlParameter pamNombre = new SqlParameter("NombreJuego", nombre);
            SqlParameter pamIDGenero = new SqlParameter("IDGenero", idGenero);
            SqlParameter pamImagen = new SqlParameter("ImagenJuego", imagen);
            SqlParameter pamPrecio = new SqlParameter("PrecioJuego", precio);
            SqlParameter pamDescripcion = new SqlParameter("Descripcion", descripcion);
            SqlParameter pamIDEditor = new SqlParameter("IDEditor", idEditor);
            this.context.Database.ExecuteSqlRaw(sql, pamIdJuego, pamNombre, pamIDGenero, pamImagen, pamPrecio, pamDescripcion, pamIDEditor);
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

        public async Task CrearGeneroAsync(int id, string nombre)
        {
            Genero genero = new Genero();
            genero.IdGenero = id;
            genero.NombreGenero = nombre;
            this.context.Generos.Add(genero);
            await this.context.SaveChangesAsync();
        }

        public async Task CrearEditorAsync(int id, string nombre)
        {
            Editor editor = new Editor();
            editor.IDEditor = id;
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

        public void ModificarGenero(int idGenero, string nombre)
        {
            string sql = "SP_MODIFICAR_GENERO @IDGenero, @NombreGenero";
            SqlParameter pamId = new SqlParameter("IDGenero", idGenero);
            SqlParameter pamNombre = new SqlParameter("NombreGenero", nombre);
            this.context.Database.ExecuteSqlRaw(sql, pamId, pamNombre);
        }

        public void ModificarEditor(int idEditor, string nombre)
        {
            string sql = "SP_MODIFICAR_EDITOR @IDEditor, @NombreEditor";
            SqlParameter pamId = new SqlParameter("IDEditor", idEditor);
            SqlParameter pamNombre = new SqlParameter("NombreEditor", nombre);
            this.context.Database.ExecuteSqlRaw(sql, pamId, pamNombre);
        }


        public async Task<List<Juego>> GetGrupoJuegosAsync(int posicion)
        {
            string sql = "SP_GRUPO_JUEGOS @posicion";
            SqlParameter pamPosicion = new SqlParameter("posicion", posicion);
            var consulta = this.context.Juegos.FromSqlRaw(sql, pamPosicion);
            return await consulta.ToListAsync();
        }

        public async Task<int> GetNumeroJuegosAsync()
        {
            return await this.context.Juegos.CountAsync();
        }

        public void InsertarPedido(DateTime fecha, string ciudad, string pais, int idusuario, double total)
        {
            string sql = "SP_INSERT_PEDIDO @FechaPedido, @Ciudad, @Pais, @IDUsuario, @Total";
            SqlParameter pamFecha = new SqlParameter("FechaPedido", fecha);
            SqlParameter pamCiudad = new SqlParameter("Ciudad", ciudad);
            SqlParameter pamPais = new SqlParameter("Pais", pais);
            SqlParameter pamIdUsu = new SqlParameter("IDUsuario", idusuario);
            SqlParameter pamTotal = new SqlParameter("Total", total);
            this.context.Database.ExecuteSqlRaw(sql, pamFecha, pamCiudad, pamPais, pamIdUsu, pamTotal);
        }
        public void InsertarDetallePedido(int idjuego, double total, int cantidad, int idpedido)
        {
            string sql = "SP_INSERT_DETALLE_PEDIDO @IDJuego, @Total, @Cantidad, @IDPedido";
            SqlParameter pamidjuego = new SqlParameter("IDJuego", idjuego);
            SqlParameter pamtotal = new SqlParameter("Total", total);
            SqlParameter pamcantidad = new SqlParameter("Cantidad", cantidad);
            SqlParameter pamidpedido = new SqlParameter("IDPedido", idpedido);
            this.context.Database.ExecuteSqlRaw(sql, pamidjuego, pamtotal, pamcantidad, pamidpedido);
        }
    }
}
