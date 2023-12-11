namespace Entities.DTOs
{
    /// <summary>
    /// DTO de Plano
    /// </summary>
    public class PlanoDTO
    {
        /// <summary>
        /// Identificador unico del usuario
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Estado de usuario
        /// </summary>
        public Entities.Enums.BluePrintStaus ID_Estado { get; set; }
    }
}