using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//new
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Sport_E.Models;
using System.Net;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System.Web.Security;
using System.Web.UI;
using System.Security.Principal;

namespace Sport_E.Controllers
{
  
    public class MaintenanceController : Controller
    {
        private ApplicationDbContext ds = new ApplicationDbContext();
        public MaintenanceController() :
            this(new UserManager<Models.ApplicationUser>(new UserStore<Models.ApplicationUser>(new Models.ApplicationDbContext())))
        { }

        public MaintenanceController(UserManager<Models.ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        private UserManager<Models.ApplicationUser> UserManager { get; set; }

        // GET: Maintenance
    
        public ActionResult Index()
        {
            return View();
        }

        // GET: Maintenance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Maintenance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maintenance/Create
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

        // GET: Maintenance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Maintenance/Edit/5
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

        // GET: Maintenance/Delete/5
        public ActionResult Delete(string email)
        {
            return View(new UserDelete { UserName = email });
        }

        // POST: Maintenance/Delete/5
        [HttpPost]
        public ActionResult Delete(string email, FormCollection collection)
        {
            var itemToDelete = ds.UserProfiles.SingleOrDefault(user => user.Email == email);

            if (itemToDelete == null)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {

                // Remove the object
                var userMain = ds.Users.SingleOrDefault(user => user.Email == itemToDelete.Email);
              //  var applicationUser = UserManager.Users.SingleOrDefault(au => au.UserName == email);

                ds.UserProfiles.Remove(itemToDelete);
                ds.SaveChanges();
                ds.Users.Remove(userMain);
                ds.SaveChanges();
                //    string usernumber = userMain.Id;
                /*
                                for (int i = 0; i < 4; i++)
                                {
                                    var userRole = ds.Roles.Find(usernumber);
                                    ds.Roles.Remove(userRole);
                                    ds.SaveChanges();
                                }
                */
                // AuthenticationManager.Signout(DefaultAuthenticationTypes.ApplicationCookie);
                //FormsAuthentication.SignOut();
                // Session.Abandon();
                //return RedirectToRoute("Index", "Maintenance");

               // return RedirectResult("View", "Maintenance");

                return RedirectToAction("Index", "Maintenance"); 

                /*   var result = await UserManager.DeleteAsync(applicationUser);

                   if (result.Succeeded)
                   {
                       // Good result, redirect...
                       return RedirectToAction("Index");
                   }
                   else
                   {
                       ModelState.AddModelError("", "Unable to delete user");
                       var errors = string.Join(", ", result.Errors);
                       ModelState.AddModelError("", errors);
                       return View();
                   }
              */
            }
        }
    }


 
public class UserDelete
    {
        [Display(Name = "User account")]
        [Required]
        public string UserName { get; set; }
    }
}
