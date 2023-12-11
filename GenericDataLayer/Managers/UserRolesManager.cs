namespace GenericDataLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Entities.Helpers;
    using Entities.DTOs;
    using static Entities.Enums;
    using System.Threading.Tasks;

    public class UserRolesManager : IDisposable
    {
        bool disposed = false;

        public UserRolesManager() { }

        /// <summary>
        /// Search for a role
        /// </summary>
        /// <param name="id">role id to search</param>
        /// <returns>list with the role</returns>
        public List<Rol> Find(ulong id)
        {
            var parameters = new object[] { id };

            var lstRole = ReturnListaGenerica("Roles", "Find", parameters);

            return lstRole;
        }

        public IList<UsuarioGetRequestDTO> SelectUsers(int idRole)
        {
            var usuario = new UsuarioGetRequestDTO();

            var lstUser = new SPS_SDL_SQL.UserRoles().SelectUsers(idRole);

            var retVal = new List<UsuarioGetRequestDTO>();

            foreach (var item in lstUser)
            {
                usuario = new UsuarioGetRequestDTO()
                {
                    ID = item.ID,
                    LastName = item.LastName,
                    Name = item.Name,
                    Alias = item.Alias,
                    Status = item.Status
                };

                retVal.Add(usuario);
            }

            return retVal;
        }

        public async Task<List<Rol>> Select(int idUsuario)
        {
            var rol = new Rol();

            var lstGrupo = await new SPS_SDL_SQL.UserRoles().Select(idUsuario);

            var retVal = new List<Rol>();

            foreach (var item in lstGrupo)
            {
                rol = new Rol()
                {
                    ID = item.ID,
                    ID_Estado = (RolStatus)item.ID_Estado,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion
                };

                retVal.Add(rol);
            }

            return retVal;
        }

        /// <summary>
        /// Crea un nuevo grupo
        /// </summary>
        /// <param name="rol">grupo a insertar</param>
        /// <returns>grupo insertado</returns>
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
        /// Actualiza un grupo
        /// </summary>
        /// <param name="rol">grupo a actualizar</param>
        /// <returns>grupo actualizado</returns>
        public MgrResponse<Rol> Update(Rol rol)
        {
            var mgrResponse = new MgrResponse<Rol>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(rol);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Roles().Update(param);

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
        /// Elimina lógicamente un grupo
        /// </summary>
        /// <param name="idRol">id del grupo a eliminar</param>
        /// <returns>estado de la eliminacion</returns>
        public RolStatus Delete(int idRol)
        {
            new SPS_SDL_SQL.Roles().Delete(idRol);

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