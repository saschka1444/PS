using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.Models;

namespace SmartShop.Controllers
{
    public class ProductController : Controller
    {
        private SmartShopContext db = new SmartShopContext();

        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View(db.Smartphone.ToList());
        }

        //
        // GET: /Product/Details/5
        public ActionResult Details(Int32 id)
        {
            Smartphone smartphone = db.Smartphone.Find(id);
            if (smartphone == null)
            {
                return HttpNotFound();
            }
            return View(smartphone);
        }

        //
        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Smartphone smartphone)
        {
            if (ModelState.IsValid)
            {
                db.Smartphone.Add(smartphone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smartphone);
        }

        //
        // GET: /Product/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Smartphone smartphone = db.Smartphone.Find(id);
            if (smartphone == null)
            {
                return HttpNotFound();
            }
            return View(smartphone);
        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Smartphone smartphone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smartphone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smartphone);
        }

        //
        // GET: /Product/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Smartphone smartphone = db.Smartphone.Find(id);
            if (smartphone == null)
            {
                return HttpNotFound();
            }
            return View(smartphone);
        }

        //
        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Smartphone smartphone = db.Smartphone.Find(id);
            db.Smartphone.Remove(smartphone);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
