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
    public class CustomersController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Customers
        public ActionResult Index(string sortOrder, string searchString)
        {
            //The sorting customers list comes here
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.AddressSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "address_asc" ? "address_desc" : "address_asc";
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "type_asc" ? "type_desc" : "type_asc";
            var customers = from c in db.Customers
                            select c;
            //var customerTypes = from t in db.CustomerTypes
            //                    select t;
            //var customerTypeDesc = customerTypes.Include(t => t.Description);
            //customers = customers.Include(c => c.CustomerType.Description);
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.Name);
                    break;
                case "name_asc":
                    customers = customers.OrderBy(c => c.Name);
                    break;
                case "address_desc":
                    customers = customers.OrderByDescending(c => c.Address);
                    break;
                case "address_asc":
                    customers = customers.OrderBy(c => c.Address);
                    break;
                case "type_desc":
                    customers = customers.OrderByDescending(c => c.CustomerType.Description);
                    break;
                case "type_asc":
                    customers = customers.OrderBy(c => c.CustomerType.Description);
                    break;
                default:
                    customers = customers.OrderBy(c => c.Name);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.Name.Contains(searchString) 
                || c.Address.Contains(searchString) 
                || c.CustomerType.Description.Contains(searchString));
            }

            return View(customers.ToList());
            //filtered list ends here
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Description");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CustomerTypeID,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Description", customer.CustomerTypeID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            //prepei na kalei to viewmodel
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Description", customer.CustomerTypeID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CustomerTypeID,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Description", customer.CustomerTypeID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
