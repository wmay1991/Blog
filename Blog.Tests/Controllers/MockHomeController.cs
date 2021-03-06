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
    internal class MockBlogController
    {
            private bool _withModelError = false;
            private PostViewModel _blogViewModel = new PostViewModel();
            private Posts _blogs = new Posts();
            private Mock<DbSet<Posts>> _mockPost;

            public MockBlogController WithModelError()
            {
                this._withModelError = true;
                return this;
            }

            public MockBlogController PostCreate(PostViewModel blogViewModel)
            {
                this._blogViewModel = blogViewModel;
                return this;
            }

            public MockBlogController PostCreateBlog(Posts blogs, PostViewModel blogViewModel)
            {
                this._blogs.PostId = blogViewModel.PostId;
                this._blogs.PostDate = blogViewModel.PostDate;
                this._blogs.PostTitle = blogViewModel.PostTitle;
                this._blogs.PostAuthor = blogViewModel.PostAuthor;
                this._blogs.PostTease = blogViewModel.PostTease;
                this._blogs.PostBody = blogViewModel.PostBody;
                return this;
            }

            public void VerifyAdd(Func<Times> times)
            {

                var mockBlogs = new Mock<System.Data.Entity.DbSet<Posts>>();
                var controller = GetMockedController(mockBlogs);

                if (_withModelError)
                {

                    controller.ViewData.ModelState.AddModelError("Key", "Value");   // any error will do
                }
                controller.NewPost(_blogViewModel);

                mockBlogs.Verify(x => x.Add(It.Is<Posts>(b => b.PostId == _blogViewModel.PostId)), times);
            }

            public void VerfiyEditRequest(Func<Times> times)
            {
                _mockPost = new Mock<DbSet<Posts>>();
                _mockPost.Setup(s => s.Find(It.IsAny<int?>())).Returns(new Posts());
                var BlogController = GetMockedController(_mockPost);
                Guid blogEditId = new Guid();
                var editBlogsResult = BlogController.Edit(blogEditId);

                _mockPost.Verify(x => x.Find(It.Is<Posts>(b => b.PostId == _blogViewModel.PostId)), times);

            }

            public void VerifyBlogUpdate(Func<Times> times)
            {
                _mockPost = new Mock<DbSet<Posts>>();
                _mockPost.Setup(s => s.Find(It.IsAny<int?>())).Returns(new Posts());
                var BlogController = GetMockedController(_mockPost);
                BlogController.Edit(_blogViewModel);
                _mockPost.Verify(x => x.Find(It.Is<Posts>(b => b.PostId == _blogViewModel.PostId)), times);

            }

            private BlogController GetMockedController(Mock<DbSet<Posts>> mockBlogs)
            {
                var mockContext = new Mock<PostContext>();
                mockContext.Setup(x => x.Posts).Returns(mockBlogs.Object);

                return new BlogController(mockContext.Object);
            }
        }
    }
