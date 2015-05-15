using System;
using System.Threading;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Models;
using NUnit.Framework;
using NUnit.Core;
using Moq;
using Blog.Domain;

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

        //Home Page

        [Test]
        public void Index() {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexShowBlogs()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsNotNull(result);
        }

        //Add

        [Test]
        public void NewPost() {
            var controller = new BlogController();
            var result = controller.NewPost() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void NewPostFailed(){

            var controller = new BlogController();
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

            var controller = new BlogController();
            var result = controller.NewPost(_blogViewModel) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void NewPostSuccessWithOutTease() {

            _blogViewModel = new BlogViewModel();
            _blogViewModel.PostTitle = "New Post";
            _blogViewModel.PostAuthor = "Person";
            _blogViewModel.PostBody = "Blah Blah Blah Charlie Brown";

            var controller = new BlogController();
            var result = controller.NewPost(_blogViewModel) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void AddBlogSuccess()
        {
            mockBlogController.PostCreate(new BlogViewModel { PostAuthor = "Person3", PostBody = "This is a blog111" , PostTitle = "ASP.NET 3.5" })
               .VerifyAdd(Times.Once);
            
        }
        
        //Edit

        [Test]
        public void UpdateBlogRequest()
        {
            Guid blogId = new Guid("5D172C7F-FA1F-4C4E-B558-1A46FE251C7E");

           var controller = new BlogController();
           var result = controller.Edit(blogId) as ViewResult;

            Assert.IsAssignableFrom(typeof(ViewResult), result);
        }

        [Test]
        public void PostUpdateSuccess()
        {
            BlogViewModel vm = new BlogViewModel { BlogId = Guid.NewGuid() , PostAuthor = "Person2", PostBody = "This is edited", PostTitle = "HTML5", PostDate = DateTime.Now };
            Blogs blog = new Blogs();
            mockBlogController
                .PostCreate(vm)
                .PostCreateBlog(blog, vm)
                .VerifyBlogUpdate(Times.Once);
        }

        //Details

        [Test]
        public void DetailsRender()
        {
            var controller = new BlogController();
            Guid blogId = new Guid("5D172C7F-FA1F-4C4E-B558-1A46FE251C7E");
            var result = controller.Details(blogId) as ViewResult;

            Assert.IsAssignableFrom(typeof(ViewResult), result);

        }

        [Test]
        public void DetailsDoNotRender()
        {
            var controller = new BlogController();
            Guid blogId = Guid.Empty;
            var result = controller.Details(blogId) as HttpStatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);

        }

        //Search

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
