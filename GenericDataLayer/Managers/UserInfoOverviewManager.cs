namespace GenericDataLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using static Entities.Enums;

    public class UserInfoOverviewManager : IDisposable, IUserInfoOverviewManager
    {
        public MSSQLDataLayer.UserInfoOverview UserInfoOverview { get; set; }

        public UserInfoOverviewManager(MSSQLDataLayer.UserInfoOverview userInfoOverview)
        {
            this.UserInfoOverview = userInfoOverview ?? new MSSQLDataLayer.UserInfoOverview();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IList<Entities.DTOs.UserInfoOverview> Select()
        {
            return UserInfoOverview.Select();
        }

        public UserInfoStatus Delete(int id)
        {
            return UserInfoOverview.Delete(id);
        }

    }
}