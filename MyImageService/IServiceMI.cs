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
        List<tb_customer> GetCustomers();

        [OperationContract]
        List<tb_order> GetOrders();

        [OperationContract]
        List<tb_printsize> GetPrintsizes();

        [OperationContract]
        void CreatePrintsizes();

        [OperationContract]
        void UpdatePrintsize();

        [OperationContract]
        List<tb_customer> SearchCustomerByName(string fname, string lname);

        
     
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
