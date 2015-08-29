using MiddleLayer.Representations;
using SQLConnectionLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public sealed class DataProxy
    {
        private static readonly DataProxy _instance = new DataProxy();
        public static DataProxy Instance
        {
            get
            {
                return _instance;
            }
        }

        ISQLConnection dataSource;

        private DataProxy()
        {
            dataSource = new TestDatabase();
        }

        public CustomerBase_Representation GetCustomerById(long id)
        {
            return RepresentationConverter.convertCustomer(dataSource.GetCustomerById(id));
        }
        public void UpdateCustomer(CustomerBase_Representation customer)
        {
            dataSource.UpdateCustomer(RepresentationConverter.convertCustomer(customer));
        }

        public ObservableCollection<CustomerBase_Representation> GetContacts(CustomerBase_Representation firm)
        {
            return new ObservableCollection<CustomerBase_Representation>(dataSource.GetContacts(RepresentationConverter.convertCustomer(firm)).Select(c => RepresentationConverter.convertCustomer(c)));
        }
        public void DeleteContact(CustomerBase_Representation firm, CustomerBase_Representation agent)
        {
            dataSource.DeleteContact(RepresentationConverter.convertCustomer(firm), RepresentationConverter.convertCustomer(agent));
        }
        public void AddContact(CustomerBase_Representation firm, CustomerBase_Representation agent)
        {
            dataSource.AddContact(RepresentationConverter.convertCustomer(firm), RepresentationConverter.convertCustomer(agent));
        }

        public City_Representation GetCityById(long id)
        {
            return RepresentationConverter.convertCity(dataSource.GetCityById(id));
        }
    }
}
