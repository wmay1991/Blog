using System;
using System.Threading;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Models;
using NUnit.Framework;
using NUnit.Core;
using Moq;
using Blog.Domain;
using System.Data.Entity;
using Blog.Data;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private BlogViewModel _blogViewModel = new BlogViewModel();
        private MockBlogController mockBlogController;


        [SetUp]
        public void SetUp()
        {
            mockBlogController = new MockBlogController();
        }

        //Home Page

        [Test]
        public void IndexShowBlogs()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsNotNull(result);
        }

        //Add

        [Test]
        public void NewPost()
        {
            var controller = new BlogController();
            var result = controller.NewPost() as ViewResult;

            Assert.IsNotNull(result);
        }


        [Test]
        public void AddBlogSuccess()
        {
            mockBlogController.PostCreate(new BlogViewModel { PostAuthor = "Person3", PostBody = "This is a blog111", PostTitle = "ASP.NET 3.5" })
               .VerifyAdd(Times.Once);

        }

        //Edit

        [Test]
        public void UpdateBlogRequest()
        {
            var blogId = Guid.NewGuid();
            var existingBlog = new Blogs
            {
                PostId = blogId,
                PostAuthor = "Person2",
            };


            var mockBlogs = new Mock<DbSet<Blogs>>();
            mockBlogs.Setup(x => x.Find(blogId)).Returns(existingBlog);

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockBlogs.Object);

            var controller = new BlogController(mockContext.Object);

            var result = controller.Edit(blogId) as ViewResult;

            Assert.IsAssignableFrom(typeof(ViewResult), result);
        }

        [Test]
        public void PostUpdateSuccess()
        {
            var blogId = Guid.NewGuid();
            var existingBlog = new Blogs
            {
                PostId = blogId,
                PostAuthor = "Person2",
            };

            var updatedBlog = new BlogViewModel
            {
                BlogId = blogId,
                PostAuthor = "Whitney",
            };

            var mockBlogs = new Mock<DbSet<Blogs>>();
            mockBlogs.Setup(x => x.Find(blogId)).Returns(existingBlog);

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockBlogs.Object);

            var controller = new BlogController(mockContext.Object);
            controller.Edit(updatedBlog);


            mockContext.Verify(x => x.SaveChanges());
        }


        //Details

        [Test]
        public void DetailsRender()
        {
            var postId = Guid.NewGuid();

            var existingBlog = new Blogs
            {
                PostId = postId,
                PostAuthor = "Person",
                PostTitle = "Title",
                PostDate = DateTime.Now,
                PostBody = "Read this!",
                //BlogComments = new BlogComments
                //{
                //    PostId = postId,
                //    CommentId = Guid.NewGuid(),
                //    CommentAuthor = "Hello",
                //    CommentBody = "How are you?",
                //    CommentDate = DateTime.Now
                //};
            };

            var blogComments = new BlogComments
            {
                PostId = postId,
                Blog = existingBlog,
                CommentId = Guid.NewGuid(),
                CommentAuthor = "Hello",
                CommentBody = "How are you?",
                CommentDate = DateTime.Now
            };

            var mockBlogs = new Mock<DbSet<Blogs>>();
            //mockBlogs.Setup(x => x.Find(blogId)).Returns(existingBlog);
            //mockBlogs.Setup(x => x.First(x =>).Returns(mockBlogs.Object);
            mockBlogs.Setup(x => x.Find(postId)).Returns(existingBlog);

            //var mockComments = new Mock<DbSet<BlogComments>>();
            //mockComments.Setup(x => x.Find(blogId)).Returns(blogComments);


            var mockContext = new Mock<BlogContext>();
 
            mockContext.Setup(x => x.Blogs).Returns(mockBlogs.Object);


            var controller = new BlogController(mockContext.Object);
            var result = controller.Details(postId) as ViewResult;

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
            string searchTerm = "Saturday";

            var controller = new HomeController();
            var result = controller.Search(searchTerm) as ViewResult;
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

        //Comments
        [Test]
        public void AddNewComment()
        {

            var existingBlog = new Blogs
            {
                PostId = Guid.NewGuid(),
            };

            var newComment = new BlogComments
            {
                Blog = existingBlog,
                CommentId = Guid.NewGuid(),
                CommentAuthor = "Whitney May",
                CommentDate = DateTime.Now,
                CommentBody = "Great blog!",
            };

            var blogWithComments = new CommentViewModel
            {
                Blog = existingBlog,
                CommentId = Guid.NewGuid(),
                CommentAuthor = "Whitney May",
                CommentDate = DateTime.Now,
                CommentBody = "Great blog!",
            };

            var mockBlogs = new Mock<DbSet<BlogComments>>();
            mockBlogs.Setup(x => x.Find(blogWithComments.BlogId)).Returns(newComment);

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(x => x.BlogComments).Returns(mockBlogs.Object);

            var controller = new BlogController(mockContext.Object);
            controller.AddComment(blogWithComments);

            mockContext.Verify(x => x.SaveChanges());

        }

    }
}
