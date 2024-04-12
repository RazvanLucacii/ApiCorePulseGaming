using ApiCorePulseGaming.Models;
using ApiCorePulseGaming.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCorePulseGaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuegosController : ControllerBase
    {
        private IRepositoryJuegos repo;

        public JuegosController(IRepositoryJuegos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Juego>>> GetJuegos()
        {
            return await this.repo.GetJuegosAsync();
        }

        [HttpGet("[action]/{posicion}")]
        public async Task<ActionResult<List<Juego>>> GetPaginacionJuegos(int posicion)
        {
            return await this.repo.GetGrupoJuegosAsync(posicion);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Juego>> GetJuego(int id)
        {
            return await this.repo.FindJuegoAsync(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<List<Juego>>> GetJuegosGeneros(int id)
        {
            return await this.repo.GetJuegosGenerosAsync(id);
        }


    }
}
