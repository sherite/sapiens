namespace GenericDataLayer.Controllers
{
    using System;
    using System.Net;
    using System.Web.Http;
    using GenericDataLayer.Configuration;
    using GenericDataLayer.Managers;
    using Helpers;
    using NLog;
    using System.Collections.Generic;
    using Entities;
    using System.Linq;
    using Swashbuckle.Swagger.Annotations;
    using Entities.DTOs;
    using Entities.Helpers;
    using System.Web.Http.Cors;

    /// <summary>
    /// Groups Controller
    /// </summary>
    [RoutePrefix("api/v1/groups")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GroupsController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //private static readonly ulong idEntity = Convert.ToUInt64(ConfigurationHandler.idEntity());
        private readonly GroupsManager groupsManager = new GroupsManager();

        /// <summary>
        /// Constructor
        /// </summary>
        public GroupsController()
        { }

        /// <summary>
        /// Get a group based on its identifier
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns>a group based in id passed as parameter</returns>
        [HttpGet]
        [Route("{id}", Name = "GroupsGetById")]
        [SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(MgrResponse<GroupDTO>))]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(GroupDTO))]
        public IHttpActionResult GetByID(int id)
        {
            if (!this.ModelState.IsValid)
                return this.Content(HttpStatusCode.BadRequest, this.ModelState.ToMgrResponse<GroupDTO>());

            var groups = this.groupsManager.Find(id);

            if (groups == null)
            {
                Logger.Debug("Data not found.");

                return this.NotFound();
            }

            var groupDTO = new GroupDTO();

            foreach (var group in groups)
            {
                groupDTO = group.ToDTO();
            }

            var usersManager = new UserGroupsManager();

            var users = usersManager.SelectUsers((int)groupDTO.ID).OrderBy(x => x.Name).ToList();

            groupDTO.Users = users;

            return this.Ok(groupDTO);
        }

        /// <summary>
        /// Get a list of groups based in its filtered conditions
        /// </summary>
        /// <param name="idGroup">group identifier</param>
        /// <param name="alias">group alias</param>
        /// <param name="orderingDTO">ordering condition</param>
        /// <param name="paginedDTO">paging condition</param>
        /// <param name="userId">user identifier</param>
        /// <returns>a list of groups based in filters passed as parameters</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri]int? idGroup = null,
                                     [FromUri]string alias = null,
                                     [FromUri]OrderingDTO orderingDTO = null,
                                     [FromUri]PaginedDTO paginedDTO = null,
                                     [FromUri]int? userId = null)
        {
            Paging pagined = null;

            if (paginedDTO != null && paginedDTO.IsEnabled)
                pagined = new Paging(paginedDTO.Page, paginedDTO.Rows, paginedDTO.TotalRows);

            var order = orderingDTO?.OrderingRow == null ?
                new Ordering("NOMBRE", OrderingDB.ASC) :
                new Ordering(orderingDTO.OrderingRow, orderingDTO.OrdinationSense);

            var groups = this.groupsManager.Select(pagined, order, idGroup, userId);

            var lstDTO = new List<GroupDTO>();

            foreach (var item in groups)
            {
                lstDTO.Add(item.ToDTO());
            }

            foreach (var group in lstDTO)
            {
                var usersManager = new UserGroupsManager();

                var users = usersManager.SelectUsers((int)group.ID).OrderBy(x => x.Name).ToList();

                group.Users = users;
            }

            return this.Ok(lstDTO);
        }

        /// <summary>
        /// Post a new group
        /// </summary>
        /// <param name="group">group to post</param>
        /// <returns>Manager Response object with post result</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(GroupDTO group)
        {
            if (!this.ModelState.IsValid)
                return this.Content(HttpStatusCode.BadRequest, this.ModelState.ToMgrResponse<GroupDTO>());

            var mgrResponse = this.groupsManager.Insert(group.ToModel()).ToDTO<Group, GroupDTO>();

            if (mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKInstantImpact)
                return this.Content(HttpStatusCode.BadRequest, mgrResponse);

            return this.CreatedAtRoute("GroupsGetById", new { mgrResponse.Object.ID }, mgrResponse);
        }

        /// <summary>
        /// Modify an existing group
        /// </summary>
        /// <param name="group">group to modify</param>
        /// <returns>Manager Response with group modified</returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(GroupDTO group)
        {
            if (!this.ModelState.IsValid)
                return this.Content(HttpStatusCode.BadRequest, this.ModelState.ToMgrResponse<GroupDTO>());

            var mgrResponse = this.groupsManager.Update(group.ToModel()).ToDTO<Group, GroupDTO>();

            if (mgrResponse.Status != Entities.Enums.RespStatusCodeGeneric.OKInstantImpact)
                return this.Content(HttpStatusCode.BadRequest, mgrResponse);

            return this.Ok(mgrResponse);
        }

        /// <summary>
        /// Delete a group
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns>result of delete operation</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var resp = this.groupsManager.Delete(id);

            if (resp == Entities.Enums.GroupStatus.NotFound)
            {
                Logger.Debug("Data not found.");

                return this.NotFound();
            }

            return this.Ok(resp);
        }
    }
}