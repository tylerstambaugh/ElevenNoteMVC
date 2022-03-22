using ElevenNote.Data;
using ElevenNote.Models.CategoryModels;
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
        [Authorize]
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

        //GET: /Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            var svc = CreateCategoryService();
            var allCats = svc.GetAllCategories();

            var sevs = Enum.GetValues(typeof(Severity))
                 .Cast<Severity>()
                 .Select(v => v.ToString()).ToList();

            ViewData["Severities"] = sevs.Select(s => new SelectListItem
            {
                Text = s.ToString(),
                Value = s
            });

            return View();
        }

        //POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryCreate cc)
        {
            var svc = CreateCategoryService();
            var allCats = svc.GetAllCategories();

            if (ModelState.IsValid)
            {
                if (svc.CreateCategory(cc))
                    return RedirectToAction("Index");
            }
            ViewData["Error"] = "Invalid Category Create Model";
            return View(cc);
        }

        //GET /Category/Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {

            return View();
        }
    }
}