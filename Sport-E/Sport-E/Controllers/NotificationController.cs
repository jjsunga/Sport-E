using Sport_E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sport_E.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager manager = new Manager();

        // GET: Notification
        public ActionResult Index()
        {
            var o = manager.NotificationsAll();
            return View(o);
        }

        // GET: Notification/Details/5
        public ActionResult Details()
        {
            var o = manager.NotificationsPending();
            return View(o);
        }

        // GET: Notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notification/Create
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

        // GET: Notification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notification/Edit/5
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

        // GET: Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            var notificationToRead = db.Notification.SingleOrDefault(e => e.Id == id);
            notificationToRead.Read = true;
            db.SaveChanges();
            return RedirectToAction("Details", "Notification"); ;
        }

        // POST: Notification/Delete/5
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
