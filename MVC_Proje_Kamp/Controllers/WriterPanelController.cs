using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace MVC_Proje_Kamp.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        public IActionResult WriterProfile()
        {
            return View();
        }



        [Authorize(AuthenticationSchemes = "WriterScheme")]
        public IActionResult MyHeadings()
        {
            var values = headingManager.GetListByWriter();

            return View(values);
        }



        [HttpGet]
        public IActionResult NewHeading()
        {
            List<SelectListItem> category = (from i in categoryManager.GetCategoryList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryId.ToString()
                                             }).ToList();

            ViewBag.categories = category;

            return View();

        }



        [HttpPost]
        public IActionResult NewHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterId = 3;
            p.HeadingStatus = true;
            headingManager.HeadingAdd(p);

            return RedirectToAction("MyHeadings", "WriterPanel");
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
            p.WriterId = 3;
            headingManager.HeadingUpdate(p);

            return RedirectToAction("MyHeadings");
        }



        public IActionResult Delete(int id)
        {
            var delete = headingManager.GetById(id);
            delete.HeadingStatus = false;
            headingManager.HeadingDelete(delete);

            return RedirectToAction("MyHeadings");
        }



        public IActionResult AllHeadings(int p = 1)
        {
            var allHeadings = headingManager.GetHeadingList().ToPagedList(p, 4);

            return View(allHeadings);
        }

        
    }
}
