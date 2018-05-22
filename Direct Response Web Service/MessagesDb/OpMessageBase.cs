using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response_Web_Service.MessagesDb
{
    [DataContract]
    
    [KnownType(typeof(OpMessageInsert))]
    [KnownType(typeof(OpMessageSelect))]
    [KnownType(typeof(OpMessageDelete))]
    public abstract class OpMessageBase : Operation
    {
        public override OperationResult execute(Direct_Response_UsersDbEntities entities)
        {
            IEnumerable<BMessageInfo> ieMessage =
                from message in entities.NonDeliveredMessages
                select new BMessageInfo
                {
                    IdMessage = message.IdMessage,
                    To = message.To,
                    ToId = message.ToId,
                    From = message.From,
                    FromId = message.FromId,
                    FromImage = message.FromImage,
                    Message = message.Message
                };
            OpResCollection opObj = new OpResCollection();
            opObj.Status = true;
            opObj.BaseObjectArray = ieMessage.ToArray();
            return opObj;
        }
    }
    public class OpMessageSelect : OpMessageBase
    {

    }
    public class OpMessageInsert : OpMessageBase
    {
        private BMessageInfo message;
        public BMessageInfo Message
        {
            get { return message; }
            set { message = value; }
        }
        public override OperationResult execute(Direct_Response_UsersDbEntities entities)
        {
            entities.NonDeliveredInsert(message.To, message.ToId, message.From, message.FromId, message.FromImage, message.Message);
            
            return base.execute(entities);
        }

    }
    public class OpMessageDelete : OpMessageBase
    {
        private int idMessage;
        public int IdMessage
        {
            get { return idMessage; }
            set { idMessage = value; }
        }
        public override OperationResult execute(Direct_Response_UsersDbEntities entities)
        {
            if (idMessage > 0)
                entities.NonDeliveredDelete(idMessage);
            return base.execute(entities);
        }
    }
}
