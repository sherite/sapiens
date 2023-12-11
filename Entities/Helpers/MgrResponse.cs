using System.Collections.Generic;
using static Entities.Enums;

namespace Entities.Helpers
{

    /// <summary>
    /// Manager de Respuestas
    /// </summary>
    public class MgrResponse<T>
    {
        /// <summary>
        /// Lista de Errores
        /// </summary>
        public Dictionary<string, string> Errores { get; set; }

        /// <summary>
        /// Objeto a devolver
        /// </summary>
        public T Object { get; set; }

        /// <summary>
        /// Estado de la respuesta
        /// </summary>
        public RespStatusCodeGeneric Status { get; set; }
    }
}