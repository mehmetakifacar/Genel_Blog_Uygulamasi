using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Proje_Kamp.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        public IActionResult Index()
        {
            var headingValues = headingManager.GetHeadingList();

            return View(headingValues);
        }



        [HttpGet]
        public IActionResult AddHeading()
        {
            List<SelectListItem> category = (from i in categoryManager.GetCategoryList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryId.ToString()
                                             }).ToList();




            List<SelectListItem> writer = (from i in writerManager.GetWriterList()
                                           select new SelectListItem
                                           {
                                               Text = i.WriterName + " " + i.WriterSurname,
                                               Value = i.WriterId.ToString()
                                           }).ToList();


            ViewBag.categories = category;
            ViewBag.writers = writer;

            return View();
        }



        [HttpPost]
        public IActionResult AddHeading(Heading p)
        {
            headingManager.HeadingAdd(p);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            List<SelectListItem> category = (from i in categoryManager.GetCategoryList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryId.ToString()
                                             }).ToList();

            ViewBag.categories = category;

            var update = headingManager.GetById(id);

            return View(update);
        }



        [HttpPost]
        public IActionResult Update(Heading p)
        {
            headingManager.HeadingUpdate(p);

            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var delete = headingManager.GetById(id);
            delete.HeadingStatus = false;
            headingManager.HeadingDelete(delete);

            return RedirectToAction("Index");
        }
    }
}
