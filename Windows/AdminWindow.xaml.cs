using FanaticMotors.Data;
using FanaticMotors.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
        private MySQL _mySQL = new MySQL();
        private String _nickName = string.Empty;

        private String _pilotName = string.Empty;
        private String _pilotSurname = string.Empty;
        private String _pilotNumber = string.Empty;

        private String _birthDay = string.Empty;
        private String _birthMonth = string.Empty;
        private String _birthYear = string.Empty;

        private Pilot _selectedPilot = null;
        private String _selectedStatus = null;

        private List<Pilot> _pilotList = new List<Pilot>();
        private ObservableCollection<Pilot> _pilotDirectory = new ObservableCollection<Pilot>();
        private ObservableCollection<String> _statusDirectory = new ObservableCollection<String>();

        private List<String> _statusList = new List<string> { "retired", "competing" };
        private bool _focusableCombo = false;

        #endregion

        #region Public Attributes
        public MySQL MySQL { get => _mySQL; set { _mySQL = value; OnPropertyChanged(); } }
        public string NickName { get => _nickName; set { _nickName = value; OnPropertyChanged(); } }

        public string BirthDay { get => _birthDay; set { _birthDay = value; OnPropertyChanged(); } }
        public string BirthMonth { get => _birthMonth; set { _birthMonth = value; OnPropertyChanged(); } }
        public string BirthYear { get => _birthYear; set { _birthYear = value; OnPropertyChanged(); } }

        public string PilotName { get => _pilotName; set { _pilotName = value; OnPropertyChanged(); } }
        public string PilotSurname { get => _pilotSurname; set { _pilotSurname = value; OnPropertyChanged(); } }
        public string PilotNumber { get => _pilotNumber; set { _pilotNumber = value; OnPropertyChanged(); } }

        public List<Pilot> PilotList { get => _pilotList; set { _pilotList = value; OnPropertyChanged(); } }
        public ObservableCollection<Pilot> PilotDirectory { get => _pilotDirectory; set { _pilotDirectory = value; OnPropertyChanged(); } }
        public List<string> StatusList { get => _statusList; set { _statusList = value; OnPropertyChanged(); } }

        public Pilot SelectedPilot { get => _selectedPilot; set { _selectedPilot = value; OnPropertyChanged(); if (_selectedPilot != null) { SelectedStatus = _selectedPilot.PilotStatus; } } }

        public String SelectedStatus { get => _selectedStatus; set { _selectedStatus = value; OnPropertyChanged(); if (_selectedStatus != null) { SelectedPilot.PilotStatus = _selectedStatus; } } }

        public ObservableCollection<string> StatusDirectory { get => _statusDirectory; set { _statusDirectory = value; OnPropertyChanged(); } }

        public bool FocusableCombo { get => _focusableCombo; set { _focusableCombo = value; OnPropertyChanged(); } }


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
            LoadPilots(); 
            StatusDirectory = new ObservableCollection<String>(StatusList);
        }

        private void CheckNumber_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            String tag = txt.Tag as string;
            String number = string.Empty;

            try
            {

                if (e.Key.ToString().Any(char.IsDigit))
                {
                    Match match = Regex.Match(e.Key.ToString(), @"\d+");

                    if (match.Success)
                    {
                        number = match.Value;

                        switch (tag)
                        {
                            case "DAY":
                                if(BirthDay.Length<2)
                                    BirthDay += number;
                                else
                                    txt.Text = BirthDay;
                                break;
                            case "MONTH":
                                if (BirthMonth.Length < 2)
                                    BirthMonth += number;
                                else
                                    txt.Text = BirthMonth;
                                break;
                            case "YEAR":
                                if (BirthYear.Length < 4)
                                    BirthYear += number;
                                else
                                    txt.Text = BirthYear;
                                break;
                        }
                    }                    
                }
                else if (e.Key == Key.Back || e.Key == Key.Delete)
                {
                    switch (tag)
                    {
                        case "DAY":
                            BirthDay = txt.Text;
                            break;
                        case "MONTH":
                            BirthMonth = txt.Text;
                            break;
                        case "YEAR":
                            BirthYear = txt.Text;
                            break;
                    }
                }
                else
                {
                    switch (tag)
                    {
                        case "DAY":
                            txt.Text = BirthDay;
                            break;
                        case "MONTH":
                            txt.Text = BirthMonth;
                            break;
                        case "YEAR":
                            txt.Text = BirthYear;
                            break;
                    }
                }
            }
            catch(Exception ex) { }
        }

        private bool ValidateBirthValue(String date_String)
        {
            bool valid = false;

            try
            {
                DateTime date = DateTime.ParseExact(date_String,"yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex) { }

            return valid;
        }

        private void SavePilot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(PilotName) && !String.IsNullOrEmpty(PilotSurname) && !String.IsNullOrEmpty(PilotNumber) && !String.IsNullOrEmpty(BirthDay) && !String.IsNullOrEmpty(BirthMonth) && !String.IsNullOrEmpty(BirthYear))
                {

                    Pilot p = new Pilot(PilotName, PilotSurname, PilotNumber, BirthDay, BirthMonth, BirthYear);
                    if (!p.CheckIfPilotExists())
                        if (!p.CheckIfNumberExists())
                        {
                            p.InsertPilotData();
                            string messageBoxText = "Pilot has been saved.";
                            string caption = "Pilot saved";
                            MessageBoxButton button = MessageBoxButton.OK;
                            MessageBoxImage icon = MessageBoxImage.Information;

                            MessageBox.Show(messageBoxText, caption, button, icon);
                        }
                        else
                        {
                            string messageBoxText = $"Pilot number {p.PilotNumber} is already in use.";
                            string caption = "Pilot number error";
                            MessageBoxButton button = MessageBoxButton.OK;
                            MessageBoxImage icon = MessageBoxImage.Information;

                            MessageBox.Show(messageBoxText, caption, button, icon);
                        }
                    else
                    {
                        string messageBoxText = $"Pilot '{p.PilotName} {p.PilotSurname}' already exists.";
                        string caption = "Pilot exists";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Information;

                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }

                }
                else
                {
                    string messageBoxText = "Fill all the spaces.";
                    string caption = "Incorrect data";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Information;

                    MessageBox.Show(messageBoxText, caption, button, icon);
                }

            }catch(Exception ex)
            {
                string messageBoxText = "Error saving pilot.";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private void LoadPilots()
        {
            try
            {
                String query = $"SELECT * FROM {MySQL.TABLE_PILOTS}; ";
                DataTable dt = MySQL.MakeQuery(query);
                PilotList.Clear();

                foreach (DataRow dr in dt.Rows) 
                {
                    Pilot p = new Pilot(dr);
                    PilotList.Add(p);
                }

                PilotDirectory = new ObservableCollection<Pilot>(PilotList);

            }catch(Exception ex) { }

        }

        private void EditPilot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxFocus_Click(object sender, MouseButtonEventArgs e)
        {
            FocusableCombo = true;
        }
    }
}
