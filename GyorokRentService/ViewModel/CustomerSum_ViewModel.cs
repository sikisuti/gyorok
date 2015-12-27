using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class CustomerSum_ViewModel : ViewModelBase
    {
        private ObservableCollection<SQLConnectionLib.Customers> _customerList;
        private SQLConnectionLib.Customers _selectedCustomer;        

        public ObservableCollection<SQLConnectionLib.Customers> customerList
        {
            get
            {
                return _customerList;
            }

            set
            {
                if (_customerList == value)
                {
                    return;
                }

                _customerList = value;
                RaisePropertyChanged("customerList");
            }
        }
        public SQLConnectionLib.Customers selectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }

            set
            {
                if (_selectedCustomer == value)
                {
                    return;
                }

                _selectedCustomer = value;
                RaisePropertyChanged("selectedCustomer");
            }
        }

        public CustomerSum_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                UpdateScreen();
            }
        }

        private void UpdateScreen()
        {
            //customerList = new ObservableCollection<SQLConnectionLib.Customers>(SQLConnection.Execute.CustomersTable);
        }
    }
}
