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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GyorokRentService.ViewModel;
using MiddleLayer.Representations;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for CustomerSelector.xaml
    /// </summary>
    public partial class CustomerSelector : UserControl
    {
        public CustomerSelector()
        {
            InitializeComponent();
        }

        public CustomerSelector(CustomerType ct)
        {
            InitializeComponent();
            var viewModel = new CustomerSelector_ViewModel(ct);
            this.DataContext = viewModel;
            var customerPicker = new searchCustomer(searchCustomerType.searchCustomer);
            grdExpander.Children.Add(customerPicker);

            ((searchCustomer_ModelView)customerPicker.DataContext).CustomerSelected += (s, a) => { viewModel.CustomerSelected((CustomerBase_Representation)s); };
        }
    }
}
