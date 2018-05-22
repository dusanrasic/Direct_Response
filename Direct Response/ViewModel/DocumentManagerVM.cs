using Direct_Response.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.ViewModel
{
    public class DocumentManagerVM : INotifyPropertyChanged
    {
        private UserVM currentUserVM;
        public UserVM CurrentUserVM
        {
            get { return currentUserVM; }
            set { SetAndNotify(ref currentUserVM, value); }
        }

        public DocumentManagerVM()
        {
            InitializeCommands();
        }
        private void InitializeCommands() { }

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
