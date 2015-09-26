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
using GyorokRentService.Validations;
using Common.Dependency_Injection;

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

            BindingOperations.GetBinding(txtToolName, TextBox.TextProperty).ValidationRules.Add(DIContainer.Instance.Get<FieldNotEmpty>());
            BindingOperations.GetBinding(txtIDNumber, TextBox.TextProperty).ValidationRules.Add(DIContainer.Instance.Get<FieldNotEmpty>());
            BindingOperations.GetBinding(txtCost, TextBox.TextProperty).ValidationRules.Add(DIContainer.Instance.Get<FieldNotEmpty>());
            BindingOperations.GetBinding(txtCost, TextBox.TextProperty).ValidationRules.Add(DIContainer.Instance.Get<FieldOnlyLong>());
        }
    }
}
