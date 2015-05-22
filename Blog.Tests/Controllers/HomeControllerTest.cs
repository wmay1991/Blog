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
        private PostViewModel _blogViewModel = new PostViewModel();
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
            mockBlogController.PostCreate(new PostViewModel { PostAuthor = "Person3", PostBody = "This is a blog111", PostTitle = "ASP.NET 3.5" })
               .VerifyAdd(Times.Once);

        }

        //Edit

        [Test]
        public void UpdateBlogRequest()
        {
            var blogId = Guid.NewGuid();
            var existingBlog = new Posts
            {
                PostId = blogId,
                PostAuthor = "Person2",
            };


            var mockBlogs = new Mock<DbSet<Posts>>();
            mockBlogs.Setup(x => x.Find(blogId)).Returns(existingBlog);

            var mockContext = new Mock<PostContext>();
            mockContext.Setup(x => x.Posts).Returns(mockBlogs.Object);

            var controller = new BlogController(mockContext.Object);

            var result = controller.Edit(blogId) as ViewResult;

            Assert.IsAssignableFrom(typeof(ViewResult), result);
        }

        [Test]
        public void PostUpdateSuccess()
        {
            var blogId = Guid.NewGuid();
            var existingBlog = new Posts
            {
                PostId = blogId,
                PostAuthor = "Person2",
            };

            var updatedBlog = new PostViewModel
            {
                PostId = blogId,
                PostAuthor = "Whitney",
            };

            var mockBlogs = new Mock<DbSet<Posts>>();
            mockBlogs.Setup(x => x.Find(blogId)).Returns(existingBlog);

            var mockContext = new Mock<PostContext>();
            mockContext.Setup(x => x.Posts).Returns(mockBlogs.Object);

            var controller = new BlogController(mockContext.Object);
            controller.Edit(updatedBlog);


            mockContext.Verify(x => x.SaveChanges());
        }


        //Details

        [Test]
        public void DetailsRender()
        {
            var postId = Guid.NewGuid();

            var existingBlog = new Posts
            {
                PostId = postId,
                PostAuthor = "Person",
                PostTitle = "Title",
                PostDate = DateTime.Now,
                PostBody = "Read this!",
                BlogComments = new PostComments[] { 
                    new PostComments{CommentAuthor = "CommentPerson", 
                        CommentBody = "Great Blog!",
                        CommentDate = DateTime.Now,
                        CommentId = Guid.NewGuid(),
                        PostId = postId}
                }
            };

            var mockBlogs = new Mock<DbSet<Posts>>();
            mockBlogs.Setup(x => x.Find(postId)).Returns(existingBlog);


            var mockContext = new Mock<PostContext>();
            mockContext.Setup(x => x.Posts).Returns(mockBlogs.Object);


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

            var existingBlog = new Posts
            {
                PostId = Guid.NewGuid(),
                PostAuthor = "Blogger",
            };

            var newComment = new PostComments
            {
                Post = existingBlog,
                PostId = existingBlog.PostId,
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

            var mockBlogs = new Mock<DbSet<PostComments>>();
            mockBlogs.Setup(x => x.Find(blogWithComments.PostId)).Returns(newComment);

            var mockContext = new Mock<PostContext>();
            mockContext.Setup(x => x.PostComments).Returns(mockBlogs.Object);

            var controller = new BlogController(mockContext.Object);
            controller.AddComment(blogWithComments);

            mockContext.Verify(x => x.SaveChanges());

        }

    }
}
