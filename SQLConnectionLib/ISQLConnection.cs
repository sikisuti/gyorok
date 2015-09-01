﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace SQLConnectionLib
{
    public interface ISQLConnection
    {
        void DoBackup(string path = null);
        void DoRestore(string path);

        Customers GetCustomerById(long id);
        List<Customers> GetAllCustomers();
        void AddCustomer(Customers customer);
        void UpdateCustomer(Customers customer);

        // TODO: Is DetailedCustomer needed?
        //List<DetailedCustomers> GetDetailedCustomers();

        List<Customers> GetContacts(Customers firm);
        Contacts GetContactByFirmAndAgent(Customers firm, Customers agent);
        void DeleteContact(Customers firm, Customers agent);
        void AddContact(Customers firm, Customers agent);

        Cities GetCityById(long id);
        List<Cities> GetAllCities();
    }
}
