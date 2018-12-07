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
        // List<string> days = new List<string>();

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
            Pickup pickupToEdit = db.Pickups.Where(p => p.CustomerID == pickupcustomer.CustomerID).Single();
            pickupToEdit.PickupDay = pickup.PickupDay;
            db.SaveChanges();
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

        //public ActionResult PickupsForEmployee()
        //{
        //    if (User.IsInRole("Customer"))
        //    {
        //        return HttpNotFound();
        //    }

        //    string employeeId = User.Identity.GetUserId();
        //    var employer = db.Employee.Where(e => e.AppUserID == employeeId).FirstOrDefault();

        //    List<int> customerIds = db.Customer.Where(c => c.ZipCode == employer.ZipCode).Select(c => c.CustomerID).ToList();
        //    List<Pickup> pickupsInZip = db.Pickups.Where(p => customerIds.Contains(p.CustomerID)).Include(p => p.Customer).ToList();
        //    List<Pickup> pickupsInZipNoVacay = pickupsInZip.Where(p => !(p.VacaStart < DateTime.Now && p.VacaEnd > DateTime.Now)).ToList();

        //    return View(pickupsInZipNoVacay);
        //}

        [AllowAnonymous]
        public ActionResult PickupsByDay()
        {
            if (User.IsInRole ("Customer"))
            {
                return HttpNotFound();
            }

            string employeeId = User.Identity.GetUserId();
            var employee = db.Employee.Where(e => e.AppUserID == employeeId).FirstOrDefault();

            PickupDayViewModel pdvm = new PickupDayViewModel();

            var pickups = db.Pickups;

            var thing = pickups.ToList();
            foreach(var item in thing)
            {
                item.Customer = db.Customer.Where(c =>c.CustomerID == item.CustomerID).Single();
            }

            List<string> days = new List<string>();
            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");

            ViewBag.Days = days;

            return View(thing);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PickupsByDay(PickupDayViewModel model)
        {

            List<string> days = new List<string>();
            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");

            ViewBag.Days = days;

            if (model.DaySearch == null)
            {
                
            }
            else
            {
                string employeeId = User.Identity.GetUserId();
                var employer = db.Employee.Where(e => e.AppUserID == employeeId).FirstOrDefault();
                List<int> customerIds = db.Customer.Where(c => c.ZipCode == employer.ZipCode).Select(c => c.CustomerID).ToList();
                List<Pickup> pickupsInZip = db.Pickups.Where(p => customerIds.Contains(p.CustomerID)).Include(p => p.Customer).ToList();
                List<Pickup> pickupsInZipNoVacay = pickupsInZip.Where(p => !((p.VacaStart < DateTime.Now) && (p.VacaEnd > DateTime.Now))).ToList();
                var pickupsSameDay = pickupsInZipNoVacay.Where(p => p.PickupDay == model.DaySearch);

                PickupDayViewModel pdvm = new PickupDayViewModel();
                
                List<Pickup> Listthing = pickupsSameDay.ToList();

                return View(Listthing);
            }
            return RedirectToAction("Index", "Pickups");
        }

        public ActionResult ChangeBalance(int? id)
        {
            Customer customer = db.Customer.Find(id);
            customer.PickupStatus = true;
            customer.BalanceDue += 40;
            db.SaveChanges();
            return RedirectToAction("PickupsByDay", "Pickups");
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
