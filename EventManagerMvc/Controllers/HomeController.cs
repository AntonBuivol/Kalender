using System.Web.Mvc;

namespace EventManagerMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Event");
        }
    }
}
