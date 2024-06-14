using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Proje_Kamp.ViewModels;
using System.Security.Claims;

namespace MVC_Proje_Kamp.Controllers
{
    public class WriterPanelContentController : Controller
    {
        IContentService _contentService;
        IWriterService _writerService;
        public WriterPanelContentController(IContentService contentService, IWriterService writerService)
        {
            _contentService = contentService;
            _writerService = writerService;
        }



        [Authorize]
        public IActionResult MyContent()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                return RedirectToAction("Page403", "ErrorPage");
            }

            var writer = _writerService.GetWriterList().FirstOrDefault(x => x.Mail == email);

            if (writer == null)
            {
                return RedirectToAction("Page403", "ErrorPage");
            }

            var contents = _contentService.GetListByWriter(writer.WriterId);

            var model = new MyContentViewModel
            {
                Contents = contents
            };

            return View(model);
        }



        [HttpGet]
        public IActionResult AddContent(int id)
        {
            var headindId = new Heading
            {
                HeadingId = id
            };

            ViewBag.Id = headindId;

            return View();
        }



        public IActionResult AddContent(Content p)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var writerInfo = _writerService.GetWriterList().Where(x => x.Mail == email)
                .Select(y => y.WriterId).FirstOrDefault();

            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterId = writerInfo;
            p.ContentStatus = true;
            _contentService.ContentAdd(p);

            return RedirectToAction("MyContent");
        }


    }
}
