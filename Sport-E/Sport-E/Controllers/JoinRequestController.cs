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
    public class JoinRequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager manager = new Manager();

        // GET: Notification
        public ActionResult Index()
        {
            return View(manager.JoinRequestGetAll());
        }


        // GET: Notification/Details/5
        public ActionResult Details(int? id)
        {
            var o = manager.JoinRequestGetOne(id.GetValueOrDefault());

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
        public ActionResult Edit(string id)
        {
            string[] token = id.Split(' ');

            int JoinId = Int32.Parse(token[0]);
            var currentUserEmail = User.Identity.GetUserName();

            if (token[1] == "Accepted")
            {
                var a = id.ToString();
                var Event = db.JoinRequest.SingleOrDefault(e => e.Id == JoinId);
                var eve = Int32.Parse(Event.Event_j);

                var itemToAccept = db.Events.SingleOrDefault(e => e.Id == eve);
                var u = db.UserProfiles.Where(s => String.Compare(s.Email, currentUserEmail) == 0).SingleOrDefault();
                //var j = db.JoinRequest.SingleOrDefault(e => e.Email_j == currentUserEmail && e.Event_j == Event.Event_j);
                db.Notification.Add(new Notification { ToEmail = Event.Email_j, ChangedByEmail = currentUserEmail, ChangeDate = DateTime.Now, Description = "Join Request Approved: " + itemToAccept.EventName, Read = false });

                //if (itemToAccept != null && Event.Email_j != null && Event.Id >= 0)
                //{
                    itemToAccept.pp[Event.Id] = Event.Email_j;
                    //.SetValue(Event.Email_j, Event.Id);
                //}

                Event.PublicationStatus = "Accepted";
                
                var c = db.Events.Find(Int32.Parse(Event.Event_j));
                var b = db.UserProfiles.Where(s => s.Email.Equals(Event.Email_j)).SingleOrDefault();
                if (c.UserProfiles.Count() < c.Capacity)
                {
                    c.UserProfiles.Add(b);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Event Capacity Full!");
                    Event.PublicationStatus = "Rejected";
                }

                db.SaveChanges();
            }
            else
            {
                var a = id.ToString();
                var Event = db.JoinRequest.SingleOrDefault(e => e.Id == JoinId);
                var eve = Int32.Parse(Event.Event_j);

                var itemToAccept = db.Events.SingleOrDefault(e => e.Id == eve);
                var u = db.UserProfiles.Where(s => String.Compare(s.Email, currentUserEmail) == 0).SingleOrDefault();
                //var j = db.JoinRequest.SingleOrDefault(e => e.Email_j == currentUserEmail && e.Event_j == Event.Event_j);
                db.Notification.Add(new Notification { ToEmail = Event.Email_j, ChangedByEmail = currentUserEmail, ChangeDate = DateTime.Now, Description = " Join Request Rejected: " + itemToAccept.EventName, Read = false });

                Event.PublicationStatus = "Rejected";

                db.SaveChanges();
            }
            return RedirectToAction("Index", "JoinRequest"); ;
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
        public ActionResult Delete(int id)
        {
            return View();
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
