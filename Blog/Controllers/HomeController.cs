using System;
using System.Web.Mvc;
using Blog.Data;
using Blog.Domain;
using Blog.Models;

namespace Blog.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {

            return View();
        }

        public ActionResult NewPost() {
            ViewBag.Message = "What do you want to blog about?";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(BlogViewModel blogViewModel) {
        
            if (ModelState.IsValid) {

                var blog = new Blogs
                {
                    PostId = Guid.NewGuid(),
                    PostTitle = blogViewModel.PostTitle,
                    PostAuthor = blogViewModel.PostAuthor,
                    PostDate = DateTime.Now,
                    PostTease = blogViewModel.PostTease,
                    PostBody = blogViewModel.PostBody
                };

                var newPostDbContext = new BlogContext();
                
                newPostDbContext.Blogs.Add(blog);
                newPostDbContext.SaveChanges();

                return Redirect("Index");
            }
            return View("NewPost");
        }

        public ActionResult Contact() {

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}