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
        private static SQLConnection _instance = new SQLConnection();
        public static SQLConnection Execute
        {
            get
            {
                return _instance;
            }
        }

        private DbSettings dbSettings;

        private dbGyorokEntities db;

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
            dbSettings = new DbSettings();
            db = new dbGyorokEntities(dbSettings.GetEntityConnection());
        }

        public void SaveDb()
        {
            ////db.SaveChanges();
            //UpdateDb();
            //UpdateTables();
            //UpdateViews();
        }

        private void UpdateDb()
        {        
            //if (db != null) { db.Dispose(); }

            //try
            //{
            //    db = new dbGyorokEntities(ec);
            //    db.Connection.Open();
            //    db.Connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
            Customers actCustomer = db.Customers.Single(c => c.customerID == pCustomerID);
            actCustomer.isDeleted = true;
            db.SaveChanges();
        }

        public void delTool(long pToolID)
        {
            Tools actTool = db.Tools.Single(t => t.toolID == pToolID);
            actTool.isDeleted = true;
            db.SaveChanges();
        }

        public void addCity(Cities newCity)
        {
            db.Cities.AddObject(newCity);
            db.SaveChanges();
        }

        public void delCity(long pCityID)
        {
            Cities actCity = db.Cities.Single(t => t.cityID == pCityID);
            actCity.isDeleted = true;
            db.SaveChanges();
        }

        public bool IsCustomersConsistent()
        {
            long ver = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Customers").tableVersion; 

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

        public bool IsPartsConsistent()
        {
            long ver = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Parts").tableVersion;

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

        public bool IsRentalsConsistent()
        {
            long ver = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Rentals").tableVersion;

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

        public bool IsServiceWorksheetsConsistent()
        {
            long ver = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "ServiceWorksheets").tableVersion;

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

        public bool IsToolsConsistent()
        {
            long ver = db.TableVersions.Single<TableVersions>(tv => tv.tableName == "Tools").tableVersion;

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

        public void DoBackup(string path = null)
        {
            if (Properties.Settings.Default.ServerIP == "." ||
                Properties.Settings.Default.ServerIP.ToLower() == "localhost" ||
                Properties.Settings.Default.ServerIP == string.Empty)
            {
                    
                if (path == null)
                {
                    db.DoBackup(Properties.Settings.Default.PrimaryBackupPath + @"\");
                    var fileName = Directory.GetFiles(Properties.Settings.Default.PrimaryBackupPath).Select(x => new FileInfo(x)).OrderByDescending(f => f.LastWriteTime).FirstOrDefault().Name;
                    // TODO: ...
                    File.Copy(Properties.Settings.Default.PrimaryBackupPath + @"\" + fileName, Properties.Settings.Default.SecondaryBackupPath + @"\" + fileName, true);
                }
                else
                {
                    db.DoBackup(path);
                }                    
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
            Customers customer = db.Customers.Include("Cities").Include("Contacts.Customers.Cities").SingleOrDefault(c => c.customerID == id);
            return customer;
        }

        public void UpdateCustomer(Customers customer)
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

        public void DeleteContact(Customers firm, Customers agent)
        {
                Contacts contactForDelete = db.Contacts.SingleOrDefault(c => c.firmID == firm.customerID && c.agentID == agent.customerID);
                if (contactForDelete != null)
                {
                    db.Contacts.DeleteObject(contactForDelete);
                    db.SaveChanges();
                }
        }

        public void AddContact(Customers firm, Customers agent)
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

        public List<Customers> GetContacts(Customers firm)
        {
                return db.Customers.Where(c => c.Contacts1.Any(ca => ca.firmID == firm.customerID)).ToList();
        }

        public Cities GetCityById(long id)
        {
                return db.Cities.SingleOrDefault(c => c.cityID == id);
        }

        public void AddCustomer(Customers customer)
        {
                db.Customers.AddObject(customer);
                db.SaveChanges();
        }

        public List<Customers> GetAllCustomers()
        {
                return db.Customers.Include("Cities").Include("Contacts.Customers.Cities").Where(c => c.isDeleted == false).ToList();
        }

        public List<Cities> GetAllCities()
        {
                return db.Cities.ToList();
        }

        public Contacts GetContactByFirmAndAgent(Customers firm, Customers agent)
        {
                return db.Contacts.SingleOrDefault(c => c.firmID == firm.customerID && c.agentID == agent.customerID);
        }

        public void DeleteCityById(long id)
        {
                Cities cityToDelete = db.Cities.SingleOrDefault(c => c.cityID == id);
                db.Cities.DeleteObject(cityToDelete);
                db.SaveChanges();
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        public DbSettings GetSettings()
        {
            return dbSettings;
        }

        public void UpdateTool(Tools tool)
        {
            Tools toolToUpdate = db.Tools.SingleOrDefault(t => t.toolID == tool.toolID);
            toolToUpdate.defaultDeposit = tool.defaultDeposit;
            toolToUpdate.fromDate = tool.fromDate;
            toolToUpdate.IDNumber = tool.IDNumber;
            toolToUpdate.isDeleted = tool.isDeleted;
            toolToUpdate.rentCounter = tool.rentCounter;
            toolToUpdate.rentPrice = tool.rentPrice;
            toolToUpdate.serialNumber = tool.serialNumber;
            toolToUpdate.toolManufacturer = tool.toolManufacturer;
            toolToUpdate.toolName = tool.toolName;
            toolToUpdate.toolStatusID = tool.toolStatusID;
            db.SaveChanges();
        }

        public List<Tools> GetAllTools()
        {
            return db.Tools.Include("ToolStatuses").Where(t => t.isDeleted == false).ToList();
        }

        public Rentals GetLastRentalByToolId(long toolId)
        {
            Rentals rental = db.Rentals.Include("Customers")
                             .Include("Customers1")
                             .Include("PayTypes")
                             .Include("Tools")
                            .Where(r => r.toolID == toolId).OrderByDescending(re => re.rentalID).FirstOrDefault();

            return rental;
        }

        public RentalGroups GetRentalGroupById(long id)
        {
            return db.RentalGroups.Include("Rentals").SingleOrDefault(rg => rg.groupID == id);
        }

        public void DeleteToolById(long id)
        {
            Tools toolToDelete = db.Tools.SingleOrDefault(t => t.toolID == id);
            toolToDelete.isDeleted = true;
            db.SaveChanges();
        }

        public void DeleteRentalById(long id)
        {
            Rentals rentalToDelete = db.Rentals.SingleOrDefault(r => r.rentalID == id);
            if (rentalToDelete != null)
            {
                db.Rentals.DeleteObject(rentalToDelete);
            }            
        }

        public void AddRentalGroup(RentalGroups rentalGroup)
        {
            db.RentalGroups.AddObject(rentalGroup);
            db.SaveChanges();
        }

        public void AddRental(Rentals rental)
        {
            db.Rentals.AddObject(rental);
            db.SaveChanges();
        }

        public List<PayTypes> GetPayTypes()
        {
            return db.PayTypes.ToList();
        }
    }
}
