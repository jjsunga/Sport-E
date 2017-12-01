using Sport_E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sport_E.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager manager = new Manager();
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpcomingEvents(string requestedDate)
        {
            //var o = requestedDate.Month.ToString() + requestedDate.Year.ToString();
            string[] token = requestedDate.Split(' ');
            ViewBag.RequestedMonth = token[0];
            ViewBag.RequestedYear = token[1];
            //o = manager.CalendarMonthYear();
            return View();
        }

        // GET: Calendar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Calendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calendar/Create
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

        // GET: Calendar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Calendar/Edit/5
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

        // GET: Calendar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calendar/Delete/5
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
