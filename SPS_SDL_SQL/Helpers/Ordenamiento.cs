namespace SPS_SDL_SQL
{
    /// <summary>
    /// objeto de ordenamiento
    /// </summary>
    public class Ordenamiento
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="ordenamiento"></param>
        public Ordenamiento(string columna, OrdenamientoDB ordenamiento) 
        {
            this.OrdenamientoDTO = new OrdenamientoDTO()
            {
                ColumnaOrdenamiento = columna,
                SentidoOrdenamiento = ordenamiento
            };
        }

        /// <summary>
        /// objeto de ordenamiento
        /// </summary>
        public OrdenamientoDTO OrdenamientoDTO { get; set; }
    }

    /// <summary>
    /// DTO de ordenamiento
    /// </summary>
    public class OrdenamientoDTO
    {
        /// <summary>
        /// columna a ordenar
        /// </summary>
        public string ColumnaOrdenamiento { get; set; }

        /// <summary>
        /// sentido de ordenamiento
        /// </summary>
        public OrdenamientoDB SentidoOrdenamiento { get; set; }
    }

    /// <summary>
    /// enum de ordenamiento
    /// </summary>
    public enum OrdenamientoDB
    {
        /// <summary>
        /// Orden ascendente
        /// </summary>
        ASC = 0,

        /// <summary>
        /// Orden descendente
        /// </summary>
        DESC = 1
    }

}