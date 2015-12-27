using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.Objects;

namespace SQLConnectionLib
{
    public interface ISQLConnection : IDisposable
    {
        void DoBackup(string path = null);
        void DoRestore(string path);
        DbSettings GetSettings();

        long GetCustomersVersion();
        long GetToolsVersion();
        long GetRentalVersion();

        Customers GetCustomerById(long id);
        List<Customers> GetAllCustomers();
        long AddCustomer(Customers customer);
        void UpdateCustomer(Customers customer);
        void DeleteCustomerById(long id);

        List<Customers> GetContacts(Customers firm);
        Contacts GetContactByFirmAndAgent(Customers firm, Customers agent);
        void DeleteContact(Customers firm, Customers agent);
        void AddContact(Customers firm, Customers agent);

        Cities GetCityById(long id);
        List<Cities> GetAllCities();
        void DeleteCityById(long id);

        List<Tools> GetAllTools();
        long AddTool(Tools tool);
        void UpdateTool(Tools tool);
        void DeleteToolById(long id);

        Rentals GetLastRentalByToolId(long toolId);
        void AddRental(Rentals rental);
        void UpdateRental(Rentals rental);
        void DeleteRentalById(long id);

        RentalGroups GetRentalGroupById(long id);
        List<RentalGroups> GetAllRentalGroups();
        long AddRentalGroup(RentalGroups rentalGroup);
        void UpdateRentalGroup(RentalGroups rentalGroup);

        List<PayTypes> GetPayTypes();
    }
}
