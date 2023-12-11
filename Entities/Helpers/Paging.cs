namespace Entities.Helpers
{
    /// <summary>
    /// paginado de los select
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// pagina a recuperar
        /// </summary>
        public ulong Pagina { get; set; }

        /// <summary>
        /// filas a recuperar
        /// </summary>
        public ulong Filas { get; set; }

        /// <summary>
        /// total de registros
        /// </summary>
        public ulong TotalFilas { get; set; }

        /// <summary>
        /// total de paginas
        /// </summary>
        public ulong CantidadPaginas { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pagina">pagina a recuperar</param>
        /// <param name="filas">filas por pagina</param>
        /// <param name="totalFilas">contador de filas</param>
        public Paging(ulong pagina, ulong filas, ulong totalFilas )
        {
            this.PaginadoDTO.Page = pagina;
            this.PaginadoDTO.Rows = filas;
            this.PaginadoDTO.TotalRows = totalFilas;
        }

        /// <summary>
        /// paginado DTO
        /// </summary>
        public PaginedDTO PaginadoDTO { get; set; }
    }

    /// <summary>
    /// DTO de paginado
    /// </summary>
    public class PaginedDTO
    {
        /// <summary>
        /// Estado de habilitacion del paginado
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// pagina a devolver
        /// </summary>
        public ulong Page { get; set; }

        /// <summary>
        /// Cantidad de filas por pagina
        /// </summary>
        public ulong Rows { get; set; }

        /// <summary>
        /// Cantidad de paginas basada en las filas por pagina y la totalidad de las filas
        /// </summary>
        public ulong TotalPages { get; set; }

        /// <summary>
        /// Total de filas encontradas
        /// </summary>
        public ulong TotalRows { get; set; }

    }
}