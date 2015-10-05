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
using Common.Enumerations;

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

        public event EventHandler RentalGroupFinalizationRequested;
        public void OnRentalGroupFinalizationRequested(EventArgs e)
        {
            if (RentalGroupFinalizationRequested != null)
            {
                RentalGroupFinalizationRequested(newRentalGroup, e);
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

        private RentalRepresentation _newRental;
        public RentalRepresentation newRental
        {
            get { return _newRental; }
            set
            {
                if (_newRental != value)
                {
                    _newRental = value;
                    RaisePropertyChanged("newRental");
                }
            }
        }

        private RentalRepresentation _selectedRental;
        public RentalRepresentation selectedRental
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
        private List<PayTypeRepresentation> _payType;
        private PayTypeRepresentation _selectedPayTypes;
        //private bool _changeEnabled;        

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
        public List<PayTypeRepresentation> payType
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
        public PayTypeRepresentation selectedPayType
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
        //public bool changeEnabled
        //{
        //    get
        //    {
        //        return _changeEnabled;
        //    }

        //    set
        //    {
        //        if (_changeEnabled == value)
        //        {
        //            return;
        //        }

        //        _changeEnabled = value;
        //        RaisePropertyChanged("changeEnabled");
        //    }
        //}

        public ICommand addRent { get { return new RelayCommand(addRentExecute, () => true); } }
        void addRentExecute()
        {
            string errorMessage = string.Empty;

            if (newRental.customer != null)
            {
                if (!newRental.customer.IsValid)
                {
                    errorMessage += newRental.customer.Error;
                }
            }
            else
            {
                errorMessage += "Nincs ügyfél kiválasztva";
            }

            if (errorMessage != string.Empty) errorMessage += Environment.NewLine;

            if (newRental.tool != null)
            {
                if (!newRental.tool.IsValid)
                {
                    errorMessage += newRental.tool.Error;
                }
                if (newRental.tool.toolStatus.id != (long)ToolStatusEnum.AbleToRent)
                {
                    var r = DataProxy.Instance.GetLastRentalByToolId(newRental.tool.id);
                    errorMessage += "A gép már ki van kölcsönözve" + Environment.NewLine + "Lejárat: " + r.rentalEnd.ToLongDateString();
                }
            }
            else
            {
                errorMessage += "Nincs gép kiválasztva";
            }
            
            if (errorMessage != string.Empty)
            {
                MessageBox.Show(errorMessage, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AddNewRent();
        }
        public ICommand openRentalGroupWindow { get { return new RelayCommand(openRentalGroupWindowExecute, () => true); } }
        void openRentalGroupWindowExecute()
        {
            OnRentalGroupFinalizationRequested(EventArgs.Empty);
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
                newRental = new RentalRepresentation();
                selectedRental = new RentalRepresentation();

                selectedPayType = payType.SingleOrDefault(pt => pt.id == 1);
                newRental.payType = selectedPayType;
                
                //changeEnabled = true;
            }
        }

        //void t_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    CalcStartHour();
        //}

        //private DateTime CalcEndHour()
        //{
        //    //rentStart = DateTime.Today;
        //    //rentEnd = DateTime.Today + TimeSpan.FromDays(1);
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
        //    //rentEnd = rentStart + TimeSpan.FromDays(1);
        //}

        private void AddNewRent()
        {
            newRentalGroup.rentals.Add(newRental.Clone() as RentalRepresentation);
            newRental.tool = null;
            //changeEnabled = false;
            OnRentGroupChanged();
        }
    }
}
