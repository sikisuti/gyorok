using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace SQLConnectionLib
{
    public interface ISQLConnection : IDisposable
    {
        void DoBackup(string path = null);
        void DoRestore(string path);
        DbSettings GetSettings();

        long GetCustomersVersion();

        Customers GetCustomerById(long id);
        List<Customers> GetAllCustomers();
        long AddCustomer(Customers customer);
        void UpdateCustomer(Customers customer);

        List<Customers> GetContacts(Customers firm);
        Contacts GetContactByFirmAndAgent(Customers firm, Customers agent);
        void DeleteContact(Customers firm, Customers agent);
        void AddContact(Customers firm, Customers agent);

        Cities GetCityById(long id);
        List<Cities> GetAllCities();
        void DeleteCityById(long id);

        List<Tools> GetAllTools();
        void UpdateTool(Tools tool);
        void DeleteToolById(long id);

        Rentals GetLastRentalByToolId(long toolId);
        void AddRental(Rentals rental);
        void DeleteRentalById(long id);

        RentalGroups GetRentalGroupById(long id);
        void AddRentalGroup(RentalGroups rentalGroup);

        List<PayTypes> GetPayTypes();
    }
}
