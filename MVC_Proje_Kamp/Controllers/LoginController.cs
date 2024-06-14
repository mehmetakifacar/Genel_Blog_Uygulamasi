using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Claims;
using DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using BusinessLayer.Abstract;



namespace MVC_Proje_Kamp.Controllers
{
    public class LoginController : Controller
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        //LogIn
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(Admin p)
        {
            var context = adminManager.LogIn(p);
            if (context != null)
            {
                await HttpContext.SignInAsync("AdminScheme", context);

                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        

        //Logout   
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("AdminScheme");

            return RedirectToAction("Index", "Login");
        }


    }
}






