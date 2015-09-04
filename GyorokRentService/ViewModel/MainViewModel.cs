using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GyorokRentService.View;
using System.Timers;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        Timer t;

        private int _customerColumnSpan;
        private int _toolRowSpan;
        private bool _rentChangeEnabled;
        private bool _serviceChangeEnabled;        

        public int customerColumnSpan
        {
            get
            {
                return _customerColumnSpan;
            }

            set
            {
                if (_customerColumnSpan == value)
                {
                    return;
                }

                _customerColumnSpan = value;
                RaisePropertyChanged("customerColumnSpan");
            }
        }
        public int toolRowSpan
        {
            get
            {
                return _toolRowSpan;
            }

            set
            {
                if (_toolRowSpan == value)
                {
                    return;
                }

                _toolRowSpan = value;
                RaisePropertyChanged("toolRowSpan");
            }
        }
        public bool rentChangeEnabled
        {
            get
            {
                return _rentChangeEnabled;
            }

            set
            {
                if (_rentChangeEnabled == value)
                {
                    return;
                }

                _rentChangeEnabled = value;
                RaisePropertyChanged("rentChangeEnabled");
            }
        }
        public bool serviceChangeEnabled
        {
            get
            {
                return _serviceChangeEnabled;
            }

            set
            {
                if (_serviceChangeEnabled == value)
                {
                    return;
                }

                _serviceChangeEnabled = value;
                RaisePropertyChanged("serviceChangeEnabled");
            }
        }

        public ICommand openSettingsWindow { get { return new RelayCommand(openSettingsWindowExecute, () => true); } }
        void openSettingsWindowExecute()
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
        }
        
        public MainViewModel()
        {
            // TODO: Concurency checking
            //t = new Timer(10000);
            //t.Start();
            //t.Elapsed += new ElapsedEventHandler(t_Elapsed);

            AppMessages.CustomerExpandChanged.Register(this, setCustomerColumnSpan);
            AppMessages.ToolExpandChanged.Register(this, setToolRowSpan);
            AppMessages.NewRentAdded.Register(this, r => rentChangeEnabled = false);
            AppMessages.RentGroupClosed.Register(this, s => rentChangeEnabled = true);
            AppMessages.NewWorksheetAdded.Register(this, ws => serviceChangeEnabled = false);
            AppMessages.ServiceGroupClosed.Register(this, sg => serviceChangeEnabled = true);
            rentChangeEnabled = true;
            serviceChangeEnabled = true;
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            t.Stop();
            if (!SQLConnection.Execute.IsCustomersConsistent())
            {
                AppMessages.CustomerListModified.Send("");
            }
            if (!SQLConnection.Execute.IsPartsConsistent())
            {
                AppMessages.PartsChanged.Send("");
            }
            if (!SQLConnection.Execute.IsRentalsConsistent())
            {
                AppMessages.RentalsChanged.Send("");
            }
            if (!SQLConnection.Execute.IsServiceWorksheetsConsistent())
            {
                AppMessages.ServiceWorksheetsChanged.Send("");
            }
            if (!SQLConnection.Execute.IsToolsConsistent())
            {
                AppMessages.ToolListModified.Send("");
            }
            t.Start();
        }

        private void setCustomerColumnSpan(bool s)
        {
            if (s)
            {
                customerColumnSpan = 2;
            }
            else
            {
                customerColumnSpan = 1;
            }
        }
        private void setToolRowSpan(bool s)
        {
            if (s)
            {
                toolRowSpan = 2;
            }
            else
            {
                toolRowSpan = 1;
            }
        }
    }
}