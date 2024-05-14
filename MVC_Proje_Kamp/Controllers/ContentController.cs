using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class ContentController : Controller
    {
        ContentManager contentManager = new ContentManager(new EfContentDal());
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ContentByHeading(int id)
        {
            var contentValues = contentManager.GetListByHeadingId(id);

            return View(contentValues);

        }
    }
}
