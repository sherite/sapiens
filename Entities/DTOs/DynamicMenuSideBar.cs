using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DTOs
{
    public class DynamicMenuSideBar
    {
        [Key]
        public int? ID { get; set; }
        public int? Modulo { get; set; }
        public int? Padre { get; set; }
        public int? Orden { get; set; }
        public int? Tipo { get; set; }
        public string Accion { get; set; }
        public string Texto { get; set; }
    }
}
