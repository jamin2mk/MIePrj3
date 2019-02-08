﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using MIClient.ServiceReferenceMI;
using MIData.Models;

namespace MIClient.Controllers
//http://localhost:49993/ServiceMI.svc
{
    public class OrderController : Controller
    {

        ServiceMIClient client = new ServiceMIClient();

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowImage(HttpPostedFileBase[] images)
        {
            // random folder
            string folder = client.RandomFolder();
            string dir = Server.MapPath("~/Uploads/" + folder);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // save image into create directory
            //MImages mImages = client.SaveImages(images, dir, folder);
            MImages mImages = new MImages();
            mImages.Folder = folder;
            mImages.MImageList = new List<MImage>();

            foreach (var item in images)
            {
                string fn = Path.GetFileName(item.FileName);
                string path = Path.Combine(dir, fn);
                item.SaveAs(path);
                MImage mImage = new MImage();
                mImage.Path = folder + "/" + fn;
                mImages.MImageList.Add(mImage);
            }

            // get list size from tb_printsize
            ViewBag.Sizes = client.GetSize();
            return View(mImages);
        }

        [HttpPost]
        public PartialViewResult Calculate(MImages mImages)
        {

            MImages model = client.CalculateImage(mImages);
            mImages.Total = model.Total;
            Session["image"] = mImages;
            return PartialView("_Total", model);
        }

        public ActionResult ShipInfo()
        {
            ViewBag.ShipList = client.GetShipList();
            return View();
        }

        [HttpPost]
        public PartialViewResult GetShip(Recipient recipient)
        {
            decimal imgTotal = (Session["image"] as MImages).Total;
            recipient.Total = client.CalculateShip(recipient, imgTotal);
            Session["recipient"] = recipient;
            ViewBag.Total = recipient.Total;
            return PartialView("_FinalTotal");
        }

        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(Payment payment)
        {
            int custID = 1;
            //TempData["payment"] = payment;
            Recipient recipient = Session["recipient"] as Recipient;
            MImages mImages = Session["image"] as MImages;

            // save credit card to tb_customer
            if (payment.Mode == PayMode.CreditCard.ToString())
            {
                client.AddCreditCard(custID, payment.CardNumber);
            }

            if (!client.VerifyCreditCard(custID, payment.ExpiredDate))
            {
                return View("Upload");
            }

            // save to tb_order            
            int orderID = client.CreateOrder(custID, mImages.Folder, recipient, payment);

            // get list image-id from saving to tb_image            
            client.SaveDetailImage(mImages);

            // save to tb_OrderDetails
            client.SaveOrderDetail(orderID, mImages.Folder);



            Summary summary = new Summary
            {
                OrderID = 1,
                Name = recipient.Name,
                Phone = recipient.Phone,
                Address = recipient.Address,
                Delivery = recipient.Delivery,
                Mode = payment.Mode,
                Total = recipient.Total
            };
            return View("Summary", summary);
        }

        public ActionResult Summary()
        {
            return View();
        }

        // common method
        public ActionResult RemoveDir(string dir)
        {
            string mess = "";
            try
            {
                Directory.Delete(Server.MapPath(dir), true);
                mess = "Remove directory successful!!";
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }
            return Content(mess);
        }        
    }
}