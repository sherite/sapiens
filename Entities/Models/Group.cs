using Entities.DTOs;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// modelo de grupo
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Identificador unico del usuario
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Estado de usuario
        /// </summary>
        public Enums.GroupStatus Status { get; set; }

        /// <summary>
        /// lista de usuarios
        /// </summary>
        public IList<UsuarioGetRequestDTO> Users { get; set; }
    }
}