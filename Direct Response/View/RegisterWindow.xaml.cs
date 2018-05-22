using Direct_Response.UsersDb;
using Direct_Response.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

        }

        #region Navigation buttons
        private void windowBorderDrag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnRegisterClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region Registration of user
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string fullName = tbFullName.Text;
            string email = tbEmail.Text;
            string username = tbUserName.Text;
            string password = tbPassword.Password.ToString();
            string rePassword = tbRePassword.Password.ToString();
            string image = "https://dusanrasic.rs/img/user.png";

            if (password == rePassword)
            {
                #region Passwod Match
                Color color = Color.FromRgb(207, 216, 220);
                Brush background = new SolidColorBrush(color);
                tbPassword.BorderBrush = background;
                tbRePassword.BorderBrush = background;
                tbRePasswordFailIcon.Visibility = Visibility.Collapsed;
                tbPasswordFailIcon.Visibility = Visibility.Collapsed;
                #endregion

                if (IsValid(email))
                {
                    #region Valid email
                    Color colorMail = Color.FromRgb(207, 216, 220);
                    Brush backgroundMail = new SolidColorBrush(color);
                    tbEmail.BorderBrush = backgroundMail;
                    tbEmailFailIcon.Visibility = Visibility.Collapsed;
                    #endregion
                    OpUserSelect ous = new OpUserSelect();
                    ous.Criteria = new CriteriaUser { Username = username };
                    OperationResult obj = OperationManager.Singleton.executeOperation(ous);
                    if ((obj == null) || (!obj.Status))
                    {
                        return;
                    }
                    else
                    {
                        if (obj.DbItems.Count() == 1)
                        {
                            Color colorUserName = Color.FromRgb(219, 21, 21);
                            Brush backgroundUser = new SolidColorBrush(colorUserName);
                            tbUserName.BorderBrush = backgroundUser;
                            tbUserNameFailIcon.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            #region Valid UserName
                            Color colorUserName = Color.FromRgb(207, 216, 220);
                            Brush backgroundUser = new SolidColorBrush(colorUserName);
                            tbEmail.BorderBrush = backgroundUser;
                            tbUserNameFailIcon.Visibility = Visibility.Collapsed;
                            #endregion
                            OpUserInsert oui = new OpUserInsert();
                            oui.User = new UserDb { Username = username, Password = password, Email = email, Full_Name = fullName, Image = image };
                            OperationResult obj1 = OperationManager.Singleton.executeOperation(oui);
                            if ((obj1 == null) || (!obj1.Status))
                            {
                                return;
                            }
                            else
                            {
                                bool isWindowOpen = false;

                                foreach (Window w in Application.Current.Windows)
                                {
                                    if (w is RegistrationSuccessWindow)
                                    {
                                        isWindowOpen = true;
                                        w.Activate();
                                    }
                                }

                                if (!isWindowOpen)
                                {
                                    RegistrationSuccessWindow newwindow = new RegistrationSuccessWindow();
                                    if (newwindow.ShowDialog() ?? false)
                                    {
                                        this.DialogResult = true;
                                        this.Close();
                                    }
                                }
                            }
                        }
                    }                    
                }
                else
                {
                    #region Valid email
                    Color colorMail = Color.FromRgb(219, 21, 21);
                    Brush backgroundMail = new SolidColorBrush(colorMail);
                    tbEmail.BorderBrush = backgroundMail;
                    tbEmailFailIcon.Visibility = Visibility.Visible;
                    #endregion
                }

            }
            else
            {
                #region Password Match
                Color color = Color.FromRgb(219, 21, 21);
                Brush background = new SolidColorBrush(color);
                tbPassword.BorderBrush = background;
                tbRePassword.BorderBrush = background;
                tbRePasswordFailIcon.Visibility = Visibility.Visible;
                tbPasswordFailIcon.Visibility = Visibility.Visible;
                #endregion
            }
        } 
        #endregion

        #region Mail validation
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        } 
        #endregion
    }
}
