using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sunum.Csrf.Web.Controllers
{
    public class HomeController : BaseController
    {
        string path = "App_Data/data.txt";
        private void BindList()
        {
            var bilgiler = System.IO.File.ReadAllLines(Server.MapPath(path));
            if (bilgiler != null)
            {
                ViewBag.Bilgiler = bilgiler.ToList();
            }
            else
            {
                ViewBag.Bilgiler = new List<string>();
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            BindList();
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Index(string bilgi)
        {
            System.IO.File.AppendAllLines(Server.MapPath(path), new[] { bilgi });
            BindList();
            return View();
        }

    }
}
