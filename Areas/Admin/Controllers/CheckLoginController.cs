using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn3.Areas.Admin.Controllers
{
    public class CheckLoginController : Controller
    {
        // GET: Admin/CheckLogin
        public CheckLoginController()
        {
            if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/login");
            }
        }
    }
}