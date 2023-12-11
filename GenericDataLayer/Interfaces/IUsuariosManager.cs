using System.Collections.Generic;

using Entities;
using static Entities.Enums;
using Entities.Helpers;

namespace GenericDataLayer.Managers
{
    /// <summary>
    /// interface users manager
    /// </summary>
    public interface IUsersManager
    {
        /// <summary>
        /// Deletes a user by id.
        /// </summary>
        /// <param name="idUsuario">The identifier usuario.</param>
        /// <returns>UserStatus</returns>
        UserStatus Delete(ulong idUsuario);

        /// <summary>
        /// Log in a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(string user, string password);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IList<User> Find(int id);

        /// <summary>
        /// Inserts the specified usuario.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        MgrResponse<User> Insert(User usuario);

        /// <summary>
        /// Returns the lista generica.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entidad">The entidad.</param>
        /// <param name="metodo">The metodo.</param>
        /// <param name="parametros">The parametros.</param>
        /// <returns></returns>
        IList<T> ReturnListaGenerica<T>(string entidad, string metodo, object[] parametros);

        /// <summary>
        /// Selects the specified paginado.
        /// </summary>
        /// <param name="paginado">The paginado.</param>
        /// <param name="orden">The orden.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="idUsuario">The identifier usuario.</param>
        /// <returns></returns>
        IList<User> Select(Paging paginado, Ordering orden, string alias, ulong? idUsuario);

        /// <summary>
        /// Updates the specified usuario.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        MgrResponse<User> Update(User usuario);
    }
}