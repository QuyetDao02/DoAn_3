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
    public class ChiTietHoaDonNhapsController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/ChiTietHoaDonNhaps
        public ActionResult Index()
        {
            var chiTietHoaDonNhaps = db.ChiTietHoaDonNhaps.Include(c => c.DoNoiThat).Include(c => c.HoaDonNhap);
            return View(chiTietHoaDonNhaps.ToList());
        }

        // GET: Admin/ChiTietHoaDonNhaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chiTietHoaDonNhap = db.ChiTietHoaDonNhaps.Find(id);
            if (chiTietHoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDonNhap);
        }

        // GET: Admin/ChiTietHoaDonNhaps/Create
        public ActionResult Create()
        {
            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT");
            ViewBag.MaHDN = new SelectList(db.HoaDonNhaps, "MaHDN", "MaHDN");
            return View();
        }

        // POST: Admin/ChiTietHoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHDN,MaDNT,SoLuong,Gia")] ChiTietHoaDonNhap chiTietHoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDonNhaps.Add(chiTietHoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT", chiTietHoaDonNhap.MaDNT);
            ViewBag.MaHDN = new SelectList(db.HoaDonNhaps, "MaHDN", "MaHDN", chiTietHoaDonNhap.MaHDN);
            return View(chiTietHoaDonNhap);
        }

        // GET: Admin/ChiTietHoaDonNhaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chiTietHoaDonNhap = db.ChiTietHoaDonNhaps.Find(id);
            if (chiTietHoaDonNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT", chiTietHoaDonNhap.MaDNT);
            ViewBag.MaHDN = new SelectList(db.HoaDonNhaps, "MaHDN", "MaHDN", chiTietHoaDonNhap.MaHDN);
            return View(chiTietHoaDonNhap);
        }

        // POST: Admin/ChiTietHoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHDN,MaDNT,SoLuong,Gia")] ChiTietHoaDonNhap chiTietHoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHoaDonNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDNT = new SelectList(db.DoNoiThats, "MaDNT", "TenDNT", chiTietHoaDonNhap.MaDNT);
            ViewBag.MaHDN = new SelectList(db.HoaDonNhaps, "MaHDN", "MaHDN", chiTietHoaDonNhap.MaHDN);
            return View(chiTietHoaDonNhap);
        }

        // GET: Admin/ChiTietHoaDonNhaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chiTietHoaDonNhap = db.ChiTietHoaDonNhaps.Find(id);
            if (chiTietHoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDonNhap);
        }

        // POST: Admin/ChiTietHoaDonNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHoaDonNhap chiTietHoaDonNhap = db.ChiTietHoaDonNhaps.Find(id);
            db.ChiTietHoaDonNhaps.Remove(chiTietHoaDonNhap);
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
