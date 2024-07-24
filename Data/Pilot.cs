using FanaticMotors.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FanaticMotors.Data
{
    public class Pilot : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Private Attributes
        private int _pilotId = 0;
        private String _pilotName = string.Empty;
        private String _pilotSurname = string.Empty;
        private String _pilotFullName = string.Empty;
        private String _pilotNumber = string.Empty;
        private String _pilotStatus = string.Empty;

        private String _birthDay = string.Empty;
        private String _birthMonth = string.Empty;
        private String _birthYear = string.Empty;                                           
        private String _fullDate = string.Empty;

        private MySQL _mySQL = new MySQL();

        #endregion

        #region Public Attributes
        public string BirthDay { get => _birthDay; set { _birthDay = value; OnPropertyChanged(); } }
        public string BirthMonth { get => _birthMonth; set { _birthMonth = value; OnPropertyChanged(); } }
        public string BirthYear { get => _birthYear; set { _birthYear = value; OnPropertyChanged(); } }

        public string PilotName { get => _pilotName; set { _pilotName = value; OnPropertyChanged(); } }
        public string PilotSurname { get => _pilotSurname; set { _pilotSurname = value; OnPropertyChanged(); } }
        public string PilotNumber { get => _pilotNumber; set { _pilotNumber = value; OnPropertyChanged(); } }

        public string FullDate { get => _fullDate; set { _fullDate = value; OnPropertyChanged(); } }

        public MySQL MySQL { get => _mySQL; set { _mySQL = value; OnPropertyChanged(); } }

        public string PilotFullName { get => _pilotFullName; set { _pilotFullName = value; OnPropertyChanged(); } }

        public string PilotStatus { get => _pilotStatus; set { _pilotStatus = value; OnPropertyChanged(); } }

        public int PilotId { get => _pilotId; set { _pilotId = value; OnPropertyChanged(); } }

        #endregion



        public Pilot() { }

        public Pilot(String name, String surname, String number, String day, String month, String year)
        {
            PilotName = name;
            PilotSurname = surname;
            PilotNumber = number;
            FullDate = year+"-"+month+"-"+day;
        }

        public Pilot(DataRow dr)
        {
            PilotId = Convert.ToInt32(dr["Pilot_Id"]);
            PilotName = Convert.ToString(dr["Pilot_Name"]);
            PilotSurname = Convert.ToString(dr["Pilot_Surname"]);
            PilotFullName = PilotName+ " "+PilotSurname;
            PilotNumber = Convert.ToString(dr["Pilot_Number"]);
            FullDate = Convert.ToString(dr["Pilot_BirthDay"]);
            PilotStatus = Convert.ToString(dr["Pilot_Status"]);
        }

        public bool InsertPilotData()
        {
            bool error = false;

            try
            {
                String query = $"INSERT INTO {MySQL.TABLE_PILOTS} (Pilot_Name, Pilot_Surname, Pilot_Number, Pilot_BirthDay) VALUES ('{PilotName}','{PilotSurname}',{PilotNumber},'{FullDate}'); ";
                MySQL.InsertQuery(query);

            }catch(Exception e)
            {
                error = true;
            }
            return error;
        }

        public bool CheckIfPilotExists()
        {
            bool exists = false;

            try
            {
                String query = $"SELECT COUNT(*) AS CANT FROM {MySQL.TABLE_PILOTS} WHERE Pilot_Name = '{PilotName}' AND Pilot_Surname = '{PilotSurname}'; ";
                int cant =  Convert.ToInt32(MySQL.MakeQuery(query).Rows[0]["CANT"]);

                if (cant > 0)
                    exists = true;
            }
            catch (Exception e)
            {
            }
            return exists;
        }
        public bool CheckIfNumberExists()
        {
            bool exists = false;

            try
            {
                String query = $"SELECT COUNT(*) AS CANT FROM {MySQL.TABLE_PILOTS} WHERE Pilot_Number = {PilotNumber}; ";
                int cant = Convert.ToInt32(MySQL.MakeQuery(query).Rows[0]["CANT"]);

                if (cant > 0)
                    exists = true;
            }
            catch (Exception e)
            {
            }
            return exists;
        }
    }
}
