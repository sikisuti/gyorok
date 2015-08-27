using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using LoggerLib;

namespace SQLConnectionLib
{
    public sealed class SQLConnection
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool PersistSecurityInfo { get; set; }
        public bool MultipleActiveResultSets { get; set; }
        public string App { get; set; }
        public string MetaData { get; set; }
        public string Provider { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private static readonly SQLConnection execute = new SQLConnection();
        private dbGyorokEntities db;
        EntityConnectionStringBuilder sb;
        private EntityConnection ec;

        private long customersVersion;
        private long partsVersion;
        private long rentalsVersion;
        private long serviceWorksheetsVersion;
        private long toolsVersion;

        private ObjectSet<Customers> customersTable;
        public ObjectSet<Customers> CustomersTable
        {
            get { return customersTable; }
            set { customersTable = value; }
        }

        private ObjectSet<Contacts> contactsTable;
        public ObjectSet<Contacts> ContactsTable
        {
            get { return contactsTable; }
            set { contactsTable = value; }
        }

        private ObjectSet<Parts> partsTable;
        public ObjectSet<Parts> PartsTable
        {
            get { return partsTable; }
            set { partsTable = value; }
        }

        private ObjectSet<Rentals> rentalsTable;
        public ObjectSet<Rentals> RentalsTable
        {
            get { return rentalsTable; }
            set { rentalsTable = value; }
        }

        private ObjectSet<PayTypes> payTypesTable;
        public ObjectSet<PayTypes> PayTypesTable
        {
            get { return payTypesTable; }
            set { payTypesTable = value; }
        }

        private ObjectSet<RentalGroups> rentalGroupsTable;
        public ObjectSet<RentalGroups> RentalGroupsTable
        {
            get { return rentalGroupsTable; }
            set { rentalGroupsTable = value; }
        }

        private ObjectSet<Tools> toolsTable;
        public ObjectSet<Tools> ToolsTable
        {
            get { return toolsTable; }
            set { toolsTable = value; }
        }

        private ObjectSet<ServiceWorksheets> serviceWorksheetsTable;
        public ObjectSet<ServiceWorksheets> ServiceWorksheetsTable
        {
            get { return serviceWorksheetsTable; }
            set { serviceWorksheetsTable = value; }
        }

        private ObjectSet<ServiceGroups> serviceGroupsTable;
        public ObjectSet<ServiceGroups> ServiceGroupsTable
        {
            get { return serviceGroupsTable; }
            set { serviceGroupsTable = value; }
        }        

        private ObjectSet<ErrorTypes> errorTypesTable;
        public ObjectSet<ErrorTypes> ErrorTypesTable
        {
            get { return errorTypesTable; }
            set { errorTypesTable = value; }
        }

        private ObjectSet<ToolStatuses> toolStatusesTable;
        public ObjectSet<ToolStatuses> ToolStatusesTable
        {
            get { return toolStatusesTable; }
            set { toolStatusesTable = value; }
        }

        private ObjectSet<ServiceSum> serviceSumView;
        public ObjectSet<ServiceSum> ServiceSumView
        {
            get { return serviceSumView; }
            set { serviceSumView = value; }
        }

        private ObjectSet<RentalSum> rentalSumView;
        public ObjectSet<RentalSum> RentalSumView
        {
            get { return rentalSumView; }
            set { rentalSumView = value; }
        }

        private ObjectSet<DetailedCustomers> detailedCustomersView;

        public ObjectSet<DetailedCustomers> DetailedCustomersView
        {
            get { return detailedCustomersView; }
            set { detailedCustomersView = value; }
        }        

        private ObjectSet<Cities> citiesTable;
        public ObjectSet<Cities> CitiesTable
        {
            get { return citiesTable; }
            set { citiesTable = value; }
        }
        

        private SQLConnection()
        {
            //Logger.Execute.WriteLog("SQL connection instantiated", EventLogEntryType.Information);
        }

        //public void Init(string serverIP)
        //{
        //    EntityConnectionStringBuilder sb = new EntityConnectionStringBuilder();
        //    string userName;
        //    string password;
        //    string ip;

        //    sb.Metadata = @"res://*/dbGyorok.csdl|res://*/dbGyorok.ssdl|res://*/dbGyorok.msl";
        //    sb.Provider = @"System.Data.SqlClient";
        //    userName = "gyorok";
        //    password = "gyorok";
        //    if (new List<string> { "", ".", "local", "localhost" }.Contains(serverIP))
        //    {
        //        ip = ".";
        //    }
        //    else
        //    {
        //        ip = serverIP; 
        //    }
        //    sb.ProviderConnectionString = @"data source=" + ip + @"\sqlexpress;initial catalog=dbGyorok;persist security info=False;user id=" + userName + ";password=" + password + ";multipleactiveresultsets=True;App=EntityFramework";
        //    //sb.ProviderConnectionString = @"data source=.\sqlexpress;initial catalog=dbGyorok;integrated security=True;multipleactiveresultsets=True;App=EntityFramework";
        //    ec = new EntityConnection(sb.ConnectionString);            
            
        //    UpdateDb();
        //    UpdateTables();
        //    UpdateViews();    
        //}

        public void Init()
        {
            SqlConnectionStringBuilder pConnStr = new SqlConnectionStringBuilder();

            pConnStr.DataSource = DataSource;
            pConnStr.InitialCatalog = InitialCatalog;
            pConnStr.IntegratedSecurity = IntegratedSecurity;
            pConnStr.UserID = UserName;
            pConnStr.Password = Password;
            pConnStr.PersistSecurityInfo = PersistSecurityInfo;
            pConnStr.MultipleActiveResultSets = MultipleActiveResultSets;
            pConnStr.ApplicationName = App;

            sb = new EntityConnectionStringBuilder();
            sb.Provider = Provider;
            sb.ProviderConnectionString = pConnStr.ConnectionString;
            sb.Metadata = MetaData;

            ec = new EntityConnection(sb.ConnectionString);

            try
            {
                UpdateDb();
                UpdateTables();
                UpdateViews();

                customersVersion = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Customers").tableVersion;
                partsVersion = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Parts").tableVersion;
                rentalsVersion = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Rentals").tableVersion;
                serviceWorksheetsVersion = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "ServiceWorksheets").tableVersion;
                toolsVersion = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Tools").tableVersion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static SQLConnection Execute
        {
            get
            {                
                return execute;
            }
        }

        public void SaveDb()
        {
            db.SaveChanges();
            UpdateDb();
            UpdateTables();
            UpdateViews();
        }

        private void UpdateDb()
        {        
            if (db != null) { db.Dispose(); }

            try
            {
                db = new dbGyorokEntities(ec);
                db.Connection.Open();
                db.Connection.Close();
            }
            catch (Exception ex) // Ez soha nem dob hibát !?
            {
                throw ex;
            }
        }

        private void UpdateTables()
        {
            customersTable = db.Customers;
            contactsTable = db.Contacts;
            partsTable = db.Parts;
            rentalsTable = db.Rentals;
            payTypesTable = db.PayTypes;
            rentalGroupsTable = db.RentalGroups;
            toolsTable = db.Tools;
            serviceWorksheetsTable = db.ServiceWorksheets;
            errorTypesTable = db.ErrorTypes;
            toolStatusesTable = db.ToolStatuses;
            serviceGroupsTable = db.ServiceGroups;
            citiesTable = db.Cities;
        }

        private void UpdateViews()
        {
            serviceSumView = db.ServiceSum;
            rentalSumView = db.RentalSum;
            DetailedCustomersView = db.DetailedCustomers;
        }

        public void delCustomer(long pCustomerID)
        {
            Customers actCustomer;

            try
            {
                actCustomer = SQLConnection.Execute.CustomersTable.Single<Customers>(c => c.customerID == pCustomerID);
                actCustomer.isDeleted = true;
                SaveDb();
           
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void delTool(long pToolID)
        {
            Tools actTool;

            try
            {
                actTool = SQLConnection.Execute.ToolsTable.Single<Tools>(t => t.toolID == pToolID);
                actTool.isDeleted = true;
                SaveDb();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void addCity(Cities newCity)
        {
            try
            {
                CitiesTable.AddObject(newCity);
                SaveDb();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void delCity(long pCityID)
        {
            Cities actCity;

            try
            {
                actCity = SQLConnection.Execute.CitiesTable.Single<Cities>(t => t.cityID == pCityID);
                actCity.isDeleted = true;
                SaveDb();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsCustomersConsistent()
        {
            long ver;

            try
            {
                using (dbGyorokEntities gyorokDB = new dbGyorokEntities(sb.ConnectionString))
                {
                    ver = gyorokDB.TableVersions.Single<TableVersions>(tv => tv.tableName == "Customers").tableVersion; 
                }

                if (customersVersion >= ver)
                {
                    return true;
                }
                else
                {
                    customersVersion = ver;
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool IsPartsConsistent()
        {
            long ver;

            try
            {
                using (dbGyorokEntities gyorokDB = new dbGyorokEntities(sb.ConnectionString))
                {
                    ver = gyorokDB.TableVersions.Single<TableVersions>(tv => tv.tableName == "Parts").tableVersion;
                }

                if (partsVersion >= ver)
                {
                    return true;
                }
                else
                {
                    partsVersion = ver;
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool IsRentalsConsistent()
        {
            long ver;

            try
            {
                using (dbGyorokEntities gyorokDB = new dbGyorokEntities(sb.ConnectionString))
                {
                    ver = gyorokDB.TableVersions.Single<TableVersions>(tv => tv.tableName == "Rentals").tableVersion;
                }

                if (rentalsVersion >= ver)
                {
                    return true;
                }
                else
                {
                    rentalsVersion = ver;
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool IsServiceWorksheetsConsistent()
        {
            long ver;

            try
            {
                using (dbGyorokEntities gyorokDB = new dbGyorokEntities(sb.ConnectionString))
                {
                    ver = gyorokDB.TableVersions.Single<TableVersions>(tv => tv.tableName == "ServiceWorksheets").tableVersion;
                }

                if (serviceWorksheetsVersion >= ver)
                {
                    return true;
                }
                else
                {
                    serviceWorksheetsVersion = ver;
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool IsToolsConsistent()
        {
            long ver;

            try
            {
                using (dbGyorokEntities gyorokDB = new dbGyorokEntities(sb.ConnectionString))
                {
                    ver = gyorokDB.TableVersions.Single<TableVersions>(tv => tv.tableName == "Tools").tableVersion;
                }

                if (toolsVersion >= ver)
                {
                    return true;
                }
                else
                {
                    toolsVersion = ver;
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public void DoBackup(string path)
        {
            try
            {
                using (dbGyorokEntities gyorokDB = new dbGyorokEntities(sb.ConnectionString))
                {
                    gyorokDB.DoBackup(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DoRestore(string path)
        {
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder pConnStr = new SqlConnectionStringBuilder();

            pConnStr.DataSource = @".\sqlexpress";
            pConnStr.InitialCatalog = @"master";
            pConnStr.IntegratedSecurity = true;
            //pConnStr.UserID = UserName;
            //pConnStr.Password = Password;
            pConnStr.PersistSecurityInfo = false;
            pConnStr.MultipleActiveResultSets = false;            

            try
            {
                using (connection = new SqlConnection(pConnStr.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(@"DoRestore", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter(@"path", path));

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {            
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                throw ex;
            }
        }
    }
}
