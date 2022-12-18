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
    public class DonHangBansController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/DonHangBans
        public ActionResult Index()
        {
            var donHangBans = db.DonHangBans.Include(d => d.KhachHang);
            return View(donHangBans.ToList());
        }

        // GET: Admin/DonHangBans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan == null)
            {
                return HttpNotFound();
            }
            return View(donHangBan);
        }

        // GET: Admin/DonHangBans/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            return View();
        }

        // POST: Admin/DonHangBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDHB,MaKH,NgayLap,DiaChi,TinhTrang,Ghichu,Tongtien")] DonHangBan donHangBan)
        {
            if (ModelState.IsValid)
            {
                db.DonHangBans.Add(donHangBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donHangBan.MaKH);
            return View(donHangBan);
        }

        // GET: Admin/DonHangBans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donHangBan.MaKH);
            return View(donHangBan);
        }

        // POST: Admin/DonHangBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDHB,MaKH,NgayLap,DiaChi,TinhTrang,Ghichu,Tongtien")] DonHangBan donHangBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHangBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donHangBan.MaKH);
            return View(donHangBan);
        }

        // GET: Admin/DonHangBans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan == null)
            {
                return HttpNotFound();
            }
            return View(donHangBan);
        }

        // POST: Admin/DonHangBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            db.DonHangBans.Remove(donHangBan);
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
