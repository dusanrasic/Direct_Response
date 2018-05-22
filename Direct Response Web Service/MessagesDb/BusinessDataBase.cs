using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response_Web_Service.MessagesDb
{
    [DataContract]
    [KnownType(typeof(BMessageInfo))]
    public abstract class BusinessDataBase
    {
    }

    [DataContract]
    public class BMessageInfo : BusinessDataBase
    {
        public int IdMessage { get; set; }
        public string To { get; set; }
        public int ToId { get; set; }
        public string From { get; set; }
        public int FromId { get; set; }
        public string FromImage { get; set; }
        public string Message { get; set; }
    }
}
