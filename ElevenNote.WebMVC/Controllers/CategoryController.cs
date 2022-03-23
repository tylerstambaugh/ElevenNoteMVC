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
            //var allCats = svc.GetAllCategories();


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
            var svc = CreateCategoryService();
            var CatToEdit = svc.GetCategoryById(id);

            var model = new CategoryEdit
            {
                CategoryId = CatToEdit.CategoryId,
                Name = CatToEdit.Name,
                Severity = CatToEdit.Severity
            };
            return View(model);
        }

        //POST /Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.CategoryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var svc = CreateCategoryService();
            

            if (svc.EditCategory(model))
            {
                TempData["SaveResultDelete"] = $"Category Id {id} was updated";
                return Redirect("/Category");
            }

            ModelState.AddModelError("", $"Category Id {id} could not be updated");

            return View(model);
        }

        //GET /Category/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var svc = CreateCategoryService();
            var CatToDel = svc.GetCategoryById(id);

            return View(new CategoryDelete
            {
                CategoryId = CatToDel.CategoryId,
                Name = CatToDel.Name
            });
        }

        //POST /Category/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            var svc = CreateCategoryService();

            svc.DeleteCategory(id);

            TempData["SaveResultDelete"] = $"Note Id {id} was deleted.";

            return Redirect("/Category");



            //if(!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //if(model.CategoryId != id)
            //{
            //    ModelState.AddModelError("", "Id Mismatch");
            //    return View(model);
            //}

            //var svc = CreateCategoryService();

            //if (svc.DeleteCategory(id))
            //{
            //    TempData["SaveResult"] = $"Category Id {id} was deleted.";
            //    return RedirectToAction("Index");
            //}
            //ModelState.AddModelError("", $"Category Id {id} could not be updated");
            //return View(model);
        }
    }
}
