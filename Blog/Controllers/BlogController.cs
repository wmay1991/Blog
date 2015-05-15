using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using Blog.Models;
using Blog.Data;
using System.Net;
using System.Data.Entity;
using Blog.Domain;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public BlogController(BlogContext blogContext)
        {
            db = blogContext;
        }

        public BlogController()
        {
            db = new BlogContext();
        }

        public ActionResult Details(Guid blogId)
        {
            if (blogId == Guid.Empty)
            {
                return HttpNotFound();
            }
            var model = db.Blogs.Find(blogId);
            var viewModel = new BlogViewModel(model);

            return View(viewModel);
        }

        public ActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(BlogViewModel blogViewModel)
        {

            if (ModelState.IsValid)
            {
                Blogs blog = new Blogs();
                blogViewModel.BlogId = Guid.NewGuid();
                blogViewModel.PostDate = DateTime.Now;
                var model = new BlogViewModel(blog, blogViewModel);
                

                db.Blogs.Add(blog);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View("NewPost");
        }


        public ActionResult Edit(Guid blogId)
        {
            if (blogId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var model = db.Blogs.Find(blogId);
            var viewModel = new BlogViewModel(model);

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Edit(BlogViewModel blogViewModel)
        {
            if (ModelState.IsValid)
            {

                var blogToUpdate = db.Blogs.First(b => b.PostId == blogViewModel.BlogId);
                if (blogToUpdate == null)
                {
                    return HttpNotFound();
                }
                var model = new BlogViewModel(blogToUpdate, blogViewModel);

                db.Entry(blogToUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           return RedirectToAction("Index", "Home");
        }


    }
}