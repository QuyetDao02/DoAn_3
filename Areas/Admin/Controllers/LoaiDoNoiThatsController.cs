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
    public class LoaiDoNoiThatsController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/LoaiDoNoiThats
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAll()
        {
            return Json(db.LoaiDoNoiThats.Select(t => new { t.MaLDNT, t.TenLDNT, t.MoTa }).ToList(), JsonRequestBehavior.AllowGet);
        }
        //GET: Admin/LoaiDoNoiThats/Details/5

        public ActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetailsData(int? id)
        {
            var x = (from sp in db.LoaiDoNoiThats where sp.MaLDNT == id select new { sp.MaLDNT, sp.TenLDNT, sp.MoTa }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/LoaiDoNoiThats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiDoNoiThats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLDNT,TenLDNT,MoTa")] LoaiDoNoiThat loaiDoNoiThat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.LoaiDoNoiThats.Add(loaiDoNoiThat);
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

        // GET: Admin/LoaiDoNoiThats/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Admin/LoaiDoNoiThats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLDNT,TenLDNT,MoTa")] LoaiDoNoiThat loaiDoNoiThat)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.LoaiDoNoiThats where ds.MaLDNT == loaiDoNoiThat.MaLDNT select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.TenLDNT = loaiDoNoiThat.TenLDNT;
                    sp.MoTa = loaiDoNoiThat.MoTa;

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

        // GET: Admin/LoaiDoNoiThats/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LoaiDoNoiThat loaiDoNoiThat = db.LoaiDoNoiThats.Find(id);
        //    if (loaiDoNoiThat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(loaiDoNoiThat);
        //}

        //// POST: Admin/LoaiDoNoiThats/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LoaiDoNoiThat loaiDoNoiThat = db.LoaiDoNoiThats.Find(id);
        //    db.LoaiDoNoiThats.Remove(loaiDoNoiThat);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public string Delete(LoaiDoNoiThat del)
        {
            if (del != null)
            {
                var ds = (from sp in db.LoaiDoNoiThats where sp.MaLDNT == del.MaLDNT select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.LoaiDoNoiThats.Remove(ds);
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
