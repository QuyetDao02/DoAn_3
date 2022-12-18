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
    public class KhachHangsController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/KhachHangs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getAll()
        {
            return Json(db.KhachHangs.Select(t => new { t.MaKH, t.TenKH, t.email, t.Diachi, t.Sdt, t.NguoiDung.username }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetailsData(int? id)
        {
            var x = (from sp in db.KhachHangs where sp.MaKH == id select new { sp.MaKH, sp.TenKH, sp.email, sp.Diachi, sp.Sdt, sp.NguoiDung.username }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/KhachHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.NguoiDungs, "userID", "username");
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,Sdt,email,Diachi")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index");

                }
                catch
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/KhachHangs/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,Sdt,email,Diachi")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.KhachHangs where ds.MaKH == khachHang.MaKH select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.TenKH = khachHang.TenKH;
                    sp.email = khachHang.email;
                    sp.Sdt = khachHang.Sdt;
                    sp.Diachi = khachHang.Diachi;

                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/KhachHangs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    KhachHang khachHang = db.KhachHangs.Find(id);
        //    if (khachHang == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(khachHang);
        //}

        //// POST: Admin/KhachHangs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    KhachHang khachHang = db.KhachHangs.Find(id);
        //    db.KhachHangs.Remove(khachHang);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public string Delete(KhachHang del)
        {
            if (del != null)
            {
                var ds = (from sp in db.KhachHangs where sp.MaKH == del.MaKH select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.KhachHangs.Remove(ds);
                    db.SaveChanges();
                    return "Xóa sản phẩm thành công";
                }
                else
                {
                    return "Xóa sản phẩm không thành công";
                }
            }
            else
            {
                return "Xóa sản phẩm không thành công";
            }

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
