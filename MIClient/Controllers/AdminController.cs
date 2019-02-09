using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIClient.Models;
using MIClient.ServiceReferenceMI;

namespace MIClient.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        ServiceMIClient client = new ServiceMIClient();

        public ActionResult Login()
        {
            Session.Remove("admin");
            return View();
        }
        [HttpPost]
        public ActionResult Login(string uid, string pwd)
        {
            if (client.AdminLogin(uid, pwd))
            {

                Session["admin"] = uid;
                return RedirectToAction("Index");

            }
            return View("Error");
        }

        public ActionResult Index()
        {
            ViewData["No"] = "";
            ViewData["ViewName"] = "Index";
            ViewData["AllOrder"] = client.GetOrders().ToList().Count();
            ViewData["AllSize"] = client.GetPrintsizes().ToList().Count();
            ViewData["AllCustomer"] = client.CountAllCustomer();
            var a = client.GetOrderNotFinished();
            return View(client.GetOrderNotFinished());
        }
        //CUSTOMER
        public ActionResult Customer()
        {
            ViewData["AllOrder"] = client.GetOrders().ToList().Count();
            ViewData["AllSize"] = client.GetPrintsizes().ToList().Count();
            ViewData["AllCustomer"] = client.CountAllCustomer();
            ViewData["ViewName"] = "Customer";
            var a = client.GetCustomers();
            return View(a);
        }

        public ActionResult SearchByName(string search)
        {
            var a = client.SearchCustomerByName(search);
            return View("Customer",a);
        }
        //SIZE
        public ActionResult Size()
        {
            ViewData["AllOrder"] = client.GetOrders().ToList().Count();
            ViewData["AllSize"] = client.GetPrintsizes().ToList().Count();
            ViewData["AllCustomer"] = client.CountAllCustomer();
            ViewData["ViewName"] = "Size";
            return View(client.GetPrintsizes());
        }

        public ActionResult CreateSize()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateSize(Size newSize)
        {
            if (ModelState.IsValid)
            {
                ServiceReferenceMI.tb_printsize newtbSize = new ServiceReferenceMI.tb_printsize();

                newtbSize.pr_id = newSize.pr_id;
                newtbSize.pr_price = newSize.pr_price;
                newtbSize.pr_size = newSize.pr_size;

                if (client.ValidatePrintSize(newtbSize) == false)
                {
                    Session["Error"] = "This Size is already existed";
                    return RedirectToAction("CreateSize");
                }

                client.CreatePrintsizes(newtbSize);
                Session.Remove("Error");
                return RedirectToAction("Size");
            }
            return View();


        }

        public ActionResult DeleteSize(int id)
        {
            client.DeletePrintsizes(id);
            return RedirectToAction("Size");
        }

        public ActionResult EditSize(int id)
        {
            Size editsize = new Size();
            var getsize = client.GetOnePrintsize(id);
            editsize.pr_id = getsize.pr_id;
            editsize.pr_price = getsize.pr_price;
            editsize.pr_size = getsize.pr_size;

            return View(editsize);
        }
        [HttpPost]
        public ActionResult EditSize(Size updateSize)
        {
            if (ModelState.IsValid)
            {
                ServiceReferenceMI.tb_printsize uptbSize = new ServiceReferenceMI.tb_printsize();

                uptbSize.pr_id = updateSize.pr_id;
                uptbSize.pr_price = updateSize.pr_price;
                uptbSize.pr_size = updateSize.pr_size;

                client.UpdatePrintsize(uptbSize);
                return RedirectToAction("Size");
            }
            return View();
        }
        //ORDER
        public ActionResult Order()
        {
            ViewData["AllOrder"] = client.GetOrders().ToList().Count();
            ViewData["AllSize"] = client.GetPrintsizes().ToList().Count();
            ViewData["AllCustomer"] = client.CountAllCustomer();
            ViewData["ViewName"] = "Order";
            Session["active"] = "active";
            return View(client.GetOrders());
        }

        public ActionResult ChangeStatus(int id)
        {
            var a = client.GetOneOrders(id);
            Status ups = new Status();
            ups.o_cus_id = a.o_cus_id;
            ups.o_date = a.o_date;
            ups.o_deli_date = a.o_deli_date;
            ups.o_dt_id = a.o_dt_id;
            ups.o_folder = a.o_folder;
            ups.o_id = a.o_id;
            ups.o_pay = a.o_pay;
            ups.o_pr_id = a.o_pr_id;
            ups.o_recip = a.o_recip;
            ups.o_recip_phone = a.o_recip_phone;
            ups.o_shipadd = a.o_shipadd;
            ups.o_status = a.o_status;
            ups.o_s_id = a.o_s_id;

            return View(ups);
        }

        [HttpPost]

        public ActionResult ChangeStatus(Status stats)
        {
            if (ModelState.IsValid)
            {

                tb_order upStat = new tb_order();

                upStat.o_cus_id = stats.o_cus_id;
                upStat.o_date = stats.o_date;
                upStat.o_deli_date = stats.o_deli_date;
                upStat.o_dt_id = stats.o_dt_id;
                upStat.o_folder = stats.o_folder;
                upStat.o_id = stats.o_id;
                upStat.o_pay = stats.o_pay;
                upStat.o_pr_id = stats.o_pr_id;
                upStat.o_recip = stats.o_recip;
                upStat.o_recip_phone = stats.o_recip_phone;
                upStat.o_shipadd = stats.o_shipadd;
                upStat.o_status = stats.o_status;
                upStat.o_s_id = stats.o_s_id;

                client.UpdateStatus(upStat);
                return RedirectToAction("Order");
            }

            return View();
        }



    }
}
