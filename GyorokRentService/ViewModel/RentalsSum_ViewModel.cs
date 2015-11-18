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

namespace GyorokRentService.ViewModel
{
    class RentalsSum_ViewModel : ViewModelBase
    {
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

        RentCalculates calc;
       
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
            DataProxy.Instance.UpdateRental(selectedRental);
            
            //rentToClose = SQLConnection.Execute.RentalsTable.Single(r => r.rentalID == selectedRent.rentalID);
            //rentToClose.isPaid = true;
            //rentToClose.rentalRealEnd = DateTime.Now;

            //rentedTool = SQLConnection.Execute.ToolsTable.Single(t => t.toolID == rentToClose.toolID);
            //rentedTool.toolStatusID = 1;
            
            SQLConnection.Execute.SaveDb();
            RentalSum tempRS = new RentalSum();
            //tempRS = selectedRent;
            //RefreshRentList(rentToClose.groupID);
            //selectedRent = rentGroupItems.Single(rg => rg.rentalID == tempRS.rentalID);
            AppMessages.RentalPaid.Send("");
        }
        public ICommand reOpenRental { get { return new RelayCommand(reOpenRentalExecute, () => true); } }
        void reOpenRentalExecute()
        {
            Rentals rentToOpen = new Rentals();
            //rentToOpen = SQLConnection.Execute.RentalsTable.Single(r => r.rentalID == selectedRent.rentalID);
            rentToOpen.isPaid = false;
            SQLConnection.Execute.SaveDb();
            RentalSum tempRS = new RentalSum();
            //tempRS = selectedRent;
            //RefreshRentList(rentToOpen.groupID);
            //selectedRent = rentGroupItems.Single(rg => rg.rentalID == tempRS.rentalID);
        }
        public ICommand saveDeposit { get { return new RelayCommand(saveDepositExecute, () => true); } }
        void saveDepositExecute()
        {
            DataProxy.Instance.UpdateRentalGroup(selectedRentalGroup);
        }
        public ICommand print { get { return new RelayCommand(printExecute, () => true); } }
        void printExecute()
        {
            // TODO: unique rental printing
            //new Print.PrintRent(RentDetails);
        }

        public RentalsSum_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                calc = new RentCalculates();   
            }
        }

        //public void RefreshRentList(long gl)
        //{
        //    long prevRentID = 0;

        //    if (selectedRent != null)
        //    {
        //        prevRentID = selectedRent.rentalID; 
        //    }

        //    RentDetails = SQLConnection.Execute.RentalSumView.Where(rs => rs.groupID == gl).ToList();
        //    rentGroupItems = new ObservableCollection<SQLConnectionLib.RentalSum>(RentDetails);

        //    if (rentGroupItems.Count > 0)
        //    {
        //        if (prevRentID != 0 && rentGroupItems.Select(rgi => rgi.rentalID).Contains(prevRentID))
        //        {
        //            selectedRent = SQLConnection.Execute.RentalSumView.Single(rs => rs.rentalID == prevRentID);
        //        }
        //        else
        //        {
        //            selectedRent = rentGroupItems[0]; 
        //        }
        //    }
            
        //}

        public void GroupSelectActions(long gID)
        {
            //Cities cCity;
            //long? cityID;

            //isExpanded = false;
            //selectedCustomerName = RentDetails[0].customerName;
            //cIDNumber = RentDetails[0].cIDNumber;
            //mothersName = RentDetails[0].mothersName;
            //cityID = RentDetails[0].customerCityID;
            //if (cityID != null)
            //{
            //    cCity = SQLConnection.Execute.CitiesTable.Single<Cities>(c => c.cityID == cityID);
            //    cAddress = cCity.postalCode + " " + cCity.city + ", " + RentDetails[0].customerAddress;               
            //}
            //else
            //{
            //    cAddress = RentDetails[0].customerAddress;
            //}
            //cPhone = RentDetails[0].customerPhone;
            //if (RentDetails[0].birthDate != null)
            //{
            //    birthDate = ((DateTime)RentDetails[0].birthDate).ToString("D");
            //}
            //else
            //{
            //    birthDate = string.Empty;
            //}
            //workPlace = RentDetails[0].workPlace;
            //contactName = RentDetails[0].contactName;
            //cComment = RentDetails[0].comment;
            //cProblems = RentDetails[0].problems;
            //deposit = (long)SQLConnection.Execute.RentalGroupsTable.Single(rg => rg.groupID == gID).deposit;
            //rentalStart = SQLConnection.Execute.RentalsTable.Where(r => r.groupID == gID).Select(r => r.rentalStart).First().ToString("D");
            //countTotalCost();
        }
        
        //private void CalcPartCost()
        //{
        //    try
        //    {
        //        partCost = calc.getRentCost(selectedRent.rentalStart, calc.getNowInHour(), (long)selectedRent.actualPrice, (float)selectedRent.discount);
        //        if (!isClean)
        //        {
        //            partCost += Properties.Settings.Default.CostOfClean;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        partCost = null;
        //    }
        //}

        public void countTotalCost()
        {
            //totalCost = 0;
            //foreach (var item in RentDetails)
            //{
            //    if (item.isPaid != true)
            //    {
            //        totalCost += calc.getRentCost(item.rentalStart, calc.getNowInHour(), (long)item.actualPrice, (float)item.discount);
            //    }
            //}
        }
    }
}
