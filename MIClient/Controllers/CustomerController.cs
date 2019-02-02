using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MIClient.ServiceReferenceMI;
namespace MIClient.Controllers
{
    public class CustomerController : Controller
    {
        ServiceMIClient client = new ServiceMIClient();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string cus_email, string cus_pass)
        {
            try
            {
                if (client.UserLogin(cus_email, cus_pass))
                {
                    tb_customer customer = client.GetCustomer(cus_email);
                    Session["Login"] = cus_email;
                    Session["FName"] = customer.cus_fname;
                    return RedirectToAction("YourProfile", "Customer");
                }
                else
                {
                    ViewBag.LoginFailed = "Email or Password was wrong!";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);               
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Customer");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tb_customer addCust, string repwd)
        {
            try
            {
                if (addCust.cus_pass.Equals(repwd))
                {
                    client.AddCustomer(addCust);
                    Session.Clear(); // xoa toan bo session de dang nhap tai khoan khac.
                    return RedirectToAction("Login", "Customer");
                }
                if (!addCust.cus_pass.Equals(repwd))
                {
                    ViewBag.PwdNotMatch = "Password and Confirm Password aren't match!";                 
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Register Failed!");
                }
            }
            catch (Exception e)
            {              
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }
        public ActionResult YourProfile(string cus_email)
        {
            try
            {
                    string email = (string)Session["Login"];
                    var model = client.GetCustomer(email);
                    return View(model);  
            }
            
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error: "+e.Message);
            }
            return RedirectToAction("Login", "Customer");
        }
        public ActionResult ChangePassword(string cus_email)
        {
            string email = (string)Session["Login"];
            //var model = client.GetCustomer(email);
            //return View(model);
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(tb_customer upCust, string pass1, string pass2, string OldPwd)
        {
            tb_customer customer = client.GetCustomer(Session["Login"].ToString());
            if (customer.cus_pass.Equals(OldPwd))
            {
               // customer.cus_pass = OldPwd;
                if (OldPwd.Equals(upCust))
                {
                ViewBag.OldPwdEqualNewPwd = "Old password can't equal new password!";
                return View();
                }
            }
            if (!upCust.Equals(pass1))
            {
                ViewBag.NewPwdNotEqual = "New password and new confirm password aren't match!";
                return View();
            }
            if(!customer.cus_pass.Equals(upCust) && upCust.Equals(pass1))
            {
                client.UpdatePassword(upCust);
                return RedirectToAction("Login", "Customer");
            }
            return View();


        }
        public ActionResult EditProfile(string cus_email)
        {
            try
            {
                string email = (string)Session["Login"];
                var model = client.GetCustomer(email);
                return View(model);
            }
            catch (Exception e)
            {
                
                ModelState.AddModelError("", e.Message);
            }
            return RedirectToAction("Login", "Customer");

        }
        [HttpPost]
        public ActionResult EditProfile(tb_customer upCust)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    client.UpdateProfile(upCust);
                    return RedirectToAction("YourProfile", "Customer");
                }
                else
                {
                    ViewBag.EditFailed = "Edit profile was failed!";
                }
                
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);               
            }          
            return View();
        }
        public ActionResult FollowOrder(string cus_email)
        {
            string email = (string)Session["Login"];
            var model = client.GetOrder(email);
            return View(model);
        }
    }
}