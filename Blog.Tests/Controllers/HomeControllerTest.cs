using System;
using System.Threading;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Models;
using NUnit.Framework;
using NUnit.Core;
using Moq;

namespace Blog.Tests.Controllers {
    [TestFixture]
    public class HomeControllerTest
    {
        private BlogViewModel _blogViewModel = new BlogViewModel();
        private MockHomeController mockBlogController;

        
        [SetUp]
        public void SetUp() {
            mockBlogController = new MockHomeController();
        }

        [Test]
        public void Index() {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexShowBlogs()
        {
            // Arrange
            var controller = new HomeController();

            //Act
            var result = controller.Index();

            //Assert
            //Assert.IsNotNull();
        }

        [Test]
        public void NewPost() {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.NewPost() as ViewResult;

            // Assert
            Assert.AreEqual("What do you want to blog about?", result.ViewBag.Message);
        }

        [Test]
        public void NewPostFailed(){

            var controller = new HomeController();
            controller.ModelState.AddModelError("Key", "Value");
            var result = controller.NewPost(_blogViewModel) as ViewResult;

            Assert.AreEqual("NewPost", result.ViewName);
        }

        [Test]
        public void NewPostSuccessWithTease() { 

            _blogViewModel = new BlogViewModel();
            _blogViewModel.PostTitle = "New Post";
            _blogViewModel.PostAuthor = "Person";
            _blogViewModel.PostTease = "Wahh wahh wahh";
            _blogViewModel.PostBody = "Blah Blah Blah Charlie Brown";

            var controller = new HomeController();
            var result = controller.NewPost(_blogViewModel) as RedirectResult;

            Assert.AreEqual("Index", result.Url);
        }

        [Test]
        public void NewPostSuccessWithOutTease() {

            _blogViewModel = new BlogViewModel();
            _blogViewModel.PostTitle = "New Post";
            _blogViewModel.PostAuthor = "Person";
            _blogViewModel.PostBody = "Blah Blah Blah Charlie Brown";

            var controller = new HomeController();
            var result = controller.NewPost(_blogViewModel) as RedirectResult;

            Assert.AreEqual("Index", result.Url);
        }

        [Test]
        public void AddBlogSuccess()
        {
            mockBlogController.PostCreate(new BlogViewModel { PostAuthor = "Person3", PostBody = "This is a blog111" , PostTitle = "ASP.NET 3.5" })
               .VerifyAdd(Times.Once);
        }
        
        [Test]
        public void UpdateBlogRequest()
        {
            // Arrange
            var controller = new HomeController();
            Guid blogEditId = new Guid("f687796a-93af-48ed-a561-4bbdd61a2142");

            // Act
            var result = controller.Edit(blogEditId) as ViewResult;

            // Assert
            Assert.IsAssignableFrom(typeof(ViewResult), result);
        }

        [Test]
        public void PostUpdateSuccess()
        {

            mockBlogController.PostCreate(new BlogViewModel {PostAuthor ="Person2", PostBody= "This is edited", PostTitle = "HTML5"})
                .VerifyBlogUpdate(Times.Once);
        }

        [Test]
        public void DetailsRender()
        {
            var controller = new HomeController();
            Guid blogId = new Guid("f687796a-93af-48ed-a561-4bbdd61a2142");
            var result = controller.Details(blogId) as ViewResult;

            Assert.IsAssignableFrom(typeof(ViewResult), result);

        }

        [Test]
        public void DetailsDoNotRender()
        {
            var controller = new HomeController();
            Guid blogId = Guid.Empty;
            var result = controller.Details(blogId) as HttpStatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);

        }

        [Test]
        public void SearchPageRenders()
        {
            var controller = new HomeController();
            var result = controller.Search(".NET") as ViewResult;
            Assert.IsAssignableFrom(typeof(ViewResult), result);
        }

        [Test]
        public void SearchPageDoesNotRenders()
        {
            var controller = new HomeController();
            string searchText = null;
            var result = controller.Search(searchText) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
