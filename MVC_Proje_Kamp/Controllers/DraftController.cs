using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class DraftController : Controller
    {
        DraftManager draftManager = new DraftManager(new EfDraftDal());
        public IActionResult Index()
        {
            var draftList = draftManager.GetDraftList();

            return View(draftList);
        }



        [HttpGet]
        public IActionResult DraftDetails(int id)
        {
            var draftValues = draftManager.GetById(id);

            return View(draftValues);
        }



        [HttpPost]
        public IActionResult DraftDetails(Draft p)
        {
            draftManager.DraftAdd(p);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult GetDraftDetails(int id)
        {
            var draftValues = draftManager.GetById(id);

            return View(draftValues);
        }
    }
}
