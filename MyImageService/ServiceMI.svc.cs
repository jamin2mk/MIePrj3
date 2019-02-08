using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


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
          var rs =  db.tb_printsize.Where(m => m.pr_size.ToLower().Contains(size.ToLower()));
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
    }
}
