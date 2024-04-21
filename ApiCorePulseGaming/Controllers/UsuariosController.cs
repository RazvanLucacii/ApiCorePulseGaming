using ApiCorePulseGaming.Models;
using ApiCorePulseGaming.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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

        /// <summary>
        /// Obtiene una lista de Usuarios, tabla Usuarios.
        /// </summary>
        /// <remarks>
        /// Permite buscar todos los Usuarios
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await this.repoUsuarios.GetUsuariosAsync();
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> PerfilUsuario()
        {
            Claim claim = HttpContext.User.FindFirst(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return usuario;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register(Usuario usu)
        {
            await this.repoUsuarios.RegisterUserAsync(usu.Nombre, usu.Password, usu.Apellidos, usu.Email, usu.Telefono, usu.IDRole);
            return Ok();
        }
    }
}
