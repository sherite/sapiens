namespace Entities
{
    /// <summary>
    /// Objeto para guardar datos de auditoria
    /// </summary>
    public class Auditable
    {
        /// <summary>
        /// identificador unico de auditoria
        /// </summary>
        public ulong IDAuditoria { get; set; }


        /// <summary>
        /// Version de la fila
        /// </summary>
        public ulong RowVersion { get; set; }
    }
}