using Entities.DTOs;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// modelo de grupo
    /// </summary>
    public class Rol
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
        public Enums.RolStatus ID_Estado { get; set; }

        /// <summary>
        /// lista de usuarios
        /// </summary>
        public IList<UsuarioGetRequestDTO> Users { get; set; }

    }
}