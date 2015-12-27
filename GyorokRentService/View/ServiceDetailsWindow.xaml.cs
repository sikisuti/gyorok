using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using SQLConnectionLib;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for ServiceDetailsWindow.xaml
    /// </summary>
    public partial class ServiceDetailsWindow : Window, INotifyPropertyChanged
    {
        private Customers shownCustomer;
        public Customers ShownCustomer
        {
            get { return shownCustomer; }
            set
            {
                if (shownCustomer == value)
                {
                    return;
                }

                shownCustomer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ShownCustomer"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public ServiceDetailsWindow(long customerID)
        {
            InitializeComponent();

            //ShownCustomer = SQLConnection.Execute.CustomersTable.FirstOrDefault(c => c.customerID == customerID);
        }
    }
}
