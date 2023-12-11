namespace GenericDataLayer.Controllers
{
    using System;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Swashbuckle.Swagger.Annotations;
    using GenericDataLayer.Configuration;
    using GenericDataLayer.Managers;
    using Helpers;
    using NLog;
    using System.Collections.Generic;
    using Entities;
    using Entities.Helpers;
    using System.Web.Http.Cors;

    /// <summary>
    /// gestion de Roles
    /// </summary>
    [RoutePrefix("api/v1/rights")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RightsController : ApiController
    {
        /// <summary>
        /// logger de nlog 
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // entidad sobre la que se esta operando
        //private static readonly ulong idEntity = Convert.ToUInt64(ConfigurationHandler.idEntity());

        /// <summary>
        /// Manager de usuarios
        /// </summary>
        private readonly RightsManager rightsManager = new RightsManager();

        /// <summary>
        /// constructor parameterless
        /// </summary>
        public RightsController()
        { }

        /// <summary>
        /// Obtiene una lista de un usuario determinado
        /// </summary>
        /// <param name="id">Identificador unico</param>
        /// <returns>Mgr Response</returns>
        [HttpGet]
        [Route("{id}", Name = "rightsGetById")]
        [ResponseType(typeof(Entities.DTOs.RightDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult GetByID(ulong id)
        {
            var rights = this.rightsManager.Find(id);

            if (rights == null)
            {
                Logger.Debug("data not found.");

                return this.NotFound();
            }

            var rightDTO = new Entities.DTOs.RightDTO();

            foreach (var right in rights)
            {
                rightDTO = right.ToDTO();
            }

            return this.Ok(rightDTO);
        }

        /// <summary>
        /// Lista de usuarios
        /// </summary>
        /// <param name="idRight">id de usuario</param>
        /// <param name="alias">alias del usuario</param>
        /// <param name="orderingDTO">objeto ordenamiento</param>
        /// <param name="paginedDTO">objeto paginado</param>
        /// <returns>lista generica de usuarios</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri]ulong? idRight = null,
                                     [FromUri]string alias = null,
                                     [FromUri]OrderingDTO orderingDTO = null,
                                     [FromUri]PaginedDTO paginedDTO = null)
        {
            Paging paging = null;

            if (paginedDTO != null && paginedDTO.IsEnabled)
            {
                paging = new Paging(paginedDTO.Page, paginedDTO.Rows, paginedDTO.TotalRows);
            }

            var ordering = orderingDTO?.OrderingRow == null ? new Ordering(nameof(Rol.Nombre), OrderingDB.ASC) : new Ordering(orderingDTO.OrderingRow, orderingDTO.OrdinationSense);

            var rights = this.rightsManager.Select(paging, ordering, idRight);

            if (rights == null)
            {
                Logger.Debug("data not found.");

                return this.NotFound();
            }

            var lstDTO = new List<Entities.DTOs.RightDTO>();

            foreach (var right in rights)
            {
                lstDTO.Add(right.ToDTO());
            }

            return this.Ok(lstDTO);
        }

        /// <summary>
        /// Agrega un usuario
        /// </summary>
        /// <param name="permiso">Usuario</param>
        /// <returns>Mgr Response</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(MgrResponse<Entities.DTOs.RightDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(MgrResponse<Entities.DTOs.RightDTO>))]
        public IHttpActionResult Post(Entities.DTOs.RightDTO permiso)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content(HttpStatusCode.BadRequest,
                    this.ModelState.ToMgrResponse<Entities.DTOs.RightDTO>());
            }

            var modelo = permiso.ToModel();

            var mgrResponse = this.rightsManager.Insert(modelo)
                                .ToDTO<Permiso, Entities.DTOs.RightDTO>();

            if (mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKInstantImpact &&
                mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKConfirmationPending)
            {
                return this.Content(HttpStatusCode.BadRequest, mgrResponse);
            }

            return this.CreatedAtRoute("PermisosGetById", new { mgrResponse.Object.ID }, mgrResponse);
        }

        /// <summary>
        /// Modifica un usuario 
        /// </summary>
        /// <param name="permiso">usuario</param>
        /// <returns>Mgr Response</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(MgrResponse<Entities.DTOs.RightDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(MgrResponse<Entities.DTOs.RightDTO>))]
        public IHttpActionResult Put(Entities.DTOs.RightDTO permiso)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content(HttpStatusCode.BadRequest,
                    this.ModelState.ToMgrResponse<Entities.DTOs.RightDTO>());
            }

            var modelo = permiso.ToModel();

            var mgrResponse = this.rightsManager.Update(modelo).ToDTO<Permiso, Entities.DTOs.RightDTO>();

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
        [ResponseType(typeof(Entities.Enums.RightStatus))]
        [SwaggerResponse(HttpStatusCode.NotFound, type: typeof(Entities.Enums.RightStatus))]
        public IHttpActionResult Delete(ulong id)
        {
            var resp = this.rightsManager.Delete(id);

            if (resp == Entities.Enums.RightStatus.NotFound)
            {
                Logger.Debug("No se encontraron datos");

                return this.NotFound();
            }

            return this.Ok(resp);
        }
    }
}