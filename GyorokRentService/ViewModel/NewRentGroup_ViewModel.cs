using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GyorokRentService.View;
using System.Collections.ObjectModel;
using SQLConnectionLib;
using MiddleLayer.Representations;
using MiddleLayer;

namespace GyorokRentService.ViewModel
{
    class NewRentGroup_ViewModel : ViewModelBase
    {
        public event EventHandler rentGroupAccepted;
        public void OnRentGroupAccepted()
        {
            if (rentGroupAccepted != null)
            {
                rentGroupAccepted(null, null);
            }
        }

        private RentalGroup_Representation _rentalGroup;
        private Rental_Representation _selectedRent;   
        
        public RentalGroup_Representation rentalGroup
        {
            get
            {
                return _rentalGroup;
            }

            set
            {
                if (_rentalGroup == value)
                {
                    return;
                }

                _rentalGroup = value;
                RaisePropertyChanged("rentalGroup");
            }
        }
        public Rental_Representation selectedRent
        {
            get
            {
                return _selectedRent;
            }

            set
            {
                if (_selectedRent == value)
                {
                    return;
                }

                _selectedRent = value;
                RaisePropertyChanged("selectedRent");
            }
        }

        public ICommand deleteRent { get { return new RelayCommand(deleteRentExecute, () => true); } }
        void deleteRentExecute()
        {
            DataProxy.Instance.DeleteRentalById(selectedRent.id);
            AppMessages.NewRentRemoved.Send(selectedRent);
            rentalGroup.rentals.Remove(selectedRent);
        }
        public ICommand cancelGroup { get { return new RelayCommand(cancelGroupExecute, () => true); } }
        void cancelGroupExecute()
        {
            AppMessages.RentGroupClosed.Send(null);            
        }
        public ICommand acceptGroup { get { return new RelayCommand(acceptGroupExecute, () => true); } }
        void acceptGroupExecute()
        {
            if (rentalGroup.rentals.Count == 0)
            {
                return;
            }

            RentalGroup_Representation rentalGroupToAdd = new RentalGroup_Representation() {  };
            foreach (Rental_Representation rental in rentalGroup.rentals)
            {                
                rental.tool.toolStatus.id = 3;
                rental.tool.rentCounter += 1;
                rental.rentalStart = CalcStartHour();
                rental.isPaid = false;
                rental.customer.rentCounter += 1;
                rentalGroupToAdd.rentals.Add(rental);
            }
            DataProxy.Instance.AddRentalGroup(rentalGroupToAdd);
            new Print.PrintRent(rentalGroupToAdd.id);
            OnRentGroupAccepted();
            AppMessages.RentGroupClosed.Send(null);
        }

        public NewRentGroup_ViewModel()
        {
            
        }

        public NewRentGroup_ViewModel(RentalGroup_Representation r)
        {
            rentalGroup = r;
        }
        
        private DateTime CalcStartHour()
        {
            int hour;
            if (DateTime.Now.Minute > 30)
            {
                if (DateTime.Now.Hour == 23)
                {
                    hour = 0;
                }
                else
                {
                    hour = DateTime.Now.Hour + 1;
                }
            }
            else
            {
                hour = DateTime.Now.Hour;
            }
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
        }
    }
}
