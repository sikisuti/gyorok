using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using SQLConnectionLib;
using System.Configuration;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GyorokRentService.View;
using System.Windows.Forms;

namespace GyorokRentService.ViewModel
{
    class Settings_ViewModel : ViewModelBase
    {
        private string _serverIP;
        private string _userName;
        private string _password;
        private string _companyName;
        private string _companyAddress;
        private string _companyPhone1;
        private string _companyPhone2;
        private string _openTime;
        private long _costOfClean;
        private string _backupPath;
        private string _restorePath;
        private string _primaryBackupPath;
        private string _secondaryBackupPath; 
        
        public string serverIP
        {
            get
            {
                return _serverIP;
            }

            set
            {
                if (_serverIP == value)
                {
                    return;
                }



                _serverIP = value;
                RaisePropertyChanged("serverIP");
            }
        }
        public string userName
        {
            get
            {
                return _userName;
            }

            set
            {
                if (_userName == value)
                {
                    return;
                }

                _userName = value;
                RaisePropertyChanged("userName");
            }
        }
        public string password
        {
            get
            {
                return _password;
            }

            set
            {
                if (_password == value)
                {
                    return;
                }

                _password = value;
                RaisePropertyChanged("password");
            }
        }
        public string companyName
        {
            get
            {
                return _companyName;
            }

            set
            {
                if (_companyName == value)
                {
                    return;
                }

                _companyName = value;
                RaisePropertyChanged("companyName");
            }
        }
        public string companyAddress
        {
            get
            {
                return _companyAddress;
            }

            set
            {
                if (_companyAddress == value)
                {
                    return;
                }

                _companyAddress = value;
                RaisePropertyChanged("companyAddress");
            }
        }
        public string companyPhone1
        {
            get
            {
                return _companyPhone1;
            }

            set
            {
                if (_companyPhone1 == value)
                {
                    return;
                }

                _companyPhone1 = value;
                RaisePropertyChanged("companyPhone1");
            }
        }
        public string companyPhone2
        {
            get
            {
                return _companyPhone2;
            }

            set
            {
                if (_companyPhone2 == value)
                {
                    return;
                }

                _companyPhone2 = value;
                RaisePropertyChanged("companyPhone2");
            }
        }
        public string openTime
        {
            get
            {
                return _openTime;
            }

            set
            {
                if (_openTime == value)
                {
                    return;
                }

                _openTime = value;
                RaisePropertyChanged("openTime");
            }
        }
        public long costOfClean
        {
            get
            {
                return _costOfClean;
            }

            set
            {
                if (_costOfClean == value)
                {
                    return;
                }

                _costOfClean = value;
                RaisePropertyChanged("costOfClean");
            }
        }
        public string backupPath
        {
            get
            {
                return _backupPath;
            }

            set
            {
                if (_backupPath == value)
                {
                    return;
                }

                _backupPath = value;
                RaisePropertyChanged("backupPath");
            }
        }
        public string restorePath
        {
            get
            {
                return _restorePath;
            }

            set
            {
                if (_restorePath == value)
                {
                    return;
                }

                _restorePath = value;
                RaisePropertyChanged("restorePath");
            }
        }
        public string PrimaryBackupPath
        {
            get
            {
                return _primaryBackupPath;
            }

            set
            {
                if (_primaryBackupPath == value)
                {
                    return;
                }

                _primaryBackupPath = value;
                RaisePropertyChanged("PrimaryBackupPath");
            }
        }
        public string SecondaryBackupPath
        {
            get
            {
                return _secondaryBackupPath;
            }

            set
            {
                if (_secondaryBackupPath == value)
                {
                    return;
                }

                _secondaryBackupPath = value;
                RaisePropertyChanged("SecondaryBackupPath");
            }
        }

        public ICommand saveSettings { get { return new RelayCommand(saveSettingsExecute, () => true); } }
        void saveSettingsExecute()
        {
            Properties.Settings.Default.ServerIP = serverIP;
            Properties.Settings.Default.CompanyName = companyName;
            Properties.Settings.Default.CompanyAddress = companyAddress;
            Properties.Settings.Default.CompanyPhone1 = companyPhone1;
            Properties.Settings.Default.CompanyPhone2 = companyPhone2;
            Properties.Settings.Default.OpenTime = openTime;
            Properties.Settings.Default.CostOfClean = costOfClean;
            Properties.Settings.Default.PrimaryBackupPath = PrimaryBackupPath;
            Properties.Settings.Default.SecondaryBackupPath = SecondaryBackupPath;
            Properties.Settings.Default.Save();
            AppMessages.SettingsSaved.Send("");
        }
        public ICommand selectBackupPath { get { return new RelayCommand(selectBackupPathExecute, () => true); } }
        void selectBackupPathExecute()
        {
            FolderBrowserDialog saveDlg = new FolderBrowserDialog();
            saveDlg.ShowDialog();
            if (saveDlg.SelectedPath != null && saveDlg.SelectedPath != string.Empty)
            {
                backupPath = saveDlg.SelectedPath;
            }
        }
        public ICommand selectPrimaryBackupPath { get { return new RelayCommand(selectPrimaryBackupPathExecute, () => true); } }
        void selectPrimaryBackupPathExecute()
        {
            FolderBrowserDialog saveDlg = new FolderBrowserDialog();
            saveDlg.ShowDialog();
            if (saveDlg.SelectedPath != null && saveDlg.SelectedPath != string.Empty)
            {
                PrimaryBackupPath = saveDlg.SelectedPath;
            }
        }
        public ICommand selectSecondaryBackupPath { get { return new RelayCommand(selectSecondaryBackupPathExecute, () => true); } }
        void selectSecondaryBackupPathExecute()
        {
            FolderBrowserDialog saveDlg = new FolderBrowserDialog();
            saveDlg.ShowDialog();
            if (saveDlg.SelectedPath != null && saveDlg.SelectedPath != string.Empty)
            {
                SecondaryBackupPath = saveDlg.SelectedPath;
            }
        }
        public ICommand doBackup { get { return new RelayCommand(doBackupExecute, () => true); } }
        void doBackupExecute()
        {
            try
            {
                if (backupPath != null && backupPath != string.Empty)
                {
                    SQLConnection.Execute.DoBackup(backupPath + @"\"); 
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mentés nem sikerült!");
                return;
            }
            MessageBox.Show("Mentés kész!");
        }
        public ICommand selectRestorePath { get { return new RelayCommand(selectRestorePathExecute, () => true); } }
        void selectRestorePathExecute()
        {
            OpenFileDialog saveDlg = new OpenFileDialog();
            saveDlg.ShowDialog();
            if (saveDlg.FileName != null && saveDlg.FileName != string.Empty)
            {
                restorePath = saveDlg.FileName;
            }
        }
        public ICommand doRestore { get { return new RelayCommand(doRestoreExecute, () => true); } }
        void doRestoreExecute()
        {
            try
            {
                SQLConnection.Execute.DoRestore(restorePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Visszaállítás nem sikerült!");
                return;
            }
            MessageBox.Show("Visszaállítás kész!");
        }

        public Settings_ViewModel()
        {
            serverIP = Properties.Settings.Default.ServerIP;
            userName = Properties.Settings.Default.UserName;
            password = Properties.Settings.Default.Password;
            companyName = Properties.Settings.Default.CompanyName;
            companyAddress = Properties.Settings.Default.CompanyAddress;
            companyPhone1 = Properties.Settings.Default.CompanyPhone1;
            companyPhone2 = Properties.Settings.Default.CompanyPhone2;
            openTime = Properties.Settings.Default.OpenTime;
            costOfClean = Properties.Settings.Default.CostOfClean;
            PrimaryBackupPath = Properties.Settings.Default.PrimaryBackupPath;
            SecondaryBackupPath = Properties.Settings.Default.SecondaryBackupPath;
        }
    }
}
