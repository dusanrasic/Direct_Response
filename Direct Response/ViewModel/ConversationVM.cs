using Direct_Response.Model;
using Direct_Response.UsersDb;
using Direct_Response.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.ViewModel
{
    public class ConversationVM : MessageVM
    {
        private ObservableCollectionVM<Message> messages;
        public ObservableCollectionVM<Message> Messages
        {
            get { return messages; }
        }
       
        public ConversationVM(Conversation conversation):base(conversation)
        {
            this.messages = new ObservableCollectionVM<Message>(conversation.Messages, ViewModelUtility.viewModelFactory);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            this.AddMessage = new RelayCommand
            (
                delegate (object o)
                {
                    Conversation conversation = this.Model as Conversation;
                    string text = o.ToString();
                    if(text != "")
                    {
                        Message message = ViewModelUtility.modelFactory("Sent", conversation, text);
                        if (message != null)
                            conversation.Messages.Add(message);
                    }
                    
                    //Message message = ViewModelUtility.modelFactory( "Sent" , conversation, text);
                    //if (message != null)
                    //    conversation.Messages.Add(message);
                }
            );
            this.AddConversation = new RelayCommand
            (
                delegate (object o)
                {
                    Conversation conversation = this.Model as Conversation;
                    Model.User user = o as Model.User;
                    string idUserName = conversation.IdUserName.ToString();
                    Message con = ViewModelUtility.modelFactory("Conversation", conversation, "", user, idUserName);
                    if (con != null)
                        conversation.Messages.Add(con);
                }
            );
        }

        public RelayCommand AddMessage { get; set; }
        public RelayCommand AddConversation { get; set; }
    }
}
