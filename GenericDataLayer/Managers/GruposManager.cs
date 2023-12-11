namespace GenericDataLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using GenericDataLayer.Helpers;
    using System.Linq;
    using Entities;
    using static Entities.Enums;
    using Entities.Helpers;

    public class GroupsManager : IDisposable
    {
        bool disposed = false;

        public List<Group> Find(int id)
        {
            return Select(null, null, id, null);
        }

        public List<Group> Select(Paging paging, Ordering ordering, int? groupId, int? userId)
        {
            var retVal = new List<Group>();

            var lstGroup = new SPS_SDL_SQL.Groups().Select(paging, ordering, groupId, userId);

            if (ordering != null)
            {
                var orderingRow = typeof(Group).GetProperty(ordering.OrderingDTO.OrderingRow);

                Func<Group, object> orderByFunc = null;

                if (ordering.OrderingDTO.OrderingRow.ToUpper() == "NOMBRE")
                    orderByFunc = group => group.Name;
                else
                    orderByFunc = group => group.ID;

                if (ordering.OrderingDTO.OrdinationSense.ToString() == "ASC")
                    lstGroup = lstGroup.OrderBy(orderByFunc).ToList();
                else
                    lstGroup = lstGroup.OrderByDescending(orderByFunc).ToList();

            }

            foreach (var item in lstGroup)
            {
                var group = new Group()
                {
                    ID = item.ID,
                    Status = item.Status,
                    Name = item.Name,
                    Description = item.Description
                };

                retVal.Add(group);
            }

            return retVal;
        }

        public MgrResponse<Group> Insert(Group group)
        {
            var mgrResponse = new MgrResponse<Group>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(group);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Groups().Insert(param);

            group = new Group()
            {
                ID = result.ID,
                Status = (GroupStatus)result.Status,
                Name = result.Name,
                Description = result.Description
            };

            mgrResponse.Object = group;

            return mgrResponse;
        }

        public MgrResponse<Group> Update(Group group)
        {
            var mgrResponse = new MgrResponse<Group>();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(group);
            var json2Bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var param = System.Convert.ToBase64String(json2Bytes);

            var result = new SPS_SDL_SQL.Groups().Update(param);

            group.ID = result.ID;
            group.Name = result.Name;
            group.Description = result.Description;
            group.Status = result.Status;

            new SPS_SDL_SQL.UserGroups().DeleteUsers(group.ID.Value);

            foreach(var user in group.Users)
            {
                new SPS_SDL_SQL.UserGroups().Insert(user.ID, (int)group.ID);
            }

            mgrResponse.Object = group;

            return mgrResponse;
        }

        public GroupStatus Delete(int groupId)
        {
            new SPS_SDL_SQL.Groups().Delete(groupId);

            return GroupStatus.Inactive;
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
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        public List<Group> ReturnGenericList(string entity, string method, object[] parameters)
        {
            var retVal = new List<Group>();

            return retVal;
        }
    }
}