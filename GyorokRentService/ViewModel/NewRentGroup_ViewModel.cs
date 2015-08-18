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

namespace GyorokRentService.ViewModel
{
    class NewRentGroup_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db = new dbGyorokEntities();
        List<SQLConnectionLib.Rentals> newRents;

        private ObservableCollection<GroupRentals> _groupRentals = new ObservableCollection<GroupRentals>();
        private GroupRentals _selectedRent;
        private string _customerName;
        private DateTime _rentStart;
        private long _totalCost;
        private long _deposit;
        private string _comment;        
        
        public ObservableCollection<GroupRentals> groupRentals
        {
            get
            {
                return _groupRentals;
            }

            set
            {
                if (_groupRentals == value)
                {
                    return;
                }

                _groupRentals = value;
                RaisePropertyChanged("groupRentals");
            }
        }
        public GroupRentals selectedRent
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
        public string customerName
        {
            get
            {
                return _customerName;
            }

            set
            {
                if (_customerName == value)
                {
                    return;
                }

                _customerName = value;
                RaisePropertyChanged("customerName");
            }
        }
        public DateTime rentStart
        {
            get
            {
                return _rentStart;
            }

            set
            {
                if (_rentStart == value)
                {
                    return;
                }

                _rentStart = value;
                RaisePropertyChanged("rentStart");
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
        public string comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                _comment = value;
                RaisePropertyChanged("comment");
            }
        }

        public ICommand deleteRent { get { return new RelayCommand(deleteRentExecute, () => true); } }
        void deleteRentExecute()
        {
            Rentals delRent = new Rentals();

            delRent = newRents.Find(nr => nr.toolID == selectedRent.ToolID);
            newRents.Remove(delRent);
            AppMessages.NewRentRemoved.Send(delRent);
            totalCost -= (long)selectedRent.TotalPrice;
            deposit = 0;
            foreach (var item in newRents)
            {
                deposit += (long)SQLConnection.Execute.ToolsTable.Single(t => t.toolID == item.toolID).defaultDeposit;
            }
            groupRentals.Remove(selectedRent);
        }
        public ICommand cancelGroup { get { return new RelayCommand(cancelGroupExecute, () => true); } }
        void cancelGroupExecute()
        {
            AppMessages.RentGroupClosed.Send(newRents);            
        }
        public ICommand acceptGroup { get { return new RelayCommand(acceptGroupExecute, () => true); } }
        void acceptGroupExecute()
        {
            if (newRents.Count == 0)
            {
                return;
            }

            SQLConnectionLib.Customers c = new SQLConnectionLib.Customers();
            SQLConnectionLib.RentalGroups rg = new SQLConnectionLib.RentalGroups();
            SQLConnectionLib.Tools t = new SQLConnectionLib.Tools();
            long cIDtemp;

            rg.deposit = deposit;
            rg.comment = comment;
            SQLConnection.Execute.RentalGroupsTable.AddObject(rg);
            //db.SaveChanges();
            cIDtemp = newRents[0].customerID;
            c = SQLConnection.Execute.CustomersTable.Single(cust => cust.customerID == cIDtemp);
            CalcStartHour();
            foreach (Rentals item in newRents)
            {

                t = SQLConnection.Execute.ToolsTable.Single(tool => tool.toolID == item.toolID);
                t.toolStatusID = 3;
                t.rentCounter += 1;
                item.groupID = rg.groupID;
                item.rentalStart = rentStart;
                item.isPaid = false;
                SQLConnection.Execute.RentalsTable.AddObject(item);
                c.rentCounter += 1;
            }
            SQLConnection.Execute.SaveDb();
            new Print.PrintRent(rg.groupID);
            AppMessages.RentGroupClosed.Send(newRents);
        }

        public NewRentGroup_ViewModel()
        {
            
        }

        public NewRentGroup_ViewModel(ref List<Rentals> r)
        {
            try
            {
                newRents = r;
                RentCalculates calc = new RentCalculates();

                Rentals rTemp = new Rentals();
                rTemp = r[0];
                customerName = SQLConnection.Execute.CustomersTable.Single(c => c.customerID == rTemp.customerID).customerName;
                CalcStartHour();
                totalCost = 0;
                deposit = 0;
                foreach (Rentals item in r)
                {
                    groupRentals.Add(new GroupRentals());
                    groupRentals[groupRentals.Count - 1].ToolName = SQLConnection.Execute.ToolsTable.Single(t => t.toolID == item.toolID).toolName;
                    groupRentals[groupRentals.Count - 1].ToolNumber = SQLConnection.Execute.ToolsTable.Single(t => t.toolID == item.toolID).IDNumber;
                    groupRentals[groupRentals.Count - 1].ToolID = item.toolID;
                    groupRentals[groupRentals.Count - 1].RentEnd = item.rentalEnd;
                    groupRentals[groupRentals.Count - 1].IntervalDay = calc.getIntervalDays(rentStart, item.rentalEnd);
                    groupRentals[groupRentals.Count - 1].IntervalHour = calc.getIntervalHours(rentStart, item.rentalEnd);
                    groupRentals[groupRentals.Count - 1].ActualPrice = item.actualPrice;
                    groupRentals[groupRentals.Count - 1].Discount = item.discount;
                    groupRentals[groupRentals.Count - 1].TotalPrice = calc.getRentCost(rentStart, item.rentalEnd, (long)item.actualPrice, (float)item.discount);

                    totalCost += groupRentals[groupRentals.Count - 1].TotalPrice;
                    deposit += (long)SQLConnection.Execute.ToolsTable.Single(t => t.toolID == item.toolID).defaultDeposit;
                }
            }
            catch (Exception)
            {                
            }
        }
        
        private void CalcStartHour()
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
            rentStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
        }
    }

    public class GroupRentals
    {
        private string toolName;
        public string ToolName
        {
            get { return toolName; }
            set { toolName = value; }
        }

        private string toolNumber;
        public string ToolNumber
        {
            get { return toolNumber; }
            set { toolNumber = value; }
        }

        private long toolID;
        public long ToolID
        {
            get { return toolID; }
            set { toolID = value; }
        }        

        private DateTime rentEnd;
        public DateTime RentEnd
        {
            get { return rentEnd; }
            set { rentEnd = value; }
        }

        private int intervalDay;
        public int IntervalDay
        {
            get { return intervalDay; }
            set { intervalDay = value; }
        }

        private int intervalHour;
        public int IntervalHour
        {
            get { return intervalHour; }
            set { intervalHour = value; }
        }
        

        private long? actualPrice;
        public long? ActualPrice
        {
            get { return actualPrice; }
            set { actualPrice = value; }
        }

        private float? discount;
        public float? Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        private long totalPrice;
        public long TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        
    }
}
