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

        public void CreatePrintsizes()
        {
            throw new NotImplementedException();
        }

        public List<tb_admin> GetAdmins(string username)
        {
            return db.tb_admin.ToList();
        }

        public List<tb_customer> GetCustomers()
        {
            return db.tb_customer.ToList();
        }

        public List<tb_order> GetOrders()
        {
            return db.tb_order.ToList();
        }

        public List<tb_printsize> GetPrintsizes()
        {
            return db.tb_printsize.ToList();
        }

        public List<tb_customer> SearchCustomerByName(string fname, string lname)
        {
            var result = db.tb_customer.Where(c => c.cus_lname.ToLower().Contains(lname.ToLower()) && c.cus_fname.ToLower().Contains(fname.ToLower()));
            return result.ToList();
        }

        public void UpdatePrintsize()
        {
            throw new NotImplementedException();
        }
    }
}
