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
        ToolSelector_ViewModel viewModel;

        searchTool toolPickerUC;
        public searchTool_ModelView toolPicker_VM { get; set; }
        Window toolPickerWindow;

        public ToolSelector()
        {
            InitializeComponent();
            viewModel = new ToolSelector_ViewModel();
            DataContext = viewModel;            

            BuildSearchToolWindow();

            toolPicker_VM.ToolSelected += (s, a) => 
            {
                viewModel.selectedTool = (Tool_Representation)s;
                toolPickerWindow.Hide();
            };
            viewModel.ToolPickerExpanded += (s, a) =>
            {
                if (toolPickerWindow == null || toolPickerWindow.IsLoaded == false)
                {
                    BuildSearchToolWindow();
                }
                toolPickerWindow.Show();
                expToolPicker.IsExpanded = false;
            };
        }

        private void BuildSearchToolWindow()
        {
            if (toolPickerUC == null)
            {
                toolPickerUC = new searchTool();
                toolPicker_VM = toolPickerUC.DataContext as searchTool_ModelView;
            }

            toolPickerWindow = new Window()
            {
                Content = toolPickerUC,
                Title = "Gép választó",
                SizeToContent = SizeToContent.WidthAndHeight
            };

            toolPicker_VM.ToolSelected += (s, a) =>
            {
                viewModel.selectedTool = (Tool_Representation)s;
                toolPickerWindow.Hide();
            };
        }
    }
}
