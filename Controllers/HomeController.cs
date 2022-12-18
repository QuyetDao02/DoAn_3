using DoAn3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn3.Controllers
{
    public class HomeController : Controller
    {
        DoAn3_MvcEntities dp = new DoAn3_MvcEntities();
        public ActionResult Index()
        {
            var kq = dp.DoNoiThats.ToList();
            return View(kq);
        }
        public ActionResult GetProNew()
        {
            
            return Json(dp.DoNoiThats.Select(x => new {x.MaDNT,x.TenDNT,x.HinhAnh,x.Gia,x.Kichco,x.soluong,x.MoTa}).ToList(),JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ViewProduct()
        {
            var kq = dp.DoNoiThats.ToList();
            return PartialView(kq);
        }
        public ActionResult DetailProduct(int id)
        {
            var kq = dp.DoNoiThats.FirstOrDefault(s => s.MaDNT == id);
            return View(kq);
        }
        public ActionResult Category(int id)
        {
            var kq = dp.DoNoiThats.Where(s => s.MaLDNT == id).ToList();
            return View(kq);
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string eror = "";
            string username = form["name"];
            string password = form["password"];
            NguoiDung rowuser = dp.NguoiDungs.Where(m => m.roles == 0 && m.username == username).FirstOrDefault();
            if (rowuser == null)
            {
                eror = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (rowuser.pass.Equals(password))
                {
                    Session["UserCustomer"] = rowuser.username;
                    Session["CustomerId"] = rowuser.userID;
                    Session["FullnameCustomer"] = rowuser.KhachHang.TenKH;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    eror = "Mật khẩu không chính xác";
                }
            }


            ViewBag.Error = "<p class='text-danger'>" + eror + "</p>";
            return View();
        }
        public ActionResult LogOut()
        {
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
            Session["FullnameCustomer"] = "";
            return RedirectToAction("Index", "Home");
        }
        //public ActionResult SignUp()
        //{
        //    return View();
        //}



        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "userID,username,pass,roles")] NguoiDung nguoidung, [Bind(Include = "MaKH,TenKH,Sdt,email,Diachi")] KhachHang khachHang)
        {
            
            if (ModelState.IsValid)
            {
                nguoidung.roles = 0;
                khachHang.MaKH = nguoidung.userID;

                dp.KhachHangs.Add(khachHang);
                dp.NguoiDungs.Add(nguoidung);
                dp.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nguoidung );
        }







        public ActionResult AddCart(int id)
        {
            var sp = dp.DoNoiThats.FirstOrDefault(s => s.MaDNT == id);
            if (Session["cart"] != null)
            {
                List<ChiTietDonHangBan> ds = (List<ChiTietDonHangBan>)Session["cart"];
                var kt = ds.FirstOrDefault(s => s.MaDNT == id);
                if (kt == null)
                {
                    ChiTietDonHangBan dt = new ChiTietDonHangBan() { MaDNT = id, SoLuong = 1, DoNoiThat = sp };
                    ds.Add(dt);
                    Session["cart"] = ds;
                }
                else
                {
                    int vt = 0;
                    foreach (var item in ds)
                    {
                        if (item.MaDNT == id)
                        {
                            ds[vt].SoLuong += 1;
                            ds[vt].Gia = ds[vt].SoLuong * ds[vt].DoNoiThat.Gia;
                        }
                        vt++;
                    }
                    
                }
                Session["cart"] = ds;
            }
            else
            {
                List<ChiTietDonHangBan> ds = new List<ChiTietDonHangBan>();
                ChiTietDonHangBan dt = new ChiTietDonHangBan() { MaDNT = id, SoLuong = 1, DoNoiThat = sp };
                ds.Add(dt);
                Session["cart"] = ds;
            }
            return RedirectToAction("ViewCart", "Home");
        }
        public ActionResult ViewCart()
        {
            List<ChiTietDonHangBan> ds = (List<ChiTietDonHangBan>)Session["cart"];
            return View(ds);
        }
        public PartialViewResult Menuleft()
        {
            var ds = dp.LoaiDoNoiThats.ToList();
            return PartialView(ds);
        }
        public PartialViewResult Menuloai()
        {
            var ds = dp.LoaiDoNoiThats.ToList();
            return PartialView(ds);
        }
        public ActionResult DeleteCart(int? productid = null)
        {
            if (!Session["cart"].Equals(""))
            {
                List<ChiTietDonHangBan> ds = (List<ChiTietDonHangBan>)Session["cart"];
                int vt = 0;
                foreach (var item in ds)
                {
                    if (item.MaDNT == productid)
                    {
                        ds.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                Session["cart"] = ds;
            }
            return RedirectToAction("ViewCart", "Home");
        }
        public ActionResult UpdateCart(FormCollection x)
        {
            if (!string.IsNullOrEmpty(x["Update"]))
            {
                var listsl = x["Count"]; //mảng chứa lần lượt các giá trị số lượng của giỏ hàng
                var listarr = listsl.Split(',');
                List<ChiTietDonHangBan> ds = (List<ChiTietDonHangBan>)Session["cart"];
                int vt = 0;
                foreach (ChiTietDonHangBan item in ds)
                {
                    ds[vt].SoLuong = int.Parse(listarr[vt]);
                    ds[vt].Gia = ds[vt].DoNoiThat.Gia * ds[vt].SoLuong;  
                    vt++;
                }
                Session["cart"] = ds;

            }
            return RedirectToAction("ViewCart", "Home");
        }
        public ActionResult Pay()
        {
            List<ChiTietDonHangBan> dsdh = (List<ChiTietDonHangBan>)Session["cart"];
            if (Session["UserCustomer"].Equals(""))
            {
                return RedirectToAction("Login", "Home");
            }
            int id = int.Parse(Session["CustomerId"].ToString());
            NguoiDung us = dp.NguoiDungs.Find(id);
            ViewBag.User = us;
            return View("Pay", dsdh);

        }


        public ActionResult Checkout()
        {
            List<ChiTietDonHangBan> dsdh = (List<ChiTietDonHangBan>)Session["cart"];
            if (Session["UserCustomer"].Equals(""))
            {
                return RedirectToAction("Login", "Home");
            }
            int id = int.Parse(Session["CustomerId"].ToString());
            NguoiDung us = dp.NguoiDungs.Find(id);
            ViewBag.User = us;
            return View("Checkout", dsdh);

        }
        public ActionResult OrderNow(FormCollection frm)
        {
            //int id = int.Parse(Session["CustomerId"].ToString());
            //NguoiDung us = dp.NguoiDungs.Find(id);
            //DonHangBan DHB = new DonHangBan();
            //DHB.MaKH = id;
            //DHB.TinhTrang = true;

            //return RedirectToAction("Login", "Home");


            List<ChiTietDonHangBan> ds = (List<ChiTietDonHangBan>)Session["cart"];
            
            int id = int.Parse(Session["CustomerId"].ToString());
            NguoiDung us = dp.NguoiDungs.Find(id);
            double tongtien;
            //Lưu thông tin hóa đơn
            DonHangBan od = new DonHangBan();
            od.MaKH = id;
            od.TinhTrang = false;
            od.Ghichu = frm["Description"];
            od.NgayLap = DateTime.Today;
            od.DiaChi = frm["Adress"];
            od.Tongtien = Convert.ToDecimal(summoney(ds));
            if (saveOrder(od) == 1)
            {

                foreach (ChiTietDonHangBan item in ds)
                {
                    ChiTietDonHangBan odt = new ChiTietDonHangBan();
                    odt.MaDHB = od.MaDHB;
                    odt.MaDNT = item.MaDNT;
                    odt.Gia = item.DoNoiThat.Gia * item.SoLuong;
                    odt.SoLuong = item.SoLuong;
                    tongtien = Convert.ToDouble(item.DoNoiThat.Gia * item.SoLuong);

                    saveOrderDetail(odt);
                }
            }
            Session["cart"] = null;
            
            return RedirectToAction("Index", "Home");
        }
        public int saveOrderDetail(ChiTietDonHangBan CTDHB)
        {
            dp.ChiTietDonHangBans.Add(CTDHB);
            return dp.SaveChanges();
        }
        public double summoney(List<ChiTietDonHangBan> ds)
        {
            double sum = 0;
            foreach (ChiTietDonHangBan item in ds)
            {
                sum += Convert.ToDouble(item.Gia);
            }
            return sum;
        }
        public int saveOrder(DonHangBan od)
        {
            dp.DonHangBans.Add(od);
            return dp.SaveChanges();
        }
    }
    
}