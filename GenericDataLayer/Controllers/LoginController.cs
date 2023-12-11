using System;
using System.Web.Http;
using GenericDataLayer.Managers;
using NLog;
using Entities;
using System.Web.Http.Cors;


namespace GenericDataLayer.Controllers
{
    [RoutePrefix("api/v1/login")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController: ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly UsersManager UsersManager = new UsersManager(new SPS_SDL_SQL.Users());

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(string user, string password)
        {
            bool result;

            try
            {
                var usernameValue = "username Value: " + user;
                var passwordValue = "password Value: " + password;

                Contract.Requires<ArgumentOutOfRangeException>(!string.IsNullOrEmpty(user), usernameValue);
                Contract.Requires<ArgumentOutOfRangeException>(!string.IsNullOrEmpty(password), passwordValue);

                result = UsersManager.Login(user,password);

                if (!result)
                {
                    return this.NotFound();
                }
            }
            catch (Exception e)
            {
                var mensaje = e.Message.Replace("\r\n", string.Empty);

                Logger.Error(e, mensaje);

                return this.InternalServerError(e);
            }

            return this.Ok(result);
        }

    }
}