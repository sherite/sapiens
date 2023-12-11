namespace Entities.Helpers
{
    /// <summary>
    /// objeto de ordenamiento
    /// </summary>
    public class Ordering
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="ordenamiento"></param>
        public Ordering(string columna, OrderingDB ordenamiento) 
        {
            this.OrderingDTO = new OrderingDTO()
            {
                OrderingRow = columna,
                OrdinationSense = ordenamiento
            };
        }

        /// <summary>
        /// objeto de ordenamiento
        /// </summary>
        public OrderingDTO OrderingDTO { get; set; }
    }

    /// <summary>
    /// DTO de ordenamiento
    /// </summary>
    public class OrderingDTO
    {
        /// <summary>
        /// columna a ordenar
        /// </summary>
        public string OrderingRow { get; set; }

        /// <summary>
        /// sentido de ordenamiento
        /// </summary>
        public OrderingDB OrdinationSense { get; set; }
    }

    /// <summary>
    /// enum de ordenamiento
    /// </summary>
    public enum OrderingDB
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