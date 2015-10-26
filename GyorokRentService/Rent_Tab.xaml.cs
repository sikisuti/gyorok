using GyorokRentService.View;
using GyorokRentService.ViewModel;
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
    /// Interaction logic for Rent_Tab.xaml
    /// </summary>
    public partial class Rent_Tab : UserControl
    {
        public Rent_Tab()
        {
            InitializeComponent();

            tiNewRent.Content = new NewRent_SubTab();
            tiRentalList.Content = new RentalsSum();
        }
    }
}
