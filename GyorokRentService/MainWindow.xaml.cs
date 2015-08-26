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
                //LoggerLib.Logger.Execute.WriteLog("Application started", EventLogEntryType.Information);

                var mainVM = new MainViewModel();
                this.DataContext = mainVM;

                SQLConnection.Execute.DataSource = Properties.Settings.Default.ServerIP + @"\sqlexpress";
                SQLConnection.Execute.InitialCatalog = @"dbGyorok";
                SQLConnection.Execute.IntegratedSecurity = false;
                SQLConnection.Execute.PersistSecurityInfo = false;
                SQLConnection.Execute.MultipleActiveResultSets = true;
                SQLConnection.Execute.App = @"EntityFramework";
                SQLConnection.Execute.UserName = @"gyorok";
                SQLConnection.Execute.Password = @"gyorok";
                SQLConnection.Execute.Provider = @"System.Data.SqlClient";
                SQLConnection.Execute.MetaData = @"res://*/dbGyorok.csdl|res://*/dbGyorok.ssdl|res://*/dbGyorok.msl";

                SQLConnection.Execute.Init();

                if (Properties.Settings.Default.ServerIP == "." || 
                    Properties.Settings.Default.ServerIP.ToLower() == "localhost" ||
                    Properties.Settings.Default.ServerIP == string.Empty)
                {
                    try
                    {
                        SQLConnection.Execute.DoBackup(Properties.Settings.Default.PrimaryBackupPath + @"\");
                        var fileName = Directory.GetFiles(Properties.Settings.Default.PrimaryBackupPath).Select(x => new FileInfo(x)).OrderByDescending(f => f.LastWriteTime).FirstOrDefault().Name;
                        // TODO: ...
                        File.Copy(Properties.Settings.Default.PrimaryBackupPath + @"\" + fileName, Properties.Settings.Default.SecondaryBackupPath + @"\" + fileName, true);
                    }
                    catch (Exception ex)
                    {
                        LoggerLib.Logger.Execute.WriteExceptionToLog(ex);
                    }
                }

                serviceWorkVM = new ServiceWork_ViewModel();
                serviceSumVM = new ServiceSum_ViewModel();

                ServiceWork serviceWorkScreen = new ServiceWork();
                View.ServiceSum serviceSumScreen = new View.ServiceSum();

                InitializeComponent();

                serviceWorkScreen.DataContext = serviceWorkVM;
                grdService.Children.Add(serviceWorkScreen);

                serviceSumScreen.DataContext = serviceSumVM;
                grdServiceSum.Children.Add(serviceSumScreen);

                var UCCustomerSelector = new CustomerSelector(CustomerType.Rent);
                grdCustomer.Children.Add(UCCustomerSelector);
                var ServiceCustomerSelector = new CustomerSelector(CustomerType.Service);
                grdNewWSCustomer.Children.Add(ServiceCustomerSelector);

                var UCToolSelector = new ToolSelector();
                grdToolSelect.Children.Add(UCToolSelector);

                var UCRentalSum = new RentalsSum();
                grdRentalSum.Children.Add(UCRentalSum);

                var UCNewService = new NewService();
                grdNewService.Children.Add(UCNewService);

                var UCNewRent = new NewRent();
                grdNewRent.Children.Add(UCNewRent);
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Adatbázis kapcsolati hiba!");

                LoggerLib.Logger.Execute.WriteLog("General error" + Environment.NewLine + Environment.NewLine +
                                        "Source: " + e.Source + Environment.NewLine + Environment.NewLine +
                                        "Message:" + Environment.NewLine + e.Message + Environment.NewLine + Environment.NewLine +
                                        "Inner Exception:" + Environment.NewLine + e.InnerException,
                                        EventLogEntryType.Error);
            }
        }

        private void tcService_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TabItem selectedTab = new TabItem();

            try
            {
                selectedTab = e.AddedItems[0] as TabItem;  // Gets selected tab 
            }
            catch (System.Exception)
            {
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