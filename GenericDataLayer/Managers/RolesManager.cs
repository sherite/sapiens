namespace GenericDataLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Entities.Helpers;
    using GenericDataLayer.Helpers;
    using static Entities.Enums;
    /// <summary>
    /// Manager de Planos
    /// </summary>
    public class RolesManager : IDisposable
    {
        bool disposed = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public RolesManager()
        {

        }

        /// <summary>
        /// Busca un plano determinado
        /// </summary>
        /// <param name="id">id de plano a buscar</param>
        /// <returns>lista con el plano correspondiente al id pasado como parametro o vacia</returns>
        public List<Rol> Find(ulong id)
        {
            object[] parametros = new object[] { id };

            var lstRol = ReturnListaGenerica("Roles", "Find", parametros);

            return lstRol;
        }

        /// <summary>
        /// Selecciona una lista de planos
        /// </summary>
        /// <param name="paginado">objeto paginado</param>
        /// <param name="orden">objeto ordenamiento</param>
        /// <param name="idRol">id del plano</param>
        /// <returns></returns>
        public List<Rol> Select(Paging paginado, Ordering orden, ulong? idRol)
        {
            var rol = new Rol();

            var lstRol = new SPS_SDL_SQL.Roles().Select(paginado, orden, idRol);

            var retVal = new List<Rol>();

            foreach (var item in lstRol)
            {
                rol = new Rol()
                {
                    ID = item.ID,
                    ID_Estado = item.ID_Estado,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion
                };

                retVal.Add(rol);
            }

            return retVal;
        }

        /// <summary>
        /// Crea un nuevo plano
        /// </summary>
        /// <param name="rol">plano a insertar</param>
        /// <returns>plano insertado</returns>
        public MgrResponse<Rol> Insert(Rol rol)
        {
            var mgrResponse = new MgrResponse<Rol>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(rol);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Roles().Insert(param);

            var miRol = new Rol()
            {
                ID = result.ID,
                ID_Estado = (RolStatus)result.ID_Estado,
                Nombre = result.Nombre,
                Descripcion = result.Descripcion
            };

            mgrResponse.Object = miRol;

            return mgrResponse;
        }

        /// <summary>
        /// Actualiza un plano
        /// </summary>
        /// <param name="rol">plano a actualizar</param>
        /// <returns>plano actualizado</returns>
        public MgrResponse<Rol> Update(Rol rol)
        {
            var mgrResponse = new MgrResponse<Rol>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(rol);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Roles().Update(param);

            rol.ID = result.ID;
            rol.Nombre = result.Nombre;
            rol.Descripcion = result.Descripcion;
            rol.ID_Estado = result.ID_Estado;


            new SPS_SDL_SQL.UserRoles().DeleteUsers(rol.ID.Value);

            foreach (var user in rol.Users)
            {
                new SPS_SDL_SQL.UserRoles().Insert(user.ID, (int)rol.ID);
            }

            mgrResponse.Object = rol;

            return mgrResponse;

        }

        /// <summary>
        /// Elimina lógicamente un rol
        /// </summary>
        /// <param name="idRol">id del plano a eliminar</param>
        /// <returns>estado de la eliminacion</returns>
        public RolStatus Delete(ulong idRol)
        {
            var retVal = new SPS_SDL_SQL.Roles().Delete((int)idRol);

            return RolStatus.Inactive;
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
        public List<Rol> ReturnListaGenerica(string entidad, string metodo, object[] parametros)
        {
            var retVal = new List<Rol>();


            return retVal;
        }
    }
}