using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger.Annotations;
using GenericDataLayer.Configuration;
using GenericDataLayer.Managers;
using NLog;
using System.Collections.Generic;
using Entities;
using Entities.Helpers;
using System.Web.Http.Cors;
using GenericDataLayer.Helpers;
using Entities.DTOs;
using System.Linq;

namespace GenericDataLayer.Controllers
{
    /// <summary>
    /// gestion de Roles
    /// </summary>
    [RoutePrefix("api/v1/roles")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RolesController : ApiController
    {
        /// <summary>
        /// logger de nlog 
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //private static readonly ulong idEntidad = Convert.ToUInt64(ConfigurationHandler.idEntity());
        private readonly RolesManager rolesManager = new RolesManager();

        /// <summary>
        /// constructor parameterless
        /// </summary>
        public RolesController()
        { }

        /// <summary>
        /// Obtiene una lista de un usuario determinado
        /// </summary>
        /// <param name="id">Identificador unico</param>
        /// <returns>Mgr Response</returns>
        [HttpGet]
        [Route("{id}", Name = "rolesGetById")]
        [ResponseType(typeof(Entities.DTOs.RolDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult GetByID(ulong id)
        {
            var rol = this.rolesManager.Find(id);

            if (rol == null)
            {
                Logger.Debug("No se encontraron datos");

                return this.NotFound();
            }

            var rolDTO = new Entities.DTOs.RolDTO();

            foreach (var item in rol)
            {
                rolDTO = item.ToDTO();
            }

            var usersManager = new UserRolesManager();

            var users = usersManager.SelectUsers((int)rolDTO.ID).OrderBy(x => x.Name).ToList();

            rolDTO.Users = users;

            return this.Ok(rolDTO);
        }

        /// <summary>
        /// Lista de usuarios
        /// </summary>
        /// <param name="idRol">id de usuario</param>
        /// <param name="alias">alias del usuario</param>
        /// <param name="ordenDTO">objeto ordenamiento</param>
        /// <param name="paginadoDTO">objeto paginado</param>
        /// <returns>lista generica de usuarios</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Entities.DTOs.GenericListDTO<Entities.DTOs.RolDTO>))]
        public IHttpActionResult Get([FromUri]ulong? idRol = null,
                                     [FromUri]string alias = null,
                                     [FromUri]OrderingDTO ordenDTO = null,
                                     [FromUri]PaginedDTO paginadoDTO = null)
        {
            // Filtro por Id, User_Name y Perfil
            // Paginación y Ordenamiento(Default Nombre)
            // control paginado
            Paging paginado = null;

            if (paginadoDTO != null && paginadoDTO.IsEnabled)
            {
                paginado = new Paging(paginadoDTO.Page,
                                        paginadoDTO.Rows,
                                        paginadoDTO.TotalRows);
            }

            Ordering orden = null;
            if (ordenDTO?.OrderingRow == null)
            {
                // ordenamiento por defecto
                orden = new Ordering(nameof(Rol.Nombre), OrderingDB.ASC);
            }
            else
            {
                orden = new Ordering(ordenDTO.OrderingRow, ordenDTO.OrdinationSense);
            }

            var roles = this.rolesManager.Select(paginado, orden, idRol);

            var lstDTO = new List<Entities.DTOs.RolDTO>();

            foreach (var item in roles)
            {
                lstDTO.Add(item.ToDTO());
            }

            foreach (var rol in lstDTO)
            {
                var usersManager = new UserRolesManager();

                var users = usersManager.SelectUsers((int)rol.ID).OrderBy(x => x.Name).ToList();

                rol.Users = users;
            }

            return this.Ok(lstDTO);

        }

        /// <summary>
        /// Agrega un usuario
        /// </summary>
        /// <param name="rol">Usuario</param>
        /// <returns>Mgr Response</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(MgrResponse<Entities.DTOs.RolDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(MgrResponse<Entities.DTOs.RolDTO>))]
        public IHttpActionResult Post(Entities.DTOs.RolDTO rol)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content(HttpStatusCode.BadRequest,
                    this.ModelState.ToMgrResponse<Entities.DTOs.RolDTO>());
            }

            var modelo = rol.ToModel();

            var mgrResponse = this.rolesManager.Insert(modelo)
                                .ToDTO<Rol, Entities.DTOs.RolDTO>();

            if (mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKInstantImpact &&
                mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKConfirmationPending)
            {
                return this.Content(HttpStatusCode.BadRequest, mgrResponse);
            }

            return this.CreatedAtRoute("RolesGetById", new { mgrResponse.Object.ID }, mgrResponse);
        }

        /// <summary>
        /// Modifica un usuario 
        /// </summary>
        /// <param name="rol">usuario</param>
        /// <returns>Mgr Response</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(MgrResponse<Entities.DTOs.RolDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(MgrResponse<Entities.DTOs.RolDTO>))]
        public IHttpActionResult Put(Entities.DTOs.RolDTO rol)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content(HttpStatusCode.BadRequest,
                    this.ModelState.ToMgrResponse<Entities.DTOs.RolDTO>());
            }

            var modelo = rol.ToModel();

            var mgrResponse = this.rolesManager.Update(modelo).ToDTO<Rol, Entities.DTOs.RolDTO>();

            if (mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKInstantImpact &&
                mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKConfirmationPending)
            {
                return this.Content(HttpStatusCode.BadRequest, mgrResponse);
            }

            return this.Ok(mgrResponse);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">id del usuario a eliminar</param>
        /// <returns>Mgr Response</returns>
        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(Entities.Enums.RolStatus))]
        [SwaggerResponse(HttpStatusCode.NotFound, type: typeof(Entities.Enums.RolStatus))]
        public IHttpActionResult Delete(ulong id)
        {
            var resp = this.rolesManager.Delete(id);

            if (resp == Entities.Enums.RolStatus.NotFound)
            {
                Logger.Debug("No se encontraron datos");

                return this.NotFound();
            }

            return this.Ok(resp);
        }
    }
}