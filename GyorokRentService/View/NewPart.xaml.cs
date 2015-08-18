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
    /// Interaction logic for NewPart.xaml
    /// </summary>
    public partial class NewPart : Window
    {
        public NewPart()
        {
            InitializeComponent();
            AppMessages.PartAdded.Register(this, p => this.Close());
            AppMessages.PartModified.Register(this, p => this.Close());
            var viewModel = new NewPart_ViewModel();
            this.DataContext = viewModel;
        }

        public NewPart(Parts part, bool modifiable)
        {
            InitializeComponent();
            AppMessages.PartAdded.Register(this, p => this.Close());
            AppMessages.PartModified.Register(this, p => this.Close());
            var viewModel = new NewPart_ViewModel(part, modifiable);
            this.DataContext = viewModel;
        }
    }
}
