namespace Entities.DTOs
{
    using System.Collections.Generic;

    /// <summary>
    /// Lista generica
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericListDTO<T>
    {
        /// <summary>
        /// Lista de objeto generico
        /// </summary>
        public List<T> Lista { get; set; }

        /// <summary>
        /// total de filas recuperadas
        /// </summary>
        public ulong? TotalFilas { get; set; }

        /// <summary>
        /// Cantidad de paginas totales
        /// </summary>
        public ulong? CantidadPaginas { get; set; }
    }
}