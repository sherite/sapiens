namespace Entities.DTOs
{
    /// <summary>
    /// DTO de Permisos
    /// </summary>
    public class RightDTO
    {
        /// <summary>
        /// Identificador unico
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Nombre del permisousuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del permiso
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Estado de permiso
        /// </summary>
        public Entities.Enums.RightStatus ID_Estado { get; set; }
    }
}