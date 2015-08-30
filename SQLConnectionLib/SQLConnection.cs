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
    public class SQLConnection : ISQLConnection
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

        private dbGyorokEntities db;
        EntityConnectionStringBuilder sb;
        private EntityConnection ec;

        private long customersVersion;
        private long partsVersion;
        private long rentalsVersion;
        private long serviceWorksheetsVersion;
        private long toolsVersion;

        #region Tables

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

        #endregion

        public SQLConnection()
        {
            //Logger.Execute.WriteLog("SQL connection instantiated", EventLogEntryType.Information);

            DataSource = Properties.Settings.Default.ServerIP + @"\" + Properties.Settings.Default.ServerInstanceName;
            InitialCatalog = Properties.Settings.Default.InitialCatalog;
            IntegratedSecurity = Properties.Settings.Default.IntegratedSecurity;
            PersistSecurityInfo = Properties.Settings.Default.PersistSecurityInfo;
            MultipleActiveResultSets = Properties.Settings.Default.MultipleActiveResultSets;
            App = Properties.Settings.Default.App;
            UserName = Properties.Settings.Default.UserName;
            Password = Properties.Settings.Default.Password;
            Provider = Properties.Settings.Default.Provider;
            MetaData = Properties.Settings.Default.MetaData;
        }

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
            catch (Exception ex)
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

            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                actCustomer = db.Customers.Single(c => c.customerID == pCustomerID);
                actCustomer.isDeleted = true;
                SaveDb();
            }
        }

        public void delTool(long pToolID)
        {
            Tools actTool;

            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                actTool = db.Tools.Single(t => t.toolID == pToolID);
                actTool.isDeleted = true;
                SaveDb();
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

            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                actCity = db.Cities.Single(t => t.cityID == pCityID);
                actCity.isDeleted = true;
                SaveDb();
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

        public void DoBackup(string path = null)
        {

            try
            {
                if (Properties.Settings.Default.ServerIP == "." ||
                    Properties.Settings.Default.ServerIP.ToLower() == "localhost" ||
                    Properties.Settings.Default.ServerIP == string.Empty)
                {

                    using (dbGyorokEntities gyorokDB = new dbGyorokEntities(sb.ConnectionString))
                    {
                        if (path == null)
                        {
                            gyorokDB.DoBackup(Properties.Settings.Default.PrimaryBackupPath + @"\");
                            var fileName = Directory.GetFiles(Properties.Settings.Default.PrimaryBackupPath).Select(x => new FileInfo(x)).OrderByDescending(f => f.LastWriteTime).FirstOrDefault().Name;
                            // TODO: ...
                            File.Copy(Properties.Settings.Default.PrimaryBackupPath + @"\" + fileName, Properties.Settings.Default.SecondaryBackupPath + @"\" + fileName, true);
                        }
                        else
                        {
                            gyorokDB.DoBackup(path);
                        }
                    }
                    
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

        public Customers GetCustomerById(long id)
        {
            Customers customer;

            using (dbGyorokEntities gyorokDB = new dbGyorokEntities(ec))
            {
                customer = gyorokDB.Customers.SingleOrDefault(c => c.customerID == id);
            }

            return customer;
        }

        public void UpdateCustomer(Customers customer)
        {
            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                Customers customerToUpdate = db.Customers.SingleOrDefault(c => c.customerID == customer.customerID);
                // TODO: Implement 
                //if (customerToUpdate.rowVersion != customer.rowVersion)
                //{
                //    throw new Exception("Customer modification conflicted");
                //}
                customerToUpdate.birthDate = customer.birthDate;
                customerToUpdate.cityID = customer.cityID;
                customerToUpdate.comment = customer.comment;
                customerToUpdate.customerAddress = customer.customerAddress;
                customerToUpdate.customerName = customer.customerName;
                customerToUpdate.customerPhone = customer.customerPhone;
                customerToUpdate.defaultDiscount = customer.defaultDiscount;
                customerToUpdate.IDNumber = customer.IDNumber;
                customerToUpdate.isDeleted = customer.isDeleted;
                customerToUpdate.isFirm = customer.isFirm;
                customerToUpdate.mothersName = customer.mothersName;
                customerToUpdate.problems = customer.problems;
                customerToUpdate.rentCounter = customer.rentCounter;
                customerToUpdate.serviceCounter = customer.serviceCounter;
                customerToUpdate.workPlace = customer.workPlace;
                db.SaveChanges();
            }        
        }

        public void DeleteContact(Customers firm, Customers agent)
        {
            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                Contacts contactForDelete = db.Contacts.SingleOrDefault(c => c.firmID == firm.customerID && c.agentID == agent.customerID);
                if (contactForDelete != null)
                {
                    db.Contacts.DeleteObject(contactForDelete);
                    db.SaveChanges();
                }
            }
        }

        public void AddContact(Customers firm, Customers agent)
        {
            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                if (!db.Contacts.Any(c => c.firmID == firm.customerID && c.agentID == agent.customerID))
                {
                    Contacts newContact = new Contacts()
                    {
                        agentID = agent.customerID,
                        firmID = firm.customerID
                    };
                    db.Contacts.AddObject(newContact);
                    db.SaveChanges();
                }
            }
        }

        public List<Customers> GetContacts(Customers firm)
        {
            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                return db.Customers.Where(c => c.Contacts1.Any(ca => ca.firmID == firm.customerID)).ToList();
            }
        }

        public Cities GetCityById(long id)
        {
            using (dbGyorokEntities db = new dbGyorokEntities(ec))
            {
                return db.Cities.SingleOrDefault(c => c.cityID == id);
            }
        }
    }
}
