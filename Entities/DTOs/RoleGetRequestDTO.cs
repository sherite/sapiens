using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RoleGetRequestDTO
    {
        /// <summary>
        /// Identificador unico del usuario
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Estado de usuario
        /// </summary>
        public Entities.Enums.RolStatus ID_Estado { get; set; }

    }
}
