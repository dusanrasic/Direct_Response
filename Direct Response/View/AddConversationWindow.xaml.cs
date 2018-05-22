using Direct_Response.Model;
using Direct_Response.Properties;
using Direct_Response.UsersDb;
using Direct_Response.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Direct_Response
{
    public partial class AddConversationWindow : Window, INotifyPropertyChanged
    {
        #region Collection of user's and filtering
        private ObservableCollection<UserDb> user_Collection;
        public ObservableCollection<UserDb> User_Collection
        {
            get { return user_Collection; }
            set { user_Collection = value; }
        }
        public ICollectionView ItemsView
        {
            get { return CollectionViewSource.GetDefaultView(User_Collection); }
        }
        private bool Filter(UserDb user)
        {
            return Search == null
                || user.Full_Name.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
                || user.Username.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;

                ItemsView.Refresh(); // required    
            }
        }

        #endregion

        public UserDb User { get; set; }

        public AddConversationWindow(Model.User user)
        {
            InitializeComponent();
            this.DataContext = this;
            #region Showing data in ListBox Control
            OpUserSelect ous = new OpUserSelect();
            OperationResult obj = OperationManager.Singleton.executeOperation(ous);

            if ((obj == null) || (!obj.Status))
            {
                MessageBox.Show("Error while connectiong to the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DbItem[] items = obj.DbItems;
                UserDb[] users = items.Cast<UserDb>().ToArray();
                //Don't show current user
                var currentUser = users.SingleOrDefault(item => item.Username == user.UserName);
                users = users.Where(val => val != currentUser).ToArray();
                //Don't show already added conversation
                Conversation[] conversations = user.Root.Messages.Cast<Conversation>().ToArray();
                foreach (var conversation in conversations)
                {
                    var added = users.SingleOrDefault(item => item.Full_Name == conversation.Name);
                    users = users.Where(w => w != added).ToArray();
                }
                User_Collection = new ObservableCollection<UserDb>(users);
            }

            ItemsView.Filter = new Predicate<object>(o => Filter(o as UserDb)); 
            #endregion
        }

        private void windowBorderDrag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAddConversation_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnAddConversationClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lvAddConversation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.User = lvAddConversation.SelectedItem as UserDb;
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
