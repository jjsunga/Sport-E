using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sport_E.Controllers
{
    public class PhotoController : Controller
    {

        Manager m = new Manager();
        // GET: Photo
        public ActionResult Index()
        {
            return View("index", "home");
        }

        // GET: Photo/Details/5
        [Route("profilepicturephoto/{id}")]
        public ActionResult Details(int? id)
        {
            var email = User.Identity.GetUserName();
            var o = m.ProfilePictureGetById(email);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Attention - 9 - Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(o.ProfilePicturePhoto, o.ProfilePicturePhotoContentType);
            }
        }

        // GET: Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photo/Create
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

        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Photo/Edit/5
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

        // GET: Photo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Photo/Delete/5
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
