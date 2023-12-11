namespace Entities
{
    using System.Collections.Generic;
    using System.Web.Http;

    public interface ICRUDWeb
    {
        IHttpActionResult Insert<T>(IList<T> model);
        IHttpActionResult Delete<T>(IList<T> model);
        IHttpActionResult Update<T>(IList<T> model);
        IHttpActionResult Select<T>(IList<T> model);
    }
}