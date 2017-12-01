using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sport_E.Controllers
{
    public class CommentsController : Controller
    {
        private Manager m = new Manager();
        // GET: Comments
        public ActionResult Index()
        {
            // Fetch the collection
            var c = m.CommentGetAll();

            // Pass the collection to the view
            return View(c);
        }

        public ActionResult WithEventInfo()
        {
            return View(m.CommentGetAllWithEventInfo());
        }

        public ActionResult WithTeamName()
        {
            return View(m.CommentGetAllWithEventId());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.CommentGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        public ActionResult Create(CommentAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AddComment(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.CommentId });
            }
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
