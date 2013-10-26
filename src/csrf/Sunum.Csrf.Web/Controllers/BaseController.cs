using System.Web.Mvc;

namespace Sunum.Csrf.Web.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
