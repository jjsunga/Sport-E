using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sport_E.Controllers
{
    public class EventPhotoController : Controller
    {

        Manager m = new Manager();
        // GET: EventPhoto
        public ActionResult Index()
        {
            return View("index", "home");
        }

        // GET: EventPhoto/Details/5
        [Route("eventpicturephoto/{id}")]
        public ActionResult Details(int? id)
        {
            var o = m.EventPhotoGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Attention - 9 - Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(o.EventPicturePhoto, o.EventPicturePhotoContentType);
            }
        }

        // GET: EventPhoto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventPhoto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventPhoto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventPhoto/Edit/5
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

        // GET: EventPhoto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventPhoto/Delete/5
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
