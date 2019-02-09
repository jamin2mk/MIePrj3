using MIClient.ServiceReferenceMI;
using MIData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MIClient.Controllers
{
    public class CustomerController : Controller
    {
        ServiceMIClient client = new ServiceMIClient();

        // GET: Customer
        public ActionResult Index()
        {
            Session["Customer"] = client.GetCustomer("baochi1212@gmail.com");
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string pass)
        {
            if (client.Login(email, pass))
            {
                Session["Customer"] = client.GetCustomer(email);
                return RedirectToAction("FollowOrder");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            string mess = client.CreateCustomer(customer);
            if (mess != null)
            {
                ModelState.AddModelError("", mess);
            }
            else
            {
                ViewBag.mess = "Congratulation. Register successful.";
            }
            return View();
        }

        public ActionResult EditProfile()
        {
            int custID = ((Customer)Session["Customer"]).cus_id;
            Customer model = client.FindCustomer(custID);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(Customer customer)
        {
            if (ModelState.IsValid)
            {
                client.EditCustomer(customer);
                ViewBag.mess = "Edit profile successful.";
            }
            return View();
        }

        public ActionResult FollowOrder()
        {
            int custID = 1;
            var orderList = client.FindOrders(custID);
            return View(orderList);
        }
    }
}