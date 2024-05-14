using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class CategoryController : Controller
    {
        AdminManager _adminManager = new AdminManager(new EfAdminDal());
        CategoryManager _ctManager = new CategoryManager(new EfCategoryDal());

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList()
        {
            if (!HttpContext.User.IsInRole("A"))
            {
                return Unauthorized(); // Yetkilendirme başarısız
            }
            //if (!_adminManager.Authenticate("A", HttpContext))
            //{
            //    return Unauthorized(); // Yetkilendirme başarısız
            //}
            else
            {
                var category = _ctManager.GetCategoryList();
                return View(category);
            }


        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            //ctManager.CategoryAddBl(p);
            CategoryValidator ctvalidator = new CategoryValidator();
            ValidationResult results = ctvalidator.Validate(p);
            if (results.IsValid)
            {
                _ctManager.CategoryAdd(p);
                return RedirectToAction("GetList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
    }
}
