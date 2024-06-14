using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class ContentController : Controller
    {
        ContentManager _contentManager;
        public ContentController(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult GetAllContent(string p)
        {
            var values = _contentManager.GetContentList(p);

            return View(values);
        }



        public IActionResult ContentByHeading(int id)
        {
            var contentValues = _contentManager.GetListByHeadingId(id);

            return View(contentValues);

        }
    }
}
