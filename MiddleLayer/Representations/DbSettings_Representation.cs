using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class DbSettings_Representation : RepresentationBase
    {

        private string _ServerIP;
        public string ServerIP
        {
            get { return _ServerIP; }
            set
            {
                if (_ServerIP != value)
                {
                    _ServerIP = value;
                    RaisePropertyChanged("ServerIP");
                }
            }
        }

        private string _ServerInstanceName;
        public string ServerInstanceName
        {
            get { return _ServerInstanceName; }
            set
            {
                if (_ServerInstanceName != value)
                {
                    _ServerInstanceName = value;
                    RaisePropertyChanged("ServerInstanceName");
                }
            }
        }

        private string _primaryBackupPath;
        public string primaryBackupPath
        {
            get { return _primaryBackupPath; }
            set
            {
                if (_primaryBackupPath != value)
                {
                    _primaryBackupPath = value;
                    RaisePropertyChanged("primaryBackupPath");
                }
            }
        }

        private string _secondaryBackupPath;
        public string secondaryBackupPath
        {
            get { return _secondaryBackupPath; }
            set
            {
                if (_secondaryBackupPath != value)
                {
                    _secondaryBackupPath = value;
                    RaisePropertyChanged("secondaryBackupPath");
                }
            }
        }

        private string _InitialCatalog;
        public string InitialCatalog
        {
            get { return _InitialCatalog; }
            set
            {
                if (_InitialCatalog != value)
                {
                    _InitialCatalog = value;
                    RaisePropertyChanged("InitialCatalog");
                }
            }
        }

        private bool _IntegratedSecurity;
        public bool IntegratedSecurity
        {
            get { return _IntegratedSecurity; }
            set
            {
                if (_IntegratedSecurity != value)
                {
                    _IntegratedSecurity = value;
                    RaisePropertyChanged("IntegratedSecurity");
                }
            }
        }

        private bool _PersistSecurityInfo;
        public bool PersistSecurityInfo
        {
            get { return _PersistSecurityInfo; }
            set
            {
                if (_PersistSecurityInfo != value)
                {
                    _PersistSecurityInfo = value;
                    RaisePropertyChanged("PersistSecurityInfo");
                }
            }
        }

        private bool _MultipleActiveResultSets;
        public bool MultipleActiveResultSets
        {
            get { return _MultipleActiveResultSets; }
            set
            {
                if (_MultipleActiveResultSets != value)
                {
                    _MultipleActiveResultSets = value;
                    RaisePropertyChanged("MultipleActiveResultSets");
                }
            }
        }

        private string _ApplicationName;
        public string ApplicationName
        {
            get { return _ApplicationName; }
            set
            {
                if (_ApplicationName != value)
                {
                    _ApplicationName = value;
                    RaisePropertyChanged("ApplicationName");
                }
            }
        }

        private string _MetaData;
        public string MetaData
        {
            get { return _MetaData; }
            set
            {
                if (_MetaData != value)
                {
                    _MetaData = value;
                    RaisePropertyChanged("MetaData");
                }
            }
        }

        private string _Provider;
        public string Provider
        {
            get { return _Provider; }
            set
            {
                if (_Provider != value)
                {
                    _Provider = value;
                    RaisePropertyChanged("Provider");
                }
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }
    }
}
