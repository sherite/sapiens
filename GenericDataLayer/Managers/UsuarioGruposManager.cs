namespace GenericDataLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Entities.Helpers;
    using Entities.DTOs;
    using static Entities.Enums;
    using System.Threading.Tasks;

    public class UserGroupsManager : IDisposable
    {
        bool disposed = false;

        public UserGroupsManager() { }

        /// <summary>
        /// Busca un grupo determinado
        /// </summary>
        /// <param name="id">id de grupo a buscar</param>
        /// <returns>lista con el grupo correspondiente al id pasado como parametro o vacia</returns>
        public List<Group> Find(ulong id)
        {
            object[] parametros = new object[] { id };

            var lstGrupo = ReturnListaGenerica("Grupos", "Find", parametros);

            return lstGrupo;
        }

        public IList<UsuarioGetRequestDTO> SelectUsers(int idGroup)
        {
            var usuario = new UsuarioGetRequestDTO();

            var lstUser = new SPS_SDL_SQL.UserGroups().SelectUsers(idGroup);

            var retVal = new List<UsuarioGetRequestDTO>();

            foreach(var item in lstUser)
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

        public async Task<IList<Group>> Select(int idUsuario)
        {
            var grupo = new Group();

            var lstGrupo = await new SPS_SDL_SQL.UserGroups().Select(idUsuario);

            var retVal = new List<Group>();

            foreach (var item in lstGrupo)
            {
                grupo = new Group()
                {
                    ID = item.ID,
                    Status = item.Status,
                    Name = item.Name,
                    Description = item.Description
                };

                retVal.Add(grupo);
            }

            return retVal;
        }

        /// <summary>
        /// Crea un nuevo grupo
        /// </summary>
        /// <param name="grupo">grupo a insertar</param>
        /// <returns>grupo insertado</returns>
        public MgrResponse<Group> Insert(Group grupo)
        {
            var mgrResponse = new MgrResponse<Group>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(grupo);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Groups().Insert(param);

            var miGrupo = new Group()
            {
                ID = result.ID,
                Status = (GroupStatus)result.Status,
                Name = result.Name,
                Description = result.Description
            };

            mgrResponse.Object = miGrupo;

            return mgrResponse;
        }

        /// <summary>
        /// Actualiza un grupo
        /// </summary>
        /// <param name="grupo">grupo a actualizar</param>
        /// <returns>grupo actualizado</returns>
        public MgrResponse<Group> Update(Group grupo)
        {
            var mgrResponse = new MgrResponse<Group>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(grupo);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Groups().Update(param);

            var miGrupo = new Group()
            {
                ID = result.ID,
                Status = (GroupStatus)result.Status,
                Name = result.Name,
                Description = result.Description
            };

            mgrResponse.Object = miGrupo;

            return mgrResponse;
        }

        /// <summary>
        /// Elimina lógicamente un grupo
        /// </summary>
        /// <param name="idGrupo">id del grupo a eliminar</param>
        /// <returns>estado de la eliminacion</returns>
        public GroupStatus Delete(int idGrupo)
        {
            new SPS_SDL_SQL.Groups().Delete(idGrupo);

            return GroupStatus.Inactive;
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
        public List<Group> ReturnListaGenerica(string entidad, string metodo, object[] parametros)
        {
            var retVal = new List<Group>();


            return retVal;
        }
    }
}