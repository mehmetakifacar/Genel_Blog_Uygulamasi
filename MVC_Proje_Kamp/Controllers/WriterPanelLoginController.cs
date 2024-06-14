using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MVC_Proje_Kamp.Controllers
{
    public class WriterPanelLoginController : Controller
    {
        private readonly WriterManager _writerManager;
        public WriterPanelLoginController(WriterManager writerManager)
        {
            _writerManager = writerManager;
        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterLogin()
        {
            return View();
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> WriterLogin(Writer p)
        {
            var login = _writerManager.LogIn(p);

            if (login != null)
            {
                await HttpContext.SignInAsync("WriterScheme", login);

                return RedirectToAction("MyHeadings", "WriterPanel");
            }

            return RedirectToAction("WriterLogin");
        }
        

        
        //Logout
        public async Task<IActionResult> WriterLogOut()
        {
            await HttpContext.SignOutAsync("WriterScheme");

            return RedirectToAction("WriterLogin", "WriterPanelLogin");
        }


    }
}
