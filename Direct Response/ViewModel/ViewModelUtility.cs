using Direct_Response.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.ViewModel
{
    public static class ViewModelUtility
    {
        public static MessageVM viewModelFactory(Message message)
        {
            if (message is Conversation)
                return new ConversationVM(message as Conversation);
            if (message is Sent)
                return new SentVM(message as Sent);
            if (message is Delivered)
                return new DeliveredVM(message as Delivered);
            return null;
        }

        internal static Message modelFactory(string strMessage, Conversation parent , string text = null, User conversation = null, string idUserName = null)
        {  
            switch (strMessage.ToLower())
            {
                case "sent":
                    return new Sent(parent) { Text = text, CreationDate = System.DateTime.Now };
                case "delivered":
                    return new Delivered(parent) { Text = text, CreationDate = System.DateTime.Now};
                case "conversation":
                    return new Conversation(parent) { Name = conversation.FullName, Image = conversation.Image, IdUserName= Int32.Parse(idUserName) };
                default:
                    return null;
            }
        }
    }
}
