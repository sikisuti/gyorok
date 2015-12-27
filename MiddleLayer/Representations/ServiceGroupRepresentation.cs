using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class ServiceGroupRepresentation : RepresentationBase
    {
        private long _deposit;
        public long deposit
        {
            get { return _deposit; }
            set
            {
                if (_deposit != value)
                {
                    _deposit = value;
                    RaisePropertyChanged("deposit");
                }
            }
        }
    }
}
