using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.Model
{
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class Message : INotifyPropertyChanged
    {
        #region Property's
        [DataMember]
        private Conversation parent;
        public Conversation Parent
        {
            get { return parent; }
            set { SetAndNotify(ref parent, value); }
        }

        [DataMember]
        private string text;
        public string Text
        {
            get { return text; }
            set { SetAndNotify(ref text, value); }
        }
        [DataMember]
        private DateTime creationDate;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { SetAndNotify(ref creationDate, value); }
        }

        #endregion

        public Message(Conversation parent)
        {
            this.parent = parent;
        }

        public Message(Conversation parent, string text, DateTime creationDate)
        {
            this.parent = parent;
            this.text = text;
            this.creationDate = creationDate;
        }

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
