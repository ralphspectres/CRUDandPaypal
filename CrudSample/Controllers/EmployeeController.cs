using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrudSample.Models;

namespace CrudSample.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: /Employee/
        public ActionResult Index()
        {
            var clientemployees = db.ClientEmployees.Include(c => c.Client);
            return View(clientemployees.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEmployee clientemployee = db.ClientEmployees.Find(id);
            if (clientemployee == null)
            {
                return HttpNotFound();
            }
            return View(clientemployee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClientEmployeeId,FirstName,LastName,ClientId")] ClientEmployee clientemployee)
        {
            if (ModelState.IsValid)
            {
                db.ClientEmployees.Add(clientemployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", clientemployee.ClientId);
            return View(clientemployee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEmployee clientemployee = db.ClientEmployees.Find(id);
            if (clientemployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", clientemployee.ClientId);
            return View(clientemployee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClientEmployeeId,FirstName,LastName,ClientId")] ClientEmployee clientemployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientemployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", clientemployee.ClientId);
            return View(clientemployee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEmployee clientemployee = db.ClientEmployees.Find(id);
            if (clientemployee == null)
            {
                return HttpNotFound();
            }
            return View(clientemployee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientEmployee clientemployee = db.ClientEmployees.Find(id);
            db.ClientEmployees.Remove(clientemployee);
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
