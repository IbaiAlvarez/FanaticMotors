﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace FanaticMotors.Database
{
    public class MySQL : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
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

            MySqlCommand mysqlcommand = new MySqlCommand(Query, MySQLconnection);
            MySQLconnection.Open();
            mysqlcommand.ExecuteNonQuery();
            dt.Load(mysqlcommand.ExecuteReader());

            MySQLconnection.Close();

            return dt;
        }
        
    }
}