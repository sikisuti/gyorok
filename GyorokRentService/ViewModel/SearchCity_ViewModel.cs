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
using System.Threading.Tasks;

namespace GyorokRentService.ViewModel
{
    class SearchCity_ViewModel : ViewModelBase
    {
        public event EventHandler citySelected;
        private void onCitySelected()
        {
            if (citySelected != null)
            {
                citySelected(selectedCity, null);
            }
        }

        public List<City_Representation> allCities;

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
            if (selectedCity != null)
            {
                MessageBoxResult result;

                result = MessageBox.Show("Biztosan törlöd?", "Város törlése...", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    DataProxy.Instance.DeleteCityById(selectedCity.id);
                    RefreshCityList();
                }
            }
        }
        public ICommand selectCity { get { return new RelayCommand(selectCityExecute, () => true); } }
        void selectCityExecute()
        {
            if (selectedCity != null)
            {
                onCitySelected();
                AppMessages.CityToSelect.Send(selectedCity); 
            }
        }

        public SearchCity_ViewModel()
        {
            if (!IsInDesignMode)
            {
                searchText = "";
                AppMessages.CityListModified.Register(this, cID => { RefreshCityList(); selectedCity.id = cID; });
            }
        }

        public void RefreshCityList()
        {
            Task t = Task.Factory.StartNew(() => 
            { 
                allCities = DataProxy.Instance.GetAllCities();
                Filtering();
            });
        }

        private void Filtering()
        {
            cityList = new ObservableCollection<City_Representation>(allCities.Where(c => c.city.ToLower().StartsWith(_searchText.ToLower()) && c.isDeleted == false).OrderBy(co => co.city));
        }
    }
}
