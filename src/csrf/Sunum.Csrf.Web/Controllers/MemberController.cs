using Sunum.Csrf.Web.Services;
using System.Web.Mvc;

namespace Sunum.Csrf.Web.Controllers
{
    public class MemberController : BaseController
    {
        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var service = new FormsAuthenticationService();
            service.SignOut();
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(string user, string pass)
        {
            if (string.IsNullOrEmpty(user))
            {
                return ReturnViewWithFailMessage();
            }

            if (user != pass)
            {
                return ReturnViewWithFailMessage();
            }

            var service = new FormsAuthenticationService();
            service.SignIn(user, true);
            return RedirectToHome();
        }

        private ViewResult ReturnViewWithFailMessage()
        {
            ViewBag.Msg = "Giriş Başarısız";
            return View();
        }
    }
}
