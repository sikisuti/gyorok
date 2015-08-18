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

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewWorksheetGroupWindow.xaml
    /// </summary>
    public partial class NewWorksheetGroupWindow : Window
    {
        public NewWorksheetGroupWindow()
        {
            InitializeComponent();
        }
        public NewWorksheetGroupWindow(ref List<ServiceWorksheets> sw)
        {
            InitializeComponent();
            var viewModel = new NewWorksheetGroup_ViewModel(ref sw);
            this.DataContext = viewModel;
            AppMessages.ServiceGroupClosed.Register(this, s => this.Close());
        }
    }
}
