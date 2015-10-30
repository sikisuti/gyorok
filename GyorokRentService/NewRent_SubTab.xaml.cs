using Common.Enumerations;
using Common.Validations;
using GyorokRentService.View;
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

            UCCustomerSelector = new CustomerSelector(OperationTypeEnum.Rental);
            grdCustomer.Children.Add(UCCustomerSelector);

            UCToolSelector = new ToolSelector();
            grdToolSelect.Children.Add(UCToolSelector);

            var UCNewRent = new NewRent();
            grdNewRent.Children.Add(UCNewRent);

            UCCustomerSelector.customerPicker_VM.CustomerSelected += (s, a) => 
            {
                CustomerBaseRepresentation customer = (CustomerBaseRepresentation)s;
                UCNewRent.newRent_VM.newRental.customer = customer;
                UCNewRent.newRent_VM.newRental.discount = customer.defaultDiscount;
                UCCustomerSelector.expCustomer.IsExpanded = false;
            };

            UCCustomerSelector.viewModel.contactSelected += (s, a) =>
            {
                UCNewRent.newRent_VM.newRental.contact = (PersonRepresentation)s;
            };

            UCToolSelector.toolPicker_VM.ToolSelected += (s, a) => 
            {
                ToolRepresentation tool = s as ToolRepresentation;
                tool.ValidationRules = new ToolValidationRules();
                UCNewRent.newRent_VM.newRental.tool = tool;
            };

            UCNewRent.newRent_VM.RentGroupChanged += (s, a) =>
            {
                RentalGroup_Representation rentalGroup = s as RentalGroup_Representation;
                if (rentalGroup.rentals.Count() != 0)
                {
                    ((CustomerSelector_ViewModel)UCCustomerSelector.DataContext).isReadonly = true;
                }
                else
                {
                    ((CustomerSelector_ViewModel)UCCustomerSelector.DataContext).isReadonly = false;
                }
                ((ToolSelector_ViewModel)UCToolSelector.DataContext).selectedTool = null;
            };

            UCNewRent.newRent_VM.RentalGroupFinalizationRequested += (s, a) =>
            {
                NewRentGroupWindow rg = new NewRentGroupWindow((RentalGroup_Representation)s);
                NewRentGroup_ViewModel newRentGroup_VM = rg.DataContext as NewRentGroup_ViewModel;
                newRentGroup_VM.rentGroupAccepted += (so, ar) =>
                {
                    UCNewRent.newRent_VM.newRentalGroup = new RentalGroup_Representation();
                    UCCustomerSelector.viewModel.selectedCustomer = null;
                    UCToolSelector.viewModel.selectedTool = null;
                    rg.Close();
                };
                newRentGroup_VM.rentGroupCancelled += (so, ar) =>
                {
                    rg.Close();
                };
                rg.Show();
            };
        }
    }
}
