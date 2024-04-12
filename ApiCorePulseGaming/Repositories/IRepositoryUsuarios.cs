using ApiCorePulseGaming.Models;

namespace ApiCorePulseGaming.Repositories
{
    public interface IRepositoryUsuarios
    {
        Task<int> GetMaxIdUsuarioAsync();
        Task RegisterUserAsync(string nombre, string apellidos, string email, string password, int telefono, int IDRole);
        Task<Usuario> LogInUserAsync(string email, string password);
        Task<List<Usuario>> GetUsuariosAsync();
        Task<Usuario> FindUsuarioByIdAsync(int idUsuario);
        Task ModificarUsuarioAsync(int idUsuario, string nombre, string apellidos, string email, string password, int telefono, int IDRole);
        Task DeleteUsuarioAsync(int idUsuario);
    }
}
