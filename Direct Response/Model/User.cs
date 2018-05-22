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
    public class User : INotifyPropertyChanged
    {   

        #region Property's
        [DataMember]
        private int id;
        public int Id
        {
            get { return id; }
            set { SetAndNotify(ref id, value); }
        }

        [DataMember]
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { SetAndNotify(ref fullName, value); }
        }

        [DataMember]
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetAndNotify(ref userName, value); }
        }

        [DataMember]
        private string password;
        public string Password
        {
            get { return password; }
            set { SetAndNotify(ref password, value); }
        }

        [DataMember]
        private string email;
        public string Email
        {
            get { return email; }
            set { SetAndNotify(ref email, value); }
        }

        [DataMember]
        private string image;
        public string Image
        {
            get { return image; }
            set { SetAndNotify(ref image, value); }
        }
        
        [DataMember]
        private Conversation root = new Conversation(null);
        public Conversation Root
        {
            get { return root; }
        }
        #endregion

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
