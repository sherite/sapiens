using Entities.Helpers;
using System.Web.Http.ModelBinding;

namespace Entities.Helpers
{
    /// <summary>
    /// Extensiones de ModelState
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// aaa
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static MgrResponse<T> ToMgrResponse<T>(this ModelStateDictionary modelState)
        {
            var mgrResponse = new MgrResponse<T>();

            return mgrResponse;
        }
    }
}