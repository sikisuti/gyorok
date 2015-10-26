using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer.Representations
{
    public class CityRepresentation : RepresentationBase
    {
        private string _postalCode;
        public string postalCode
        {
            get { return _postalCode; }
            set
            {
                if (_postalCode != value)
                {
                    _postalCode = value;
                    RaisePropertyChanged("postalCode");
                } }
        }

        private string _city;
        public string city
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    RaisePropertyChanged("city");
                } }
        }

        public override string ToString()
        {
            return postalCode + " " + city;
        }
    }
}
