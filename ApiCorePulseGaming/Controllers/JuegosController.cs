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

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Juego>>> GetJuegosPrecioDesc()
        {
            return await this.repo.GetJuegosPrecioDescAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Juego>>> GetJuegosPrecioAsce()
        {
            return await this.repo.GetJuegosPrecioAsceAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetNumeroJuegos()
        {
            return await this.repo.GetNumeroJuegosAsync();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<List<DetallePedidoView>>> GetProductosPedidoUsuario(int id)
        {
            return await this.repo.GetProductosPedidoUsuarioAsync(id);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetMaxIdDetallePedido()
        {
            return await this.repo.GetMaxIdDetallePedidoAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetMaxIdPedido()
        {
            return await this.repo.GetMaxIdPedidoAsync();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> InsertPedido([FromQuery] Pedido pedido, List<Juego> carrito)
        {
            await this.repo.CreatePedidoAsync(pedido.IDUsuario, carrito);
            return Ok();
        }
    }
}
