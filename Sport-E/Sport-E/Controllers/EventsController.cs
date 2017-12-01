using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sport_E.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sport_E.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager manager = new Manager();

        // GET: Events
        public ActionResult Index(string searchName, string searchCreator, string searchCity, string searchDate, string searchSkillLevel, string searchGender)
        {
            var events = db.Events.Include("UserProfiles").Where(e => DateTime.Compare(e.EventDate, DateTime.Now) > 0).Where(e => String.Compare(e.PublicationStatus, "Published") == 0);

            if (!String.IsNullOrEmpty(searchName))
            {
                events = events.Where(e => e.EventName.Contains(searchName));
            }

            if (!String.IsNullOrEmpty(searchCreator))
            {
                events = events.Where(e => e.EventCreator.Contains(searchCreator));
            }

            if (!String.IsNullOrEmpty(searchCity))
            {
                events = events.Where(e => e.City.Contains(searchCity));
            }

            if (!String.IsNullOrEmpty(searchSkillLevel))
            {
                events = events.Where(e => e.SkillLevel.Contains(searchSkillLevel));
            }

            if (!String.IsNullOrEmpty(searchGender))
            {
                if (searchGender != "Any")
                {
                    if (searchGender == "Male")
                    {
                        events = events.Where(e => !e.Gender.Contains("Female"));
                    }
                    events = events.Where(e => e.Gender.Contains(searchGender));
                }
            }

            return View(events);
        }

        // GET: Events
        public ActionResult MyEvents(string requestedDate)
        {
            string[] token = requestedDate.Split(' ');
            ViewBag.RequestedMonth = token[0];
            ViewBag.RequestedYear = token[1];

            var events = db.Events.Include("UserProfiles").Where(e => DateTime.Compare(e.EventDate, DateTime.Now) > 0).Where(e => String.Compare(e.PublicationStatus, "Published") == 0).Where(e => e.EventCreator.Equals(User.Identity.Name));

            return View(events);

        }

        // GET: Event/AddComment/5
        public ActionResult AddComment(int? id)
        {
            // Study the Edit view

            // Attempt to fetch the matching object
            var o = manager.EventGetByIdWithPlayers(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form
                var form = new EventAddCommentForm();

                form.EventId = o.id;
                //form.EventName = o.EventName;
                //form.CommentsInEvent = o.Comments.OrderBy(t => t.CommentId);

                form.Email = User.Identity.Name;

                return View(form);
            }
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        public ActionResult AddComment(int? id, EventAddComments newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("AddComment", new { id = newItem.EventId });
            }

            if (id.GetValueOrDefault() != newItem.EventId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the upate
            
            var editedItem = manager.EventAddComments(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("AddComment", new { id = newItem.EventId });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.EventId });
            }
        }



        // GET: Events/Past
        public ActionResult Past(string searchName, string searchCreator, string searchCity, string searchDate, string searchSkillLevel, string searchGender)
        {

            //var currentUser = db.UserProfiles.Where(u => u.Email.Equals(User.Identity.Name)).SingleOrDefault();
            //var events = db.Events.Include("UserProfiles").Where(e => DateTime.Compare(e.EventDate, DateTime.Now) < 0 && e.UserProfiles.Contains(currentUser));

            var events = db.Events.Include("UserProfiles").Where(e => DateTime.Compare(e.EventDate, DateTime.Now) < 0);

            if (!String.IsNullOrEmpty(searchName))
            {
                events = events.Where(e => e.EventName.Contains(searchName));
            }

            if (!String.IsNullOrEmpty(searchCreator))
            {
                events = events.Where(e => e.EventCreator.Contains(searchCreator));
            }

            if (!String.IsNullOrEmpty(searchCity))
            {
                events = events.Where(e => e.City.Contains(searchCity));
            }

            if (!String.IsNullOrEmpty(searchSkillLevel))
            {
                events = events.Where(e => e.SkillLevel.Contains(searchSkillLevel));
            }

            if (!String.IsNullOrEmpty(searchGender))
            {
                if (searchGender != "Any")
                {
                    if (searchGender == "Male")
                    {
                        events = events.Where(e => !e.Gender.Contains("Female"));
                    }
                    events = events.Where(e => e.Gender.Contains(searchGender));
                }
            }

            return View(events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = manager.EventGetByIdWithPlayers(id.GetValueOrDefault());

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

        // GET: Events/Create
        public ActionResult Create()
        {
            var form = new EventAddForm();
            return View(form);
        }

        // POST: Events/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(EventAdd newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            
            TimeSpan ts = new TimeSpan(newItem.Hour, newItem.Minute, 0);
            newItem.EventDate = newItem.EventDate + ts;
            /*
            EventAdd addthis = new EventAdd();
            addthis.EventName = newItem.EventName;
            addthis.EventCreator = newItem.EventCreator;
            addthis.EventDate = newItem.EventDate;
            addthis.PublicationStatus = newItem.PublicationStatus;
            addthis.City = newItem.City;
            addthis.Street = newItem.Street;
            addthis.Province = newItem.Province;
            addthis.PostalCode = newItem.PostalCode;
            addthis.Country = newItem.Country;
            addthis.Gender = newItem.Gender;
            addthis.SkillLevel = newItem.SkillLevel;
            addthis.PublicationStatus = newItem.PublicationStatus;
            */

            var addedItem = manager.EventAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Index", new { id = addedItem.Id });
            }
        }



        // GET: Events/Join/5

        public ActionResult Join(string id)
        {

            int eventId = Int32.Parse(id);
            var currentUserEmail = User.Identity.GetUserName();

            var jr = db.JoinRequest.Where(s => String.Compare(s.Email_j, currentUserEmail) == 0).Where(s => String.Compare(s.Event_j, eventId.ToString()) == 0).SingleOrDefault();
            if (jr != null)
            {
                    System.Windows.Forms.MessageBox.Show("You have already made a request to this event");
            }
            else
            {

                var itemToAccept = db.Events.SingleOrDefault(e => e.Id == eventId);
                var u = db.UserProfiles.Where(s => String.Compare(s.Email, currentUserEmail) == 0).SingleOrDefault();

                db.JoinRequest.Add(new JoinRequest { Event_j = (itemToAccept.Id).ToString(), Email_j = u.Email, PublicationStatus = "Under Review" });
                db.Notification.Add(new Notification { ToEmail = itemToAccept.EventCreator, ChangedByEmail = currentUserEmail, ChangeDate = DateTime.Now, Description = "Event to Approve: " + itemToAccept.EventName, Read = false });
            }

            db.SaveChanges();
            
            return RedirectToAction("Index"); ;

        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = manager.EventGetOne(id.GetValueOrDefault());

            var currentUserEmail = User.Identity.GetUserName();

            int? eventId = id;

            var itemToAccept = db.Events.SingleOrDefault(e => e.Id == eventId);

            if ((String.Compare(itemToAccept.EventCreator, currentUserEmail) == 0))
            {
                if (o != null)
                {
                    var editForm = AutoMapper.Mapper.Map<EventEditContactInfo>(o);
                    return View(editForm);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, EventEditContactInfo newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = newItem.EventName });
            }

            if (id.GetValueOrDefault() != newItem.id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("Index");
            }

            // Attempt to do the update
            var editedItem = manager.EventEditContactInfo(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = newItem.id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("Details", new { id = newItem.id });
            }
        }

        // GET: Events/Rate/5
        public ActionResult Rate(int? id)
        {
            // Attempt to fetch the matching object
            var o = manager.EventGetOne(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure an "edit form"

                // Notice that o is a CustomerBase object
                // We must map it to a CustomerEditContactInfoForm object
                // Notice that we can use AutoMapper anywhere, 
                // and not just in the Manager class!
                var rateForm = AutoMapper.Mapper.Map<EventRate>(o);

                return View(rateForm);
            }
        }

        // POST: Events/Rate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rate(int? id, EventRate newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Rate", new { id = newItem.EventName });
            }

            if (id.GetValueOrDefault() != newItem.id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("Index");
            }

            // Attempt to do the update
            var editedItem = manager.EventRate(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Rate", new { id = newItem.id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("Details", new { id = newItem.id });
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = manager.EventGetOne(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
           
        /* var result = manager.EventDelete(id.GetValueOrDefault());

            // "result" will be true or false
            // We probably won't do much with the result, because 
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return RedirectToAction("Index");
        }
        */

       
         var o = manager.EventDelete(id.GetValueOrDefault());

            var currentUserEmail = User.Identity.GetUserName();

            int? eventId = id;

            var itemToAccept = db.Events.SingleOrDefault(e => e.Id == eventId);

            if ((String.Compare(itemToAccept.EventCreator, currentUserEmail) == 0))
            {
                if (o != null)
                {
                    var editForm = AutoMapper.Mapper.Map<EventEditContactInfo>(o);
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
         
        ////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
