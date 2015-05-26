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
            var postId = Guid.NewGuid();
            var existingPost = new Posts
            {
                PostId = postId,
                PostAuthor = "Person2",
            };

            var updatedBlog = new PostViewModel
            {
                PostId = postId,
                PostAuthor = "Whitney",
            };

            var mockPost = new Mock<DbSet<Posts>>();
            mockPost.Setup(x => x.Find(postId)).Returns(existingPost);

            var mockContext = new Mock<PostContext>();
            mockContext.Setup(x => x.Posts).Returns(mockPost.Object);

            var controller = new BlogController(mockContext.Object);
            controller.Edit(updatedBlog);


            mockContext.Verify(x => x.SaveChanges());
        }


        //Details

        [Test]
        public void DetailsRender()
        {
            var postId = Guid.NewGuid();

            var existingPost = new Posts
            {
                PostId = postId,
                PostAuthor = "Person",
                PostTitle = "Title",
                PostDate = DateTime.Now,
                PostBody = "Read this!",
                PostComments = new PostComments[] { 
                    new PostComments{CommentAuthor = "CommentPerson", 
                        CommentBody = "Great Blog!",
                        CommentDate = DateTime.Now,
                        CommentId = Guid.NewGuid(),
                        PostId = postId}
                }
            };

            var mockPost = new Mock<DbSet<Posts>>();
            mockPost.Setup(x => x.Find(postId)).Returns(existingPost);


            var mockContext = new Mock<PostContext>();
            mockContext.Setup(x => x.Posts).Returns(mockPost.Object);


            var controller = new BlogController(mockContext.Object);
            var result = controller.Details(postId) as ViewResult;

            Assert.IsAssignableFrom(typeof(ViewResult), result);

        }

        [Test]
        public void DetailsDoNotRender()
        {
            var controller = new BlogController();
            Guid postId = Guid.Empty;
            var result = controller.Details(postId) as HttpStatusCodeResult;

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

            var existingPost = new Posts
            {
                PostId = Guid.NewGuid(),
                PostAuthor = "Blogger",
            };

            var newComment = new PostComments
            {
                Post = existingPost,
                PostId = existingPost.PostId,
                CommentId = Guid.NewGuid(),
                CommentAuthor = "Whitney May",
                CommentDate = DateTime.Now,
                CommentBody = "Great blog!",
            };

            var postWithComments = new CommentViewModel
            {
                Post = existingPost,
                CommentId = Guid.NewGuid(),
                CommentAuthor = "Whitney May",
                CommentDate = DateTime.Now,
                CommentBody = "Great blog!",
            };

            var mockPost = new Mock<DbSet<PostComments>>();
            mockPost.Setup(x => x.Find(postWithComments.PostId)).Returns(newComment);

            var mockContext = new Mock<PostContext>();
            mockContext.Setup(x => x.PostComments).Returns(mockPost.Object);

            var controller = new BlogController(mockContext.Object);
            controller.AddComment(postWithComments);

            mockContext.Verify(x => x.SaveChanges());

        }

    }
}
