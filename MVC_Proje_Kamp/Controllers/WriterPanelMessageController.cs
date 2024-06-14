using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;


namespace MVC_Proje_Kamp.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public IActionResult Inbox()
        {
            var inboxList = messageManager.GetListInbox();

            return View(inboxList);
        }



        public IActionResult Sendbox()
        {
            var sendboxList = messageManager.GetListSendbox();

            return View(sendboxList);
        }



        public IActionResult GetInboxMessageDetails(int id)
        {
            var inboxDetails = messageManager.GetById(id);

            return View(inboxDetails);
        }



        public IActionResult GetSendboxMessageDetails(int id)
        {
            var sendboxDetails = messageManager.GetById(id);

            return View(sendboxDetails);
        }



        [HttpGet]
        public IActionResult NewMessage()
        {
            return View();
        }



        [HttpPost]
        public IActionResult NewMessage(Message p)
        {
            ValidationResult results = messageValidator.Validate(p);

            if (results.IsValid)
            {
                p.SenderMail = "ceren@gmail.com";
                p.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
                messageManager.MessageAdd(p);

                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View();
        }
    }
}
