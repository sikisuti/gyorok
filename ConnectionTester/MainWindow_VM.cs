using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SQLConnectionLib;
using System.Data.Entity;

namespace ConnectionTester
{
    class MainWindow_VM : ViewModelBase
    {
        private string dataSource;
        public string DataSource
        {
            get
            {
                return dataSource;
            }

            set
            {
                if (dataSource == value)
                {
                    return;
                }

                dataSource = value;
                RaisePropertyChanged("DataSource");
            }
        }
        private string initialCatalog;
        public string InitialCatalog
        {
            get
            {
                return initialCatalog;
            }

            set
            {
                if (initialCatalog == value)
                {
                    return;
                }

                initialCatalog = value;
                RaisePropertyChanged("InitialCatalog");
            }
        }
        private bool integratedSecurity;
        public bool IntegratedSecurity
        {
            get
            {
                return integratedSecurity;
            }

            set
            {
                if (integratedSecurity == value)
                {
                    return;
                }

                integratedSecurity = value;
                RaisePropertyChanged("IntegratedSecurity");
            }
        }
        private bool persistSecurityInfo;
        public bool PersistSecurityInfo
        {
            get
            {
                return persistSecurityInfo;
            }

            set
            {
                if (persistSecurityInfo == value)
                {
                    return;
                }

                persistSecurityInfo = value;
                RaisePropertyChanged("PersistSecurityInfo");
            }
        }
        private bool multipleActiveResultSets;
        public bool MultipleActiveResultSets
        {
            get
            {
                return multipleActiveResultSets;
            }

            set
            {
                if (multipleActiveResultSets == value)
                {
                    return;
                }

                multipleActiveResultSets = value;
                RaisePropertyChanged("MultipleActiveResultSets");
            }
        }
        private string applicationName;
        public string ApplicationName
        {
            get
            {
                return applicationName;
            }

            set
            {
                if (applicationName == value)
                {
                    return;
                }

                applicationName = value;
                RaisePropertyChanged("ApplicationName");
            }
        }
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                if (userName == value)
                {
                    return;
                }

                userName = value;
                RaisePropertyChanged("UserName");
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                if (password == value)
                {
                    return;
                }

                password = value;
                RaisePropertyChanged("Password");
            }
        }
        private string provider;
        public string Provider
        {
            get
            {
                return provider;
            }

            set
            {
                if (provider == value)
                {
                    return;
                }

                provider = value;
                RaisePropertyChanged("Provider");
            }
        }
        private string metaData;
        public string MetaData
        {
            get
            {
                return metaData;
            }

            set
            {
                if (metaData == value)
                {
                    return;
                }

                metaData = value;
                RaisePropertyChanged("MetaData");
            }
        }
        private string status;
        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                if (status == value)
                {
                    return;
                }

                status = value;
                RaisePropertyChanged("Status");
            }
        }

        public MainWindow_VM()
        {
        }

        public ICommand StartTest { get { return new RelayCommand(StartTestExecute, () => true); } }
        void StartTestExecute()
        {
            Status += DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + Environment.NewLine;

            SQLConnection datasource = new SQLConnection();

            List<ToolStatuses> cList = new List<ToolStatuses>();

            try
            {
                Status += "Try to open database ... ";
                Status += "Database opened successfully!" + Environment.NewLine;
            }
            catch (Exception e)
            {
                Status += "Database open failed!" + Environment.NewLine;
                Status += "Exception:" + Environment.NewLine;
                Status += e.Message + Environment.NewLine;
                if (e.InnerException != null)
                {
                    Status += "Inner exception:" + Environment.NewLine;
                    Status += e.InnerException.Message + Environment.NewLine;
                }
                Status += Environment.NewLine;
                return;
            }

            try
            {
                Status += "Try to retrieve tool statuses ... ";
                cList = datasource.ToolStatusesTable.ToList();

                Status += Environment.NewLine;
                foreach (ToolStatuses item in cList)
                {
                    Status += item.statusName + Environment.NewLine;
                }
            }
            catch (Exception e)
            {
                Status += "Customer data retrival failed!" + Environment.NewLine;
                Status += "Exception:" + Environment.NewLine;
                Status += e.Message + Environment.NewLine;
                if (e.InnerException != null)
                {
                    Status += "Inner exception:" + Environment.NewLine;
                    Status += e.InnerException.Message + Environment.NewLine;
                }
            }
            Status += Environment.NewLine;

        }

    }
}
