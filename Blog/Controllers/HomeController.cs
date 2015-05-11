using System;
using System.Web.Mvc;
using Blog.Data;
using Blog.Domain;
using Blog.Models;
using System.Data.Entity;
using System.Net;

namespace Blog.Controllers
{

    public class HomeController : Controller
    {

        private BlogContext db = new BlogContext();
        private BlogContext blogContext;

        public HomeController()
        {
            db = new BlogContext();
        }

        //For mocking and testing
        public HomeController(BlogContext blogContext)
        {
            this.blogContext = blogContext;
        }


        public ActionResult Index()
        {

            return View();
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

                blogViewModel.id = blog.PostId;

                var newPostDbContext = new BlogContext();

                newPostDbContext.Blogs.Add(blog);
                newPostDbContext.SaveChanges();

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
                var blogToUpdate = db.Blogs.Find(blogViewModel.id);
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