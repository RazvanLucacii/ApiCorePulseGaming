using ApiCorePulseGaming.Data;
using ApiCorePulseGaming.Helpers;
using ApiCorePulseGaming.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCorePulseGaming.Repositories
{
    public class RepositoryUsuarios : IRepositoryUsuarios
    {
        private JuegosContext context;

        public RepositoryUsuarios(JuegosContext context)
        {
            this.context = context;
        }

        public async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.Usuarios.MaxAsync(z => z.IdUsuario) + 1;
            }
        }

        public async Task RegisterUserAsync(string nombre, string password, string apellidos, string email, int telefono, int IDRole)
        {
            Usuario user = new Usuario();
            user.IdUsuario = await this.GetMaxIdUsuarioAsync();
            user.Password = password;
            user.Apellidos = apellidos;
            user.Nombre = nombre;
            user.Email = email;
            user.Telefono = telefono;
            user.IDRole = IDRole;
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> LogInUserAsync(string email, string password)
        {
            return await this.context.Usuarios.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await this.context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> FindUsuarioByIdAsync(int idUsuario)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.IdUsuario == idUsuario);
        }

        public async Task ModificarUsuarioAsync(int idUsuario, string password, string nombre, string apellidos, string email, int telefono, int IDRole)
        {
            Usuario usuario = await FindUsuarioByIdAsync(idUsuario);
            if (usuario == null)
            {
                throw new ArgumentNullException("El usuario no existe.");
            }

            // Actualiza los datos del usuario con los nuevos valores
            usuario.Nombre = nombre;
            usuario.Password = password;
            usuario.Apellidos = apellidos;
            usuario.Email = email;
            usuario.Telefono = telefono;
            usuario.IDRole = IDRole;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(int idUsuario)
        {
            Usuario usuario = await this.FindUsuarioByIdAsync(idUsuario);
            this.context.Usuarios.Remove(usuario);
            await this.context.SaveChangesAsync();
        }
    }
}
