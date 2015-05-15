﻿using Blog.Controllers;
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
            private Blogs _blogs = new Blogs();
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

            public MockHomeController PostCreateBlog(Blogs blogs, BlogViewModel blogViewModel)
            {
                this._blogs.PostId = blogViewModel.BlogId;
                this._blogs.PostDate = blogViewModel.PostDate;
                this._blogs.PostTitle = blogViewModel.PostTitle;
                this._blogs.PostAuthor = blogViewModel.PostAuthor;
                this._blogs.PostTease = blogViewModel.PostTease;
                this._blogs.PostBody = blogViewModel.PostBody;
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
                Guid blogEditId = new Guid("f687796a-93af-48ed-a561-4bbdd61a2142");
                var editBlogsResult = HomeController.Edit(blogEditId);

                _mockBlog.Verify(x => x.Find(It.Is<Blogs>(b => b.PostId == _blogViewModel.BlogId)), times);

            }

            public void VerifyBlogUpdate(Func<Times> times)
            {
                _mockBlog = new Mock<DbSet<Blogs>>();
                _mockBlog.Setup(s => s.Find(It.IsAny<int?>())).Returns(new Blogs());
                var HomeController = GetMockedController(_mockBlog);
                ////VerifyAdd(Times.Once);
                //HomeController.NewPost(_blogViewModel);
                HomeController.Edit(_blogViewModel);
                _mockBlog.Verify(x => x.Find(It.Is<Blogs>(b => b.PostId == _blogViewModel.BlogId)), times);

            }

            private BlogController GetMockedController(Mock<DbSet<Blogs>> mockBlogs)
            {
                var mockContext = new Mock<BlogContext>();
                mockContext.Setup(x => x.Blogs).Returns(mockBlogs.Object);

                return new BlogController(mockContext.Object);
            }
        }
    }
