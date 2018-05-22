using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.Model
{
    [Serializable]
    [DataContract]
    public class Conversation : Message, INotifyPropertyChanged
    {
        #region Property's
        [DataMember]
        private string name;
        public string Name
        {
            get { return name; }
            set { SetAndNotify(ref name, value); }
        }

        [DataMember]
        private string image;
        public string Image
        {
            get { return image; }
            set { SetAndNotify(ref image, value); }
        }

        [DataMember]
        private int idUserName;
        public int IdUserName
        {
            get { return idUserName; }
            set { SetAndNotify(ref idUserName, value); }
        }

        [DataMember]
        private int seen;
        public int Seen
        {
            get { return seen; }
            set { SetAndNotify(ref seen, value); }
        }
        [DataMember]
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get { return messages; }
        }
        #endregion

        public Conversation(Conversation parent) : base(parent) { }

        public Conversation(Conversation parent, string name,int idUserName, string image, int seen) : base(parent)
        {
            this.name = name;
            this.idUserName = idUserName;
            this.image = image;
            this.seen = seen;
        }

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
