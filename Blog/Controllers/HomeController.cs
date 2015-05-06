using System.Web.Mvc;

namespace Blog.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult NewPost() {
            ViewBag.Message = "What do you want to blog about?";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}