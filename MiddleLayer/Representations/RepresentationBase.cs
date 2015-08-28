using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer.Representations
{
    public class RepresentationBase : ViewModelBase
    {
        private long _id;
        public long id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged("readOnlyMode");
                }
            }
        }

        private bool _isDeleted;
        public bool isDeleted
        {
            get { return _isDeleted; }
            set
            {
                if (_isDeleted != value)
                {
                    _isDeleted = value;
                    RaisePropertyChanged("isDeleted");
                }
            }
        }

        private long _rowVersion;
        public long rowVersion
        {
            get { return _rowVersion; }
            set
            {
                if (_rowVersion != value)
                {
                    _rowVersion = value;
                    RaisePropertyChanged("rowVersion");
                }
            }
        }
    }
    }
}
