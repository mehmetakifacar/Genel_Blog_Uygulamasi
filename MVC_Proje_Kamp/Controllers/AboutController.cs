using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());
        public IActionResult Index()
        {
            var aboutValues = aboutManager.GetList();

            return View(aboutValues);
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Add(About p)
        {
            aboutManager.AboutAdd(p);

            return RedirectToAction("Index");
        }


        public IActionResult AboutPartial()
        {
            return PartialView();
        }
    }
}
