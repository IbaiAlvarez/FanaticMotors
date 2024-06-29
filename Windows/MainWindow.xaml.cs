using System;
using System.Windows;
using System.Windows.Input;
using FanaticMotors.Database;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Configuration;
using Shared;
using System.Globalization;
using System.Windows.Data;
using XAct;

namespace FanaticMotors.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region Private Attributes
        private MySQL _mySQL = new MySQL();

        private String _userName = string.Empty;
        private String _userPassword = string.Empty;
        private String _userNickName = string.Empty;
        private String _surname = string.Empty;
        private String _birthDate = string.Empty;

        private String _loginMessage = string.Empty;
        private bool _login = true;
        private bool _isPasswordVisible = false;
        private bool _rememberLogin = false;
        #endregion


        #region Private Attributes
        public MySQL MySQL { get => _mySQL; set { _mySQL = value; OnPropertyChanged(); } }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(); } }
        public string UserPassword { get => _userPassword; set { _userPassword = value; OnPropertyChanged(); } }

        public string LoginMessage { get => _loginMessage; set { _loginMessage = value; OnPropertyChanged(); } }

        public bool Login { get => _login; set { _login = value; OnPropertyChanged(); } }

        public string UserNickName { get => _userNickName; set { _userNickName = value; OnPropertyChanged(); } }
        public string Surname { get => _surname; set { _surname = value; OnPropertyChanged(); } }
        public string BirthDate { get => _birthDate; set { _birthDate = value; OnPropertyChanged(); } }

        public bool IsPasswordVisible { get => _isPasswordVisible; set { _isPasswordVisible = value; OnPropertyChanged(); } }

        public bool RememberLogin { get => _rememberLogin; set { _rememberLogin = value; OnPropertyChanged(); } }

        #endregion

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            LoadCredentials();
        }

        public void VerifyLogin()
        {
            LoginMessage = string.Empty;
            if (!String.IsNullOrEmpty(UserNickName) && !String.IsNullOrEmpty(UserPassword) && Login)
            {
                try
                {
                    UserPassword = txt_pass_login.Password;

                    var hasher = new XSystem.Security.Cryptography.SHA256Managed();
                    var unhashed = System.Text.Encoding.Unicode.GetBytes(UserPassword);
                    var hashed = hasher.ComputeHash(unhashed);
                    var hashedPassword2 = Convert.ToBase64String(hashed);

                    DataTable dt = MySQL.MakeQuery($"SELECT user_kind FROM users WHERE user_id='{UserNickName}' AND user_password='{hashedPassword2}';");

                    String kind = Convert.ToString(dt.Rows[0]["user_kind"]);

                    if (kind.Equals("admin"))
                    {
                        SaveCredentials();
                        AdminWindow adminWindow = new AdminWindow(UserNickName);
                        this.Close();
                        adminWindow.ShowDialog();
                    }
                    else if (kind.Equals("user"))
                    {
                        SaveCredentials();
                        UserWindow userWindow = new UserWindow(UserNickName);
                        this.Close();
                        userWindow.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    LoginMessage = "Incorrect user or password.";
                }
            }
            else if (!Login && !String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(txt_pass_register.Password) && !String.IsNullOrEmpty(BirthDate) && !String.IsNullOrEmpty(Surname) && !String.IsNullOrEmpty(UserNickName))
            {
                UserPassword = txt_pass_register.Password;

                var hasher = new XSystem.Security.Cryptography.SHA256Managed();
                var unhashed = System.Text.Encoding.Unicode.GetBytes(UserPassword);
                var hashed = hasher.ComputeHash(unhashed);
                var hashedPassword2 = Convert.ToBase64String(hashed);

                DateTime birthDate = DateTime.ParseExact(BirthDate, "dd/MM/yyyy", null);
                BirthDate = birthDate.ToString("yyyy-MM-dd");

                MySQL.InsertQuery($"INSERT INTO users (user_id,user_name,user_surnames,user_date,user_password) VALUES ('{UserNickName}','{UserName}','{Surname}','{BirthDate}','{hashedPassword2}');");

                SaveCredentials();
                Login = !Login;
                LoginMessage = string.Empty;
                UserName = string.Empty;
                Surname = string.Empty;
                BirthDate = string.Empty;
                UserPassword = string.Empty;
                LoginMessage = "Has been successfully registered!";
            }
            else
            {
                LoginMessage = "You must fill all the sections.";
            }
        }

        private void VerifyLogin_Click(object sender, RoutedEventArgs e)
        {
            VerifyLogin();
        }

        private void ChangeLogin_Click(object sender, MouseButtonEventArgs e)
        {
            Login = !Login;
            LoginMessage = string.Empty;
            UserName = string.Empty;
            Surname = string.Empty;
            UserNickName = string.Empty;
            BirthDate = string.Empty;
            UserPassword = string.Empty;
        }

        private void SaveCredentials()
        {
            Properties.Settings.Default.LOGGED_IN = true;
            if (RememberLogin)
            {
                Properties.Settings.Default.LOGIN_USER = UserNickName;
                Properties.Settings.Default.LOGIN_PASSWORD = Security.Encrypt(UserPassword);
                Properties.Settings.Default.REMEMBER_CREDENTIALS = RememberLogin;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.LOGIN_USER = string.Empty;
                Properties.Settings.Default.LOGIN_PASSWORD = string.Empty;
                Properties.Settings.Default.REMEMBER_CREDENTIALS = RememberLogin;
                Properties.Settings.Default.Save();
            }
        }

        private void LoadCredentials()
        {
            Properties.Settings.Default.LOGGED_IN = false;
            UserNickName = Properties.Settings.Default.LOGIN_USER;
            RememberLogin = Properties.Settings.Default.REMEMBER_CREDENTIALS;

            if (!String.IsNullOrEmpty(Properties.Settings.Default.LOGIN_PASSWORD))
            {
                txt_pass_login.Password = Security.Decrypt(Properties.Settings.Default.LOGIN_PASSWORD);
                UserPassword = txt_pass_login.Password;
            }
        }

        private void PreviewDown_Pass(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !Properties.Settings.Default.LOGGED_IN)
                VerifyLogin();
            else
                CheckPasswordText();
        }

        private void CheckPasswordText()
        {
            if (!IsPasswordVisible)
                UserPassword = txt_pass_login.Password;
            else
                txt_pass_login.Password = UserPassword;
        }

        private void ShowHidePass_Click(object sender, RoutedEventArgs e)
        {
            CheckPasswordText();
            IsPasswordVisible = !IsPasswordVisible;
        }
    }

    public partial class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && string.IsNullOrEmpty(str))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
