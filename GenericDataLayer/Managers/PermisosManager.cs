namespace GenericDataLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Entities.Helpers;
    using GenericDataLayer.Helpers;
    using static Entities.Enums;

    /// <summary>
    /// Manager de permisos
    /// </summary>
    public class RightsManager : IDisposable
    {
        bool disposed = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public RightsManager()
        {

        }

        /// <summary>
        /// Busca un permiso determinado
        /// </summary>
        /// <param name="id">id de permiso a buscar</param>
        /// <returns>lista con el permiso correspondiente al id pasado como parametro o vacia</returns>
        public List<Permiso> Find(ulong id)
        {
            object[] parametros = new object[] { id };

            var lstPermiso = ReturnListaGenerica("Permisos", "Find", parametros);

            return lstPermiso;
        }

        /// <summary>
        /// Selecciona una lista de permisos
        /// </summary>
        /// <param name="paginado">objeto paginado</param>
        /// <param name="orden">objeto ordenamiento</param>
        /// <param name="idPermiso">id del permiso</param>
        /// <returns></returns>
        public List<Permiso> Select(Paging paginado, Ordering orden, ulong? idPermiso)
        {
            var permiso = new Permiso();

            var lstPermiso = new SPS_SDL_SQL.Permisos().Select(paginado, orden, idPermiso);

            var retVal = new List<Permiso>();

            foreach (var item in lstPermiso)
            {
                permiso = new Permiso()
                {
                    ID = item.ID,
                    ID_Estado = item.ID_Estado,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion
                };

                retVal.Add(permiso);
            }

            return retVal;
        }

        /// <summary>
        /// Crea un nuevo permiso
        /// </summary>
        /// <param name="permiso">permiso a insertar</param>
        /// <returns>permiso insertado</returns>
        public MgrResponse<Permiso> Insert(Permiso permiso)
        {
            var mgrResponse = new MgrResponse<Permiso>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(permiso);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Permisos().Insert(param);

            var miPermiso = new Permiso()
            {
                ID = result.ID,
                ID_Estado = (RightStatus)result.ID_Estado,
                Nombre = result.Nombre,
                Descripcion = result.Descripcion
            };

            mgrResponse.Object = miPermiso;

            return mgrResponse;
        }

        /// <summary>
        /// Actualiza un permiso
        /// </summary>
        /// <param name="permiso">permiso a actualizar</param>
        /// <returns>permiso actualizado</returns>
        public MgrResponse<Permiso> Update(Permiso permiso)
        {
            var mgrResponse = new MgrResponse<Permiso>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(permiso);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Permisos().Update(param);

            var miPermiso = new Permiso()
            {
                ID = result.ID,
                ID_Estado = (RightStatus)result.ID_Estado,
                Nombre = result.Nombre,
                Descripcion = result.Descripcion
            };

            mgrResponse.Object = miPermiso;

            return mgrResponse;
        }

        /// <summary>
        /// Elimina lógicamente un grupo
        /// </summary>
        /// <param name="idPermiso">id del grupo a eliminar</param>
        /// <returns>estado de la eliminacion</returns>
        public RightStatus Delete(ulong idPermiso)
        {
            var retVal = new SPS_SDL_SQL.Permisos().Delete((int)idPermiso);

            return RightStatus.Inactive;
        }

        /// <summary>
        /// Dispose this object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// implementation of dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        /// <summary>
        /// carga dll
        /// </summary>
        public List<Permiso> ReturnListaGenerica(string entidad, string metodo, object[] parametros)
        {
            var retVal = new List<Permiso>();


            return retVal;
        }
    }
}