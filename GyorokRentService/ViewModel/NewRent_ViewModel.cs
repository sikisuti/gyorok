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
using MiddleLayer.Representations;
using MiddleLayer;

namespace GyorokRentService.ViewModel
{
    public class NewRent_ViewModel : ViewModelBase
    {
        public event EventHandler RentGroupChanged;
        public void OnRentGroupChanged()
        {
            if (RentGroupChanged != null)
            {
                RentGroupChanged(newRentalGroup, EventArgs.Empty);
            }
        }
        //Timer t;

        private RentalGroup_Representation _newRentalGroup;
        public RentalGroup_Representation newRentalGroup
        {
            get { return _newRentalGroup; }
            set
            {
                if (_newRentalGroup != value)
                {
                    _newRentalGroup = value;
                    RaisePropertyChanged("newRentalGroup");
                }
            }
        }

        private Rental_Representation _selectedRental;
        public Rental_Representation selectedRental
        {
            get { return _selectedRental; }
            set
            {
                if (_selectedRental != value)
                {
                    _selectedRental = value;
                    RaisePropertyChanged("selectedRental");
                }
            }
        }
        
        private DateTime _rentStart;
        private DateTime _rentEnd;
        private List<PayType_Representation> _payType;
        private PayType_Representation _selectedPayTypes;
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
        public List<PayType_Representation> payType
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
        public PayType_Representation selectedPayType
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
            NewRentGroupWindow rg = new NewRentGroupWindow(newRentalGroup);
            NewRentGroup_ViewModel newRetGroup_VM = rg.DataContext as NewRentGroup_ViewModel;
            newRetGroup_VM.rentGroupAccepted += (s, a) => 
            {
                newRentalGroup = new RentalGroup_Representation();
                changeEnabled = true;
            };
            rg.Show();
        }

        public NewRent_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                //t = new Timer(300000);
                //t.Start();
                //t.Elapsed += new ElapsedEventHandler(t_Elapsed);

                payType = DataProxy.Instance.GetPayTypes();
                newRentalGroup = new RentalGroup_Representation();
                selectedRental = new Rental_Representation();
                
                changeEnabled = true;
            }
        }

        //void t_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    CalcStartHour();
        //}

        private DateTime CalcEndHour()
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
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
            //rentEnd = rentStart + TimeSpan.FromDays(1);
        }

        private void AddNewRent()
        {
            if (CheckFillsOK())
            {
                newRentalGroup.rentals.Add(selectedRental.Clone() as Rental_Representation);
                selectedRental = new Rental_Representation();
                changeEnabled = false;
                OnRentGroupChanged();
            }
        }

        private bool CheckFillsOK()
        {
            if (selectedRental.customer != null || selectedRental.contact != null)
            {
                if (selectedRental.tool != null)
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

        public void customerSelected(CustomerBase_Representation customer)
        {
            selectedRental.customer = customer;
            selectedRental.discount = customer.defaultDiscount ?? 0;
        }

        public void toolSelected(Tool_Representation tool)
        {
            selectedRental.tool = tool;
        }
    }
}
