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
using Common.Enumerations;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        public NewCustomerWindow()
        {
            
        }
        public NewCustomerWindow(searchCustomerTypeEnum displayType)
        {
            InitializeComponent();
            AppMessages.ContactPersonToSelect.Register(this, (c) => this.Close());
            AppMessages.CustomerToSelect.Register(this, (c) => this.Close());
            switch (displayType)
            {
                case searchCustomerTypeEnum.Customer:
                    //var viewModel1 = new NewCustomerWindow_ViewModel(searchCustomerType.searchCustomer);
                    //viewModel1.RequestClose += (s, e) => this.Close();

                    grdMain.Children.Add(new NewCustomerUC(searchCustomerTypeEnum.Customer));
                    //this.DataContext = viewModel1;
                    break;
                case searchCustomerTypeEnum.Contact:
                    //var viewModel2 = new NewCustomerWindow_ViewModel(searchCustomerType.searchContact);
                    //viewModel2.RequestClose += (s, e) => this.Close();

                    grdMain.Children.Add(new NewCustomerUC(searchCustomerTypeEnum.Contact));
                    //this.DataContext = viewModel2;
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
