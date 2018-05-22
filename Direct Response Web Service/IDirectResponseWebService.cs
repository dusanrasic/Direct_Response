using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Direct_Response_Web_Service
{
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IDirectResponseWebService
    {
        [OperationContract]
        int Login(string userName, string fullName, int id);

        [OperationContract]
        void Logout();

        [OperationContract]
        void SendMessage(string message, string from, int fromId, string fromImage, string to, int toId);

        [OperationContract]
        string UploadImage(byte[] file, string filename);

        [OperationContract]
        void RemoveOldImage(string filename);
        
    }
}
