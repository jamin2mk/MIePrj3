using MIClient.Models;
using MIData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

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

        [OperationContract]
        tb_order GetOneOrders(int id);
        [OperationContract]
        void UpdateStatus(tb_order upStatus);

        [OperationContract]
        List<tb_order> GetOrdersByStatus(string stt);
        [OperationContract]
        List<tb_order> GetOrderNotFinished();
        // CUSTOMER
        [OperationContract]
        List<tb_customer> GetCustomers();
        [OperationContract]
        List<tb_customer> SearchCustomerByName(string search);
        [OperationContract]
        int CountAllCustomer();


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
        
                          
        // for Customer
        [OperationContract]
        Customer GetCustomer(string email);

        [OperationContract]
        string CreateCustomer(Customer customer);

        [OperationContract]
        void EditCustomer(Customer customer);

        [OperationContract]
        bool Login(string email, string pass);

        [OperationContract]
        void Logout();

        [OperationContract]
        List<FollowedOrder> FindOrders(int custID);

        // for Order
        [OperationContract]
        int CreateOrder(int custID, string folder, Recipient recipient, Payment payment);

        [OperationContract]
        void SaveDetailImage(MImages mImages);

        [OperationContract]
        void SaveOrderDetail(int orderID, string folder);

        [OperationContract]
        string RandomFolder();

        [OperationContract]
        MImages SaveImages(HttpPostedFileBase[] images, string dir, string folder);

        [OperationContract]
        List<string> GetSize();

        [OperationContract]
        tb_deliverytime GetDelivery(DateTime deliveryDate);

        [OperationContract]
        tb_shippingcategory GetShipCate(string location);

        [OperationContract]
        List<string> GetShipList();

        [OperationContract]
        MImages CalculateImage(MImages mImages);

        [OperationContract]
        decimal CalculateShip(Recipient recipient, decimal imgTotal);

        [OperationContract]
        void AddCreditCard(int custID, string custCard);

        [OperationContract]
        bool VerifyCreditCard(int custID, DateTime expiredDate);
        


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
