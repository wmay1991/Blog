using Blog.Controllers;
using Blog.Domain;
using Blog.Models;
using Blog.Data;
using Moq;
using FakeDbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Blog.Tests.Controllers
{
    internal class MockHomeController
    {
            private bool _withModelError = false;
            private BlogViewModel _blogViewModel = new BlogViewModel();
            private Mock<DbSet<Blogs>> _mockBlog;

            public MockHomeController WithModelError()
            {
                this._withModelError = true;
                return this;
            }

            public MockHomeController PostCreate(BlogViewModel blogViewModel)
            {
                this._blogViewModel = blogViewModel;
                return this;
            }

            public void VerifyAdd(Func<Times> times)
            {

                var mockBlogs = new Mock<System.Data.Entity.DbSet<Blogs>>();
                var controller = GetMockedController(mockBlogs);

                if (_withModelError)
                {

                    controller.ViewData.ModelState.AddModelError("Key", "Value");   // any error will do
                }
                controller.NewPost(_blogViewModel);

                mockBlogs.Verify(x => x.Add(It.Is<Blogs>(b => b.PostId == _blogViewModel.BlogId)), times);
            }

            public void VerfiyEditRequest(Func<Times> times)
            {
                _mockBlog = new Mock<DbSet<Blogs>>();
                _mockBlog.Setup(s => s.Find(It.IsAny<int?>())).Returns(new Blogs());
                var HomeController = GetMockedController(_mockBlog);
                var editBlogsResult = HomeController.Edit(3);

                _mockBlog.Verify(x => x.Find(It.Is<Blogs>(b => b.PostId == _blogViewModel.BlogId)), times);

            }

            public void VerifyBlogUpdate(Func<Times> times)
            {
                _mockBlog = new Mock<DbSet<Blogs>>();
                _mockBlog.Setup(s => s.Find(It.IsAny<int?>())).Returns(new Blogs());
                var HomeController = GetMockedController(_mockBlog);
                HomeController.NewPost(_blogViewModel);
                VerifyAdd(Times.Once);
                HomeController.Edit(_blogViewModel);
                _mockBlog.Verify(x => x.Find(It.Is<Blogs>(b => b.PostId == _blogViewModel.BlogId)), times);

            }

            private HomeController GetMockedController(Mock<DbSet<Blogs>> mockBlogs)
            {
                var mockContext = new Mock<BlogContext>();
                mockContext.Setup(x => x.Blogs).Returns(mockBlogs.Object);

                return new HomeController(mockContext.Object);
            }
        }
    }
