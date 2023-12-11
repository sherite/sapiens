using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// modelo de usuario
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador unico del usuario
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Alias con que se conoce al usuario en el sistema
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Estado de usuario
        /// </summary>
        public Enums.UserStatus Status { get; set; }

        /// <summary>
        /// Grupos a los que pertenece el usuario.
        /// </summary>
        public IList<Group> Groups { get; set; }

        public IList<Rol> Roles { get; set; }
    }
}