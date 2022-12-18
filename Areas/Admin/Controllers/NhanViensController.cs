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
    public class NhanViensController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/NhanViens
        public ActionResult Index()
        {
            var nhanViens = db.NhanViens.Include(n => n.NguoiDung);
            return View(nhanViens.ToList());
        }
        public ActionResult getAll()
        {
            return Json(db.NhanViens.Select(t => new { t.MaNV, t.TenNV, t.Diachi, t.Sdt, t.NguoiDung.username }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/NhanViens/Details/5
        public ActionResult Details()
        {

            return View();
        }
        [HttpGet]
        public ActionResult DetailsData(int? id)
        {
            var x = (from sp in db.NhanViens where sp.MaNV == id select new { sp.MaNV, sp.TenNV, sp.Sdt, sp.Diachi, sp.NguoiDung.username }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/NhanViens/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenNV,Sdt,Diachi")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.NhanViens.Add(nhanVien);
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

        // GET: Admin/NhanViens/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TenNV,Sdt,Diachi")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.NhanViens where ds.MaNV == nhanVien.MaNV select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.TenNV = nhanVien.TenNV;
                    sp.Sdt = nhanVien.Sdt;
                    sp.Diachi = nhanVien.Diachi;

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

        // GET: Admin/NhanViens/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NhanVien nhanVien = db.NhanViens.Find(id);
        //    if (nhanVien == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(nhanVien);
        //}

        //// POST: Admin/NhanViens/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    NhanVien nhanVien = db.NhanViens.Find(id);
        //    db.NhanViens.Remove(nhanVien);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public string Delete(NhanVien del)
        {
            if (del != null)
            {
                var ds = (from sp in db.NhanViens where sp.MaNV == del.MaNV select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.NhanViens.Remove(ds);
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
