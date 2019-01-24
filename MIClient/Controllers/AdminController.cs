using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIClient.ServiceReferenceMI;

namespace MIClient.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        ServiceMIClient client = new ServiceMIClient();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string uid, string pwd) {
            if (client.AdminLogin(uid,pwd))
            {
                return Content("Ok");

            }
            return Content("Not OK");
        }
    }
}