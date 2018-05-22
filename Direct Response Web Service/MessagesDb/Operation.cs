using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response_Web_Service.MessagesDb
{
    [DataContract]
    [KnownType(typeof(OpMessageBase))]
    public abstract class Operation
    {
        public abstract OperationResult execute(Direct_Response_UsersDbEntities entities);
    }

    [DataContract]
    [KnownType(typeof(OpResCollection))]
    public class OperationResult
    {
        [DataMember]
        public bool Status { get; set; } = true;
        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    public class OpResCollection : OperationResult
    {
        [DataMember]
        public BusinessDataBase[] BaseObjectArray { get; set; }
    }
}
