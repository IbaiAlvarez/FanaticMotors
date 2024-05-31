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

namespace FanaticMotors.Windows
{
    /// <summary>
    /// Lógica de interacción para AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Private Attributes
        private String _nickName = string.Empty;

        #endregion

        #region Public Attributes
        public string NickName { get => _nickName; set { _nickName = value; OnPropertyChanged(); } }

        #endregion


        public AdminWindow()
        {
            InitializeComponent();
        }
        public AdminWindow(String user)
        {
            DataContext = this;
            InitializeComponent();
            this.NickName = user;
        }

    }
}
