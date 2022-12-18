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
    public class NhaCungCapsController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/NhaCungCaps
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAll()
        {
            return Json(db.NhaCungCaps.Select(t => new { t.MaNCC, t.TenNCC, t.DiaChi, t.Sdt }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/NhaCungCaps/Details/5
        public ActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetailsData(int? id)
        {
            var x = (from sp in db.NhaCungCaps where sp.MaNCC == id select new { sp.MaNCC, sp.TenNCC, sp.DiaChi, sp.Sdt }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/NhaCungCaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaCungCaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,DiaChi,Sdt")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.NhaCungCaps.Add(nhaCungCap);
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
    

        // GET: Admin/NhaCungCaps/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Admin/NhaCungCaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,DiaChi,Sdt")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.NhaCungCaps where ds.MaNCC == nhaCungCap.MaNCC select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.TenNCC = nhaCungCap.TenNCC;
                    sp.DiaChi = nhaCungCap.DiaChi;
                    sp.Sdt = nhaCungCap.Sdt;

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

        // GET: Admin/NhaCungCaps/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
        //    if (nhaCungCap == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(nhaCungCap);
        //}

        //// POST: Admin/NhaCungCaps/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
        //    db.NhaCungCaps.Remove(nhaCungCap);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public string Delete(NhaCungCap del)
        {
            if (del != null)
            {
                var ds = (from sp in db.NhaCungCaps where sp.MaNCC == del.MaNCC select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.NhaCungCaps.Remove(ds);
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
