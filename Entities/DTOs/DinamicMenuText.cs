using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class Menus_Texto
    {
        [Key]
        public int ID { get; set; }
        public int Idioma { get; set; }
        public string Texto { get; set; }
    }
}
