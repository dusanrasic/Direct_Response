using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response_Web_Service
{
    public interface IClient
    {
        [OperationContract]
        void GetMessage(string message, string from, int fromId, string fromImage, string to, int toId);        
    }
}
