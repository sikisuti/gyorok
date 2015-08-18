using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Timers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GyorokRentService.View;
using System.Windows;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class NewRent_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;
        //Timer t;
        List<SQLConnectionLib.Rentals> newRentGroup = new List<SQLConnectionLib.Rentals>();
        long _customerID;
        long? _contactID;
        long _toolID;
        long _actualPrice;

        private DateTime _rentStart;
        private DateTime _rentEnd;
        private List<SQLConnectionLib.PayTypes> _payType;
        private SQLConnectionLib.PayTypes _selectedPayTypes;
        private float _discount;
        private int _rentCount;
        private bool _changeEnabled;        

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
        public DateTime rentEnd
        {
            get
            {
                return _rentEnd;
            }

            set
            {
                if (_rentEnd == value)
                {
                    return;
                }

                _rentEnd = value;
                RaisePropertyChanged("rentEnd");
            }
        }
        public List<SQLConnectionLib.PayTypes> payType
        {
            get
            {
                return _payType;
            }

            set
            {
                if (_payType == value)
                {
                    return;
                }

                _payType = value;
                RaisePropertyChanged("payType");
            }
        }
        public SQLConnectionLib.PayTypes selectedPayType
        {
            get
            {
                return _selectedPayTypes;
            }

            set
            {
                if (_selectedPayTypes == value)
                {
                    return;
                }

                _selectedPayTypes = value;
                RaisePropertyChanged("selectedPayType");
            }
        }
        public float discount
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
        public int rentCount
        {
            get
            {
                return _rentCount;
            }

            set
            {
                if (_rentCount == value)
                {
                    return;
                }

                _rentCount = value;
                RaisePropertyChanged("rentCount");
            }
        }
        public bool changeEnabled
        {
            get
            {
                return _changeEnabled;
            }

            set
            {
                if (_changeEnabled == value)
                {
                    return;
                }

                _changeEnabled = value;
                RaisePropertyChanged("changeEnabled");
            }
        }

        public ICommand addRent { get { return new RelayCommand(addRentExecute, () => true); } }
        void addRentExecute()
        {
            AddNewRent();
        }
        public ICommand openRentalGroupWindow { get { return new RelayCommand(openRentalGroupWindowExecute, () => true); } }
        void openRentalGroupWindowExecute()
        {
            NewRentGroupWindow rg = new NewRentGroupWindow(ref newRentGroup);
            rg.Show();
        }

        public NewRent_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                //db = new dbGyorokEntities();
                //t = new Timer(300000);
                //t.Start();
                //t.Elapsed += new ElapsedEventHandler(t_Elapsed);

                payType = SQLConnection.Execute.PayTypesTable.ToList();
                AddNewRentalGroup();
                CalcEndHour();
                discount = 0;
                rentCount = 0;

                AppMessages.CustomerToRent.Register(this, c => { 
                    _customerID = c.customerID;
                    discount = (float)c.defaultDiscount * 100;
                });
                AppMessages.ContactSelected.Register(this, c => _contactID = c.customerID);
                AppMessages.ToolToSelect.Register(this, tl =>
                {
                    _toolID = tl.toolID;
                    _actualPrice = tl.rentPrice;
                });
                AppMessages.ToolModified.Register(this, tl =>
                {
                    _toolID = tl.toolID;
                    _actualPrice = tl.rentPrice;
                });
                AppMessages.NewRentRemoved.Register(this, r => rentCount -= 1);
                AppMessages.RentGroupClosed.Register(this, s =>
                {
                    newRentGroup.Clear();
                    rentCount = 0;
                    changeEnabled = true;
                });
                AppMessages.NewRentAdded.Register(this, r => changeEnabled = false);
                changeEnabled = true;
            }
        }

        //void t_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    CalcStartHour();
        //}

        private void CalcEndHour()
        {
            //rentStart = DateTime.Today;
            //rentEnd = DateTime.Today + TimeSpan.FromDays(1);
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
            rentEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
            //rentEnd = rentStart + TimeSpan.FromDays(1);
        }

        private void AddNewRentalGroup()
        {  
            //db.Dispose();
            //db = new dbGyorokEntities();
            newRentGroup.Clear();
            selectedPayType = SQLConnection.Execute.PayTypesTable.Single(pt => pt.payTypeID == 1);
        }

        private void AddNewRent()
        {
            if (CheckFillsOK())
            {
                newRentGroup.Add(new Rentals());
                newRentGroup[newRentGroup.Count - 1].customerID = _customerID;
                if (_contactID != 0)
                {
                    newRentGroup[newRentGroup.Count - 1].contactID = _contactID;
                }
                newRentGroup[newRentGroup.Count - 1].toolID = _toolID;
                //newRentGroup[newRentGroup.Count - 1].rentalStart = rentStart;
                newRentGroup[newRentGroup.Count - 1].rentalEnd = rentEnd;
                newRentGroup[newRentGroup.Count - 1].actualPrice = _actualPrice;
                newRentGroup[newRentGroup.Count - 1].payTypeID = selectedPayType.payTypeID;
                newRentGroup[newRentGroup.Count - 1].discount = discount / 100;
                newRentGroup[newRentGroup.Count - 1].isClean = true;
                rentCount += 1;
                AppMessages.NewRentAdded.Send(newRentGroup[newRentGroup.Count - 1]);
            }
        }

        private bool CheckFillsOK()
        {
            if (_customerID != 0)
            {
                if (_toolID != 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Válassz cikket!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Válassz ügyfelet!");
                return false;
            }
        }
    }
}
