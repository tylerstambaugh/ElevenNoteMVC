using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new CategoryService(userId);
            return svc;
        }

        // GET: Category
        public ActionResult Index()
        {
            var svc = CreateCategoryService();
            var model = svc.GetAllCategories();
            return View(model);
        }
    }
}