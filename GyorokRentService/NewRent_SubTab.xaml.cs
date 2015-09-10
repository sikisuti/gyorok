﻿using GyorokRentService.View;
using GyorokRentService.ViewModel;
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
    /// Interaction logic for NewRent_SubTab.xaml
    /// </summary>
    public partial class NewRent_SubTab : UserControl
    {
        public NewRent_SubTab()
        {
            CustomerSelector UCCustomerSelector;
            ToolSelector UCToolSelector;

            InitializeComponent();

            UCCustomerSelector = new CustomerSelector(CustomerType.Rent);
            grdCustomer.Children.Add(UCCustomerSelector);

            UCToolSelector = new ToolSelector();
            grdToolSelect.Children.Add(UCToolSelector);

            var UCNewRent = new NewRent();
            grdNewRent.Children.Add(UCNewRent);

            UCCustomerSelector.customerPicker_VM.CustomerSelected += (s, a) => 
            {
                UCNewRent.newRent_VM.customerSelected((CustomerBase_Representation)s);
            };

            UCToolSelector.toolPicker_VM.ToolSelected += (s, a) => 
            {
                UCNewRent.newRent_VM.toolSelected((Tool_Representation)s);
            };
        }
    }
}
