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

        private PostContext db = new PostContext();

        public HomeController()
        {
            db = new PostContext();
        }

        //For mocking and testing
        public HomeController(PostContext postContext)
        {
            db = postContext;
        }


        public ActionResult Index()
        {
            var post = db.Posts.ToList();
            if (post != null)
            {
                return View(post);
            }

            return View();
        }

        public ActionResult Search(string searchTerm)
        {
            if (searchTerm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = db.Posts.Where(b => b.PostTitle.Contains(searchTerm) ||
                b.PostAuthor.Contains(searchTerm) ||
                b.PostBody.Contains(searchTerm)).ToList();

            return View(result);
        }
    }
}