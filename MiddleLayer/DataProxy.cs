using Common.Dependency_Injection;
using MiddleLayer.Representations;
using SQLConnectionLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MiddleLayer
{
    public sealed class DataProxy
    {
        Timer dbChangeCheckTimer;

        long customersVersion;
        long toolsVersion;

        private static readonly DataProxy _instance = new DataProxy();
        public static DataProxy Instance
        {
            get
            {
                return _instance;
            }
        }

        private DataProxy()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                customersVersion = dataSource.GetCustomersVersion();
                toolsVersion = dataSource.GetToolsVersion();
            }

            dbChangeCheckTimer = new Timer(10000);
            dbChangeCheckTimer.Elapsed += (s, a) =>
            {
                dbChangeCheckTimer.Stop();
                using (ISQLConnection dataSource = DataSource)
                {
                    var actVersion = dataSource.GetCustomersVersion();
                    if (customersVersion != actVersion)
                    {
                        customersVersion = actVersion;
                        OnCustomersChanged(EventArgs.Empty);
                    }

                    actVersion = dataSource.GetToolsVersion();
                    if (toolsVersion != actVersion)
                    {
                        toolsVersion = actVersion;
                        OnToolsChanged(EventArgs.Empty);
                    }
                }
                dbChangeCheckTimer.Start();
            };
            dbChangeCheckTimer.Start();
        }

        private ISQLConnection DataSource
        {
            get
            {
                return DIContainer.Instance.Get<SQLConnection>();
            }
        }

        public event EventHandler CustomersChanged;
        private void OnCustomersChanged(EventArgs e)
        {
            if (CustomersChanged != null)
            {
                CustomersChanged(null, e);
            }
        }

        public event EventHandler ToolsChanged;
        private void OnToolsChanged(EventArgs e)
        {
            if (ToolsChanged != null)
            {
                ToolsChanged(null, e);
            }
        }

        public DbSettings_Representation GetDbSettings()
        {
            return RepresentationConverter.convertDbSettings(DataSource.GetSettings());
        }

        public List<CustomerBase_Representation> GetAllCustomers()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                var customerList = dataSource.GetAllCustomers().Select(c => RepresentationConverter.convertCustomer(c));

                // Too much time!
                //foreach (var customer in customerList)
                //{
                //    // TODO: fill the contact list of the customer
                //    customer.contacts = GetContacts(customer);
                //}
                return customerList.ToList(); 
            }
        }
        public CustomerBase_Representation GetCustomerById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return RepresentationConverter.convertCustomer(dataSource.GetCustomerById(id)); 
            }
        }
        public long AddCustomer(CustomerBase_Representation customer)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.AddCustomer(RepresentationConverter.convertCustomer(customer)); 
            }
        }
        public void UpdateCustomer(CustomerBase_Representation customer)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.UpdateCustomer(RepresentationConverter.convertCustomer(customer)); 
            }
        }

        public ObservableCollection<CustomerBase_Representation> GetContacts(CustomerBase_Representation firm)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                List<Customers> contactList = dataSource.GetContacts(RepresentationConverter.convertCustomer(firm));
                List<CustomerBase_Representation> contactListRep = contactList.Select(c => RepresentationConverter.convertCustomer(c)).ToList();
                return new ObservableCollection<CustomerBase_Representation>(contactListRep); 
            }
        }
        public void DeleteContact(CustomerBase_Representation firm, CustomerBase_Representation agent)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.DeleteContact(RepresentationConverter.convertCustomer(firm), RepresentationConverter.convertCustomer(agent)); 
            }
        }
        public void AddContact(CustomerBase_Representation firm, CustomerBase_Representation agent)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.AddContact(RepresentationConverter.convertCustomer(firm), RepresentationConverter.convertCustomer(agent)); 
            }
        }

        public City_Representation GetCityById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return RepresentationConverter.convertCity(dataSource.GetCityById(id)); 
            }
        }
        public List<City_Representation> GetAllCities()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.GetAllCities().Select(c => RepresentationConverter.convertCity(c)).ToList(); 
            }
        }
        public void DeleteCityById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.DeleteCityById(id); 
            }
        }

        public List<Tool_Representation> GetAllTools()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.GetAllTools().Select(t => RepresentationConverter.convertTool(t)).ToList();
            }
        }
        public void UpdateTool(Tool_Representation tool)
        {
            using (ISQLConnection datasource = DataSource)
            {
                datasource.UpdateTool(RepresentationConverter.convertTool(tool));
            }
        }
        public void DeleteToolById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.DeleteToolById(id);
            }
        }

        public Rental_Representation GetLastRentalByToolId(long toolId)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return RepresentationConverter.convertRental(dataSource.GetLastRentalByToolId(toolId));
            }
        }
        public void AddRental(Rental_Representation rental)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.AddRental(RepresentationConverter.convertRental(rental));
            }
        }
        public void DeleteRentalById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.DeleteRentalById(id);
            }
        }

        public RentalGroup_Representation GetRentalGroupById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return RepresentationConverter.convertRentalGroup(dataSource.GetRentalGroupById(id));
            }
        }
        public void AddRentalGroup(RentalGroup_Representation rentalGroup)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                RentalGroups rentalGroupToAdd = RepresentationConverter.convertRentalGroup(rentalGroup);
                dataSource.AddRentalGroup(rentalGroupToAdd);

                foreach (Rental_Representation rental in rentalGroup.rentals)
                {
                    rental.group.id = rentalGroupToAdd.groupID;
                    AddRental(rental);
                }
            }
        }

        public List<PayType_Representation> GetPayTypes()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.GetPayTypes().Select(pt => RepresentationConverter.convertPayType(pt)).ToList();
            }
        }
    }
}
