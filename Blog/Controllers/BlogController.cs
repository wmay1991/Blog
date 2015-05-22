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
        private PostContext db = new PostContext();

        public BlogController(PostContext blogContext)
        {
            db = blogContext;
        }

        public BlogController()
        {
            db = new PostContext();
        }

        public ActionResult Details(Guid postId)
        {
            if (postId == Guid.Empty)
            {
                return HttpNotFound();
            }
            var model = db.Posts.Find(postId);
            var viewModel = new PostViewModel(model);

            viewModel.CommentViewModel = new CommentViewModel
               {
                   PostId = postId
               };

            return View(viewModel);
        }

        public ActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(PostViewModel postViewModel)
        {

            if (ModelState.IsValid)
            {
                Posts post = new Posts();
                postViewModel.PostId = Guid.NewGuid();
                postViewModel.PostDate = DateTime.Now;
                var model = new PostViewModel(post, postViewModel);


                db.Posts.Add(post);
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
                PostComments comment = new PostComments();
                comment.CommentDate = DateTime.Now;
                comment.CommentId = Guid.NewGuid();
                new CommentViewModel(comment, vm);

                db.PostComments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(Guid postId)
        {
            if (postId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var model = db.Posts.Find(postId);
            var viewModel = new PostViewModel(model);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var postId = postViewModel.PostId;
                var postToUpdate = db.Posts.Find(postId);
                if (postToUpdate == null)
                {
                    return HttpNotFound();
                }
                var model = new PostViewModel(postToUpdate, postViewModel);

                db.Entry(postToUpdate).State = EntityState.Modified;
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