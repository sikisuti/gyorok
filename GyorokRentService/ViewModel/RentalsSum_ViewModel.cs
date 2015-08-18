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

namespace GyorokRentService.ViewModel
{
    class RentalsSum_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;
        RentCalculates calc;
        List<RentalSum> RentDetails = new List<RentalSum>();
        Rentals rentToModify = new Rentals();

        private ObservableCollection<SQLConnectionLib.RentalSum> _rentGroupItems;
        private int _intervalDays;
        private int _intervalHours;        
        private SQLConnectionLib.RentalSum _selectedRent;
        private bool _isClean;
        private long _totalCost;
        private bool _isExpanded;
        private string _selectedCustomerName;
        private int _zExpander;
        private string _cIDNumber;
        private string _mothersName;
        private string _cAddress;
        private string _cPhone;
        private string _birthDate;
        private string _workPlace;
        private string _contactName;
        private string _cComment;
        private string _cProblems;
        private string _rentalStart;        
        private string _rentalEnd;
        private long? _partCost;
        private string _discount;
        private long _actualPrice;
        private long _deposit;
        private Visibility _commentSaveVisibility;
        private Visibility _rentalCloseVisibility;
        private Visibility _rentalReOpenVisibility;
        private bool _isNotPaid;
        private Visibility _saveDepositVisibility;
        private string _rComment;
        private Visibility _rentCommentSaveVisibility;        
        
        public ObservableCollection<SQLConnectionLib.RentalSum> rentGroupItems
        {
            get
            {
                return _rentGroupItems;
            }

            set
            {
                if (_rentGroupItems == value)
                {
                    return;
                }

                _rentGroupItems = value;
                RaisePropertyChanged("rentGroupItems");
            }
        }
        public int intervalDays
        {
            get
            {
                return _intervalDays;
            }

            set
            {
                if (_intervalDays == value)
                {
                    return;
                }

                _intervalDays = value;
                RaisePropertyChanged("intervalDays");
            }
        }
        public int intervalHours
        {
            get
            {
                return _intervalHours;
            }

            set
            {
                if (_intervalHours == value)
                {
                    return;
                }

                _intervalHours = value;
                RaisePropertyChanged("intervalHours");
            }
        }
        public SQLConnectionLib.RentalSum selectedRent
        {
            get
            {
                return _selectedRent;
            }

            set
            {
                if (value != null)
                {
                    rentalEnd = value.rentalEnd.ToString("D");
                    if (value.isPaid)
                    {
                        intervalDays = calc.getIntervalDays(value.rentalStart, (DateTime)value.rentalRealEnd);
                        intervalHours = calc.getIntervalHours(value.rentalStart, (DateTime)value.rentalRealEnd); 
                    }
                    else
                    {
                        intervalDays = calc.getIntervalDays(value.rentalStart, calc.getNowInHour());
                        intervalHours = calc.getIntervalHours(value.rentalStart, calc.getNowInHour()); 
                    }
                    if (intervalDays == 0 && intervalHours == 0)
                    {
                        intervalHours = 1;
                    }
                    discount = (value.discount * 100).ToString();
                    actualPrice = (long)value.actualPrice;
                    if (value.isPaid == true)
                    {
                        rentalCloseVisibility = Visibility.Hidden;
                        isNotPaid = false;
                    }
                    else
                    {
                        rentalCloseVisibility = Visibility.Visible;
                        isNotPaid = true;
                    }
                }
                else
                {
                    rentalEnd = string.Empty;
                    intervalDays = 0;
                    partCost = 0;
                    discount = string.Empty;
                    actualPrice = 0;
                }

                if (_selectedRent == value)
                {
                    return;
                }

                _selectedRent = value;
                RaisePropertyChanged("selectedRent");

                if (value != null)
                {
                    CalcPartCost(); 
                }
            }
        }
        public bool isClean
        {
            get
            {
                return _isClean;
            }

            set
            {
                if (_isClean == value)
                {
                    return;
                }

                try
                {
                    rentToModify = SQLConnection.Execute.RentalsTable.Single(rt => rt.rentalID == selectedRent.rentalID);
                    rentToModify.isClean = value;
                    SQLConnection.Execute.SaveDb();
                }
                catch (Exception)
                {
                    ;
                }

                _isClean = value;
                RaisePropertyChanged("isClean");

                CalcPartCost();
            }
        }
        public long totalCost
        {
            get
            {
                return _totalCost;
            }

            set
            {
                if (_totalCost == value)
                {
                    return;
                }

                _totalCost = value;
                RaisePropertyChanged("totalCost");
            }
        }
        public bool isExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                if (_isExpanded == value)
                {
                    return;
                }

                _isExpanded = value;
                RaisePropertyChanged("isExpanded");
            }
        }
        public string selectedCustomerName
        {
            get
            {
                return _selectedCustomerName;
            }

            set
            {
                if (_selectedCustomerName == value)
                {
                    return;
                }

                _selectedCustomerName = value;
                RaisePropertyChanged("selectedCustomerName");
            }
        }
        public int zExpander
        {
            get
            {
                return _zExpander;
            }

            set
            {
                if (_zExpander == value)
                {
                    return;
                }

                _zExpander = value;
                RaisePropertyChanged("zExpander");
            }
        }
        public string cIDNumber
        {
            get
            {
                return _cIDNumber;
            }

            set
            {
                if (_cIDNumber == value)
                {
                    return;
                }

                _cIDNumber = value;
                RaisePropertyChanged("cIDNumber");
            }
        }
        public string mothersName
        {
            get
            {
                return _mothersName;
            }

            set
            {
                if (_mothersName == value)
                {
                    return;
                }

                _mothersName = value;
                RaisePropertyChanged("mothersName");
            }
        }
        public string cAddress
        {
            get
            {
                return _cAddress;
            }

            set
            {
                if (_cAddress == value)
                {
                    return;
                }

                _cAddress = value;
                RaisePropertyChanged("cAddress");
            }
        }
        public string cPhone
        {
            get
            {
                return _cPhone;
            }

            set
            {
                if (_cPhone == value)
                {
                    return;
                }

                _cPhone = value;
                RaisePropertyChanged("cPhone");
            }
        }
        public string birthDate
        {
            get
            {
                return _birthDate;
            }

            set
            {
                if (_birthDate == value)
                {
                    return;
                }

                _birthDate = value;
                RaisePropertyChanged("birthDate");
            }
        }
        public string workPlace
        {
            get
            {
                return _workPlace;
            }

            set
            {
                if (_workPlace == value)
                {
                    return;
                }

                _workPlace = value;
                RaisePropertyChanged("workPlace");
            }
        }
        public string contactName
        {
            get
            {
                return _contactName;
            }

            set
            {
                if (_contactName == value)
                {
                    return;
                }

                _contactName = value;
                RaisePropertyChanged("contactName");
            }
        }
        public string cComment
        {
            get
            {
                return _cComment;
            }

            set
            {
                if (_cComment == value)
                {
                    return;
                }

                _cComment = value;
                RaisePropertyChanged("cComment");
            }
        }
        public string cProblems
        {
            get
            {
                return _cProblems;
            }

            set
            {
                if (_cProblems == value)
                {
                    return;
                }

                _cProblems = value;
                RaisePropertyChanged("cProblems");
            }
        }
        public string rentalStart
        {
            get
            {
                return _rentalStart;
            }

            set
            {
                if (_rentalStart == value)
                {
                    return;
                }

                _rentalStart = value;
                RaisePropertyChanged("rentalStart");
            }
        }
        public string rentalEnd
        {
            get
            {
                return _rentalEnd;
            }

            set
            {
                if (_rentalEnd == value)
                {
                    return;
                }

                _rentalEnd = value;
                RaisePropertyChanged("rentalEnd");
            }
        }
        public long? partCost
        {
            get
            {
                return _partCost;
            }

            set
            {
                if (_partCost == value)
                {
                    return;
                }

                _partCost = value;
                RaisePropertyChanged("partCost");
            }
        }
        public string discount
        {
            get
            {
                return _discount;
            }

            set
            {
                if (_discount == value)
                {
                    return;
                }

                _discount = value;
                RaisePropertyChanged("discount");
            }
        }
        public long actualPrice
        {
            get
            {
                return _actualPrice;
            }

            set
            {
                if (_actualPrice == value)
                {
                    return;
                }

                _actualPrice = value;
                RaisePropertyChanged("actualPrice");
            }
        }
        public long deposit
        {
            get
            {
                return _deposit;
            }

            set
            {
                if (_deposit == value)
                {
                    return;
                }

                _deposit = value;
                RaisePropertyChanged("deposit");
            }
        }
        public Visibility commentSaveVisibility
        {
            get
            {
                return _commentSaveVisibility;
            }

            set
            {
                if (_commentSaveVisibility == value)
                {
                    return;
                }

                _commentSaveVisibility = value;
                RaisePropertyChanged("commentSaveVisibility");
            }
        }
        public Visibility rentalCloseVisibility
        {
            get
            {
                return _rentalCloseVisibility;
            }

            set
            {
                if (value == Visibility.Hidden)
                {
                    rentalReOpenVisibility = Visibility.Visible;
                }
                else
                {
                    rentalReOpenVisibility = Visibility.Hidden;
                }

                if (_rentalCloseVisibility == value)
                {
                    return;
                }
                
                _rentalCloseVisibility = value;
                RaisePropertyChanged("rentalCloseVisibility");

            }
        }
        public Visibility rentalReOpenVisibility
        {
            get
            {
                return _rentalReOpenVisibility;
            }

            set
            {
                if (_rentalReOpenVisibility == value)
                {
                    return;
                }

                _rentalReOpenVisibility = value;
                RaisePropertyChanged("rentalReOpenVisibility");
            }
        }
        public bool isNotPaid
        {
            get
            {
                return _isNotPaid;
            }

            set
            {
                if (_isNotPaid == value)
                {
                    return;
                }

                _isNotPaid = value;
                RaisePropertyChanged("isNotPaid");
            }
        }
        public Visibility saveDepositVisibility
        {
            get
            {
                return _saveDepositVisibility;
            }

            set
            {
                if (_saveDepositVisibility == value)
                {
                    return;
                }

                _saveDepositVisibility = value;
                RaisePropertyChanged("saveDepositVisibility");
            }
        }
        public string rComment
        {
            get
            {
                return _rComment;
            }

            set
            {
                if (_rComment == value)
                {
                    return;
                }

                _rComment = value;
                RaisePropertyChanged("rComment");
            }
        }
        public Visibility rentCommentSaveVisibility
        {
            get
            {
                return _rentCommentSaveVisibility;
            }

            set
            {
                if (_rentCommentSaveVisibility == value)
                {
                    return;
                }

                _rentCommentSaveVisibility = value;
                RaisePropertyChanged("rentCommentSaveVisibility");
            }
        }

        public ICommand onExpand { get { return new RelayCommand(onExpandExecute, CanonExpandExecute); } }
        void onExpandExecute()
        {
            zExpander = 2;
            //AppMessages.RentExpandChanged.Send(true);
        }
        bool CanonExpandExecute()
        {
            return true;
        }
        public ICommand onCollapse { get { return new RelayCommand(onCollapseExecute, CanonCollapseExecute); } }
        void onCollapseExecute()
        {
            zExpander = 0;
            //AppMessages.RentExpandChanged.Send(false);
        }
        bool CanonCollapseExecute()
        {
            return true;
        }
        public ICommand mustSave { get { return new RelayCommand(mustSaveExecute, CanmustSaveExecute); } }
        void mustSaveExecute()
        {
            commentSaveVisibility = Visibility.Visible;
        }
        bool CanmustSaveExecute()
        {
            return commentSaveVisibility == Visibility.Hidden;
        }
        public ICommand rentMustSave { get { return new RelayCommand(rentMustSaveExecute, () => true); } }
        void rentMustSaveExecute()
        {
            rentCommentSaveVisibility = Visibility.Visible;
        }
        public ICommand saveComments { get { return new RelayCommand(saveCommentsExecute, () => true); } }
        void saveCommentsExecute()
        {
            long temp_cID;
            temp_cID = RentDetails[0].customerID;
            Customers modifiedCustomer = SQLConnection.Execute.CustomersTable.Single(c => c.customerID == temp_cID);
            modifiedCustomer.comment = cComment;
            modifiedCustomer.problems = cProblems;
            SQLConnection.Execute.SaveDb();

            commentSaveVisibility = Visibility.Hidden;
        }
        public ICommand saveRentComments { get { return new RelayCommand(saveRentCommentsExecute, () => true); } }
        void saveRentCommentsExecute()
        {
            long temp_rgID;
            temp_rgID = RentDetails[0].groupID;
            RentalGroups modifiedRentalgroup = SQLConnection.Execute.RentalGroupsTable.Single(rg => rg.groupID == temp_rgID);
            modifiedRentalgroup.comment = rComment;
            SQLConnection.Execute.SaveDb();

            rentCommentSaveVisibility = Visibility.Hidden;
        }
        public ICommand saveDiscount { get { return new RelayCommand(saveDiscountExecute, () => true); } }
        void saveDiscountExecute()
        {
            float tempDiscount;

            if (!float.TryParse(discount, out tempDiscount))
	        {
                tempDiscount = 0;
	        }
            rentToModify = SQLConnection.Execute.RentalsTable.Single(rt => rt.rentalID == selectedRent.rentalID);
            rentToModify.discount = tempDiscount / 100;
            SQLConnection.Execute.SaveDb();
            RefreshRentList(rentToModify.groupID);
            
            
        }
        public ICommand closeRental { get { return new RelayCommand(closeRentalExecute, () => true); } }
        void closeRentalExecute()
        {
            Rentals rentToClose = new Rentals();
            Tools rentedTool = new Tools();
            
            rentToClose = SQLConnection.Execute.RentalsTable.Single(r => r.rentalID == selectedRent.rentalID);
            rentToClose.isPaid = true;
            rentToClose.rentalRealEnd = DateTime.Now;

            rentedTool = SQLConnection.Execute.ToolsTable.Single(t => t.toolID == rentToClose.toolID);
            rentedTool.toolStatusID = 1;
            
            SQLConnection.Execute.SaveDb();
            RentalSum tempRS = new RentalSum();
            tempRS = selectedRent;
            RefreshRentList(rentToClose.groupID);
            selectedRent = rentGroupItems.Single(rg => rg.rentalID == tempRS.rentalID);
            AppMessages.RentalPaid.Send("");
        }
        public ICommand reOpenRental { get { return new RelayCommand(reOpenRentalExecute, () => true); } }
        void reOpenRentalExecute()
        {
            Rentals rentToOpen = new Rentals();
            rentToOpen = SQLConnection.Execute.RentalsTable.Single(r => r.rentalID == selectedRent.rentalID);
            rentToOpen.isPaid = false;
            SQLConnection.Execute.SaveDb();
            RentalSum tempRS = new RentalSum();
            tempRS = selectedRent;
            RefreshRentList(rentToOpen.groupID);
            selectedRent = rentGroupItems.Single(rg => rg.rentalID == tempRS.rentalID);
        }
        public ICommand saveDeposit { get { return new RelayCommand(saveDepositExecute, () => true); } }
        void saveDepositExecute()
        {
            long rgTemp;

            rgTemp = RentDetails[0].groupID;
            RentalGroups rg = SQLConnection.Execute.RentalGroupsTable.Single(rgp => rgp.groupID == rgTemp);
            rg.deposit = deposit;
            SQLConnection.Execute.SaveDb();
        }
        public ICommand depositChanged { get { return new RelayCommand(depositChangedExecute, () => true); } }
        void depositChangedExecute()
        {
            saveDepositVisibility = Visibility.Visible;
        }
        public ICommand print { get { return new RelayCommand(printExecute, () => true); } }
        void printExecute()
        {
            new Print.PrintRent(RentDetails);
        }

        public RentalsSum_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                //db = new dbGyorokEntities();
                calc = new RentCalculates();
                isClean = true;
                commentSaveVisibility = Visibility.Hidden;
                rentCommentSaveVisibility = Visibility.Hidden;
                saveDepositVisibility = Visibility.Hidden;
                rentalCloseVisibility = Visibility.Hidden;
                rentalReOpenVisibility = Visibility.Hidden;
                isNotPaid = false;

                AppMessages.GroupToSelect.Register(this, gr =>
                {
                    rComment = SQLConnection.Execute.RentalGroupsTable.Single(rg => rg.groupID == gr.GroupID).comment;
                    RefreshRentList(gr.GroupID);
                    GroupSelectActions(gr.GroupID);
                });            
            }
        }

        public void RefreshRentList(long gl)
        {
            long prevRentID = 0;

            if (selectedRent != null)
            {
                prevRentID = selectedRent.rentalID; 
            }

            RentDetails = SQLConnection.Execute.RentalSumView.Where(rs => rs.groupID == gl).ToList();
            rentGroupItems = new ObservableCollection<SQLConnectionLib.RentalSum>(RentDetails);

            if (rentGroupItems.Count > 0)
            {
                if (prevRentID != 0 && rentGroupItems.Select(rgi => rgi.rentalID).Contains(prevRentID))
                {
                    selectedRent = SQLConnection.Execute.RentalSumView.Single(rs => rs.rentalID == prevRentID);
                }
                else
                {
                    selectedRent = rentGroupItems[0]; 
                }
            }
            
        }

        public void GroupSelectActions(long gID)
        {
            Cities cCity;
            long? cityID;

            isExpanded = false;
            selectedCustomerName = RentDetails[0].customerName;
            cIDNumber = RentDetails[0].cIDNumber;
            mothersName = RentDetails[0].mothersName;
            cityID = RentDetails[0].customerCityID;
            if (cityID != null)
            {
                cCity = SQLConnection.Execute.CitiesTable.Single<Cities>(c => c.cityID == cityID);
                cAddress = cCity.postalCode + " " + cCity.city + ", " + RentDetails[0].customerAddress;               
            }
            else
            {
                cAddress = RentDetails[0].customerAddress;
            }
            cPhone = RentDetails[0].customerPhone;
            if (RentDetails[0].birthDate != null)
            {
                birthDate = ((DateTime)RentDetails[0].birthDate).ToString("D");
            }
            else
            {
                birthDate = string.Empty;
            }
            workPlace = RentDetails[0].workPlace;
            contactName = RentDetails[0].contactName;
            cComment = RentDetails[0].comment;
            cProblems = RentDetails[0].problems;
            deposit = (long)SQLConnection.Execute.RentalGroupsTable.Single(rg => rg.groupID == gID).deposit;
            rentalStart = SQLConnection.Execute.RentalsTable.Where(r => r.groupID == gID).Select(r => r.rentalStart).First().ToString("D");
            countTotalCost();
        }
        
        private void CalcPartCost()
        {
            long cleanCost;

            try
            {
                partCost = calc.getRentCost(selectedRent.rentalStart, calc.getNowInHour(), (long)selectedRent.actualPrice, (float)selectedRent.discount);
                if (!isClean)
                {
                    if (long.TryParse(ConfigurationManager.AppSettings["CostOfClean"], out cleanCost)) ;
                    {
                        partCost += cleanCost;
                    }
                }
            }
            catch (Exception)
            {

                partCost = null;
            }
        }

        public void countTotalCost()
        {
            totalCost = 0;
            foreach (var item in RentDetails)
            {
                if (item.isPaid != true)
                {
                    totalCost += calc.getRentCost(item.rentalStart, calc.getNowInHour(), (long)item.actualPrice, (float)item.discount);
                }
            }
        }
    }
}
