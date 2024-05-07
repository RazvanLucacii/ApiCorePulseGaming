using ApiCorePulseGaming.Models;
using ApiCorePulseGaming.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCorePulseGaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IRepositoryJuegos repo;
        private IRepositoryUsuarios repoUsuarios;

        public AdminController(IRepositoryJuegos repo, IRepositoryUsuarios repoUsuarios)
        {
            this.repo = repo;
            this.repoUsuarios = repoUsuarios;
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<List<Genero>>> GetGeneros()
        {
            return await this.repo.GetGenerosAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Editor>>> GetEditores()
        {
            return await this.repo.GetEditoresAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await this.repoUsuarios.GetUsuariosAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Juego>>> GetJuegos()
        {
            return await this.repo.GetJuegosAsync();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Juego>> GetJuego(int id)
        {
            return await this.repo.FindJuegoAsync(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Editor>> GetEditor(int id)
        {
            return await this.repo.FindEditorAsync(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            return await this.repo.FindGeneroAsync(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            return await this.repoUsuarios.FindUsuarioByIdAsync(id);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult> InsertUsuario(Usuario usu)
        {
            await this.repoUsuarios.RegisterUserAsync(usu.Nombre, usu.Apellidos, usu.Email, usu.Password.ToString(), usu.Telefono, usu.IDRole);
            return Ok();
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult> InsertEditor(Editor editor)
        {
            await this.repo.CrearEditorAsync(editor.NombreEditor);
            return Ok();
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult> InsertGenero(Genero genero)
        {
            await this.repo.CrearGeneroAsync(genero.NombreGenero);
            return Ok();
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult> InsertJuego(Juego juego)
        {
            await this.repo.RegistrarJuego(juego.NombreJuego, juego.IDGenero, juego.ImagenJuego, juego.PrecioJuego, juego.Descripcion, juego.IdEditor);
            return Ok();
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateJuego(Juego juego)
        {
            await this.repo.ModificarJuegoAsync(juego.IdJuego, juego.NombreJuego, juego.IDGenero, juego.ImagenJuego, juego.PrecioJuego, juego.Descripcion, juego.IdEditor);
            return Ok();
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateEditor(Editor editor)
        {
            await this.repo.ModificarEditorAsync(editor.IDEditor, editor.NombreEditor);
            return Ok();
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateGenero(Genero genero)
        {
            await this.repo.ModificarGeneroAsync(genero.IdGenero, genero.NombreGenero);
            return Ok();
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateUsuario(Usuario usu)
        {
            await this.repoUsuarios.ModificarUsuarioAsync(usu.IdUsuario, usu.Nombre, usu.Apellidos, usu.Email, usu.Password.ToString(), usu.Telefono, usu.IDRole);
            return Ok();
        }

        [Authorize]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteJuego(int id)
        {
            if (await this.repo.FindJuegoAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                await this.repo.DeleteJuegoAsync(id);
                return Ok();
            }
        }

        [Authorize]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteGenero(int id)
        {
            if (await this.repo.FindGeneroAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                await this.repo.DeleteGeneroAsync(id);
                return Ok();
            }
        }

        [Authorize]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteEditor(int id)
        {
            if (await this.repo.FindEditorAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                await this.repo.DeleteEditorAsync(id);
                return Ok();
            }
        }

        [Authorize]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            if (await this.repoUsuarios.FindUsuarioByIdAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                await this.repoUsuarios.DeleteUsuarioAsync(id);
                return Ok();
            }
        }


    }
}
