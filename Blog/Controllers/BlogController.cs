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

        public ActionResult Details(Guid postId)
        {

            if (postId == Guid.Empty)
            {
                return HttpNotFound();
            }
            //var modelId = db.Blogs.Find(postId).BlogComments.Where()

            //var model = db.Blogs.Include(c => c.BlogComments).FirstOrDefault(b => b.PostId == postId);

            var model = db.Blogs.Find(postId);
            var viewModel = new BlogViewModel(model);

            viewModel.CommentViewModel = new CommentViewModel
                {
                    BlogId = postId
                };

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
                blogViewModel.PostId = Guid.NewGuid();
                blogViewModel.PostDate = DateTime.Now;
                var model = new BlogViewModel(blog, blogViewModel);


                db.Blogs.Add(blog);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View("NewPost");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddComment(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var vm = commentViewModel;
                BlogComments comment = new BlogComments();
                comment.Blog = vm.Blog;
                comment.PostId = vm.BlogId;
                comment.CommentAuthor = vm.CommentAuthor;
                comment.CommentDate = DateTime.Now;
                comment.CommentId = Guid.NewGuid();
                comment.CommentBody = vm.CommentBody;

                db.BlogComments.Add(comment);
                db.SaveChanges();

                BlogViewModel bvm = new BlogViewModel();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogViewModel blogViewModel)
        {
            if (ModelState.IsValid)
            {
                var blogId = blogViewModel.PostId;
                var blogToUpdate = db.Blogs.Find(blogId);
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