namespace Entities.DTOs
{
    public class UsuarioGetRequestDTO
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
        public Entities.Enums.UserStatus Status { get; set; }
    }
}