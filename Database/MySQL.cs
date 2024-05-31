using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace FanaticMotors.Database
{
    public class MySQL : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private String _query = string.Empty;
        private Connection connection = new Connection();
        private MySqlConnection mysqlconnection = new MySqlConnection();

        public MySQL()
        {
            mysqlconnection = new MySqlConnection(connection.ConnString);
        }

        public string Query { get => _query; set => _query = value; }
        public Connection Connection { get => connection; set => connection = new Connection(); }
        public MySqlConnection MySQLconnection { get => mysqlconnection; set => mysqlconnection = value; }


        public DataTable MakeQuery(String query)
        {
            Query = query;
            DataTable dt = new DataTable();

            try
            {
                MySQLconnection.Open();
                MySqlCommand mysqlcommand = new MySqlCommand(Query, MySQLconnection);
                dt.Load(mysqlcommand.ExecuteReader());
            }catch (Exception ex) 
            { 
            }
            MySQLconnection.Close();

            return dt;
        }

        public bool InsertQuery(String query)
        {
            Query = query;
            bool executed = false;
            try
            {
                MySQLconnection.Open();
                MySqlCommand mysqlcommand = new MySqlCommand(Query, MySQLconnection);
                mysqlcommand.ExecuteNonQuery(); 
                executed = true;
            }
            catch(Exception ex) 
            { 
            }

            MySQLconnection.Close();

            return executed;
        }

    }
}
