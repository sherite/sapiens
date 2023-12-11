namespace GenericDataLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class DynamicMenuSideBarManager : IDisposable
    {
        public static string menu { get; set; } = "";
        public static int currRec = 0;
        public MSSQLDataLayer.DynamicMenuSideBar DynamicMenuSideBar { get; set; }

        public DynamicMenuSideBarManager(MSSQLDataLayer.DynamicMenuSideBar dynamicMenuSideBar)
        {
            this.DynamicMenuSideBar = dynamicMenuSideBar ?? new MSSQLDataLayer.DynamicMenuSideBar();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<string> Select()
        {
            var retVal = new List<string>();
            var menues = DynamicMenuSideBar.Select().ToList();
            var modulos = menues.AsEnumerable().Where(a => a.Padre == 0);

            foreach (var item in modulos)
            {
                menu = string.Empty;
                ArmarMenu(menues, item.Modulo.Value, item.ID.Value, 0);
                retVal.Add(menu);
            }

            return retVal;
        }

        static void ArmarMenu(List<Entities.DTOs.DynamicMenuSideBar> aMenu, int modulo, int id, int padre)
        {
            foreach (var elemento in aMenu)
            {
                if (elemento.Modulo == modulo && elemento.ID == id && elemento.Padre == padre && elemento.Tipo < 3)
                {
                    var eText = "'" + id.ToString() + "_" + modulo.ToString() + "_" + padre.ToString() + "'";
                    var menuID = (elemento.Tipo < 3) ? "id =" + eText : string.Empty;

                    menu += "<li class='treeview'>";
                    menu += "   <a href=''>";
                    menu += "       <i  " + menuID + " class='fa fa-folder'></i>";
                    menu += "       <span>" + elemento.Texto + "</span>";
                    menu += "       <span class='pull-right-container'>";
                    menu += "           <i class='fa fa-angle-left pull-right'></i>";
                    menu += "       </span>";
                    menu += "   </a>";

                    menu += "   <ul data-widget='tree' name='ul' name=" + eText + " class='treeview-menu'>";

                    var miSubMenu = aMenu.Where(a => a.Padre == elemento.ID).ToList();

                    foreach (var item in miSubMenu)
                    {
                        var icono = item.Tipo == 3 ? "fa fa-newspaper-o" : item.Tipo == 4 ? "fa fa-file-text-o" : "fa fa-gears";

                        if (item.Tipo > 2)
                            menu += "<li><a href='#" + item.Accion + "'><i class='" + icono + "'></i><span>" + item.Texto + "</span></a></li >";
                        else
                            ArmarMenu(aMenu, item.Modulo.Value, item.ID.Value, item.Padre.Value);
                    }

                    menu += "</ul></li>";
                }
            }
        }
    }
}