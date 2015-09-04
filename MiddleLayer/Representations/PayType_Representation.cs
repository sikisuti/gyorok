using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class PayType_Representation : RepresentationBase
    {
        private string _payTypeName;
        public string payTypeName
        {
            get { return _payTypeName; }
            set
            {
                if (_payTypeName != value)
                {
                    _payTypeName = value;
                    RaisePropertyChanged("payTypeName");
                }
            }
        }
    }
}
