using ApiCorePulseGaming.Models;
using ApiCorePulseGaming.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCorePulseGaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IRepositoryUsuarios repoUsuarios;

        public UsuariosController(IRepositoryUsuarios repoUsuarios)
        {
            this.repoUsuarios = repoUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await this.repoUsuarios.GetUsuariosAsync();
        }

        [HttpGet("[action]/{email}/{password}")]
        public async Task<ActionResult<Usuario>> Login(string email, string password)
        {
            return await this.repoUsuarios.LogInUserAsync(email, password);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register(Usuario usu)
        {
            await this.repoUsuarios.RegisterUserAsync(usu.Nombre, usu.Apellidos, usu.Email, usu.Password.ToString(), usu.Telefono, usu.IDRole);
            return Ok();
        }
    }
}
