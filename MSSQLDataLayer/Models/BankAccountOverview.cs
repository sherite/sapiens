namespace MSSQLDataLayer
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Entities.Enums;
    using Entities.Helpers;
    using Models;
    using PagedList;

    /// <summary>
    /// BankAccountOverview data layer
    /// </summary>
    public class BankAccountOverview
    {
        /// <summary>
        /// insert method
        /// </summary>
        /// <param name="bankAccountOverview"></param>
        /// <returns></returns>
        public int? Insert(Entities.DTOs.BankAccountOverview bankAccountOverview)
        {
            var id = -1;

            try
            {
                using (BaseDatos bd = new BaseDatos())
                {
                    bd.BankAccountsOverview.Add(bankAccountOverview);

                    bd.SaveChanges();

                    id = Convert.ToInt32(bankAccountOverview.AccountID);
                }
            }
            catch (Exception)
            {
            }

            return id;
        }

        /// <summary>
        /// select method
        /// </summary>
        public IList<Entities.DTOs.BankAccountOverview> Select()
        {
            IList<Entities.DTOs.BankAccountOverview> result;

            try
            {
                using (BaseDatos bd = new BaseDatos())
                {
                    result = bd.BankAccountsOverview.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBankAccountOverview"></param>
        /// <returns></returns>
        public BankAccountOverviewStatus Delete(int idBankAccountOverview)
        {
            var retVal = BankAccountOverviewStatus.Active;

            try
            {
                using (BaseDatos bd = new BaseDatos())
                {
                    var account = bd.BankAccountsOverview
                        .FirstOrDefault(a => Convert.ToInt32(a.AccountID) == idBankAccountOverview);

                    bd.BankAccountsOverview.Attach(account);
                    bd.BankAccountsOverview.Remove(account);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                retVal = BankAccountOverviewStatus.NotFound;
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankAccountOverview"></param>
        /// <returns></returns>
        public Entities.DTOs.BankAccountOverview Update(Entities.DTOs.BankAccountOverview bankAccountOverview)
        {
            var account = bankAccountOverview;

            using (BaseDatos bd = new BaseDatos())
            {
                account = bd.BankAccountsOverview.
                    FirstOrDefault(b => b.AccountID == bankAccountOverview.AccountID);

                if (account != null)
                {
                    bd.BankAccountsOverview.Attach(account);
                    account = bankAccountOverview;
                    bd.SaveChanges();
                }
            }

            return account;
        }
    }
}