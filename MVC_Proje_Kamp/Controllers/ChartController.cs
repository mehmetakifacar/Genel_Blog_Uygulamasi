using Microsoft.AspNetCore.Mvc;
using MVC_Proje_Kamp.Models;
using System.Web.Mvc;


namespace MVC_Proje_Kamp.Controllers
{
        
    public class ChartController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        
        public IActionResult CategoryChart()
        {
            return Json(BlogList(), JsonRequestBehavior.AllowGet);
        }



        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> category = new List<CategoryClass>();
            category.Add(new CategoryClass()
            {
                CategoryName = "Yazılım",
                CategoryCount = 8
            });
            category.Add(new CategoryClass()
            {
                CategoryName = "Seyahat",
                CategoryCount = 4
            });
            category.Add(new CategoryClass()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 7
            });
            category.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 1
            });

            return category;
        }
    }
}
