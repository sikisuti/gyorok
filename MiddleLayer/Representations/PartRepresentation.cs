using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class PartRepresentation : RepresentationBase
    {
        private WorksheetRepresentation _worksheet;
        public WorksheetRepresentation worksheet
        {
            get { return _worksheet; }
            set
            {
                if (_worksheet != value)
                {
                    _worksheet = value;
                    RaisePropertyChanged("worksheet");
                }
            }
        }

        private string _partName;
        public string partName
        {
            get { return _partName; }
            set
            {
                if (_partName != value)
                {
                    _partName = value;
                    RaisePropertyChanged("partName");
                }
            }
        }

        private string _partManufacturer;
        public string partManufacturer
        {
            get { return _partManufacturer; }
            set
            {
                if (_partManufacturer != value)
                {
                    _partManufacturer = value;
                    RaisePropertyChanged("partManufacturer");
                }
            }
        }

        private string _partIDNumber;
        public string partIDNumber
        {
            get { return _partIDNumber; }
            set
            {
                if (_partIDNumber != value)
                {
                    _partIDNumber = value;
                    RaisePropertyChanged("partIDNumber");
                }
            }
        }

        private long _partPrice;
        public long partPrice
        {
            get { return _partPrice; }
            set
            {
                if (_partPrice != value)
                {
                    _partPrice = value;
                    RaisePropertyChanged("partPrice");
                }
            }
        }

        private int _partQuantity;
        public int partQuantity
        {
            get { return _partQuantity; }
            set
            {
                if (_partQuantity != value)
                {
                    _partQuantity = value;
                    RaisePropertyChanged("partQuantity");
                }
            }
        }

        private bool _mustOrder;
        public bool mustOrder
        {
            get { return _mustOrder; }
            set
            {
                if (_mustOrder != value)
                {
                    _mustOrder = value;
                    RaisePropertyChanged("mustOrder");
                }
            }
        }
    }
}
