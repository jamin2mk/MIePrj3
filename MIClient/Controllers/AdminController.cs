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
        public ActionResult Login(string uid, string pwd) {
           if (client.AdminLogin(uid,pwd))
            {

                Session["admin"] = uid;
                return RedirectToAction("Index");

            }
            return View("Error");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customer()
        {
            var a = client.GetCustomers();
            return View(a);
        }
        //SIZE
        public ActionResult Size()
        {
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

                if (client.ValidatePrintSize(newtbSize)==false)
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
            return View(client.GetOrders());
        }

        public ActionResult ChangeStatus(int id)
        {
            client.GetOneOrders(id);
            return View();
        }

        

        
    }
}