using Direct_Response.Model;
using Direct_Response.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.ViewModel
{
    public class MessageVM : IViewModel<Message>
    {
        private Message message;
        public Message Model
        {
            get { return message; }
        }

        public MessageVM(Message message)
        {
            this.message = message;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            this.Delete = new RelayCommand
            (
                delegate (object o)
                {
                    Message message = this.Model;
                    message.Parent.Messages.Remove(message);
                }
            );
        }

        public RelayCommand Delete { get; set; }

        #region PropertyChangedImplementation
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
