﻿using System;
using System.Web.Mvc;
using Blog.Data;
using Blog.Domain;
using Blog.Models;
using System.Data.Entity;
using System.Net;
using System.Linq;

namespace Blog.Controllers
{

    public class HomeController : Controller
    {

        private BlogContext db = new BlogContext();

        public HomeController()
        {
            db = new BlogContext();
        }

        //For mocking and testing
        public HomeController(BlogContext blogContext)
        {
            db = blogContext;
        }


        public ActionResult Index()
        {
            var blogs = db.Blogs.ToList();
            if (blogs != null)
            {
                return View(blogs);
            }
         
            return View();
        }

        public ActionResult Details(Guid blogId)
        {
            if ( blogId == Guid.Empty)
            {
                return HttpNotFound();
            }
            var model = db.Blogs.Find(blogId);
            
            return View(model);
        }

        public ActionResult NewPost()
        {
            ViewBag.Message = "What do you want to blog about?";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(BlogViewModel blogViewModel)
        {

            if (ModelState.IsValid)
            {

                var blog = new Blogs
                {
                    PostId = Guid.NewGuid(),
                    PostTitle = blogViewModel.PostTitle,
                    PostAuthor = blogViewModel.PostAuthor,
                    PostDate = DateTime.Now,
                    PostTease = blogViewModel.PostTease,
                    PostBody = blogViewModel.PostBody
                };

                blogViewModel.BlogId = blog.PostId;

                db.Blogs.Add(blog);
                db.SaveChanges();

                return Redirect("Index");
            }
            return View("NewPost");
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Edit(int? blogId)
        {
            if(blogId == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(blogId);
        }


        [HttpPost]
        public ActionResult Edit(BlogViewModel blogViewModel)
        {
            if (ModelState.IsValid)
            {
                var postId = blogViewModel.BlogId;
                var blogToUpdate = db.Blogs.First(b => b.PostId == blogViewModel.BlogId);
                if (blogToUpdate == null)
                {
                    return HttpNotFound();
                }
                blogToUpdate.PostTitle = blogViewModel.PostTitle;
                blogToUpdate.PostDate = DateTime.Now;
                blogToUpdate.PostAuthor = blogViewModel.PostAuthor;
                blogToUpdate.PostTease = blogViewModel.PostTease;
                db.Entry(blogToUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View(blogViewModel);
        }
    }
}