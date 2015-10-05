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
using MiddleLayer.Representations;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for searchTool.xaml
    /// </summary>
    public partial class searchTool : UserControl
    {
        NewTool_ViewModel newToolViewModel;

        public searchTool()
        {
            InitializeComponent();
            var viewModel = new searchTool_ModelView();
            this.DataContext = viewModel;

            viewModel.NewToolRequested += (s, a) =>
            {
                NewTool newToolWindow = new NewTool();
                newToolViewModel = newToolWindow.DataContext as NewTool_ViewModel;
                newToolViewModel.ToolInserted += (so, ar) =>
                {
                    viewModel.selectedTool = so as ToolRepresentation;
                    viewModel.OnToolSelected();
                    newToolWindow.Close();
                };
                newToolWindow.Show();
            };
        }
    }
}
