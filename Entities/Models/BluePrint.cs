namespace Entities
{
    /// <summary>
    /// modelo de plano
    /// </summary>
    public class BluePrint
    {
        /// <summary>
        /// identificador unico
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// nombre del plano
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// estado del plano
        /// </summary>
        public Enums.BluePrintStaus ID_Estado { get; set; }
    }
}