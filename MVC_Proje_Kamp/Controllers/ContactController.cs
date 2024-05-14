using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Proje_Kamp.Controllers
{
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager(new EfContactDal());
        ContactValidator contactValidator = new ContactValidator();
        public IActionResult Index()
        {
            var contactValues = contactManager.GetContactList();

            return View(contactValues);
        }



        [HttpGet]
        public IActionResult GetContactDetails(int id)
        {
            var contactValues = contactManager.GetById(id);

            return View(contactValues);
        }



        [HttpPost]
        public IActionResult GetContactDetails(Contact p)
        {
            contactManager.ContactUpdate(p);

            return RedirectToAction("Index");
        }
    }
}
