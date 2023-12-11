using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DTOs
{
    public class MENUS
    {
        [Key]
        public int? ID { get; set; }
        public int? MODULO { get; set; }
        public int? PADRE { get; set; }
        public int? ORDEN { get; set; }
        public int? TIPO { get; set; }
        public string ACCION { get; set; }
        public int? ID_AUDITORIA { get; set; }
        public int? VERSION { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }
        public System.DateTime ULTIMA_MODIFICACION { get; set; }
    }
}
