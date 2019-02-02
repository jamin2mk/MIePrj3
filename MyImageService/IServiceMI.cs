using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace MyImageService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceMI
    {

        [OperationContract]
        bool AdminLogin(string uid, string pwd);

        [OperationContract]
        List<tb_admin> GetAdmins(string username);

        [OperationContract]
        bool UserLogin(string cus_email, string cus_pass);

        [OperationContract]
        List<tb_customer> GetCustomers(string username);

        [OperationContract]
        void AddCustomer(tb_customer addCust);

        [OperationContract]
        tb_customer GetCustomer(string email);

        [OperationContract]
        void UpdateProfile(tb_customer upCust);

        [OperationContract]
        void UpdatePassword(tb_customer upPwd);

        [OperationContract]
        tb_order GetOrder(string email);

        

    }
    //http://localhost:49993/ServiceMI.svc

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
