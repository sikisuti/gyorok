using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class ToolStatus_Representation : RepresentationBase
    {
        private string _statusName;
        public string statusName
        {
            get { return _statusName; }
            set
            {
                if (_statusName != value)
                {
                    _statusName = value;
                    RaisePropertyChanged("statusName");
                }
            }
        }
    }
}
