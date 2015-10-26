using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SQLConnectionLib;
using MiddleLayer.Representations;

namespace GyorokRentService.ViewModel
{
    class SearchRental_ViewModel : ViewModelBase
    {
        public event EventHandler RentGroupSelected;
        public void OnRentGroupSelected(EventArgs e)
        {
            if (RentGroupSelected != null)
            {
                RentGroupSelected(selectedGroup, e);
            }
        }

        private string _searchText;
        private ObservableCollection<RentalGroup_Representation> _groupSum;
        private RentalGroup_Representation _selectedGroup;
        private bool _showOlds;
        
        public string searchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText == value)
                {
                    return;
                }

                _searchText = value;
                RaisePropertyChanged("searchText");
            }
        }
        public ObservableCollection<RentalGroup_Representation> groupSum
        {
            get
            {
                return _groupSum;
            }

            set
            {
                if (_groupSum == value)
                {
                    return;
                }

                _groupSum = value;
                RaisePropertyChanged("groupSum");
            }
        }
        public RentalGroup_Representation selectedGroup
        {
            get
            {
                return _selectedGroup;
            }

            set
            {
                if (_selectedGroup == value)
                {
                    return;
                }

                _selectedGroup = value;
                RaisePropertyChanged("selectedGroup");
            }
        }
        public bool showOlds
        {
            get
            {
                return _showOlds;
            }

            set
            {
                if (_showOlds == value)
                {
                    return;
                }

                _showOlds = value;
                RaisePropertyChanged("showOlds");
                RefreshRentalList();
            }
        }

        public ICommand searchTextChanged { get { return new RelayCommand(searchTextChangedExecute, () => true); } }
        void searchTextChangedExecute()
        {
            RefreshRentalList();
        }
        public ICommand customerSelected { get { return new RelayCommand(customerSelectedExecute, CancustomerSelectedExecute); } }
        void customerSelectedExecute()
        {
            OnRentGroupSelected(EventArgs.Empty);
        }
        bool CancustomerSelectedExecute()
        {
            return true;
        }

        public SearchRental_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                searchText = "";
                showOlds = false;
                RefreshRentalList();

                AppMessages.RentGroupClosed.Register(this, gs => RefreshRentalList());
                AppMessages.RentalPaid.Register(this, s => RefreshRentalList());
                AppMessages.RentalsChanged.Register(this, rc => RefreshRentalList());
            }
        }

        private void RefreshRentalList()
        {
            if (!showOlds)
            {
                groupSum = new ObservableCollection<GroupList>(
                                (from r in SQLConnection.Execute.RentalsTable
                                 join c in SQLConnection.Execute.CustomersTable on r.customerID equals c.customerID
                                 where c.customerName.StartsWith(searchText) && !(bool)r.isPaid
                                 select new GroupList { GroupID = r.groupID, CustomerName = c.customerName, RentStart = r.rentalStart })
                                 .Distinct().OrderBy(g => g.RentStart).ToList());  
            }
            else
            {
                groupSum = new ObservableCollection<GroupList>(
                                (from r in SQLConnection.Execute.RentalsTable
                                 join c in SQLConnection.Execute.CustomersTable on r.customerID equals c.customerID
                                 where c.customerName.StartsWith(searchText)
                                 select new GroupList { GroupID = r.groupID, CustomerName = c.customerName, RentStart = r.rentalStart })
                                 .Distinct().OrderBy(g => g.RentStart).ToList()); 
            }
        }
    }
}
