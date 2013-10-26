using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Security.Application;

namespace Sunum.Xss.Web.Controllers
{
    public class HomeController : Controller
    {
        string path = "App_Data/data.txt";
        private void BindList()
        {
            var bilgiler = System.IO.File.ReadAllLines(Server.MapPath(path));
            if (bilgiler != null)
            {
                ViewBag.Bilgiler = bilgiler.Select(x => Sanitizer.GetSafeHtml(x)).ToList();
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

        [HttpPost, ValidateInput(false)]
        public ActionResult Index(string bilgi)
        {
            System.IO.File.AppendAllLines(Server.MapPath(path), new[] { bilgi });
            BindList();
            return View();
        }

    }
}
