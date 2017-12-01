using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity; //Added: Agam
using Sport_E.Models;               //Added: Agam
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Sport_E.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {

        private ApplicationDbContext ds = new ApplicationDbContext();

        public UserProfilesController() :
            this(new UserManager<Models.ApplicationUser>(new UserStore<Models.ApplicationUser>(new Models.ApplicationDbContext())))
        {

        }

        public UserProfilesController(UserManager<Models.ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        private Manager m = new Manager();

        private UserManager<Models.ApplicationUser> UserManager { get; set; }

        // GET: UserProfiles
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserProfiles/Details/5
        public ActionResult Details()
        {
            var email = User.Identity.GetUserName();
            /*
            var o = from userprofs in db.UserProfiles
                    select userprofs;


            o = o.Where(e => e.Email.Contains(email));

            foreach (UserProfile userprofs in db.UserProfiles)
            {
                string emailFromProfile = e.Email;

                foreach (string a in e.Attendees)
                {
                    if (a.Equals(/*email))
                    {
                        events = events.Where(c => c.EventName.Equals(b));
                    }
                }
            }
            var obj = m.UserProfileGetOne(o);
            */
            // string query = "SELECT Id  FROM UserProfiles WHERE Email = " + email;

            // var dat = db.Database.SqlQuery<UserProfile>(query);

            var obj = m.UserProfileGetOne(email);

            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(obj);
            }
        }

        // GET: UserProfiles/Create
        public ActionResult Create()
        {
            var form = new UserProfileAddForm();
            return View(form);
        }

        // POST: UserProfiles/Create
        [HttpPost]
        public ActionResult Create(UserProfileAdd newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            var addedItem = m.UserProfileAdd(newItem);

            if (addedItem == null)
            {
                return View(addedItem);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        // GET: UserProfiles/Edit/5
        public ActionResult Edit(int? userId)
        {
            var email = User.Identity.GetUserName();
            var o = m.UserProfileGetOne(email);
           // var o = m.UserProfileGetOneWithId(userId.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<UserProfileEditinfoForm>(o);
                return View(editForm);
            }
        }

        // POST: UserProfiles/Edit/5
        [HttpPost]
        public ActionResult Edit(string email, UserProfileEditinfo newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { email = newItem.Email });
            }

            if (email != newItem.Email)
            {
                return RedirectToAction("index");
            }

            var editedItem = m.UserProfileEditinfo(newItem);

            if (editedItem == null)
            {
                return RedirectToAction("edit", new { email = newItem.Email });
            }
            else
            {
                return RedirectToAction("details");
            }
        }

        // GET: UserProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.UserProfileGetOneWithId(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("details");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: UserProfiles/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int? id, FormCollection collection)
        {
            //var result = m.UserProfileDelete(id.GetValueOrDefault());
            var itemToDelete = ds.UserProfiles.SingleOrDefault(user => user.Id == id);

            if (itemToDelete == null)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {

                // Remove the object
               // var userMain = ds.Users.SingleOrDefault(user => user.Email == itemToDelete.Email);
                var applicationUser =
                    UserManager.Users.SingleOrDefault(au => au.Email == itemToDelete.Email);


                ds.UserProfiles.Remove(itemToDelete);
                ds.SaveChanges();

                var result = await UserManager.DeleteAsync(applicationUser);


                return RedirectToAction("Index", "Home");
            }
        }
    }
}
