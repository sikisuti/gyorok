﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class RentalGroup_Representation : RepresentationBase
    {
        private long? _deposit;
        public long? deposit
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

        private bool? _isOpen;
        public bool? isOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    RaisePropertyChanged("isOpen");
                }
            }
        }

        private string _comment;
        public string comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    RaisePropertyChanged("comment");
                }
            }
        }

        private ObservableCollection<Rental_Representation> _rentals;
        public ObservableCollection<Rental_Representation> rentals
        {
            get { return _rentals; }
            set
            {
                if (_rentals != value)
                {
                    _rentals = value;
                    RaisePropertyChanged("rentals");
                }
            }
        }

        public RentalGroup_Representation()
        {
            rentals = new ObservableCollection<Rental_Representation>();
        }
    }
}
