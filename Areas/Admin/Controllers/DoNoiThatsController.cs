using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DoAn3.Models;

namespace DoAn3.Areas.Admin.Controllers
{
    public class DoNoiThatsController : /*CheckLoginController*/ Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/DoNoiThats
        public ActionResult Index()
        {
            var doNoiThats = db.DoNoiThats.Include(d => d.LoaiDoNoiThat).Include(d => d.NhaCungCap);
            return View(doNoiThats.ToList());
        }
        public ActionResult getAll()
        {
            return Json(db.DoNoiThats.Select(t => new { t.MaDNT, t.TenDNT, t.HinhAnh, t.Kichco, t.soluong, t.Gia, t.MoTa, t.NhaCungCap.MaNCC,
                t.NhaCungCap.TenNCC, t.LoaiDoNoiThat.TenLDNT, t.LoaiDoNoiThat.MaLDNT }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/DoNoiThats/Details/5
        public ActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetailsData(int? id)
        {
            var x = (from sp in db.DoNoiThats where sp.MaDNT == id select new { sp.MaDNT, sp.TenDNT, sp.NhaCungCap.TenNCC, sp.LoaiDoNoiThat.TenLDNT, sp.HinhAnh, sp.Gia, 
                                                                                sp.Kichco, sp.soluong, sp.MoTa }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/DoNoiThats/Create
        public ActionResult Create()
        {
            ViewBag.MaLDNT = new SelectList(db.LoaiDoNoiThats, "MaLDNT", "TenLDNT");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            return View();
        }

        // POST: Admin/DoNoiThats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDNT,TenDNT,MaNCC,MaLDNT,HinhAnh,Gia,Kichco,MoTa,soluong")] DoNoiThat doNoiThat)
        {
            //ViewBag.MaLDNT = new SelectList(db.LoaiDoNoiThats, "MaLDNT", "TenLDNT", doNoiThat.MaLDNT);
            //ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", doNoiThat.MaNCC);
            if (ModelState.IsValid)
            {
                try
                {
                    db.DoNoiThats.Add(doNoiThat);
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

        // GET: Admin/DoNoiThats/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Admin/DoNoiThats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDNT,TenDNT,MaNCC,MaLDNT,HinhAnh,Gia,Kichco,MoTa,soluong")] DoNoiThat doNoiThat)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.DoNoiThats where ds.MaDNT == doNoiThat.MaDNT select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.TenDNT = doNoiThat.TenDNT;
                    sp.MaNCC = doNoiThat.MaNCC;
                    sp.MaLDNT = doNoiThat.MaLDNT;
                    sp.HinhAnh = doNoiThat.HinhAnh;
                    sp.Gia = doNoiThat.Gia;
                    sp.Kichco = doNoiThat.Kichco;
                    sp.soluong = doNoiThat.soluong;
                    sp.MoTa = doNoiThat.MoTa;

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

        // GET: Admin/DoNoiThats/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DoNoiThat doNoiThat = db.DoNoiThats.Find(id);
        //    if (doNoiThat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(doNoiThat);
        //}

        //// POST: Admin/DoNoiThats/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DoNoiThat doNoiThat = db.DoNoiThats.Find(id);
        //    db.DoNoiThats.Remove(doNoiThat);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public string Delete(DoNoiThat del)
        {
            if (del != null)
            {
                var ds = (from sp in db.DoNoiThats where sp.MaDNT == del.MaDNT select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.DoNoiThats.Remove(ds);
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
