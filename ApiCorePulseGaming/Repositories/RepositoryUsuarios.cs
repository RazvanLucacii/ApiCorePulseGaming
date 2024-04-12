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

        public async Task RegisterUserAsync(string nombre, string apellidos, string email, string password, int telefono, int IDRole)
        {
            Usuario user = new Usuario();
            user.IdUsuario = await this.GetMaxIdUsuarioAsync();
            user.Apellidos = apellidos;
            user.Nombre = nombre;
            user.Email = email;
            user.Telefono = telefono;
            user.IDRole = IDRole;
            user.Salt = HelperJuegos.GenerateSalt();
            user.Password =
                HelperJuegos.EncryptPassword(password, user.Salt);
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> LogInUserAsync(string email, string password)
        {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp =
                    HelperJuegos.EncryptPassword(password, salt);
                byte[] passUser = user.Password;
                bool response =
                    HelperJuegos.CompareArrays(temp, passUser);
                if (response == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            string sql = "SP_TODOS_USUARIOS";
            var consulta = this.context.Usuarios.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<Usuario> FindUsuarioByIdAsync(int idUsuario)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.IdUsuario == idUsuario);
        }

        public async Task ModificarUsuarioAsync(int idUsuario, string nombre, string apellidos, string email, string password, int telefono, int IDRole)
        {
            Usuario usuario = await FindUsuarioByIdAsync(idUsuario);
            if (usuario == null)
            {
                throw new ArgumentNullException("El usuario no existe.");
            }

            // Actualiza los datos del usuario con los nuevos valores
            usuario.Nombre = nombre;
            usuario.Apellidos = apellidos;
            usuario.Email = email;
            usuario.Telefono = telefono;
            usuario.IDRole = IDRole;

            // Si se proporciona una nueva contraseña, la actualiza
            if (!string.IsNullOrEmpty(password))
            {
                usuario.Salt = HelperJuegos.GenerateSalt();
                usuario.Password = HelperJuegos.EncryptPassword(password, usuario.Salt);
            }

            // Guarda los cambios en la base de datos
            this.context.Entry(usuario).State = EntityState.Modified;
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
