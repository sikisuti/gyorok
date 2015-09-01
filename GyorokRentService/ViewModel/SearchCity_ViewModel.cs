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
using MiddleLayer.Representations;
using MiddleLayer;

namespace GyorokRentService.ViewModel
{
    class SearchCity_ViewModel : ViewModelBase
    {
        public event EventHandler citySelected;
        private void onCitySelected()
        {
            if (citySelected != null)
            {
                citySelected(selectCity, null);
            }
        }

        private ObservableCollection<City_Representation> _cityList;
        public ObservableCollection<City_Representation> cityList
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

        private City_Representation _selectedCity;
        public City_Representation selectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    RaisePropertyChanged("selectedCity");
                }
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
            if (selectedCity != null)
            {
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
            ObservableCollection<City_Representation> cities = DataProxy.Instance.GetAllCities();
            cityList = new ObservableCollection<City_Representation>(cities.Where(c => c.city.StartsWith(_searchText) && c.isDeleted == false).OrderBy(co => co.city));            
        }
    }
}
