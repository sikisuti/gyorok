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
    /// Interaction logic for NewRent.xaml
    /// </summary>
    public partial class NewRent : UserControl
    {
        public NewRent()
        {
            InitializeComponent();
            var viewModel = new NewRent_ViewModel();
            this.DataContext = viewModel;
        }
    }
}
