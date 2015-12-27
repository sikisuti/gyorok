﻿using GyorokRentService.ViewModel;
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

namespace GyorokRentService.View
{
    /// <summary>
    /// Interaction logic for NewService.xaml
    /// </summary>
    public partial class NewService : UserControl
    {
        public NewService_ViewModel newService_VM { get; set; }

        public NewService()
        {
            InitializeComponent();
            newService_VM = new NewService_ViewModel();
            DataContext = newService_VM;
        }
    }
}
