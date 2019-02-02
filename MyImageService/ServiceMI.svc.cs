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

        public List<tb_customer> GetCustomers(string cus_email)
        {
            return db.tb_customer.ToList();
        }

        public bool UserLogin(string cus_email, string cus_pass)
        {
            List<tb_customer> customers = GetCustomers(cus_email);

            for(int i = 0; i < customers.Count; i++)
            {
                if(customers[i].cus_email.Equals(cus_email) && customers[i].cus_pass.Equals(cus_pass))
                {
                return true;
                }
            }
            return false;
        }


        public void AddCustomer(tb_customer addCust)
        {
            db.tb_customer.Add(addCust);
            db.SaveChanges();
        }

        public tb_customer GetCustomer(string email)
        {
            return db.tb_customer.SingleOrDefault(x => x.cus_email.Equals(email));
        }

        public void UpdateProfile(tb_customer upCust)
        {
            var search = GetCustomer(upCust.cus_email);
            if(search != null)
            {
                search.cus_fname = upCust.cus_fname;
                search.cus_lname = upCust.cus_lname;

                search.cus_gender = upCust.cus_gender;
                search.cus_dob = upCust.cus_dob;

                search.cus_add = upCust.cus_add;
                search.cus_phone = upCust.cus_phone;

                search.cus_email = upCust.cus_email;
                //search.cus_pass = upCust.cus_pass;

                db.SaveChanges();
            }
            
        }

        public void UpdatePassword(tb_customer upPwd)
        {
            var search = GetCustomer(upPwd.cus_email);
            if(search != null)
            {
                search.cus_pass = upPwd.cus_pass;

                db.SaveChanges();
            }
        }

        public tb_order GetOrder(string email)
        {
            int id = (from a in db.tb_customer where a.cus_email == email select a.cus_id).First();
            return db.tb_order.SingleOrDefault(x => x.o_cus_id == id);
            //return db.tb_order.SingleOrDefault().ToList();
        }
    }
}
