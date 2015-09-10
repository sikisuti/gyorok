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
using SQLConnectionLib;
using MiddleLayer.Representations;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewRentGroupWindow.xaml
    /// </summary>
    public partial class NewRentGroupWindow : Window
    {
        public NewRentGroupWindow()
        {
            InitializeComponent();
        }
        public NewRentGroupWindow(RentalGroup_Representation r)
        {
            InitializeComponent();
            var viewModel = new NewRentGroup_ViewModel(r);
            this.DataContext = viewModel;
            AppMessages.RentGroupClosed.Register(this, s => this.Close());
        }
    }
}
