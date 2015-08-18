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
    /// Interaction logic for NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        public NewCustomerWindow()
        {
            
        }
        public NewCustomerWindow(searchCustomerType displayType)
        {
            InitializeComponent();
            AppMessages.ContactPersonToSelect.Register(this, (c) => this.Close());
            AppMessages.CustomerToSelect.Register(this, (c) => this.Close());
            switch (displayType)
            {
                case searchCustomerType.searchCustomer:
                    //var viewModel1 = new NewCustomerWindow_ViewModel(searchCustomerType.searchCustomer);
                    //viewModel1.RequestClose += (s, e) => this.Close();

                    grdMain.Children.Add(new NewCustomerUC(searchCustomerType.searchCustomer));
                    //this.DataContext = viewModel1;
                    break;
                case searchCustomerType.searchContact:
                    //var viewModel2 = new NewCustomerWindow_ViewModel(searchCustomerType.searchContact);
                    //viewModel2.RequestClose += (s, e) => this.Close();

                    grdMain.Children.Add(new NewCustomerUC(searchCustomerType.searchContact));
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
