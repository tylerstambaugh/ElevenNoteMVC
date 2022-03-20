using ElevenNote.Models.NoteModels;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    public class NoteController : Controller
    {
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new NoteService(userId);
            return svc;
        }

        [Authorize]
        // GET: /Note/Index
        public ActionResult Index()
        {
            var svc = CreateNoteService();
            var model = svc.GetNotes();
            return View(model);
        }

        //GET: /Note/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }


        //POST: /Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var svc = CreateNoteService();

            if (svc.CreateNote(model))
            {
                TempData["SaveResult"] = "Note was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        //GET: / Note/Details/{id}
        [HttpGet]
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //GET: /Note/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var svc = CreateNoteService();
            var deets = svc.GetNoteById(id);
            var model = new NoteEdit
            {
                NoteId = deets.NoteId,
                Title = deets.Title,
                Content = deets.Content
            };
            return View(model);
        }

        //POST: /Note/Edit{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var svc = CreateNoteService();

            if(svc.UpdateNote(model))
            {
                TempData["SaveResult"] = $"Note Id {id} was updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"Note Id {id} could not be updated");
            return View(model);
        }

        //GET /Note/Delete/{id}
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //POST /Note/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateNoteService();

            svc.DeleteNote(id);

            TempData["SaveResult"] = $"Note Id {id} was deleted.";

            return RedirectToAction("Index");
        }
    }
}