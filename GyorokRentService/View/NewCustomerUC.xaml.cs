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

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewCustomerUC.xaml
    /// </summary>
    public partial class NewCustomerUC : UserControl
    {
        public NewCustomerUC()
        {
            
        }
        public NewCustomerUC(searchCustomerType displayType)
        {
            InitializeComponent();
            switch (displayType)
            {
                case searchCustomerType.searchCustomer:
                    var DC1 = new ViewModel.NewCustomer_ViewModel(searchCustomerType.searchCustomer);
                    this.DataContext = DC1;
                    break;
                case searchCustomerType.searchContact:
                    var DC2 = new ViewModel.NewCustomer_ViewModel(searchCustomerType.searchContact);
                    this.DataContext = DC2;
                    break;
                default:
                    break;
            }
        }
    }
}
