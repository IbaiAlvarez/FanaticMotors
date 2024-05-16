using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticMotors.Database
{
    public class Connection : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        private static readonly string _connectionIp = "192.168.1.35";
        private static readonly string _connectionPort = "3306";
        private static readonly string _databaseName = "fanaticMotors";
        private static readonly string _username = "admin";
        private static readonly string _password = "pass";
        private readonly string _connString = $"datasource={_connectionIp};port={_connectionPort};username={_username};password={_password};database={_databaseName};";

        public Connection() { }
        

        public string ConnString {  get { return _connString; } }
    }
}
