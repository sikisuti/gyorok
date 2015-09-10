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
        public searchCustomer_ModelView customerPicker_VM { get; set; }
        Window customerPickerWindow;

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

            customerPicker_VM = customerPicker.DataContext as searchCustomer_ModelView;

            customerPickerWindow = new Window()
            {
                Content = customerPicker,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            customerPicker_VM.CustomerSelected += (s, a) => 
            {
                viewModel.CustomerSelected((CustomerBase_Representation)s);
                customerPickerWindow.Hide();
            };
            viewModel.customerPickerExpanded += (s, a) =>
            {
                if (customerPicker_VM.allCustomer == null && customerPicker_VM.IsBusy == false)
                {
                    customerPicker_VM.RefreshCustomerList();
                }
                customerPickerWindow.Show();
                //grdExpander.Children.Add(customerPicker);
            };
            viewModel.customerPickerCollapsed += (s, a) => { grdExpander.Children.Clear(); };
        }
    }
}
