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

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for RentalsSum.xaml
    /// </summary>
    public partial class RentalsSum : UserControl
    {
        RentalsSum_ViewModel viewModel { get; set; }

        public RentalsSum()
        {
            InitializeComponent();

            viewModel = new RentalsSum_ViewModel();
            DataContext = viewModel;
        }

        private void expRentalGroupChooser_Expanded(object sender, RoutedEventArgs e)
        {
            SearchRental_ViewModel searchRental_VM = new SearchRental_ViewModel();
            Window searchRentalWindow = new Window()
            {
                Title = "Kölcsönzés választó",
                Content = searchRental_VM
            };
            searchRentalWindow.Show();
        }

        private void expRentalGroupChooser_Collapsed(object sender, RoutedEventArgs e)
        {

        }
    }
}
