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
using Common.Validations;
using System.Text.RegularExpressions;

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for ToolSelector.xaml
    /// </summary>
    public partial class ToolSelector : UserControl
    {
        public ToolSelector_ViewModel viewModel { get; set; }

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
                Tool_Representation tool = s as Tool_Representation;
                tool.ValidationRules = new ToolValidationRules();
                viewModel.selectedTool = tool;
                toolPickerWindow.Hide();
            };
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtDefaultDeposit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text.Length == 0)
            {
                ((TextBox)sender).Text = "0";
            }
        }

        private void txtDefaultDeposit_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text.Length == 0)
            {
                ((TextBox)sender).Text = "0";
            }
        }
    }
}
