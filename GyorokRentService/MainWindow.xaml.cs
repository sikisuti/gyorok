using System.Windows;
using GyorokRentService.ViewModel;
using GyorokRentService.View;
using System.Windows.Controls;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Reflection;
using SQLConnectionLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NLog;
using GyorokRentService.Components;
using System.Threading;
using System.Threading.Tasks;

namespace GyorokRentService
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ServiceWork_ViewModel serviceWorkVM;
        ServiceSum_ViewModel serviceSumVM;


        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            try
            {
                logger.Info("Application started");

                var mainVM = new MainViewModel();
                this.DataContext = mainVM;

                //Task initDbTask = Task.Factory.StartNew(InitDatabase);
                //initDbTask.ContinueWith((x) => { ProcessStatusDisplayViewModel.Instance.ProcessList.Add(new ProcessItem("End of db init")); }, TaskScheduler.FromCurrentSynchronizationContext());

                //DoBackup();

                //serviceWorkVM = new ServiceWork_ViewModel();
                //serviceSumVM = new ServiceSum_ViewModel();

                //ServiceWork serviceWorkScreen = new ServiceWork();
                //View.ServiceSum serviceSumScreen = new View.ServiceSum();

                InitializeComponent();

                //serviceWorkScreen.DataContext = serviceWorkVM;
                //grdService.Children.Add(serviceWorkScreen);

                //serviceSumScreen.DataContext = serviceSumVM;
                //grdServiceSum.Children.Add(serviceSumScreen);

                var UCCustomerSelector = new CustomerSelector(CustomerType.Rent);
                grdCustomer.Children.Add(UCCustomerSelector);
                //var ServiceCustomerSelector = new CustomerSelector(CustomerType.Service);
                //grdNewWSCustomer.Children.Add(ServiceCustomerSelector);

                //var UCToolSelector = new ToolSelector();
                //grdToolSelect.Children.Add(UCToolSelector);

                //var UCRentalSum = new RentalsSum();
                //grdRentalSum.Children.Add(UCRentalSum);

                //var UCNewService = new NewService();
                //grdNewService.Children.Add(UCNewService);

                //var UCNewRent = new NewRent();
                //grdNewRent.Children.Add(UCNewRent);
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Általános hiba!");

                logger.Fatal(e, "General error");
            }
        }

        private void tcService_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TabItem selectedTab = new TabItem();

            try
            {
                selectedTab = e.AddedItems[0] as TabItem;  // Gets selected tab 
            }
            catch (System.Exception ex)
            {
                logger.Error(ex, "Tab selection failure");
            }

            if (selectedTab != null)
            {
                switch (selectedTab.Name)
                {
                    case "tiWorksheets":
                        serviceSumVM.RefreshServiceList();

                        //grdServiceSum.Children.Clear();
                        //var rs = new GyorokRentService.View.ServiceSum();
                        //grdServiceSum.Children.Add(rs);
                        break;

                    case "tiService":
                        serviceWorkVM.RefreshGroupList();

                        //grdService.Children.Clear();
                        //var sw = new GyorokRentService.View.ServiceWork();
                        //grdService.Children.Add(sw);
                        break;

                    case "tiNewWorksheet":
                        //grdNewWSCustomer.Children.Clear();
                        break;

                    default:
                        break;
                }
            }

        }
    }
}