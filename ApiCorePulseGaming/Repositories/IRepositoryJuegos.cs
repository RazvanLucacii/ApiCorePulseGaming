using ApiCorePulseGaming.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ApiCorePulseGaming.Repositories
{
    public interface IRepositoryJuegos
    {
        Task<List<Juego>> GetJuegosAsync();
        List<Juego> GetJuegosPrecioAsce();
        List<Juego> GetJuegosPrecioDesc();
        Task<List<Juego>> GetJuegosGenerosAsync(int idgenero);
        Task<Juego> FindJuegoAsync(int IdJuego);
        Task RegistrarJuego(string nombre, int idGenero, string imagen, double precio, string descripcion, int idEditor);
        Task<List<Editor>> GetEditoresAsync();
        Task<List<Genero>> GetGenerosAsync();
        Task DeleteJuegoAsync(int idjuego);
        void ModificarJuego(int idJuego, string nombre, int idGenero, string imagen, double precio, string descripcion, int idEditor);
        Task CrearEditorAsync(int id, string nombre);
        Task CrearGeneroAsync(int id, string nombre);
        void ModificarEditor(int idEditor, string nombre);
        void ModificarGenero(int idGenero, string nombre);
        Task DeleteEditorAsync(int idEditor);
        Task DeleteGeneroAsync(int idGenero);
        Task<Editor> FindEditorAsync(int idEditor);
        Task<Genero> FindGeneroAsync(int idGenero);
        Task<int> GetNumeroJuegosAsync();
        Task<List<Juego>> GetGrupoJuegosAsync(int posicion);
        void InsertarPedido(DateTime fecha, string ciudad, string pais, int idusuario, double total);
        void InsertarDetallePedido(int idjuego, double total, int cantidad, int idpedido);
    }
}
