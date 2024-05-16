using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySqlConnector;
using FanaticMotors.Database;
using XSystem.Security.Cryptography;
using System.Data;
using System.Security.Policy;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FanaticMotors.Windows;
using FanaticMotors.Shared;

namespace FanaticMotors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region Private Attributes
        private MySQL _mySQL = new MySQL();

        private String _userName = string.Empty;
        private String _userPassword = string.Empty;

        private String _loginMessage = string.Empty;
        #endregion


        #region Private Attributes
        public MySQL MySQL { get => _mySQL; set { _mySQL = value; OnPropertyChanged(Internal.Method()); } }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(Internal.Method()); } }
        public string UserPassword { get => _userPassword; set { _userPassword = value; OnPropertyChanged(Internal.Method()); } }

        public string LoginMessage { get => _loginMessage; set { _loginMessage = value; OnPropertyChanged(Internal.Method()); } }
    
        #endregion

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            //Prueba();
        }


        public void Prueba()
        {
            try
            {
                DataTable dt = new DataTable();

                //Codify password
                /*
                var hasher = new SHA256Managed();
                var unhashed = System.Text.Encoding.Unicode.GetBytes("pass");
                var hashed = hasher.ComputeHash(unhashed);
                var hashedPassword = Convert.ToBase64String(hashed);

                Connection con = new Connection();
                MySqlConnection mysqlconnection = new MySqlConnection(con.ConnString);
                MySqlCommand mysqlcommand = new MySqlCommand($"INSERT INTO users VALUES ('ibai.a','Ibai','Ibai Alvarez Hernando','2000-09-10','{hashedPassword}','admin');", mysqlconnection);
                mysqlconnection.Open();
                mysqlcommand.ExecuteNonQuery();


                var hasher2 = new SHA256Managed();
                var unhashed2 = System.Text.Encoding.Unicode.GetBytes("pass");
                var hashed2 = hasher.ComputeHash(unhashed2);
                var hashedPassword2 = Convert.ToBase64String(hashed2);

                String query = $"SELECT COUNT(*) AS CANT FROM users WHERE user_id='ibai.a' AND user_password='{hashedPassword2}';";
                mysqlcommand = new MySqlCommand(query, mysqlconnection);
                dt.Load(mysqlcommand.ExecuteReader());
                int cant = Convert.ToInt32(dt.Rows[0]["CANT"]);

                mysqlconnection.Close();
                */

                var hasher2 = new SHA256Managed();
                var unhashed2 = System.Text.Encoding.Unicode.GetBytes("pass");
                var hashed2 = hasher2.ComputeHash(unhashed2);
                var hashedPassword2 = Convert.ToBase64String(hashed2);

                dt = MySQL.MakeQuery($"SELECT user_kind FROM users WHERE user_id='ibai.a' AND user_password='{hashedPassword2}';");

                int cant = Convert.ToInt32(dt.Rows[0]["CANT"]);

            }
            catch(Exception ex) { }
        }

        private void VerifyLogin_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(UserPassword))
            {
                try
                {
                    var hasher = new SHA256Managed();
                    var unhashed = System.Text.Encoding.Unicode.GetBytes(UserPassword);
                    var hashed = hasher.ComputeHash(unhashed);
                    var hashedPassword2 = Convert.ToBase64String(hashed);

                    DataTable dt = MySQL.MakeQuery($"SELECT user_kind FROM users WHERE user_id='{UserName}' AND user_password='{hashedPassword2}';");

                    String kind = Convert.ToString(dt.Rows[0]["user_kind"]);

                    if (kind.Equals("admin")) {
                        AdminWindow adminWindow = new AdminWindow(UserName);
                        this.Close();
                        adminWindow.ShowDialog();
                    }
                    else if (kind.Equals("admin")) {
                        UserWindow userWindow = new UserWindow(UserName);
                        this.Close();
                        userWindow.ShowDialog();
                    }
                }
                catch(Exception ex) 
                {
                    LoginMessage = "Incorrect user or password.";
                }
            }

        }
    }
}
