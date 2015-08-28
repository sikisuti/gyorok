using MiddleLayer.Representations;
using SQLConnectionLib;
using System;
using System.Collections.Generic;
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
        }

        public CustomerBase_Representation GetCustomerById(long id)
        {
            return RepresentationConverter.convertCustomer(dataSource.GetCustomerById(id));
        }
        public void UpdateCustomer(CustomerBase_Representation customer)
        {
            dataSource.UpdateCustomer(RepresentationConverter.convertCustomer(customer));
        }

        public void DeleteContact(CustomerBase_Representation firm, CustomerBase_Representation agent)
        {
            dataSource.DeleteContact(RepresentationConverter.convertCustomer(agent), RepresentationConverter.convertCustomer(contact));
        }
        public void AddContact(CustomerBase_Representation firm, CustomerBase_Representation agent)
        {
            dataSource.AddContact(RepresentationConverter.convertCustomer(firm), RepresentationConverter.convertCustomer(agent));
        }
    }
}
