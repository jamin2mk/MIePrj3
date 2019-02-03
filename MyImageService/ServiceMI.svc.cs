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

        public List<tb_customer> GetCustomers()
        {
            return db.tb_customer.ToList();
        }

        public List<tb_customer> SearchCustomerByName(string fname, string lname)
        {
            var result = db.tb_customer.Where(c => c.cus_lname.ToLower().Contains(lname.ToLower()) && c.cus_fname.ToLower().Contains(fname.ToLower()));
            return result.ToList();
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
            var a = db.tb_order.Find(upStatus.o_id);
            if (a != null)
            {                
                a.o_status = upStatus.o_status;
                db.SaveChanges();
            }
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
