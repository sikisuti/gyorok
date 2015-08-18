using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SQLConnectionLib;
using System.Collections.ObjectModel;
using System.Windows;

namespace GyorokRentService.ViewModel
{
    class SearchCity_ViewModel : ViewModelBase
    {
        private ObservableCollection<Cities> _cityList;
        public ObservableCollection<Cities> cityList
        {
            get
            {
                return _cityList;
            }

            set
            {
                if (_cityList == value)
                {
                    return;
                }

                _cityList = value;
                RaisePropertyChanged("cityList");
            }
        }
        private long _selectedCityID;
        public long selectedCityID
        {
            get
            {
                return _selectedCityID;
            }

            set
            {
                if (_selectedCityID == value)
                {
                    return;
                }

                _selectedCityID = value;
                RaisePropertyChanged("selectedCityID");
            }
        }
        private string _searchText;
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

        public ICommand searchTextChanged { get { return new RelayCommand(searchTextChangedExecute, () => true); } }
        void searchTextChangedExecute()
        {
            RefreshCityList();
        }
        public ICommand openAddNewCity { get { return new RelayCommand(openAddNewCityExecute, () => true); } }
        void openAddNewCityExecute()
        {
            View.NewCityWindow nc = new View.NewCityWindow();
            nc.ShowDialog();
        }
        public ICommand delCity { get { return new RelayCommand(delCityExecute, () => true); } }
        void delCityExecute()
        {
            if (selectedCityID != 0)
            {
                MessageBoxResult result;

                result = MessageBox.Show("Biztosan törlöd?", "Város törlése...", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    SQLConnection.Execute.delCity(selectedCityID);
                    RefreshCityList();
                }
            }
        }
        public ICommand selectCity { get { return new RelayCommand(selectCityExecute, () => true); } }
        void selectCityExecute()
        {
            if (selectedCityID != 0)
            {
                Cities actCity = new Cities();

                actCity = SQLConnection.Execute.CitiesTable.Single<Cities>(c => c.cityID == selectedCityID);
                AppMessages.CityToSelect.Send(actCity); 
            }
        }

        public SearchCity_ViewModel()
        {
            if (!IsInDesignMode)
            {
                searchText = "";
                RefreshCityList();
                AppMessages.CityListModified.Register(this, cID => { RefreshCityList(); selectedCityID = cID; });
            }
        }

        private void RefreshCityList()
        {
            cityList = new ObservableCollection<Cities>(SQLConnection.Execute.CitiesTable.Where<Cities>(c => c.city.StartsWith(_searchText) && c.isDeleted == false).OrderBy<Cities, string>(co => co.city).ToList<Cities>());            
        }
    }
}
