using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sunum.Csrf.Web.Controllers
{
    public class HomeController : BaseController
    {
        object _lock = new object();

        string path = "App_Data/data.txt";
        private void BindList()
        {
            string[] bilgiler;
            lock (_lock)
            {
                 bilgiler = System.IO.File.ReadAllLines(Server.MapPath(path));    
            }
            
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

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(string bilgi)
        {
            lock (_lock)
            {
                System.IO.File.AppendAllLines(Server.MapPath(path), new[] { bilgi });
            }            
            BindList();
            return View();
        }

    }
}
