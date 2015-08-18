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

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewTool.xaml
    /// </summary>
    public partial class NewTool : Window
    {
        public NewTool()
        {
            InitializeComponent();
            var viewModel = new NewTool_ViewModel();
            this.DataContext = viewModel;
            AppMessages.ToolToSelect.Register(this, t => this.Close());
        }
    }
}
