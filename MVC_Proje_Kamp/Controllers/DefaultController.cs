using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class DefaultController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        ContentManager contentManager = new ContentManager(new EfContentDal());

        public IActionResult Headings(int id)
        {
            var headingList = headingManager.GetHeadingList();
            var contentList = contentManager.GetListByHeadingId(id);
            ViewBag.Content = contentList;

            return View(headingList);
        }



        public IActionResult Index()
        {
            return View();
        }



        public IActionResult HomePage()
        {
            return View();
        }
    }
}
