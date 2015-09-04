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
    /// Interaction logic for ToolSelector.xaml
    /// </summary>
    public partial class ToolSelector : UserControl
    {
        public ToolSelector()
        {
            InitializeComponent();
            var viewModel = new ToolSelector_ViewModel();
            this.DataContext = viewModel;

            searchTool searchToolWindow = new searchTool();
            searchTool_ModelView searchToolVM = new searchTool_ModelView();
            searchToolWindow.DataContext = searchToolVM;
            this.grdExpander.Children.Add(searchToolWindow);

            searchToolVM.ToolSelected += (s, a) => { viewModel.selectedTool = (Tool_Representation)s; };
            viewModel.ToolPickerExpanded += (s, a) =>
            {
                if (searchToolVM.allTools == null && searchToolVM.IsBusy == false)
                {
                    searchToolVM.RefreshToolList();
                }
            };
        }
    }
}
