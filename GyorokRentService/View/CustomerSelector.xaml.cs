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
using MiddleLayer;
using Common.Enumerations;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for CustomerSelector.xaml
    /// </summary>
    public partial class CustomerSelector : UserControl
    {
        CustomerSelector_ViewModel viewModel;

        Window customerPickerWindow;
        searchCustomer customerPicker;
        public searchCustomer_ModelView customerPicker_VM { get; set; }

        Window contactPickerWindow;
        searchCustomer contactPicker;
        public searchCustomer_ModelView contactPicker_VM { get; set; }

        Window cityPickerWindow;
        SearchCity_ViewModel cityPicker_VM;

        public CustomerSelector()
        {
            InitializeComponent();
        }

        public CustomerSelector(CustomerType ct)
        {
            InitializeComponent();
            viewModel = new CustomerSelector_ViewModel(ct);
            this.DataContext = viewModel;

            BuildSearchCustomerWindow();
                
            viewModel.customerPickerExpanded += (s, a) =>
            {
                if (customerPickerWindow == null || !customerPickerWindow.IsLoaded)
                {
                    BuildSearchCustomerWindow();
                }
                customerPickerWindow.Show();
                expCustomer.IsExpanded = false;
            };

            viewModel.contactRequest += (s, a) =>
            {
                if (contactPickerWindow == null || !contactPickerWindow.IsLoaded)
                {
                    BuildSearchContactWindow();
                }
                contactPickerWindow.Show();
            };

            viewModel.cityRequest += (s, a) => 
            {
                if (cityPickerWindow == null || !cityPickerWindow.IsLoaded)
                {
                    BuildCityChooserWindow();
                }
                cityPickerWindow.Show();
            };
        }

        private void BuildSearchCustomerWindow()
        {
            if (customerPicker == null)
            {
                customerPicker = new searchCustomer(searchCustomerTypeEnum.Customer);
                customerPicker_VM = customerPicker.DataContext as searchCustomer_ModelView; 
            }
            customerPickerWindow = new Window()
            {
                Title = "Ügyfél választó",
                Content = customerPicker,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            customerPicker_VM.CustomerSelected += (s, a) =>
            {
                viewModel.selectedCustomer = (CustomerBase_Representation)s;
                customerPickerWindow.Hide();
            };
        }

        private void BuildSearchContactWindow()
        {
            contactPicker = new searchCustomer(searchCustomerTypeEnum.Contact);
            contactPicker_VM = contactPicker.DataContext as searchCustomer_ModelView;
            contactPickerWindow = new Window()
            {
                Title = "Kapcsolattartó választó",
                Content = contactPicker,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            contactPicker_VM.CustomerSelected += (s, a) =>
            {
                DataProxy.Instance.AddContact(viewModel.selectedCustomer, (CustomerBase_Representation)s);
                viewModel.selectedCustomer.contacts.Add((CustomerBase_Representation)s);
                contactPickerWindow.Hide();
            };
        }

        private void BuildCityChooserWindow()
        {
            cityPickerWindow = new SearchCity();
            cityPicker_VM = cityPickerWindow.DataContext as SearchCity_ViewModel;

            cityPicker_VM.citySelected += (s, a) =>
            {
                viewModel.selectedCustomer.city = (City_Representation)s;
                cityPickerWindow.Hide();
            };
        }
    }
}
