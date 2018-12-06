using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class PickupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pickups
        public ActionResult Index()
        {
            string customer = User.Identity.GetUserId();
            var pickupcustomer = db.Customer.Where(c => c.AppUserID == customer).Single();
            List<Pickup> pickupsInZip = db.Pickups.Where(p => p.CustomerID == pickupcustomer.CustomerID).ToList();

            return View(pickupsInZip);
        }

        // GET: Pickups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // GET: Pickups/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Address");
            return View();
        }

        // POST: Pickups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickupId,PickupDay,VacaStart,VacaEnd,ExtraPickup")] Pickup pickup)
        {
            string AppCutID = User.Identity.GetUserId();
            var pickupcustomer = db.Customer.Where(s => s.AppUserID == AppCutID).Single();
            pickup.CustomerID = pickupcustomer.CustomerID;

            if (ModelState.IsValid)
            {
                db.Pickups.Add(pickup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Address", pickup.CustomerID);
            return View(pickup);
        }

        // GET: Pickups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "PickupDay", pickup.CustomerID);
            return View(pickup);
        }

        // POST: Pickups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickupId,PickupDay,VacaStart,VacaEnd,ExtraPickup")] Pickup pickup)
        {
            string AppCutID = User.Identity.GetUserId();
            var pickupcustomer = db.Customer.Where(s => s.AppUserID == AppCutID).Single();
            pickup.CustomerID = pickupcustomer.CustomerID;

            if (ModelState.IsValid)
            {
                db.Entry(pickup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "PickupDay", pickup.CustomerID);
            return View(pickup);
        }

        // GET: Pickups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Pickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            db.Pickups.Remove(pickup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PickupsForEmployee()
        {
            if (User.IsInRole("Customer"))
            {
                return HttpNotFound();
            }

            string employeeId = User.Identity.GetUserId();
            var employer = db.Employee.Where(e => e.AppUserID == employeeId).FirstOrDefault();

            List<int> customerIds = db.Customer.Where(c => c.ZipCode == employer.ZipCode).Select(c => c.CustomerID).ToList();
            List<Pickup> pickupsInZip = db.Pickups.Where(p => customerIds.Contains(p.CustomerID)).Include(p => p.Customer).ToList();
            List<Pickup> pickupsInZipNoVacay = pickupsInZip.Where(p => !(p.VacaStart < DateTime.Now && p.VacaEnd > DateTime.Now)).ToList();

            return View(pickupsInZipNoVacay);
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
