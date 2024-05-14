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



namespace MVC_Proje_Kamp.Controllers
{
    public class LoginController : Controller
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());

        [AllowAnonymous]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Cookie tabanlı oturumu sonlandır

            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        //logın
        public async Task<IActionResult> Index(Admin p)
        {
            var context = adminManager.Login(p);
            if (context != null)
            {
                await HttpContext.SignInAsync(context);

                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }


            //MvcContext mvcContext = new MvcContext();

            //var adminValues = mvcContext.Admins.FirstOrDefault
            //    (x => x.UserName == p.UserName && x.Password == p.Password);

            //if (adminValues != null)
            //{
            //    var claims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name,p.UserName),
            //        new Claim(ClaimTypes.Role, adminValues.Role)
            //    };

            //    var userIdentity = new ClaimsIdentity(claims, "Login");
            //    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            //    await HttpContext.SignInAsync(principal);

            //    return RedirectToAction("Index", "AdminCategory");
            //}
            //else
            //{
            //    return RedirectToAction("Index");
            //}
        }
    }
}
