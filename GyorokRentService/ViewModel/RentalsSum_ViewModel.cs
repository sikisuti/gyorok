using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Globalization;
using System.Windows;
using SQLConnectionLib;
using System.Configuration;
using MiddleLayer.Representations;
using MiddleLayer;
using Common.Enumerations;
using NLog;

namespace GyorokRentService.ViewModel
{
    class RentalsSum_ViewModel : ViewModelBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private RentalGroup_Representation _selectedRentalGroup;
        public RentalGroup_Representation selectedRentalGroup
        {
            get { return _selectedRentalGroup; }
            set
            {
                if (_selectedRentalGroup != value)
                {
                    _selectedRentalGroup = value;
                    RaisePropertyChanged("selectedRentalGroup");
                }
            }
        }
       
        private RentalRepresentation _selectedRental;  
        public RentalRepresentation selectedRental
        {
            get
            {
                return _selectedRental;
            }

            set
            {
                if (_selectedRental == value)
                {
                    return;
                }

                _selectedRental = value;
                RaisePropertyChanged("selectedRental");
            }
        }
        
        public ICommand saveComments { get { return new RelayCommand(saveCommentsExecute, () => true); } }
        void saveCommentsExecute()
        {
            DataProxy.Instance.UpdateCustomer(selectedRentalGroup.rentals.FirstOrDefault().customer);
        }
        public ICommand saveRentComments { get { return new RelayCommand(saveRentCommentsExecute, () => true); } }
        void saveRentCommentsExecute()
        {
            DataProxy.Instance.UpdateRentalGroup(selectedRentalGroup);
        }
        public ICommand saveDiscount { get { return new RelayCommand(saveDiscountExecute, () => true); } }
        void saveDiscountExecute()
        {
            DataProxy.Instance.UpdateRental(selectedRental);
        }
        public ICommand closeRental { get { return new RelayCommand(closeRentalExecute, () => true); } }
        void closeRentalExecute()
        {
            selectedRental.rentalRealEnd = DateTime.Now;
            selectedRental.isPaid = true;
            selectedRental.tool.toolStatus.id = (long)ToolStatusEnum.AbleToRent;
            DataProxy.Instance.UpdateRental(selectedRental);
        }
        public ICommand reOpenRental { get { return new RelayCommand(reOpenRentalExecute, () => true); } }
        void reOpenRentalExecute()
        {
            if (selectedRental.tool.toolStatus.id == (long)ToolStatusEnum.AbleToRent)
            {
                selectedRental.rentalRealEnd = null;
                selectedRental.isPaid = false;
                selectedRental.tool.toolStatus.id = (long)ToolStatusEnum.Rented;
                DataProxy.Instance.UpdateRental(selectedRental); 
            }
            else
            {
                MessageBox.Show("Az eszköz már bérelt!", "Foglalt eszköz", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public ICommand saveDeposit { get { return new RelayCommand(saveDepositExecute, () => true); } }
        void saveDepositExecute()
        {
            DataProxy.Instance.UpdateRentalGroup(selectedRentalGroup);
        }
        public ICommand print { get { return new RelayCommand(printExecute, () => true); } }
        void printExecute()
        {
            try
            {
                new Print.PrintRent(selectedRentalGroup);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Printing error");
            }
        }

        public RentalsSum_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
            }
        }
    }
}
