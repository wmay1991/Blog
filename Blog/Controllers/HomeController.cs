using System;
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

        public ActionResult Search(string searchTerm)
        {
            if (searchTerm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = db.Blogs.Where(b => b.PostTitle.Contains(searchTerm) ||
                b.PostAuthor.Contains(searchTerm) ||
                b.PostBody.Contains(searchTerm)).ToList();

            return View(result);
        }
    }
}