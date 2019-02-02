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
        //ADMIN
        [OperationContract]
        bool AdminLogin(string uid, string pwd);

        [OperationContract]
        List<tb_admin> GetAdmins(string username);

        
        //ORDER 
        [OperationContract]
        List<tb_order> GetOrders();

        // CUSTOMER
        [OperationContract]
        List<tb_customer> GetCustomers();
        [OperationContract]
        List<tb_customer> SearchCustomerByName(string fname, string lname);

        //PRINT SIZE
        [OperationContract]
        List<tb_printsize> GetPrintsizes();

        [OperationContract]
        tb_printsize GetOnePrintsize(int id);

        [OperationContract]
        void CreatePrintsizes(tb_printsize newSize);

        [OperationContract]
        void DeletePrintsizes(int id);

        [OperationContract]
        void UpdatePrintsize(tb_printsize updateSize);

        [OperationContract]
        bool ValidatePrintSize(tb_printsize valsize);

        [OperationContract]
        List<tb_printsize> SearchPrintSizebyName(string size);

        
     
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
