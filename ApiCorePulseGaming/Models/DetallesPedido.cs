using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiCorePulseGaming.Models
{
    [Table("DetallesPedido")]
    public class DetallesPedido
    {
        [Key]
        [Column("IDDetallePedido")]
        public int IDDetallePedido { get; set; }

        [Column("IDJuego")]
        public int IDJuego { get; set; }

        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Column("IDPedido")]
        public int IDPedido { get; set; }

        [Column("PrecioUnitario")]
        public decimal PrecioUnitario { get; set; }
    }
}
