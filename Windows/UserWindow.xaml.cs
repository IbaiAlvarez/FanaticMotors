using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FanaticMotors.Database;
using FanaticMotors.Shared;

namespace FanaticMotors.Windows
{
    /// <summary>
    /// Lógica de interacción para UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window, INotifyPropertyChanged
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
        #endregion


        #region Private Attributes
        public MySQL MySQL { get => _mySQL; set { _mySQL = value; OnPropertyChanged(Internal.Method()); } }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(Internal.Method()); } }

        #endregion

        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(String user)
        {
            InitializeComponent();
            this.UserName = user;
        }


    }
}
