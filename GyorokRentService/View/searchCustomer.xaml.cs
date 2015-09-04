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
    /// Interaction logic for searchCustomer.xaml
    /// </summary>
    public partial class searchCustomer : UserControl
    {
        public searchCustomer_ModelView dataContext { get; set; }

        public searchCustomer()
        {
        }

        public searchCustomer(searchCustomerType displayType)
        {
            InitializeComponent();
            dataContext = new searchCustomer_ModelView(displayType);
            this.DataContext = dataContext;
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
        }
    }
}
