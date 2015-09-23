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
using MiddleLayer;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for searchCustomer.xaml
    /// </summary>
    public partial class searchCustomer : UserControl
    {
        public searchCustomer_ModelView dataContext { get; set; }

        NewCustomer_ViewModel newCustomerVM;
        NewCustomerUC newCustomerUC;
        Window newCustomerWindow;

        public searchCustomer()
        {
        }

        public searchCustomer(searchCustomerTypeEnum displayType)
        {
            InitializeComponent();
            dataContext = new searchCustomer_ModelView(displayType);
            this.DataContext = dataContext;

            dataContext.NewCustomerRequested += (s, a) =>
            {
                newCustomerUC = new NewCustomerUC(displayType);
                newCustomerWindow = new Window()
                {
                    Title = "Új ügyfél",
                    Content = newCustomerUC,
                    SizeToContent = SizeToContent.WidthAndHeight
                };
                newCustomerVM = newCustomerUC.DataContext as NewCustomer_ViewModel;
                newCustomerVM.CustomerInserted += (so, ar) =>
                {
                    dataContext.selectedCustomer = (CustomerBase_Representation)so;
                    dataContext.OnCustomerSelected(EventArgs.Empty);
                    newCustomerWindow.Close();
                };
                newCustomerWindow.Show();
            };

            DataProxy.Instance.CustomersChanged += (s, a) =>
            {
                dataContext.RefreshCustomerList();
            };
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
        }
    }
}
