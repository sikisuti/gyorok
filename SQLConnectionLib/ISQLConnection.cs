using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace SQLConnectionLib
{
    public interface ISQLConnection
    {
        Customers GetCustomerById(long id);
        void UpdateCustomer(Customers customer);

        List<Customers> GetContacts(Customers firm);
        void DeleteContact(Customers firm, Customers agent);
        void AddContact(Customers firm, Customers agent);

        Cities GetCityById(long id);
    }
}
