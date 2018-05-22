using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.Model
{
    [Serializable]
    [DataContract]
    public class Sent : Message
    {
        public Sent(Conversation parent) : base(parent)
        {
        }
    }
}
