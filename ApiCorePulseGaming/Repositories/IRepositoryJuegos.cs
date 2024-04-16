using ApiCorePulseGaming.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ApiCorePulseGaming.Repositories
{
    public interface IRepositoryJuegos
    {
        Task<List<Juego>> GetJuegosAsync();
        Task<List<Juego>> GetJuegosPrecioAsceAsync();
        Task<List<Juego>> GetJuegosPrecioDescAsync();
        Task<List<Juego>> GetJuegosGenerosAsync(int idgenero);
        Task<Juego> FindJuegoAsync(int IdJuego);
        Task RegistrarJuego(string nombre, int idGenero, string imagen, double precio, string descripcion, int idEditor);
        Task<List<Editor>> GetEditoresAsync();
        Task<List<Genero>> GetGenerosAsync();
        Task DeleteJuegoAsync(int idjuego);
        Task ModificarJuegoAsync(int idJuego, string nombre, int idGenero, string imagen, double precio, string descripcion, int idEditor);
        Task CrearEditorAsync(int id, string nombre);
        Task CrearGeneroAsync(int id, string nombre);
        Task ModificarEditorAsync(int idEditor, string nombre);
        Task ModificarGeneroAsync(int idGenero, string nombre);
        Task DeleteEditorAsync(int idEditor);
        Task DeleteGeneroAsync(int idGenero);
        Task<Editor> FindEditorAsync(int idEditor);
        Task<Genero> FindGeneroAsync(int idGenero);
        Task<int> GetNumeroJuegosAsync();
        Task<List<Juego>> GetGrupoJuegosAsync(int posicion);
        Task<List<Juego>> GetProductosEnCarritoAsync(List<int> idsJuegos);
        Task<Pedido> CreatePedidoAsync(int idusuario, List<Juego> carrito);
        Task<List<DetallePedidoView>> GetProductosPedidoUsuarioAsync(int idUsuario);
        Task<int> GetMaxIdDetallePedidoAsync();
        Task<int> GetMaxIdPedidoAsync();
    }
}
