using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sapiens_Assistances_API;
using Sapiens_Assistances_API.Controllers;

using System.Web.Mvc;

namespace Sapiens_Assistances_API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
