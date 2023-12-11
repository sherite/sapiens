using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using GenericDataLayer.Configuration;
using GenericDataLayer.Managers;
using GenericDataLayer.Helpers;
using NLog;
using Swashbuckle.Swagger.Annotations;
using Entities;
using System.Linq;
using Entities.Helpers;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace GenericDataLayer.Controllers
{
    [RoutePrefix("api/v1/users")]
    [EnableCors(origins: "*",headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly UsersManager UsersManager = new UsersManager(new SPS_SDL_SQL.Users());

        [HttpGet]
        [Route("{id}", Name = "UsersGetById")]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            var userDTO = new Entities.DTOs.UserDTO();

            try
            {
                var enteredValue = "id Value: " + id.ToString();

                Contract.Requires<ArgumentOutOfRangeException>(id > 0, enteredValue);

                var users = await this.UsersManager.Find(id);

                if (users.Count() == 0 || users == null)  
                { 
                    var mensaje = "GetByID Method: Parameter Name: " + enteredValue + ". Data not found.";

                    Logger.Debug(mensaje);

                    return this.NotFound();
                }

                foreach (var item in users)
                {
                    var groupsManager = new UserGroupsManager();

                    item.Groups = (IList<Group>)groupsManager.Select(item.ID);

                    userDTO = item.ToDTO();

                    groupsManager.Dispose();
                }

                foreach (var item in users)
                {
                    var rolesManager = new UserRolesManager();

                    item.Roles = (IList<Rol>)rolesManager.Select(item.ID);

                    userDTO = item.ToDTO();

                    rolesManager.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                var mensaje = "GetByID Method: " + e.Message.Replace("\r\n", string.Empty);

                Logger.Debug(e, mensaje);

                return this.BadRequest(e.Message);
            }
            catch (Exception e)
            {
                var mensaje = e.Message.Replace("\r\n", string.Empty);

                Logger.Error(e, mensaje );

                return this.InternalServerError(e);
            }

            return this.Ok(userDTO);
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Entities.DTOs.GenericListDTO<Entities.DTOs.UserDTO>))]
        public async Task<IHttpActionResult> Get([FromUri]ulong? idUser = null,
                                     [FromUri]string alias = null,
                                     [FromUri]OrderingDTO orderDTO = null,
                                     [FromUri]PaginedDTO paginedDTO = null)
        {

            var lstDTO = new List<Entities.DTOs.UserDTO>();

            try
            {
                Paging pagined = null;

                if (paginedDTO != null && paginedDTO.IsEnabled)
                {
                    pagined = new Paging(paginedDTO.Page,
                                            paginedDTO.Rows,
                                            paginedDTO.TotalRows);
                }

                Ordering order = null;
                if (orderDTO?.OrderingRow == null)
                {
                    order = new Ordering(nameof(Entities.User.Alias), OrderingDB.ASC);
                }
                else
                {
                    order = new Ordering(orderDTO.OrderingRow, orderDTO.OrdinationSense);
                }

                var users = await this.UsersManager.Select(pagined, order, alias, idUser);

                foreach (var item in users)
                {
                    lstDTO.Add(item.ToDTO());
                }

                foreach(var usr in lstDTO)
                {
                    var groupsManager = new UserGroupsManager();

                    var groups = await groupsManager.Select(usr.ID);

                    usr.Groups = groups.OrderBy(x => x.Name).ToList();

                }

                foreach (var usr in lstDTO)
                {
                    var rolesManager = new UserRolesManager();

                    var roles = await rolesManager.Select(usr.ID);

                    usr.Roles = roles.OrderBy(x => x.Nombre).ToList();

                }
            }
            catch (Exception e)
            {
                Logger.Error("UsersController.Get", e.StackTrace);

                return this.InternalServerError(e);
            }

            return this.Ok(lstDTO);
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(MgrResponse<Entities.DTOs.UserDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(MgrResponse<Entities.DTOs.UserDTO>))]
        public IHttpActionResult Post(Entities.DTOs.UserDTO user)
        {
            var mgrResponse = new MgrResponse<Entities.DTOs.UserDTO>();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.Content(HttpStatusCode.BadRequest,
                        this.ModelState.ToMgrResponse<Entities.DTOs.UserDTO>());
                }

                var model = user.ToModel();

                mgrResponse = this.UsersManager.Insert(model)
                                    .ToDTO<User, Entities.DTOs.UserDTO>();

                if (mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKInstantImpact &&
                    mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKConfirmationPending)
                {
                    return this.Content(HttpStatusCode.BadRequest, mgrResponse);
                }
            }
            catch (Exception e)
            {
                Logger.Error("UsersController.Post: " + e.Message);

                mgrResponse.Errores = new Dictionary<string, string>
                {
                    { "error", e.Message }
                };

                return this.Content(HttpStatusCode.InternalServerError, mgrResponse);
            }

            return this.CreatedAtRoute("UsersGetById", new { mgrResponse.Object.ID }, mgrResponse);
        }

        [HttpPut]
        [Route("")]
        [ResponseType(typeof(MgrResponse<Entities.DTOs.UserDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(MgrResponse<Entities.DTOs.UserDTO>))]
        public IHttpActionResult Put(Entities.DTOs.UserDTO user)
        {
            var mgrResponse = new MgrResponse<Entities.DTOs.UserDTO>();
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.Content(HttpStatusCode.BadRequest,
                        this.ModelState.ToMgrResponse<Entities.DTOs.UserDTO>());
                }

                var model = user.ToModel();

                mgrResponse = this.UsersManager.Update(model).ToDTO<User, Entities.DTOs.UserDTO>();

                if (mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKInstantImpact &&
                    mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKConfirmationPending)
                {
                    return this.Content(HttpStatusCode.BadRequest, mgrResponse);
                }
            }
            catch (Exception e)
            {
                Logger.Error("UsersController.Put", e.StackTrace);

                return this.Content(HttpStatusCode.InternalServerError, mgrResponse);
            }

            return this.Ok(mgrResponse);
        }

        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(Entities.Enums.UserStatus))]
        [SwaggerResponse(HttpStatusCode.NotFound, type: typeof(Entities.Enums.UserStatus))]
        public IHttpActionResult Delete(int id)
        {
            var resp = Entities.Enums.UserStatus.NotFound;

            try
            {
                resp = this.UsersManager.Delete( Convert.ToUInt64(id));

                if (resp == Entities.Enums.UserStatus.NotFound)
                {
                    Logger.Debug("Data not found.");

                    return this.StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Logger.Error("UsersController.Delete", e.StackTrace);

                return this.Content(HttpStatusCode.InternalServerError, resp);
            }

            return this.StatusCode(HttpStatusCode.NoContent);
        }
    }
}