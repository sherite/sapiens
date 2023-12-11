using System.Collections.Generic;

namespace Entities.DTOs
{
    /// <summary>
    /// DTO de Grupo
    /// </summary>
    public class GroupDTO : GroupGetRequestDTO
    {
        /// <summary>
        /// Lista de Usuarios
        /// </summary>
        public IList<UsuarioGetRequestDTO> Users { get; set; }
    }
}