namespace Entities
{
    /// <summary>
    /// modelo de permiso
    /// </summary>
    public class Permiso
    {
        /// <summary>
        /// Identificador unico del permiso
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Nombre del permiso
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del permiso
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Estado de permiso
        /// </summary>
        public Enums.RightStatus ID_Estado { get; set; }
    }
}