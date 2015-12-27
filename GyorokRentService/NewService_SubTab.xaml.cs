using Common.Enumerations;
using GyorokRentService.View;
using MiddleLayer.Representations;
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

namespace GyorokRentService
{
    /// <summary>
    /// Interaction logic for NewService_SubTab.xaml
    /// </summary>
    public partial class NewService_SubTab : UserControl
    {
        public NewService_SubTab()
        {
            CustomerSelector UCCustomerSelector;

            InitializeComponent();

            UCCustomerSelector = new CustomerSelector(OperationTypeEnum.Service);
            grdNewWSCustomer.Children.Add(UCCustomerSelector);

            var UCNewService = new NewService();
            grdNewService.Children.Add(UCNewService);

            UCCustomerSelector.customerPicker_VM.CustomerSelected += (s, a) =>
            {
                CustomerBaseRepresentation customer = (CustomerBaseRepresentation)s;
                UCNewService.newService_VM.newService.customer = customer;
                UCNewService.newService_VM.newService.discount = customer.defaultDiscount;
                UCCustomerSelector.expCustomer.IsExpanded = false;
            };
        }
    }
}
