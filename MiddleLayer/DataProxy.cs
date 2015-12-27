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
        long rentalVersion;

        public int HoursPerDay { get; set; }

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
                rentalVersion = dataSource.GetRentalVersion();
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

                    actVersion = dataSource.GetRentalVersion();
                    if (rentalVersion != actVersion)
                    {
                        rentalVersion = actVersion;
                        OnRentalChanged(EventArgs.Empty);
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

        public event EventHandler RentalChanged;
        private void OnRentalChanged(EventArgs e)
        {
            if (RentalChanged != null)
            {
                RentalChanged(null, e);
            }
        }

        public DbSettingsRepresentation GetDbSettings()
        {
            return RepresentationConverter.convertDbSettings(DataSource.GetSettings());
        }

        public List<CustomerBaseRepresentation> GetAllCustomers()
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
        public CustomerBaseRepresentation GetCustomerById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return RepresentationConverter.convertCustomer(dataSource.GetCustomerById(id)); 
            }
        }
        public long AddCustomer(CustomerBaseRepresentation customer)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.AddCustomer(RepresentationConverter.convertCustomer(customer)); 
            }
        }
        public void UpdateCustomer(CustomerBaseRepresentation customer)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.UpdateCustomer(RepresentationConverter.convertCustomer(customer)); 
            }
        }
        public void DeleteCustomerById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.DeleteCustomerById(id);
            }
        }

        public ObservableCollection<CustomerBaseRepresentation> GetContacts(CustomerBaseRepresentation firm)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                List<Customers> contactList = dataSource.GetContacts(RepresentationConverter.convertCustomer(firm));
                List<CustomerBaseRepresentation> contactListRep = contactList.Select(c => RepresentationConverter.convertCustomer(c)).ToList();
                return new ObservableCollection<CustomerBaseRepresentation>(contactListRep); 
            }
        }
        public void DeleteContact(CustomerBaseRepresentation firm, CustomerBaseRepresentation agent)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.DeleteContact(RepresentationConverter.convertCustomer(firm), RepresentationConverter.convertCustomer(agent)); 
            }
        }
        public void AddContact(CustomerBaseRepresentation firm, CustomerBaseRepresentation agent)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.AddContact(RepresentationConverter.convertCustomer(firm), RepresentationConverter.convertCustomer(agent)); 
            }
        }

        public CityRepresentation GetCityById(long id)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return RepresentationConverter.convertCity(dataSource.GetCityById(id)); 
            }
        }
        public List<CityRepresentation> GetAllCities()
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

        public List<ToolRepresentation> GetAllTools()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.GetAllTools().Select(t => RepresentationConverter.convertTool(t)).ToList();
            }
        }
        public long AddTool(ToolRepresentation tool)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.AddTool(RepresentationConverter.convertTool(tool));
            }
        }
        public void UpdateTool(ToolRepresentation tool)
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

        public RentalRepresentation GetLastRentalByToolId(long toolId)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return RepresentationConverter.convertRental(dataSource.GetLastRentalByToolId(toolId));
            }
        }
        public List<RentalGroup_Representation> GetAllRentalGroups()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                List<RentalGroup_Representation> rtn = new List<RentalGroup_Representation>();
                dataSource.GetAllRentalGroups().ForEach(rg => rtn.Add(RepresentationConverter.convertRentalGroup(rg)));

                return rtn;
            }
        }
        public void AddRental(RentalRepresentation rental)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.AddRental(RepresentationConverter.convertRental(rental));
            }
        }
        public void UpdateRental(RentalRepresentation rental)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.UpdateRental(RepresentationConverter.convertRental(rental));
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
        public long AddRentalGroup(RentalGroup_Representation rentalGroup)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                RentalGroups rentalGroupToAdd = RepresentationConverter.convertRentalGroup(rentalGroup);
                //foreach (RentalRepresentation rental in rentalGroup.rentals)
                //{
                    //rentalGroupToAdd.Rentals.Add(RepresentationConverter.convertRental(rental));
                    //dataSource.UpdateTool(RepresentationConverter.convertTool(rental.tool));
                    //dataSource.UpdateCustomer(RepresentationConverter.convertCustomer(rental.customer));
                //}
                rentalGroup.id = dataSource.AddRentalGroup(rentalGroupToAdd);

                //foreach (RentalRepresentation rental in rentalGroup.rentals)
                //{
                //    rental.group = rentalGroup;
                //    AddRental(rental);
                //}

                return rentalGroup.id;
            }
        }
        public void UpdateRentalGroup(RentalGroup_Representation rentalGroup)
        {
            using (ISQLConnection dataSource = DataSource)
            {
                dataSource.UpdateRentalGroup(RepresentationConverter.convertRentalGroup(rentalGroup));
            }
        }

        public List<PayTypeRepresentation> GetPayTypes()
        {
            using (ISQLConnection dataSource = DataSource)
            {
                return dataSource.GetPayTypes().Select(pt => RepresentationConverter.convertPayType(pt)).ToList();
            }
        }
    }
}
