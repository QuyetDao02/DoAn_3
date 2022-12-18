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
    public class NguoiDungsController : Controller
    {
        private DoAn3_MvcEntities db = new DoAn3_MvcEntities();

        // GET: Admin/NguoiDungs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getAll()
        {
            return Json(db.NguoiDungs.Select(t => new { t.userID, t.username, t.pass, t.roles }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/NguoiDungs/Details/5
        public ActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetailsData(int? id)
        {
            var x = (from sp in db.NguoiDungs where sp.userID == id select new { sp.userID, sp.username, sp.pass, sp.roles }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/NguoiDungs/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,username,pass,roles")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.NguoiDungs.Add(nguoiDung);
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

        // GET: Admin/NguoiDungs/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,username,pass,roles")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.NguoiDungs where ds.userID == nguoiDung.userID select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.username = nguoiDung.username;
                    sp.pass = nguoiDung.pass;
                    sp.roles = nguoiDung.roles;

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

        // GET: Admin/NguoiDungs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NguoiDung nguoiDung = db.NguoiDungs.Find(id);
        //    if (nguoiDung == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(nguoiDung);
        //}

        //POST: Admin/NguoiDungs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    NguoiDung nguoiDung = db.NguoiDungs.Find(id);
        //    db.NguoiDungs.Remove(nguoiDung);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public string Delete(NguoiDung del)
        {
            if (del != null)
            {
                var ds = (from sp in db.NguoiDungs where sp.userID == del.userID select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.NguoiDungs.Remove(ds);
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
