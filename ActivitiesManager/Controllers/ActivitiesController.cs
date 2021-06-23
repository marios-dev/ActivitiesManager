using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ActivitiesManager.DAL;
using ActivitiesManager.Models;

namespace ActivitiesManager.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Activities
        public ActionResult Index(string sortOrder, string searchString)
        {
            //Sorting activities list starts here
            //Sorting with Date
            //unable to sort with ActivityType due to object type
            ViewBag.StartDateSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "startdate_asc" ? "startdate_desc" : "startdate_asc";
            ViewBag.EndDateSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "enddate_asc" ? "enddate_desc" : "enddate_asc";
            ViewBag.ActivityTypeSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "type_asc" ? "type_desc" : "type_asc";
            var activities = from a in db.Activities
                             select a;
            var activityType = from a in db.ActivityTypes
                               select a;

            switch (sortOrder)
            {
                case "startdate_desc":
                    activities = activities.OrderByDescending(a => a.StartDate);
                    break;
                case "enddate_desc":
                    activities = activities.OrderByDescending(a => a.EndDate);
                    break;
                case "type_desc":
                    activities = activities.OrderByDescending(a => a.ActivityType);
                    break;
                default:
                    activities = activities.OrderBy(a => a.StartDate);
                    break;
            }
            //Search through ActivityType
            if (!String.IsNullOrEmpty(searchString))
            {
                activities = activities.Where(a => a.ActivityType.Description.Contains(searchString));
            }
            activities = activities.Include(a => a.ActivityType);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.ActivityTypeID = new SelectList(db.ActivityTypes, "ID", "Description");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,Description,StartDate,EndDate,ActivityTypeID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityTypeID = new SelectList(db.ActivityTypes, "ID", "Description", activity.ActivityTypeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", activity.CustomerID);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityTypeID = new SelectList(db.ActivityTypes, "ID", "Description", activity.ActivityTypeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", activity.CustomerID);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,StartDate,EndDate,ActivityTypeID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTypeID = new SelectList(db.ActivityTypes, "ID", "Description", activity.ActivityTypeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", activity.CustomerID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
