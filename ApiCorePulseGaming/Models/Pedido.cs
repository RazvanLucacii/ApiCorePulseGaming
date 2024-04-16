using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiCorePulseGaming.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        [Column("IDPedido")]
        public int IDPedido { get; set; }

        [Column("FechaPedido")]
        public DateTime FechaPedido { get; set; }

        [Column("IDUsuario")]
        public int IDUsuario { get; set; }

        [Column("Total")]
        public decimal Total { get; set; }
    }
}
