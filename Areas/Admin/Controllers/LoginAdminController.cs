using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn3.Models;

namespace DoAn3.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        DoAn3_MvcEntities db = new DoAn3_MvcEntities();
        // GET: Admin/LoginAdmin
        public ActionResult Login()
        {
            if (!Session["UserAdmin"].Equals(""))
            {
                return RedirectToAction("Index", "DoNoiThats");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection field)
        {
            string eror = "";
            string username = field["name"];
            string password = field["password"];
            NguoiDung rowuser = db.NguoiDungs.Where(m =>m.roles == 1 && m.username == username).FirstOrDefault();
            if(rowuser == null)
            {
                eror = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (rowuser.pass.Equals(password))
                {
                    Session["UserAdmin"] = rowuser.username;
                    Session["UserID"] = rowuser.userID;
                    Session["Fullname"] = rowuser.NhanVien.TenNV;
                    return RedirectToAction("Index", "DoNoiThats");
                }
                else
                {
                    eror = "Mật khẩu không chính xác";
                }
            }


            ViewBag.Error = "<p class='text-danger'>" + eror + "</p>";
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserID"] = "";
            Session["Fullname"] = "";
            return RedirectToAction("Login", "LoginAdmin");
        }
    }
}