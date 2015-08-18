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
using GyorokRentService.ViewModel;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for SearchCustomerWindow.xaml
    /// </summary>
    public partial class SearchCustomerWindow : Window
    {
        public SearchCustomerWindow()
        {            
        }
        public SearchCustomerWindow(searchCustomerType displayType)
        {
            InitializeComponent();
            AppMessages.ContactPersonToSelect.Register(this, (c) => this.Close());
            AppMessages.CustomerToSelect.Register(this, (c) => this.Close());
            var UI = new searchCustomer(displayType);
            grdMain.Children.Add(UI);
        }
    }
}
