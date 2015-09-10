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
        public searchTool_ModelView toolPicker_VM { get; set; }
        Window toolPickerWindow;

        public ToolSelector()
        {
            InitializeComponent();
            var viewModel = new ToolSelector_ViewModel();
            this.DataContext = viewModel;

            searchTool toolPickerUC = new searchTool();
            toolPicker_VM = new searchTool_ModelView();
            toolPickerUC.DataContext = toolPicker_VM;

            toolPickerWindow = new Window()
            {
                Content = toolPickerUC,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            toolPicker_VM.ToolSelected += (s, a) => 
            {
                viewModel.selectedTool = (Tool_Representation)s;
                toolPickerWindow.Hide();
            };
            viewModel.ToolPickerExpanded += (s, a) =>
            {
                if (toolPicker_VM.allTools == null && toolPicker_VM.IsBusy == false)
                {
                    toolPicker_VM.RefreshToolList();
                }
                toolPickerWindow.Show();
            };
        }
    }
}
