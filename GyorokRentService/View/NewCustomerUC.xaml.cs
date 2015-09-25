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
using Common.Enumerations;
using MiddleLayer.Representations;
using GyorokRentService.Validations;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewCustomerUC.xaml
    /// </summary>
    public partial class NewCustomerUC : UserControl
    {
        NewCustomer_ViewModel viewModel;

        SearchCity cityChooserWindow;
        SearchCity_ViewModel cityChooserVM;

        public NewCustomerUC(searchCustomerTypeEnum displayType)
        {
            InitializeComponent();

            BindingOperations.GetBinding(txtCustomerName, TextBox.TextProperty).ValidationRules.Add(new FieldNotEmpty() { ValidatesOnTargetUpdated = true });
            BindingOperations.GetBinding(txtCustomerPhone, TextBox.TextProperty).ValidationRules.Add(new FieldNotEmpty() { ValidatesOnTargetUpdated = true });

            viewModel = new NewCustomer_ViewModel(displayType);
            DataContext = viewModel;

            viewModel.CityRequested += (s, a) =>
            {
                cityChooserWindow = new SearchCity();
                cityChooserVM = cityChooserWindow.DataContext as SearchCity_ViewModel;
                cityChooserVM.citySelected += (so, ar) =>
                {
                    viewModel.newCustomer.city = (City_Representation)so;
                    cityChooserWindow.Close();
                };
                cityChooserWindow.Show();
            };
        }
    }
}
