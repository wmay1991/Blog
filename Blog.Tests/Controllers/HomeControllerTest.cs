using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog;
using Blog.Controllers;

namespace Blog.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index() {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NewPost() {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.NewPost() as ViewResult;

            // Assert
            Assert.AreEqual("What do you want to blog about?", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact() {
            // Arrange
            var controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
