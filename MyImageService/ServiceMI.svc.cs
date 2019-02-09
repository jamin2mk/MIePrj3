using MIData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Data.Entity.Validation;

namespace MyImageService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceMI : IServiceMI
    {
        MyImageEntities db = new MyImageEntities();

        public bool AdminLogin(string uid, string pwd)
        {
            List<tb_admin> admins = GetAdmins(uid);
            for (int i = 0; i < admins.Count; i++)
            {
                if (admins[i].ad_username.Equals(uid) && admins[i].ad_password.Equals(pwd))
                {
                    return true;
                }
            }

            return false;
        }

        public List<tb_admin> GetAdmins(string username)
        {
            return db.tb_admin.ToList();
        }
        //CUSTOMER
        public List<tb_customer> GetCustomers()
        {
            return db.tb_customer.ToList();
        }

        public List<tb_customer> SearchCustomerByName(string fname, string lname)
        {
            var result = db.tb_customer.Where(c => c.cus_lname.ToLower().Contains(lname.ToLower()) && c.cus_fname.ToLower().Contains(fname.ToLower()));
            return result.ToList();
        }

        public int CountAllCustomer()
        {
            return GetCustomers().ToList().Count();
        }

        //ORDER
        public List<tb_order> GetOrders()
        {
            return db.tb_order.ToList();
        }

        public tb_order GetOneOrders(int id)
        {
            return db.tb_order.Find(id);
        }

        public void UpdateStatus(tb_order upStatus)
        {


            try
            {
                var a = db.tb_order.Find(upStatus.o_id);
                if (a != null)
                {
                    a.o_cus_id = upStatus.o_cus_id;
                    a.o_date = upStatus.o_date;
                    a.o_deli_date = upStatus.o_deli_date;
                    a.o_dt_id = upStatus.o_dt_id;
                    a.o_folder = upStatus.o_folder;
                    a.o_id = upStatus.o_id;
                    a.o_pay = upStatus.o_pay;
                    a.o_pr_id = upStatus.o_pr_id;
                    a.o_recip = upStatus.o_recip;
                    a.o_recip_phone = upStatus.o_recip_phone;
                    a.o_shipadd = upStatus.o_shipadd;
                    a.o_status = upStatus.o_status;
                    a.o_s_id = upStatus.o_s_id;

                    db.SaveChanges();
                }


            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public List<tb_order> GetOrdersByStatus(string stt)
        {
            var a = db.tb_order.Where(m => m.o_status.ToLower().Equals(stt.ToLower()));
            return a.ToList();
        }

        public List<tb_order> GetOrderNotFinished()
        {

            var a = db.tb_order.Where(m => m.o_status.Equals("Delivery") || m.o_status.Equals("Waiting"));
            return a.ToList();
        }



        //PRINT SIZE
        public List<tb_printsize> GetPrintsizes()
        {
            return db.tb_printsize.ToList();
        }

        public tb_printsize GetOnePrintsize(int id)
        {
            return db.tb_printsize.Find(id);
        }

        public void DeletePrintsizes(int id)
        {
            var a = db.tb_printsize.Find(id);
            db.tb_printsize.Remove(a);
            db.SaveChanges();
        }
        public void CreatePrintsizes(tb_printsize newSize)
        {
            db.tb_printsize.Add(newSize);
            db.SaveChanges();
        }
        public List<tb_printsize> SearchPrintSizebyName(string size)
        {
            var rs = db.tb_printsize.Where(m => m.pr_size.ToLower().Contains(size.ToLower()));
            return rs.ToList();

        }

        public bool ValidatePrintSize(tb_printsize valsize)
        {
            List<tb_printsize> list = SearchPrintSizebyName(valsize.pr_size);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].pr_size == valsize.pr_size)
                {
                    return false;
                }
            }
            return true;
        }

        public void UpdatePrintsize(tb_printsize updateSize)
        {
            var a = db.tb_printsize.Find(updateSize.pr_id);
            if (a != null)
            {
                a.pr_size = updateSize.pr_size;
                a.pr_price = updateSize.pr_price;
                db.SaveChanges();
            }
        }


        // for Customer

        public Customer GetCustomer(string email)
        {
            tb_customer cust = db.tb_customer.Where(c => c.cus_email.Equals(email)).FirstOrDefault();
            return new Customer { cus_id = cust.cus_id, cus_fname = cust.cus_fname };
        }

        public string CreateCustomer(Customer customer)
        {
            string mess = null;
            var check = db.tb_customer.Where(c => c.cus_email.Equals(customer.cus_email)).FirstOrDefault();
            if (check != null)
            {
                mess = "Email is existed. Please try with another.";
                return mess;
            }
            if (!customer.cus_pass.Equals(customer.pass_confirm))
            {
                mess = "Password and Password Confirm are not matched.";
                return mess;
            }
            tb_customer newCust = new tb_customer();
            newCust.cus_fname = customer.cus_fname;
            newCust.cus_lname = customer.cus_lname;
            newCust.cus_gender = customer.cus_gender;
            newCust.cus_dob = customer.cus_dob;
            newCust.cus_phone = customer.cus_phone;
            newCust.cus_add = customer.cus_add;
            newCust.cus_email = customer.cus_email;
            newCust.cus_card = CryptoLib.EncryptString(customer.cus_card);
            newCust.cus_pass = CryptoLib.EncryptString(customer.cus_pass);
            db.tb_customer.Add(newCust);
            db.SaveChanges();
            return mess;
        }

        public void EditCustomer(Customer customer)
        {
            tb_customer cust = db.tb_customer.Find(customer.cus_id);
            if (cust != null)
            {
                cust.cus_fname = customer.cus_fname;
                cust.cus_lname = customer.cus_lname;
                cust.cus_gender = customer.cus_gender;
                cust.cus_dob = customer.cus_dob;
                cust.cus_phone = customer.cus_phone;
                cust.cus_add = customer.cus_add;
                cust.cus_email = customer.cus_email;
                cust.cus_card = CryptoLib.EncryptString(customer.cus_card);
                cust.cus_pass = CryptoLib.EncryptString(customer.cus_pass);
                db.SaveChanges();
            }
        }

        public bool Login(string email, string pass)
        {
            tb_customer user = db.tb_customer.Where(u => u.cus_email.Equals(email)).FirstOrDefault();
            if (user != null)
            {
                if (CryptoLib.DecryptString(user.cus_pass).Equals(pass))
                {
                    return true;
                }
            }
            return false;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public List<FollowedOrder> FindOrders(int custID)
        {
            List<FollowedOrder> orders = (from o in db.tb_order
                                          where o.o_cus_id == custID
                                          select new FollowedOrder { OrderDate = o.o_date, ShipOrder = o.o_deli_date, ShipAddress = o.o_shipadd, Payment = o.o_pay, Status = o.o_status }).ToList();
            return orders;
        }

        // for Order
        public int CreateOrder(int custID, string folder, Recipient recipient, Payment payment)
        {
            try
            {
                int deliveryID = GetDelivery(recipient.Delivery).dt_id;
                int shipID = GetShipCate(recipient.Province).s_id;
                tb_order order = new tb_order { o_cus_id = custID, o_date = DateTime.Now, o_pay = payment.Mode, o_shipadd = recipient.Address, o_folder = folder, o_pr_id = 1, o_recip = recipient.Name, o_recip_phone = recipient.Phone.ToString(), o_deli_date = recipient.Delivery, o_s_id = shipID, o_dt_id = deliveryID, o_status = "Waiting" };
                db.tb_order.Add(order);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            var orderID = db.tb_order.Where(o => o.o_folder == folder).FirstOrDefault().o_id;
            return orderID;
        }

        public void SaveDetailImage(MImages mImages)
        {
            try
            {
                foreach (var mImage in mImages.MImageList)
                {
                    tb_image image = new tb_image { img_size = mImage.Size, img_copies = mImage.Copies, img_link = mImage.Path };
                    db.tb_image.Add(image);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveOrderDetail(int orderID, string folder)
        {
            try
            {
                var addedImages = db.tb_image.Where(item => item.img_link.Contains(folder)).ToList();
                foreach (var img in addedImages)
                {
                    tb_orderdetail orderDetail = new tb_orderdetail { orderdetail_o_id = orderID, orderdetail_img_id = img.img_id };
                    db.tb_orderdetail.Add(orderDetail);
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RandomFolder()
        {
            int xxxx = new Random().Next(1000, 9999);
            string folder = "folder_" + xxxx;

            return "folder_" + xxxx;
        }

        public MImages SaveImages(HttpPostedFileBase[] images, string dir, string folder)
        {
            MImages mImages = new MImages();
            foreach (var img in images)
            {
                string imgName = Path.GetFileName(img.FileName);
                string path = Path.Combine(dir, imgName);
                img.SaveAs(path);
                mImages.MImageList.Add(new MImage { Path = Path.Combine(folder, imgName) });
            }
            return mImages;
        }

        public MImages CalculateImage(MImages mImages)
        {
            var model = new MImages();

            var orderSize = from o in mImages.MImageList
                            group o by new { o.Size, o.Copies } into groupSize
                            select new MImage { Size = groupSize.Key.Size, Copies = groupSize.Sum(s => s.Copies) };

            var printSize = from p in db.tb_printsize
                            select new MImage { Size = p.pr_size, Price = p.pr_price };

            model.MImageList = (from o in orderSize
                                join p in printSize on o.Size equals p.Size
                                select new MImage { Size = o.Size, Price = p.Price, Copies = o.Copies }).ToList();

            foreach (var mImage in model.MImageList)
            {
                model.Total += (mImage.Price * mImage.Copies);
            }
            return model;
        }

        public decimal CalculateShip(Recipient recipient, decimal imgTotal)
        {
            decimal total;
            total = GetShipCate(recipient.Province).s_price + (decimal)GetDelivery(recipient.Delivery).dt_ratio * imgTotal;
            return total;
        }

        public void AddCreditCard(int custID, string custCard)
        {
            try
            {
                var customer = db.tb_customer.Find(custID);
                customer.cus_card = CryptoLib.EncryptString(custCard);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool VerifyCreditCard(int custID, DateTime expiredDate)
        {
            string encryptedCard = (from c in db.tb_customer
                                    where c.cus_id == custID
                                    select c.cus_card).FirstOrDefault();

            string decryptedCard = CryptoLib.DecryptString(encryptedCard);

            if (decryptedCard.Length != 20 || expiredDate < DateTime.Now.Date)
            {
                return false;
            }
            return true;
        }

        public List<string> GetSize()
        {
            List<string> sizes = new List<string>();
            sizes = (from s in db.tb_printsize
                     select s.pr_size).ToList();
            return sizes;
        }

        public tb_deliverytime GetDelivery(DateTime deliveryDate)
        {
            int days = (deliveryDate.Date - DateTime.Now.Date).Days;
            tb_deliverytime delivery = (from d in db.tb_deliverytime
                                        where d.dt_num >= days
                                        orderby d.dt_num ascending
                                        select d).First();
            return delivery;
        }

        public tb_shippingcategory GetShipCate(string location)
        {
            tb_shippingcategory shipping = (from l in db.tb_shippingcategory
                                            where l.s_location == location
                                            select l).FirstOrDefault();
            return shipping;
        }

        public List<string> GetShipList()
        {
            var shipList = (from s in db.tb_shippingcategory
                            select s.s_location).ToList();
            return shipList;
        }

    }
}
