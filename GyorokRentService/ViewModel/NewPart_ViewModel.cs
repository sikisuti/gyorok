using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace GyorokRentService.ViewModel
{
    class NewPart_ViewModel : ViewModelBase
    {
        bool requestModify;
        SQLConnectionLib.Parts modifiablePart;
        //dbGyorokEntities db;

        private string _partName;
        private int _partQuantity;
        private long _partPrice;
        private string _partManufacturer;
        private string _partIDNumber;
        private bool _mustOrder;
        private bool _modifiable;        

        public string partName
        {
            get
            {
                return _partName;
            }

            set
            {
                if (_partName == value)
                {
                    return;
                }

                _partName = value;
                RaisePropertyChanged("partName");
            }
        }
        public int partQuantity
        {
            get
            {
                return _partQuantity;
            }

            set
            {
                if (_partQuantity == value)
                {
                    return;
                }

                _partQuantity = value;
                RaisePropertyChanged("partQuantity");
            }
        }
        public long partPrice
        {
            get
            {
                return _partPrice;
            }

            set
            {
                if (_partPrice == value)
                {
                    return;
                }

                _partPrice = value;
                RaisePropertyChanged("partPrice");
            }
        }
        public string partManufacturer
        {
            get
            {
                return _partManufacturer;
            }

            set
            {
                if (_partManufacturer == value)
                {
                    return;
                }

                _partManufacturer = value;
                RaisePropertyChanged("partManufacturer");
            }
        }
        public string partIDNumber
        {
            get
            {
                return _partIDNumber;
            }

            set
            {
                if (_partIDNumber == value)
                {
                    return;
                }

                _partIDNumber = value;
                RaisePropertyChanged("partIDNumber");
            }
        }
        public bool mustOrder
        {
            get
            {
                return _mustOrder;
            }

            set
            {
                if (_mustOrder == value)
                {
                    return;
                }

                _mustOrder = value;
                RaisePropertyChanged("mustOrder");
            }
        }
        public bool modifiable
        {
            get
            {
                return _modifiable;
            }

            set
            {
                if (_modifiable == value)
                {
                    return;
                }

                _modifiable = value;
                RaisePropertyChanged("modifiable");
            }
        }

        public ICommand addPart { get { return new RelayCommand(addPartExecute, () => true); } }
        void addPartExecute()
        {
            SQLConnectionLib.Parts p = new SQLConnectionLib.Parts();

            p.partName = partName;
            p.partQuantity = partQuantity;
            p.partPrice = partPrice;
            p.partManufacturer = partManufacturer;
            p.partIDNumber = partIDNumber;
            p.mustOrder = mustOrder;
            if (requestModify)
            {
                p.partID = modifiablePart.partID;
                AppMessages.PartModified.Send(p);
            }
            else
            {
                AppMessages.PartAdded.Send(p);
            }
            
        }

        public NewPart_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                //db = new dbGyorokEntities();
                partQuantity = 1;
                mustOrder = false;
                requestModify = false;
                modifiable = true;
            }
        }

        public NewPart_ViewModel(SQLConnectionLib.Parts changablePart, bool changable)
        {
            modifiable = changable;
            modifiablePart = changablePart;
            partName = changablePart.partName;
            partManufacturer = changablePart.partManufacturer;
            partIDNumber = changablePart.partIDNumber;
            partPrice = changablePart.partPrice;
            partQuantity = changablePart.partQuantity;
            mustOrder = changablePart.mustOrder;
            requestModify = true;
        }
    }
}
