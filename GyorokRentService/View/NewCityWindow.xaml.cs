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

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewCityWindow.xaml
    /// </summary>
    public partial class NewCityWindow : Window
    {
        public NewCityWindow()
        {
            InitializeComponent();
            AppMessages.CityListModified.Register(this, c => this.Close());
        }
    }
}
