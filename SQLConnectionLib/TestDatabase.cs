using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLConnectionLib
{
    public class TestDatabase : ISQLConnection
    {
        List<Customers> testCustomers = new List<Customers>();

        public TestDatabase()
        {
            testCustomers.Add(new Customers() { customerID = 1, birthDate = new DateTime(1980, 3, 2), customerAddress = "Almafa utca 32.", customerName = "Teszt Béla Károly", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" });
            testCustomers.Add(new Customers() { customerID = 2, birthDate = new DateTime(1980, 3, 2), customerAddress = "Almafa utca 32.", customerName = "Gipsz Jakab", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" });
            testCustomers.Add(new Customers() { customerID = 3, birthDate = new DateTime(1980, 3, 2), customerAddress = "Almafa utca 32.", customerName = "Halocsekerentyű Leokádia", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" });
            testCustomers.Add(new Customers() { customerID = 4, birthDate = new DateTime(1980, 3, 2), customerAddress = "Almafa utca 32.", customerName = "Vaszlavics Ilona", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" });
            testCustomers.Add(new Customers() { customerID = 5, birthDate = new DateTime(1980, 3, 2), customerAddress = "Almafa utca 32.", customerName = "Kotyogó Júlia", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" });

            testCustomers.Add(new Customers() { customerID = 100, customerAddress = "Almafa utca 32.", customerName = "Óvárosi Csavarbolt", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = true, serviceCounter = 0, rentCounter = 0 });
            testCustomers.Add(new Customers() { customerID = 101, customerAddress = "Almafa utca 32.", customerName = "ISD Dunaferr", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = true, serviceCounter = 0, rentCounter = 0 });
            testCustomers.Add(new Customers() { customerID = 102, customerAddress = "Almafa utca 32.", customerName = "Hankook", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = true, serviceCounter = 0, rentCounter = 0 });
        }

        public void AddContact(Customers firm, Customers agent)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(Customers firm, Customers agent)
        {
            throw new NotImplementedException();
        }

        public void DoBackup(string path = null)
        {
            throw new NotImplementedException();
        }

        public void DoRestore(string path)
        {
            throw new NotImplementedException();
        }

        public Cities GetCityById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Customers> GetContacts(Customers firm)
        {
            return new List<Customers>() { testCustomers.Single(c => c.customerID == 1), testCustomers.Single(c => c.customerID == 2) };
        }

        public Customers GetCustomerById(long id)
        {
            return testCustomers.SingleOrDefault(c => c.customerID == id);
        }

        public void UpdateCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }
    }
}
