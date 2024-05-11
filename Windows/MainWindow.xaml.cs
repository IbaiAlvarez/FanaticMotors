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

namespace FanaticMotors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Prueba();
        }

        public void Prueba()
        {
            try
            {
                //Codify password
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
                DataTable dt = new DataTable();
                dt.Load(mysqlcommand.ExecuteReader());
                int cant = Convert.ToInt32(dt.Rows[0]["CANT"]);

                mysqlconnection.Close();
            }
            catch(Exception ex) { }
        }
    }
}
