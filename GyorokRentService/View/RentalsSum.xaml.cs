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

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for RentalsSum.xaml
    /// </summary>
    public partial class RentalsSum : UserControl
    {
        RentalsSum_ViewModel viewModel { get; set; }

        Window searchRentalWindow;
        SearchRental_ViewModel searchRental_VM;

        public RentalsSum()
        {
            InitializeComponent();

            viewModel = new RentalsSum_ViewModel();
            DataContext = viewModel;
        }

        private void expRentalGroupChooser_Expanded(object sender, RoutedEventArgs e)
        {
            if (searchRentalWindow == null || !searchRentalWindow.IsLoaded)
            {
                BuildSearchRentalGroupWindow();
            }
            searchRentalWindow.Show();
        }

        private void BuildSearchRentalGroupWindow()
        {
            if (searchRental_VM == null)
            {
                searchRental_VM = new SearchRental_ViewModel();
                searchRental_VM.RentGroupSelected += (s, a) =>
                {
                    viewModel.selectedRentalGroup = (RentalGroup_Representation)s;
                    searchRentalWindow.Hide();
                    expRentalGroupChooser.IsExpanded = false;
                };
            }

            searchRentalWindow = new Window()
            {
                Title = "Kölcsönzés választó",
                Content = new SearchRental() { DataContext = searchRental_VM },
                SizeToContent = SizeToContent.WidthAndHeight
            };
        }
    }
}
