namespace MSSQLDataLayer
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Models;

    /// <summary>
    /// DynamicMenuSideBar data layer
    /// </summary>
    public class DynamicMenuSideBar
    {
        /// <summary>
        /// select method
        /// </summary>CON
        public IList<Entities.DTOs.DynamicMenuSideBar> Select()
        {
            var result = new List<Entities.DTOs.DynamicMenuSideBar>();

            try
            {
                using (BaseDatos bd = new BaseDatos())
                {
                    var reader = new AppSettingsReader();

                    var idioma = (string)reader.GetValue("Language", typeof(string));

                    var query = from s in bd.Menus.AsEnumerable()
                                join a in bd.MenusTexto on s.ID equals a.ID
                                where a.Idioma.ToString() == idioma
                                select new Entities.DTOs.DynamicMenuSideBar()
                                {
                                    ID = s.ID.Value,
                                    Modulo = s.MODULO.Value,
                                    Padre = s.PADRE.Value,
                                    Orden = s.ORDEN.Value,
                                    Tipo = s.TIPO.Value,
                                    Accion = s.ACCION,
                                    Texto = a.Texto
                                };


                    result = query.DefaultIfEmpty().ToList();
                }
            }
            catch (System.Exception e)
            {

                var a = e;
                throw;
            }

            return result;
        }
    }
}