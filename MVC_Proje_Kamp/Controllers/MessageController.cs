using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MVC_Proje_Kamp.Controllers
{
    public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        [Authorize]
        public IActionResult Inbox()
        {
            var messageList = messageManager.GetListInbox();

            return View(messageList);

        }



        public IActionResult Sendbox()
        {
            var messageList = messageManager.GetListSendbox();

            return View(messageList);
        }



        [HttpGet]
        public IActionResult GetInboxDetails(int id)
        {
            var inboxValues = messageManager.GetById(id);

            return View(inboxValues);
        }



        [HttpGet]
        public IActionResult GetSendboxDetails(int id)
        {
            var sendboxValues = messageManager.GetById(id);

            return View(sendboxValues);
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
