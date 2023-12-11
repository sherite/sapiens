using System.Collections.Generic;

namespace Entities.DTOs
{
    /// <summary>
    /// DTO de Usuario
    /// </summary>
    public class RolDTO : RoleGetRequestDTO
    {
        /// <summary>
        /// Lista de Usuarios
        /// </summary>
        public IList<UsuarioGetRequestDTO> Users { get; set; }
    }
}