using System.Collections.Generic;

namespace Entities.DTOs
{
    /// <summary>
    /// DTO de Usuario
    /// </summary>
    public class UserDTO : UsuarioGetRequestDTO
    {
       /// <summary>
        /// Grupos a los que pertenece el usuario.
        /// </summary>
        public IList<Group> Groups { get; set; }

        public IList<Rol> Roles { get; set; }
    }
}