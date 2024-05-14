using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class AdminCategoryController : Controller
    {

        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        [Authorize]
        public IActionResult Index()
        {
            var categoryValues = categoryManager.GetCategoryList();

            return View(categoryValues);
        }



        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }



        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult result = categoryValidator.Validate(p);

            if (result.IsValid)
            {
                categoryManager.CategoryAdd(p);

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



        public IActionResult Delete(int id)
        {
            var delete = categoryManager.GetById(id);
            categoryManager.CategoryDelete(delete);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            var update = categoryManager.GetById(id);

            return View(update);
        }



        [HttpPost]
        public IActionResult Update(Category p)
        {
            categoryManager.CategoryUpdate(p);

            return RedirectToAction("Index");
        }




    }
}
