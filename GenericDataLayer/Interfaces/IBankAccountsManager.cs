namespace GenericDataLayer.Managers
{
    using System.Collections.Generic;
    using static Entities.Enums;

    public interface IBankAccountsOverviewManager
    {
        IList<Entities.DTOs.BankAccountOverview> Select();
    }

    public interface IUserInfoOverviewManager
    {
        IList<Entities.DTOs.UserInfoOverview> Select();

        UserInfoStatus Delete(int id);
    }
}