using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using GenericDataLayer.Configuration;
using Microsoft.Win32.SafeHandles;
using SPS_SDL_SQL;
using Entities;
using static Entities.Enums;
using Entities.Helpers;
using System.Threading.Tasks;

namespace GenericDataLayer.Managers
{
    public class UsersManager : IDisposable
    {
        bool disposed = false;
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        readonly Assembly SDL = Assembly.LoadFrom(ConfigurationHandler.EnginePath());

        public Users Usuarios { get; set; }

        public UsersManager(Users usuarios)
        {
            this.Usuarios = usuarios ?? new Users();
        }

        public async Task<IList<User>> Find(int id)
        {
            var retVal = await Select(null, null, null, (ulong)id);
            
            return retVal.ToList();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string user, string password)
        {
            var retVal = Usuarios.Login(user, password);

            return retVal;
        }

        public MgrResponse<User> Insert(User user)
        {
            Contract.Requires<Exception>( user != null, "User can not to be null.");
            Contract.Requires<Exception>(!string.IsNullOrEmpty(user.Alias),"User Alias can not to be empty.");
            Contract.Requires<Exception>(!string.IsNullOrEmpty(user.Name),"User Name can not to be empty.");
            Contract.Requires<Exception>(!string.IsNullOrEmpty(user.LastName),"User Last Name can not to be empty.");

            var mgrResponse = new MgrResponse<User>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Users().Insert(param);

            mgrResponse.Object = new User()
            {
                ID = result.ID,
                Alias = result.Alias,
                Name = result.Name,
                LastName = result.LastName,
                Status = (UserStatus)result.Status,
                Groups = result.Groups,
                Roles = result.Roles
            };

            return mgrResponse;
        }

        public MgrResponse<User> Update(User usuario)
        {
            Contract.Requires<Exception>(!string.IsNullOrEmpty(usuario.Alias), "El alias del usuario no puede estar vacío");
            Contract.Requires<Exception>(!string.IsNullOrEmpty(usuario.Name), "El nombre del usuario no puede estar vacío");
            Contract.Requires<Exception>(!string.IsNullOrEmpty(usuario.LastName), "El apellido del usuario no puede estar vacío");

            var mgrResponse = new MgrResponse<User>();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(usuario);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);
            var result = new SPS_SDL_SQL.Users().Update(param);

            var miUsuario = new User()
            {
                ID = result.ID,
                Alias = result.Alias,
                Name = result.Name,
                LastName = result.LastName,
                Status = (UserStatus)result.Status
            };

            new SPS_SDL_SQL.UserGroups().Delete(usuario.ID);

            foreach(var grupo in usuario.Groups)
            {
               new SPS_SDL_SQL.UserGroups().Insert(usuario.ID, grupo.ID.Value);
            }

            foreach(var rol in usuario.Roles)
            {
                new SPS_SDL_SQL.UserRoles().Insert(usuario.ID, rol.ID.Value);
            }

            mgrResponse.Object = miUsuario;

            return mgrResponse;
        }

        public UserStatus Delete(ulong idUsuario)
        {
            new SPS_SDL_SQL.UserGroups().Delete((int)idUsuario);
            new SPS_SDL_SQL.UserRoles().Delete((int)idUsuario);
            new SPS_SDL_SQL.Users().Delete((int)idUsuario);

            return UserStatus.Inactive;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        public IList<T> ReturnListaGenerica<T>(string entidad, string metodo, object[] parametros)
        {
            var retVal = new List<T>();

            try
            {
                foreach (Type clase in SDL.GetTypes().Where(x => x.IsClass))
                {
                    if (clase.Name == entidad)
                    {
                        Type t = SDL.GetType("SPS_SDL_SQL." + entidad);
                        retVal = (List<T>)t.InvokeMember(metodo, BindingFlags.InvokeMethod, null, t, parametros);
                    }
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }

            return retVal;
        }

        public async Task<IList<User>> Select(Paging paginado, Ordering orden, string alias, ulong? idUsuario)
        {
            var retVal = new List<User>();

            var lstUsuarios = await Usuarios.Select(paginado, orden, idUsuario);

            foreach (var item in lstUsuarios)
            {
                var usuario = new User()
                {
                    ID = item.ID,
                    Alias = item.Alias,
                    Name = item.Name,
                    LastName = item.LastName,
                    Status = (UserStatus)item.Status,
                };

                retVal.Add(usuario);
            }

            return retVal;
        }
    }
}