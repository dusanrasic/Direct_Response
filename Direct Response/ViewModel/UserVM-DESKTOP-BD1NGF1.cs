using Direct_Response.Model;
using Direct_Response.Properties;
using Direct_Response.UsersDb;
using Direct_Response.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.XPath;

namespace Direct_Response.ViewModel
{
    public class UserVM : IViewModel<Model.User>, INotifyPropertyChanged
    {
        #region Property's
        private Model.User user;
        public Model.User Model
        {
            get { return user; }
        }

        private MessageVM currentMessage;
        public MessageVM CurrentMessageVM
        {
            get { return currentMessage; }
            set { SetAndNotify(ref currentMessage, value); }
        }

        private ConversationVM currentConversation;
        public ConversationVM CurrentConversationVM
        {
            get { return currentConversation; }
            set { SetAndNotify(ref currentConversation, value); }
        }

        private ConversationVM rootVM;
        public ConversationVM RootVM
        {
            get { return rootVM; }
        }

        #endregion
        
        public UserVM(Model.User user)
        {
            this.user = user;
            this.rootVM = new ConversationVM(user.Root);
        }

        private void InitializeCommands()
        {
            this.Save = new RelayCommand
                (
                    delegate (object o)
                    {
                        string path = "C:\\Users\\duki9\\OneDrive\\Desktop\\" + user.UserName + user.Id + "_msg.dr";
                        if (!File.Exists(path))
                        {
                            XmlWriterSettings xmlSettings = new XmlWriterSettings();
                            xmlSettings.Indent = true;
                            using (XmlWriter writer = XmlWriter.Create(path, xmlSettings))
                            {
                                DataContractSerializer serializer = new DataContractSerializer(typeof(Model.User));
                                serializer.WriteObject(writer, this.Model);
                            }
                        }
                        else
                        {
                            
                        }
                    }
                );
        }

        public RelayCommand Save { get; set; }

        #region PropertyChangedImpl
        protected void SetAndNotify<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
