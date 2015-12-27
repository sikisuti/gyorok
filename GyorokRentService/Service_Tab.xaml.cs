using GyorokRentService.View;
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

namespace GyorokRentService
{
    /// <summary>
    /// Interaction logic for Service_Tab.xaml
    /// </summary>
    public partial class Service_Tab : UserControl
    {
        public Service_Tab()
        {
            InitializeComponent();

            tiNewService.Content = new NewService_SubTab();
            //tiWorksheet.Content = new ServiceWork();
            //tiWorksheetList.Content = new ServiceSum();
        }
    }
}
