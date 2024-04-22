namespace ApiCorePulseGaming.Models
{
    public class PedidoJuegos
    {
        public Pedido? pedido { get; set; }
        public List<Juego>? carrito {  get; set; }
    }
}
