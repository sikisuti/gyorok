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
using System.Threading.Tasks;
using MiddleLayer;

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

        private bool _IsBusy;
        public bool IsBusy  
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        private List<RentalGroup_Representation> allRentalGroups;

        private string _searchText;
        private ObservableCollection<RentalGroup_Representation> _rentalGroups;
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
        public ObservableCollection<RentalGroup_Representation> rentalGroups
        {
            get
            {
                return _rentalGroups;
            }

            set
            {
                if (_rentalGroups == value)
                {
                    return;
                }

                _rentalGroups = value;
                RaisePropertyChanged("rentalGroups");
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
                FilterList();
            }
        }

        public ICommand searchTextChanged { get { return new RelayCommand(searchTextChangedExecute, () => true); } }
        void searchTextChangedExecute()
        {
            FilterList();
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
                DataProxy.Instance.RentalChanged += (s, a) =>
                {
                    RefreshRentalList();
                };

                searchText = "";
                showOlds = false;
                RefreshRentalList();
            }
        }

        private void RefreshRentalList()
        {
            Task t = new Task(() =>
            {
                IsBusy = true;
                allRentalGroups = DataProxy.Instance.GetAllRentalGroups().Where(rg => rg.rentals.Count > 0).ToList();
                FilterList();
                IsBusy = false;
            });
            t.Start();            
        }

        private void FilterList()
        {
            rentalGroups = new ObservableCollection<RentalGroup_Representation>(
                            allRentalGroups.Where(rg => 
                                (showOlds ? true : rg.rentals.Any(r => !r.isPaid)) && 
                                rg.rentals.FirstOrDefault().customer.customerName.ToLower().Contains(searchText.ToLower()))
                                .ToList());  
        }
    }
}
