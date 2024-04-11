using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiCorePulseGaming.Models
{
    [Table("Editor")]
    public class Editor
    {
        [Key]
        [Column("IDEditor")]
        public int IDEditor { get; set; }
        [Column("NombreEditor")]
        public string NombreEditor { get; set; }
    }
}
