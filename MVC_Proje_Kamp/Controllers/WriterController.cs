using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class WriterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        WriterValidator wtValidator = new WriterValidator();
        public IActionResult Index()
        {
            var writerValues = writerManager.GetWriterList();

            return View(writerValues);
        }



        [HttpGet]
        public IActionResult AddWriter()
        {
            return View();
        }



        [HttpPost]
        public IActionResult AddWriter(Writer p)
        {
            ValidationResult result = wtValidator.Validate(p);

            if (result.IsValid)
            {
                writerManager.WriterAdd(p);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(p);
            }
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            var writerValue = writerManager.GetById(id);

            return View(writerValue);
        }



        [HttpPost]
        public IActionResult Update(Writer p)
        {
            ValidationResult result = wtValidator.Validate(p);

            if (result.IsValid)
            {
                writerManager.WriterUpdate(p);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(p);
        }
    }
}
