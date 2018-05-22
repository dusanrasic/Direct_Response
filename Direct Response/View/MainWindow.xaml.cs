using Direct_Response.Model;
using Direct_Response.Properties;
using Direct_Response.UsersDb;
using Direct_Response.Utility;
using Direct_Response.View;
using Direct_Response.ViewModel;
using Direct_Response_Web_Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;


namespace Direct_Response
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        string username = "";
        string password = "";
        bool remember = false;
        private DocumentManagerVM documentManagerVM = new DocumentManagerVM();
        Conversation Conversation { get; set; }
        Conversation ConversationSeen { get; set; }
        public static IDirectResponseWebService Server;
        private static DuplexChannelFactory<IDirectResponseWebService> _channelFactory;
        private SoundPlayer soundPlayer = new SoundPlayer();
        private bool settingsActive = false;


        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IDirectResponseWebService>(new ClientCallback(), "DirectResponseServiceEndPoint");
            Server = _channelFactory.CreateChannel();
            ((INotifyCollectionChanged)MessageList.Items).CollectionChanged += ListView_CollectionChanged;
            this.DataContext = documentManagerVM;
            soundPlayer.SoundLocation = @Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Assets\\intuition.wav";

            if (Settings.Default.DR_Username == "username" && Settings.Default.DR_Password == "password")
            {
                LogInSection.Visibility = Visibility.Visible;
                LandingSection.Visibility = Visibility.Collapsed;
                DirectResponseBorder.Visibility = Visibility.Collapsed;
            }
            else
            {
                LogInSection.Visibility = Visibility.Collapsed;
                LandingSection.Visibility = Visibility.Visible;
                DirectResponseBorder.Visibility = Visibility.Visible;

                Color color = Color.FromRgb(38, 50, 56);
                Brush background = new SolidColorBrush(color);
                windowBorderDrag.Background = background;

                string username = Settings.Default.DR_Username;
                string password = Settings.Default.DR_Password;

                OpUserSelect ous = new OpUserSelect();
                ous.Criteria = new CriteriaUser { Username = username, Password = password };
                OperationResult obj = OperationManager.Singleton.executeOperation(ous);

                if ((obj == null) || (!obj.Status))
                {
                    MessageBox.Show("Error while connectiong to the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (obj.DbItems.Count() == 1)
                    {
                        DbItem[] items = obj.DbItems;
                        UserDb[] users = items.Cast<UserDb>().ToArray();

                        int returnValue = Server.Login(users[0].Username, users[0].Full_Name, users[0].User_Id);
                        if (returnValue == 1)
                        {
                            MessageBox.Show("You are already loged in!");
                            Settings.Default.DR_Username = "";
                            Settings.Default.DR_Password = "";
                            Settings.Default.DR_Remember_Me = false;

                            LogInSection.Visibility = Visibility.Visible;
                            LandingSection.Visibility = Visibility.Collapsed;
                            DirectResponseBorder.Visibility = Visibility.Collapsed;

                            Color color2 = Color.FromRgb(0, 0, 0);
                            Brush background2 = new SolidColorBrush(color2);
                            windowBorderDrag.Background = background2;

                            Server.Logout();
                        }
                        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Assets\\" + users[0].Username + users[0].User_Id + "_msg.dr";
                        if (File.Exists(path))
                        {
                            using (XmlReader reader = XmlReader.Create(path))
                            {
                                DataContractSerializer serializer = new DataContractSerializer(typeof(Model.User));
                                Model.User user = serializer.ReadObject(reader) as Model.User;
                                this.documentManagerVM.CurrentUserVM = new UserVM(user);
                            }
                        }
                        else
                        {
                            this.documentManagerVM.CurrentUserVM = new UserVM(new Model.User()
                            {
                                FullName = users[0].Full_Name,
                                UserName = users[0].Username,
                                Email = users[0].Email,
                                Password = users[0].Password,
                                Image = users[0].Image,
                                Id = users[0].User_Id
                            });
                        }
                    }
                }
            }
        }

        #region Scroll to the end of conversation.
        private void ListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // scroll the new item into view   
                MessageList.ScrollIntoView(e.NewItems[0]);
                tbMessage.Text = "";

                if (ListOfConversations.SelectedIndex > 0)
                {
                    int currentConversation = this.documentManagerVM.CurrentUserVM.RootVM.Messages.IndexOf(this.documentManagerVM.CurrentUserVM.CurrentConversationVM);
                    this.documentManagerVM.CurrentUserVM.Model.Root.Messages.Move(currentConversation, 0);
                }
            }
        }
        #endregion

        #region Navigation buttons
        private void windowBorderDrag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void WindowClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else
                this.WindowState = System.Windows.WindowState.Maximized;
        }

        #endregion

        #region Hyperlink for Register Form
        private void HyperLinkRegister_Click(object sender, RoutedEventArgs e)
        {
            
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is RegisterWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                RegisterWindow newwindow = new RegisterWindow();
                if (newwindow.ShowDialog() ?? false)
                {
                    //this.WindowState = WindowState.Normal;
                }
            }
        }
        #endregion

        #region Animation for Window Startup
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation logInHeightAnimation = new DoubleAnimation();
            logInHeightAnimation.From = 0;
            logInHeightAnimation.To = 300;
            logInHeightAnimation.Duration = TimeSpan.FromSeconds(5);
            logInHeightAnimation.EasingFunction = new QuadraticEase();

            LogInFormRow.BeginAnimation(HeightProperty, logInHeightAnimation);


            ThicknessAnimation logoAnimation = new ThicknessAnimation();
            logoAnimation.From = new Thickness(0, 80, 0, 0);
            logoAnimation.To = new Thickness(0, 0, 0, 0);
            logoAnimation.Duration = TimeSpan.FromSeconds(5);
            logoAnimation.EasingFunction = new QuadraticEase();
            logoAnimation.BeginTime = TimeSpan.FromSeconds(3);

            LogoRow.BeginAnimation(MarginProperty, logoAnimation);


            DoubleAnimation logInOpacityAnimation = new DoubleAnimation();
            logInOpacityAnimation.From = 0;
            logInOpacityAnimation.To = 1;
            logInOpacityAnimation.Duration = TimeSpan.FromSeconds(3);
            logInOpacityAnimation.EasingFunction = new QuadraticEase();
            logInOpacityAnimation.BeginTime = TimeSpan.FromSeconds(4.5);

            LogInForm.BeginAnimation(OpacityProperty, logInOpacityAnimation);

        }
        #endregion

        #region SlideMenu Animation
        private void showMenu_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation showMenuAnimation = new ThicknessAnimation();
            showMenuAnimation.From = new Thickness(0, 0, 175, 0);
            showMenuAnimation.To = new Thickness(0, 0, 0, 0);
            showMenuAnimation.Duration = TimeSpan.FromSeconds(1);
            showMenuAnimation.EasingFunction = new QuadraticEase();

            Menu.BeginAnimation(MarginProperty, showMenuAnimation);

            ThicknessAnimation showSettings = new ThicknessAnimation();
            showSettings.From = new Thickness(250, 0, 0, 0);
            showSettings.To = new Thickness(0, 0, 0, 0);
            showSettings.Duration = TimeSpan.FromSeconds(0);
            showSettings.EasingFunction = new QuadraticEase();

            btnSettings.BeginAnimation(MarginProperty, showSettings);

            ThicknessAnimation resizeMessages = new ThicknessAnimation();
            resizeMessages.From = new Thickness(-175, 0, 0, 0);
            resizeMessages.To = new Thickness(0, 0, 0, 0);
            resizeMessages.Duration = TimeSpan.FromSeconds(1);
            resizeMessages.EasingFunction = new QuadraticEase();

            Messages.BeginAnimation(MarginProperty, resizeMessages);

            showMenu.Visibility = Visibility.Collapsed;
            hideMenu.Visibility = Visibility.Visible;
            btnAddConversation.Visibility = Visibility.Visible;

        }

        private void hideMenu_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation hideMenuAnimation = new ThicknessAnimation();
            hideMenuAnimation.From = new Thickness(0, 0, 0, 0);
            hideMenuAnimation.To = new Thickness(0, 0, 175, 0);
            hideMenuAnimation.Duration = TimeSpan.FromSeconds(1);
            hideMenuAnimation.EasingFunction = new QuadraticEase();

            Menu.BeginAnimation(MarginProperty, hideMenuAnimation);

            ThicknessAnimation hideSettings = new ThicknessAnimation();
            hideSettings.From = new Thickness(0, 0, 0, 0);
            hideSettings.To = new Thickness(250, 0, 0, 0);
            hideSettings.Duration = TimeSpan.FromSeconds(0);
            hideSettings.EasingFunction = new QuadraticEase();
            hideSettings.BeginTime = TimeSpan.FromSeconds(1);

            btnSettings.BeginAnimation(MarginProperty, hideSettings);

            ThicknessAnimation resizeMessages = new ThicknessAnimation();
            resizeMessages.From = new Thickness(0, 0, 0, 0);
            resizeMessages.To = new Thickness(-175, 0, 0, 0);
            resizeMessages.Duration = TimeSpan.FromSeconds(1);
            resizeMessages.EasingFunction = new QuadraticEase();

            Messages.BeginAnimation(MarginProperty, resizeMessages);

            showMenu.Visibility = Visibility.Visible;
            hideMenu.Visibility = Visibility.Collapsed;
            btnAddConversation.Visibility = Visibility.Collapsed;

        }
        #endregion

        #region Login/LogOut Section
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = tbUserName.Text;
            password = tbPassword.Password.ToString();
            remember = chbRememberMe.IsChecked ?? false;
            OpUserSelect ous = new OpUserSelect();
            ous.Criteria = new CriteriaUser { Username = username };
            OperationResult obj = OperationManager.Singleton.executeOperation(ous);

            if ((obj == null) || (!obj.Status))
            {
                MessageBox.Show("Error while connectiong to the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (obj.DbItems.Count() == 1)
                {
                    ous.Criteria = new CriteriaUser { Password = password };
                    OperationResult obj1 = OperationManager.Singleton.executeOperation(ous);

                    if (obj1.DbItems.Count() == 1)
                    {
                        #region Setting Property's and Login Section
                        Settings.Default.DR_Username = username;
                        Settings.Default.DR_Password = password;
                        Settings.Default.DR_Remember_Me = remember;

                        LogInSection.Visibility = Visibility.Collapsed;
                        LandingSection.Visibility = Visibility.Visible;
                        DirectResponseBorder.Visibility = Visibility.Visible;

                        Color color = Color.FromRgb(38, 50, 56);
                        Brush background = new SolidColorBrush(color);
                        windowBorderDrag.Background = background;
                        #endregion

                        DbItem[] items = obj.DbItems;
                        UserDb[] users = items.Cast<UserDb>().ToArray();

                        int returnValue = Server.Login(users[0].Username, users[0].Full_Name, users[0].User_Id);
                        if (returnValue == 1)
                        {
                            MessageBox.Show("You are already loged in!");
                            Settings.Default.DR_Username = "";
                            Settings.Default.DR_Password = "";
                            Settings.Default.DR_Remember_Me = false;

                            LogInSection.Visibility = Visibility.Visible;
                            LandingSection.Visibility = Visibility.Collapsed;
                            DirectResponseBorder.Visibility = Visibility.Collapsed;

                            Color color2 = Color.FromRgb(0, 0, 0);
                            Brush background2 = new SolidColorBrush(color2);
                            windowBorderDrag.Background = background2;

                            Server.Logout();
                        }
                        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Assets\\" + users[0].Username + users[0].User_Id + "_msg.dr";
                        if (File.Exists(path))
                        {
                            using (XmlReader reader = XmlReader.Create(path))
                            {
                                DataContractSerializer serializer = new DataContractSerializer(typeof(Model.User));
                                Model.User user = serializer.ReadObject(reader) as Model.User;
                                this.documentManagerVM.CurrentUserVM = new UserVM(user);
                            }
                        }
                        else
                        {
                            this.documentManagerVM.CurrentUserVM = new UserVM(new Model.User()
                            {
                                FullName = users[0].Full_Name,
                                UserName = users[0].Username,
                                Email = users[0].Email,
                                Password = users[0].Password,
                                Image = users[0].Image,
                                Id = users[0].User_Id
                            });
                        }
                    }
                    else
                    {
                        Color color = Color.FromRgb(219, 21, 21);
                        Brush background = new SolidColorBrush(color);
                        tbPassword.BorderBrush = background;
                        Color colorGray = Color.FromRgb(55, 71, 79);
                        Brush borderbrush = new SolidColorBrush(colorGray);
                        tbUserName.BorderBrush = borderbrush;
                        tbUserNameFailIcon.Visibility = Visibility.Collapsed;
                        tbPasswordFailIcon.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Color color = Color.FromRgb(219, 21, 21);
                    Brush background = new SolidColorBrush(color);
                    tbUserName.BorderBrush = background;
                    tbPassword.BorderBrush = background;
                    tbUserNameFailIcon.Visibility = Visibility.Visible;
                    tbPasswordFailIcon.Visibility = Visibility.Visible;
                }

            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.DR_Username = "";
            Settings.Default.DR_Password = "";
            Settings.Default.DR_Remember_Me = false;

            LogInSection.Visibility = Visibility.Visible;
            LandingSection.Visibility = Visibility.Collapsed;
            DirectResponseBorder.Visibility = Visibility.Collapsed;

            Color color = Color.FromRgb(0, 0, 0);
            Brush background = new SolidColorBrush(color);
            windowBorderDrag.Background = background;

            Model.User user = this.documentManagerVM.CurrentUserVM.Model;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Assets\\" + user.UserName + user.Id + "_msg.dr";
            if (!File.Exists(path))
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(path, xmlSettings))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Model.User));
                    serializer.WriteObject(writer, user);
                }
            }
            else
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(path, xmlSettings))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Model.User));
                    serializer.WriteObject(writer, user);
                }
            }

            Server.Logout();
        }
        #endregion

        #region Settings Animation
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState.Equals(WindowState.Normal))
            {
                ThicknessAnimation settingsAnimation = new ThicknessAnimation();
                settingsAnimation.From = new Thickness(0, 0, 0, 0);
                settingsAnimation.To = new Thickness(0, -560, 0, 0);
                settingsAnimation.Duration = TimeSpan.FromSeconds(1);
                settingsAnimation.EasingFunction = new QuadraticEase();

                settingsPanel.BeginAnimation(MarginProperty, settingsAnimation);
            }
            else
            {
                ThicknessAnimation settingsAnimation = new ThicknessAnimation();
                settingsAnimation.From = new Thickness(0, 0, 0, 0);
                settingsAnimation.To = new Thickness(0, -690, 0, 0);
                settingsAnimation.Duration = TimeSpan.FromSeconds(1);
                settingsAnimation.EasingFunction = new QuadraticEase();

                settingsPanel.BeginAnimation(MarginProperty, settingsAnimation);
            }


            btnSettings.Visibility = Visibility.Collapsed;
            hideSettings.Visibility = Visibility.Visible;
            settingsActive = true;


        }

        private void hideSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState.Equals(WindowState.Normal))
            {
                ThicknessAnimation settingsAnimation = new ThicknessAnimation();
                settingsAnimation.From = new Thickness(0, -560, 0, 0);
                settingsAnimation.To = new Thickness(0, 0, 0, 0);
                settingsAnimation.Duration = TimeSpan.FromSeconds(1);
                settingsAnimation.EasingFunction = new QuadraticEase();

                settingsPanel.BeginAnimation(MarginProperty, settingsAnimation);
            }
            else
            {
                ThicknessAnimation settingsAnimation = new ThicknessAnimation();
                settingsAnimation.From = new Thickness(0, -690, 0, 0);
                settingsAnimation.To = new Thickness(0, 0, 0, 0);
                settingsAnimation.Duration = TimeSpan.FromSeconds(1);
                settingsAnimation.EasingFunction = new QuadraticEase();

                settingsPanel.BeginAnimation(MarginProperty, settingsAnimation);
            }


            btnSettings.Visibility = Visibility.Visible;
            hideSettings.Visibility = Visibility.Collapsed;
            settingsActive = false;
        }
        #endregion

        #region Adding Conversation Section
        private void btnAddConversation_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is AddConversationWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                AddConversationWindow newwindow = new Direct_Response.AddConversationWindow(this.documentManagerVM.CurrentUserVM.Model);
                if (newwindow.ShowDialog() ?? false)
                {
                    Model.User currentUser = this.documentManagerVM.CurrentUserVM.Model;
                    Conversation conversation = new Conversation(currentUser.Root);
                    conversation.Name = newwindow.User.Full_Name;
                    conversation.Image = newwindow.User.Image;
                    conversation.IdUserName = newwindow.User.User_Id;
                    conversation.Seen = 0;
                    currentUser.Root.Messages.Add(conversation);
                    int currentUserindex = currentUser.Root.Messages.IndexOf(conversation);
                    currentUser.Root.Messages.Move(currentUserindex, 0);
                    int tmpIndex = currentUser.Root.Messages.IndexOf(conversation);
                    ListOfConversations.SelectedIndex = tmpIndex;
                }
            }
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

        #region Selection of current conversation
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.documentManagerVM.CurrentUserVM.CurrentConversationVM = ListOfConversations.SelectedItem as ConversationVM;
            grdMessageControl.Visibility = Visibility.Visible;

            if (ListOfConversations.SelectedItem == null)
            {
                return;
            }
            else
            {
                ConversationSeen = this.documentManagerVM.CurrentUserVM.CurrentConversationVM.Model as Conversation;
                ConversationVM ConversationSennUpdated = this.documentManagerVM.CurrentUserVM.CurrentConversationVM;
                if (ConversationSeen.Seen != 0)
                {
                    ConversationSeen.Seen = 0;
                }
            }
        }
        #endregion

        #region Window State
        private void Window_Closed(object sender, EventArgs e)
        {
            if (remember == true)
            {
                Settings.Default.DR_Remember_Me = remember;
                Settings.Default.Save();
                Server.Logout();
            }
            else
            {
                Settings.Default.DR_Username = "username";
                Settings.Default.DR_Password = "password";
                Settings.Default.DR_Remember_Me = false;
                Settings.Default.Save();
                Server.Logout();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Model.User user = this.documentManagerVM.CurrentUserVM.Model;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Assets\\" + user.UserName + user.Id + "_msg.dr";
            if (!File.Exists(path))
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(path, xmlSettings))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Model.User));
                    serializer.WriteObject(writer, user);
                }
            }
            else
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(path, xmlSettings))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Model.User));
                    serializer.WriteObject(writer, user);
                }
            }
            Server.Logout();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (settingsActive == true)
            {
                if (this.WindowState.Equals(WindowState.Normal))
                {
                    ThicknessAnimation settingsAnimation = new ThicknessAnimation();
                    settingsAnimation.From = new Thickness(0, 0, 0, 0);
                    settingsAnimation.To = new Thickness(0, -560, 0, 0);
                    settingsAnimation.Duration = TimeSpan.FromSeconds(0);
                    settingsAnimation.EasingFunction = new QuadraticEase();

                    settingsPanel.BeginAnimation(MarginProperty, settingsAnimation);
                }
                else
                {
                    ThicknessAnimation settingsAnimation = new ThicknessAnimation();
                    settingsAnimation.From = new Thickness(0, 0, 0, 0);
                    settingsAnimation.To = new Thickness(0, -690, 0, 0);
                    settingsAnimation.Duration = TimeSpan.FromSeconds(0);
                    settingsAnimation.EasingFunction = new QuadraticEase();

                    settingsPanel.BeginAnimation(MarginProperty, settingsAnimation);
                }
            }

        }
        #endregion

        #region Sending message to server and Reciving
        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = tbMessage.Text;
            Conversation con = documentManagerVM.CurrentUserVM.CurrentConversationVM.Model as Conversation;
            string to = con.Name;
            int toId = con.IdUserName;
            string from = this.documentManagerVM.CurrentUserVM.Model.FullName;
            string fromImage = this.documentManagerVM.CurrentUserVM.Model.Image;
            int fromId = this.documentManagerVM.CurrentUserVM.Model.Id;
            Server.SendMessage(message, from, fromId, fromImage, to, toId);
        }

        public void GetMessage(string text, string from, int fromId, string fromImage, string to, int toId)
        {

            Conversation[] conversations = this.documentManagerVM.CurrentUserVM.Model.Root.Messages.Cast<Conversation>().ToArray();

            foreach (Conversation c in conversations)
            {
                if (c.Name == from && c.IdUserName == fromId)
                    Conversation = c;
            }
            if (Conversation != null)
            {
                Message message = ViewModelUtility.modelFactory("Delivered", Conversation, text);
                if (message != null)
                    Conversation.Messages.Add(message);
                ConversationVM selected = ListOfConversations.SelectedItem as ConversationVM;
                if (Conversation.Name != selected.Model.Parent.Name)
                {
                    Conversation.Seen = 1;
                    ListOfConversations.ItemsSource = null;
                    ListOfConversations.ItemsSource = this.documentManagerVM.CurrentUserVM.RootVM.Messages;
                    soundPlayer.Play();

                }

                else
                {
                    Conversation.Seen = 0;
                    ListOfConversations.ItemsSource = null;
                    ListOfConversations.ItemsSource = this.documentManagerVM.CurrentUserVM.RootVM.Messages;
                    soundPlayer.Play();
                }

            }
            else
            {
                Conversation con = new Conversation(this.documentManagerVM.CurrentUserVM.Model.Root);
                con.Name = from;
                con.Image = fromImage;
                con.IdUserName = fromId;
                con.Messages.Add(new Delivered(con)
                {
                    CreationDate = DateTime.UtcNow,
                    Text = text
                });

                con.Seen = 1;

                this.documentManagerVM.CurrentUserVM.Model.Root.Messages.Add(con);
                soundPlayer.Play();

            }

        }

        private void OnReturnAddMessage(object sender, ExecutedRoutedEventArgs e)
        {
            string text = tbMessage.Text;
            Conversation con = documentManagerVM.CurrentUserVM.CurrentConversationVM.Model as Conversation;
            string to = con.Name;
            int toId = con.IdUserName;
            string from = this.documentManagerVM.CurrentUserVM.Model.FullName;
            string fromImage = this.documentManagerVM.CurrentUserVM.Model.Image;
            int fromId = this.documentManagerVM.CurrentUserVM.Model.Id;
            Server.SendMessage(text, from, fromId, fromImage, to, toId);
            RelayCommand cmd = documentManagerVM.CurrentUserVM.CurrentConversationVM.AddMessage;
            cmd.Execute(text);
            tbMessage.Clear();
        }
        #endregion

        #region Update the user detail's
        private void btnUpdateImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png, *.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            if (dialog.ShowDialog() ?? false)
            {
                FileInfo fileInfo = new FileInfo(dialog.FileName);
                long numBytes = fileInfo.Length;
                double dLen = Convert.ToDouble(fileInfo.Length / 1000000);
                // Because of limited size of 4MB
                if (dLen < 4)
                {
                    FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader binaryReader = new BinaryReader(fileStream);
                    byte[] data = binaryReader.ReadBytes((int)numBytes);
                    binaryReader.Close();

                    string fileName = this.documentManagerVM.CurrentUserVM.Model.UserName + this.documentManagerVM.CurrentUserVM.Model.Id.ToString() + dialog.SafeFileName;
                    int userId = this.documentManagerVM.CurrentUserVM.Model.Id;
                    //action
                    string status = Server.UploadImage(data, fileName);
                    fileStream.Close();
                    fileStream.Dispose();
                    if (status != null)
                    {
                        string password = this.documentManagerVM.CurrentUserVM.Model.Password;
                        int id = documentManagerVM.CurrentUserVM.Model.Id;
                        string fullName = documentManagerVM.CurrentUserVM.Model.FullName;
                        string username = documentManagerVM.CurrentUserVM.Model.UserName;
                        string image = status;
                        string email = documentManagerVM.CurrentUserVM.Model.Email;
                        OpUserUpdate ouu = new OpUserUpdate();
                        ouu.User = new UserDb { User_Id = id, Username = username, Full_Name = fullName, Image = image, Password = password, Email = email };
                        OperationResult obj = OperationManager.Singleton.executeOperation(ouu);
                        if ((obj == null) || (!obj.Status))
                        {
                            return;
                        }
                        else
                        {
                            bool isWindowOpen = false;

                            foreach (Window w in Application.Current.Windows)
                            {
                                if (w is UpdateSuccessWindow)
                                {
                                    isWindowOpen = true;
                                    w.Activate();
                                }
                            }

                            if (!isWindowOpen)
                            {
                                UpdateSuccessWindow newwindow = new UpdateSuccessWindow();
                                if (newwindow.ShowDialog() ?? false)
                                {
                                    this.documentManagerVM.CurrentUserVM.Model.Image = status;
                                    Server.RemoveOldImage(status);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to upload the file", "File Error");
                    }
                }
                else
                {
                    // Display message if the file was too large to upload
                    MessageBox.Show("The file selected exceeds the size limit for uploads.", "File Size");
                }
            }
        }

        private void passwordInfo_PasswordChanged(object sender, RoutedEventArgs e)
        {
            btnUpdate.IsEnabled = true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string password = passwordInfo.Password.ToString();
            string rePassword = rePasswordInfo.Password.ToString();
            int id = documentManagerVM.CurrentUserVM.Model.Id;
            string fullName = documentManagerVM.CurrentUserVM.Model.FullName;
            string username = documentManagerVM.CurrentUserVM.Model.UserName;
            string image = documentManagerVM.CurrentUserVM.Model.Image;
            string email = documentManagerVM.CurrentUserVM.Model.Email;

            if (password == rePassword)
            {
                OpUserUpdate ouu = new OpUserUpdate();
                ouu.User = new UserDb { User_Id = id, Username = username, Full_Name = fullName, Image = image, Password = password, Email = email };
                OperationResult obj = OperationManager.Singleton.executeOperation(ouu);
                if ((obj == null) || (!obj.Status))
                {
                    return;
                }
                else
                {
                    bool isWindowOpen = false;

                    foreach (Window w in Application.Current.Windows)
                    {
                        if (w is UpdateSuccessWindow)
                        {
                            isWindowOpen = true;
                            w.Activate();
                        }
                    }

                    if (!isWindowOpen)
                    {
                        UpdateSuccessWindow newwindow = new UpdateSuccessWindow();
                        if (newwindow.ShowDialog() ?? false)
                        {
                            passwordInfo.Password = "";
                            rePasswordInfo.Password = "";
                            btnUpdate.IsEnabled = false;
                            Color color = Color.FromRgb(55, 71, 79);
                            Brush background = new SolidColorBrush(color);
                            passwordInfo.BorderBrush = background;
                            rePasswordInfo.BorderBrush = background;
                        }
                    }
                }
            }
            else
            {
                Color color = Color.FromRgb(219, 21, 21);
                Brush background = new SolidColorBrush(color);
                passwordInfo.BorderBrush = background;
                rePasswordInfo.BorderBrush = background;
            }
        }
        #endregion
    }
}
