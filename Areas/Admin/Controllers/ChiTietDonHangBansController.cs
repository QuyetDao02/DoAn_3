using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn3.Models;

namespace DoAn3.Areas.Admin.Controllers
{
    public class ChiTietDonHangBansController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/ChiTietDonHangBans
        public ActionResult Index()
        {
            var chiTietDonHangBans = db.ChiTietDonHangBans.Include(c => c.DonHangBan).Include(c => c.DoNoiThat);
            return View(chiTietDonHangBans.ToList());
        }

        // GET: Admin/ChiTietDonHangBans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHangBan chiTietDonHangBan = db.ChiTietDonHangBans.Find(id);
            if (chiTietDonHangBan == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHangBan);
        }

        // GET: Admin/ChiTietDonHangBans/Create
        public ActionResult Create()
        {
            ViewBag.MaDHB = new SelectList(db.DonHangBans, "MaDHB", "DiaChi");
            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT");
            return View();
        }

        // POST: Admin/ChiTietDonHangBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDHB,MaDNT,SoLuong,Gia")] ChiTietDonHangBan chiTietDonHangBan)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonHangBans.Add(chiTietDonHangBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDHB = new SelectList(db.DonHangBans, "MaDHB", "DiaChi", chiTietDonHangBan.MaDHB);
            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT", chiTietDonHangBan.MaDNT);
            return View(chiTietDonHangBan);
        }

        // GET: Admin/ChiTietDonHangBans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHangBan chiTietDonHangBan = db.ChiTietDonHangBans.Find(id);
            if (chiTietDonHangBan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDHB = new SelectList(db.DonHangBans, "MaDHB", "DiaChi", chiTietDonHangBan.MaDHB);
            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT", chiTietDonHangBan.MaDNT);
            return View(chiTietDonHangBan);
        }

        // POST: Admin/ChiTietDonHangBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDHB,MaDNT,SoLuong,Gia")] ChiTietDonHangBan chiTietDonHangBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHangBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDHB = new SelectList(db.DonHangBans, "MaDHB", "DiaChi", chiTietDonHangBan.MaDHB);
            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT", chiTietDonHangBan.MaDNT);
            return View(chiTietDonHangBan);
        }

        // GET: Admin/ChiTietDonHangBans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHangBan chiTietDonHangBan = db.ChiTietDonHangBans.Find(id);
            if (chiTietDonHangBan == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHangBan);
        }

        // POST: Admin/ChiTietDonHangBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDonHangBan chiTietDonHangBan = db.ChiTietDonHangBans.Find(id);
            db.ChiTietDonHangBans.Remove(chiTietDonHangBan);
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
