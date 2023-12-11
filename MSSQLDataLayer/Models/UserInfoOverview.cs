namespace MSSQLDataLayer
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Entities.Enums;
    using Models;

    /// <summary>
    /// BankAccountOverview data layer
    /// </summary>
    public class UserInfoOverview
    {
        /// <summary>
        /// insert method
        /// </summary>
        /// <param name="userInfoOverview"></param>
        /// <returns></returns>
        public int? Insert(Entities.DTOs.UserInfoOverview userInfoOverview)
        {
            var id = -1;

            try
            {
                using (BaseDatos bd = new BaseDatos())
                {
                    bd.UserInfoOverview.Add(userInfoOverview);

                    bd.SaveChanges();

                    id = Convert.ToInt32(userInfoOverview.ID);
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
        public IList<Entities.DTOs.UserInfoOverview> Select()
        {
            IList<Entities.DTOs.UserInfoOverview> result;

            try
            {
                using (BaseDatos bd = new BaseDatos())
                {
                    result = bd.UserInfoOverview.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// delete an element
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfoStatus Delete(int id)
        {
            var retVal = UserInfoStatus.Active;

            try
            {
                using (BaseDatos bd = new BaseDatos())
                {
                    var user = bd.UserInfoOverview
                        .FirstOrDefault(a => Convert.ToInt32(a.ID) == id);

                    bd.UserInfoOverview.Attach(user);
                    bd.UserInfoOverview.Remove(user);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                retVal = UserInfoStatus.NotFound;
            }

            return retVal;
        }

        /// <summary>
        /// update an element
        /// </summary>
        /// <param name="userInfoOverview"></param>
        /// <returns></returns>
        public Entities.DTOs.UserInfoOverview Update(Entities.DTOs.UserInfoOverview userInfoOverview)
        {
            var user = userInfoOverview;

            using (BaseDatos bd = new BaseDatos())
            {
                user = bd.UserInfoOverview.
                    FirstOrDefault(b => b.ID == userInfoOverview.ID);

                if (user != null)
                {
                    bd.UserInfoOverview.Attach(user);
                    user = userInfoOverview;
                    bd.SaveChanges();
                }
            }

            return user;
        }
    }
}