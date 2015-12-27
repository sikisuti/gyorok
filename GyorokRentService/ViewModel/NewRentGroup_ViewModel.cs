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
using NLog;
using Common.Enumerations;

namespace GyorokRentService.ViewModel
{
    class NewRentGroup_ViewModel : ViewModelBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public event EventHandler rentGroupAccepted;
        public void OnRentGroupAccepted()
        {
            if (rentGroupAccepted != null)
            {
                rentGroupAccepted(null, null);
            }
        }

        public event EventHandler rentGroupCancelled;
        public void OnRentGroupCancelled(EventArgs e)
        {
            if (rentGroupCancelled != null)
            {
                rentGroupCancelled(null, e);
            }
        }

        private RentalGroup_Representation _rentalGroup;
        private RentalRepresentation _selectedRent;   
        
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
        public RentalRepresentation selectedRent
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
                RaisePropertyChanged("deleteRent");
            }
        }

        public ICommand deleteRent { get { return new RelayCommand(deleteRentExecute, canDeleteRentExecute); } }
        void deleteRentExecute()
        {
            rentalGroup.RemoveRental(selectedRent);
            selectedRent = null;
            rentalGroup.ResetDeposit();
        }
        bool canDeleteRentExecute()
        {
            return selectedRent != null;
        }
        public ICommand cancelGroup { get { return new RelayCommand(cancelGroupExecute, () => true); } }
        void cancelGroupExecute()
        {
            OnRentGroupCancelled(EventArgs.Empty);          
        }
        public ICommand acceptGroup { get { return new RelayCommand(acceptGroupExecute, () => true); } }
        void acceptGroupExecute()
        {
            if (rentalGroup.rentals.Count == 0) return;
            
            foreach (RentalRepresentation rental in rentalGroup.rentals)
            {                
                rental.tool.toolStatus.id = (long)ToolStatusEnum.Rented;
                rental.actualPrice = rental.tool.rentPrice;
                rental.tool.rentCounter += 1;
                rental.rentalStart = DateTime.Now;
                rental.isPaid = false;
                rental.customer.rentCounter += 1;
                rental.rentalStart = DateTime.Now;
                switch (rental.rentTerm)
                {
                    case Common.Enumerations.RentTermEnum.OneHour:
                        rental.rentalEnd = rental.rentalStart.AddHours(1);
                        break;
                    case Common.Enumerations.RentTermEnum.HalfDay:
                        rental.rentalEnd = rental.rentalStart.AddHours((int)Math.Round((double)(Properties.Settings.Default.HoursPerDay) / 2, 0));
                        break;
                    case Common.Enumerations.RentTermEnum.OneDay:
                        rental.rentalEnd = rental.rentalStart.AddDays(1);
                        break;
                    case Common.Enumerations.RentTermEnum.ThreeDays:
                        rental.rentalEnd = rental.rentalStart.AddDays(3);
                        break;
                    case Common.Enumerations.RentTermEnum.OneWeek:
                        rental.rentalEnd = rental.rentalStart.AddDays(7);
                        break;
                    case Common.Enumerations.RentTermEnum.Custom:
                        break;
                    default:
                        break;
                }
            }
            rentalGroup.id = DataProxy.Instance.AddRentalGroup(rentalGroup);
            try
            {
                new Print.PrintRent(rentalGroup);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Printing error");
            }
            OnRentGroupAccepted();
        }

        public NewRentGroup_ViewModel()
        {
            
        }

        public NewRentGroup_ViewModel(RentalGroup_Representation r)
        {
            rentalGroup = r;
            r.ResetDeposit();
            foreach (RentalRepresentation rental in rentalGroup.rentals)
            {
                rental.rentalStart = DateTime.Today;
            }
        }
        
        //private DateTime CalcStartHour()
        //{
        //    int hour;
        //    if (DateTime.Now.Minute > 30)
        //    {
        //        if (DateTime.Now.Hour == 23)
        //        {
        //            hour = 0;
        //        }
        //        else
        //        {
        //            hour = DateTime.Now.Hour + 1;
        //        }
        //    }
        //    else
        //    {
        //        hour = DateTime.Now.Hour;
        //    }
        //    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
        //}
    }
}
