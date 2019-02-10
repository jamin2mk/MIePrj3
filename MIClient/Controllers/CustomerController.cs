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
        public ActionResult Login(Login newLogin)
        {
            if (client.Login(newLogin.email, newLogin.password))
            {
                var a = client.GetCustomer(newLogin.email);
                Session["Customer"] = client.GetCustomer(newLogin.email);
                Session["CustomerID"] = a.cus_id;
                Session["CustomerName"] = a.cus_fname;
                return RedirectToAction("Index");
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
            return RedirectToAction("Login");
        }

        public ActionResult ShowProfile()
        {
            var a = (int)Session["CustomerID"];
            var b = client.FindCustomer(a);
            return View(b);
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
                return RedirectToAction("ShowProfile");
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword newPass)
        {
            if (ModelState.IsValid)
            {
                var a = (int)Session["CustomerID"];
                var b = client.GetCustomerPassword(a);

                if (newPass.OldPassword != b.OldPassword)
                {
                    ModelState.AddModelError("","Old password is incorrect");
                    return View();
                }

                client.ChangePassword(newPass,a);
                return RedirectToAction("Index");

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