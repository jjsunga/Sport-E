using Microsoft.AspNet.Identity;
using Sport_E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sport_E.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager manager = new Manager();
        // GET: Admin
        public ActionResult Index()
        {
            return View(manager.EventPendingGetAll());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            var o = manager.EventGetOne(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            string [] token = id.Split(' ');
             
            int eventId = Int32.Parse(token[0]);
            var currentUserEmail = User.Identity.GetUserName();

            if (token[1] == "Accepted")
            {
                var itemToAccept = db.Events.SingleOrDefault(e => e.Id == eventId);

                itemToAccept.PublicationStatus = "Published";
                
                db.Notification.Add(new Notification { ToEmail = itemToAccept.EventCreator, ChangedByEmail = currentUserEmail, ChangeDate = DateTime.Now, Description = "Approved Event: " + itemToAccept.EventName, Read = false});

                db.SaveChanges();
            }
            else
            {
                var itemToReject = db.Events.SingleOrDefault(e => e.Id == eventId);

                itemToReject.PublicationStatus = "Rejected";
                
                db.Notification.Add(new Notification { ToEmail = itemToReject.EventCreator, ChangedByEmail = currentUserEmail, ChangeDate = DateTime.Now, Description = "Approved Event: " + itemToReject.EventName, Read = false });

                db.SaveChanges();
            }
            return RedirectToAction("Index", "Admin"); ;
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
