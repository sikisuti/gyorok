using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class NewCity_ViewModel : ViewModelBase
    {
        private string _postalCode;
        public string postalCode
        {
            get
            {
                return _postalCode;
            }

            set
            {
                if (_postalCode == value)
                {
                    return;
                }

                _postalCode = value;
                RaisePropertyChanged("postalCode");
            }
        }
        private string _city;
        public string city
        {
            get
            {
                return _city;
            }

            set
            {
                if (_city == value)
                {
                    return;
                }

                _city = value;
                RaisePropertyChanged("city");
            }
        }

        public ICommand addNewCity { get { return new RelayCommand(addNewCityExecute, () => true); } }
        void addNewCityExecute()
        {
            if (postalCode == "" || city == "")
            {
                return;
            }

            Cities newCity = new Cities();

            newCity.postalCode = postalCode;
            newCity.city = city;
            SQLConnection.Execute.addCity(newCity);
            AppMessages.CityListModified.Send(newCity.cityID);
        }

        public NewCity_ViewModel()
        {

        }
    }
}
