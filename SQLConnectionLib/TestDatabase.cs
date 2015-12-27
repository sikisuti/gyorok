using System;
using System.Collections.Generic;
//using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace SQLConnectionLib
{
    public class TestDatabase : ISQLConnection
    {
        List<Cities> testCities = new List<Cities>();
        List<Customers> testCustomers = new List<Customers>();
        List<DetailedCustomers> testDetailedCustomers = new List<DetailedCustomers>();
        List<Contacts> testContacts = new List<Contacts>();

        public TestDatabase()
        {
            Cities city01 = new Cities() { cityID = 1, city = "Dunaújváros", isDeleted = false, postalCode = "2400" };
            testCities.Add(city01);
            Cities city02 = new Cities() { cityID = 2, city = "Győr", isDeleted = false, postalCode = "1234" };
            testCities.Add(city02);
            Cities city03 = new Cities() { cityID = 3, city = "Balassagyarmat", isDeleted = false, postalCode = "5847" };
            testCities.Add(city03);

            Customers person01 = new Customers() { customerID = 1, birthDate = new DateTime(1980, 3, 2), cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Teszt Béla Károly", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" };
            testCustomers.Add(person01);
            Customers person02 = new Customers() { customerID = 2, birthDate = new DateTime(1980, 3, 2), cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Gipsz Jakab", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" };
            testCustomers.Add(person02);
            Customers person03 = new Customers() { customerID = 3, birthDate = new DateTime(1980, 3, 2), cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Halocsekerentyű Leokádia", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" };
            testCustomers.Add(person03);
            Customers person04 = new Customers() { customerID = 4, birthDate = new DateTime(1980, 3, 2), cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Vaszlavics Ilona", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" };
            testCustomers.Add(person04);
            Customers person05 = new Customers() { customerID = 5, birthDate = new DateTime(1980, 3, 2), cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Kotyogó Júlia", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr" };
            testCustomers.Add(person05);

            Customers firm01 = new Customers() { customerID = 100, cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Óvárosi Csavarbolt", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = true,  serviceCounter = 0, rentCounter = 0 };
            testCustomers.Add(firm01);
            Customers firm02 = new Customers() { customerID = 101, cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "ISD Dunaferr", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = true, serviceCounter = 0, rentCounter = 0 };
            testCustomers.Add(firm02);
            Customers firm03 = new Customers() { customerID = 102, cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Hankook", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isDeleted = false, isFirm = true, serviceCounter = 0, rentCounter = 0 };
            testCustomers.Add(firm03);

            Contacts firm01contact01 = new Contacts() { contactID = 1, firmID = firm01.customerID, agentID = person02.customerID };
            testContacts.Add(firm01contact01);
            Contacts firm02contact01 = new Contacts() { contactID = 2, firmID = firm02.customerID, agentID = person01.customerID };
            testContacts.Add(firm02contact01);
            Contacts firm02contact02 = new Contacts() { contactID = 3, firmID = firm02.customerID, agentID = person04.customerID };
            testContacts.Add(firm02contact02);

            testDetailedCustomers.Add(new DetailedCustomers() { customerID = 5, birthDate = new DateTime(1980, 3, 2), cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Kotyogó Júlia", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isFirm = false, mothersName = "Kovács Laura", serviceCounter = 0, rentCounter = 0, workPlace = "ISD Dunaferr", city = "Dunaújváros", cityDeleted = false, customerDeleted = false, postalCode = "2400" });

            testDetailedCustomers.Add(new DetailedCustomers() { customerID = 102, cityID = city01.cityID, customerAddress = "Almafa utca 32.", customerName = "Hankook", customerPhone = "+36/30/517-6289", IDNumber = "JD245874", isFirm = true, serviceCounter = 0, rentCounter = 0, city = "Dunaújváros", cityDeleted = false, customerDeleted = false, postalCode = "2400" });
        }

        public List<Customers> GetAllCustomers()
        {
            return testCustomers;
        }
        public long AddCustomer(Customers customer)
        {
            testCustomers.Add(customer);
            return customer.customerID;
        }

        public List<Customers> GetContacts(Customers firm)
        {
            List<long> agentIDList = testContacts.Where(c => c.firmID == firm.customerID).Select(co => co.agentID).ToList();
            return testCustomers.Where(c => agentIDList.Contains(c.customerID)).ToList();
        }
        public void AddContact(Customers firm, Customers agent)
        {
            long maxID = testContacts.Max(c => c.contactID);
            testContacts.Add(new Contacts() { contactID = maxID + 1, firmID = firm.customerID, agentID = agent.customerID });
        }
        public void DeleteContact(Customers firm, Customers agent)
        {
            Contacts contactToDelete = GetContactByFirmAndAgent(firm, agent);
            if (contactToDelete != null)
            {
                testContacts.Remove(contactToDelete);
            }
        }

        public Cities GetCityById(long id)
        {
            return testCities.SingleOrDefault(c => c.cityID == id);
        }

        public void DoBackup(string path = null)
        {
            throw new NotImplementedException();
        }
        public void DoRestore(string path)
        {
            throw new NotImplementedException();
        }

        public Customers GetCustomerById(long id)
        {
            Customers cust = testCustomers.SingleOrDefault(c => c.customerID == id);
            cust.Cities = testCities.SingleOrDefault(c => c.cityID == cust.cityID);

            return cust;
        }

        public List<DetailedCustomers> GetDetailedCustomers()
        {
            return testDetailedCustomers;
        }

        public void UpdateCustomer(Customers customer)
        {
            testCustomers[testCustomers.FindIndex(c => c.customerID == customer.customerID)] = customer;
        }

        public List<Cities> GetAllCities()
        {
            return testCities;
        }

        public Contacts GetContactByFirmAndAgent(Customers firm, Customers agent)
        {
            return testContacts.SingleOrDefault(c => c.firmID == firm.customerID && c.agentID == agent.customerID);
        }

        public void DeleteCityById(long id)
        {
            testCities.Remove(testCities.SingleOrDefault(c => c.cityID == id));
        }

        public void Dispose()
        {
        }

        public DbSettings GetSettings()
        {
            DbSettings dbSettings = new DbSettings();
            dbSettings.ServerIP = "Teszt adatbázis";
            return dbSettings;
        }

        public void UpdateTool(Tools tool)
        {
            throw new NotImplementedException();
        }

        public List<Tools> GetAllTools()
        {
            throw new NotImplementedException();
        }

        public Rentals GetLastRentalByToolId(long toolId)
        {
            throw new NotImplementedException();
        }

        public RentalGroups GetRentalGroupById(long id)
        {
            throw new NotImplementedException();
        }

        public void DeleteToolById(long id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRentalById(long id)
        {
            throw new NotImplementedException();
        }

        public long AddRentalGroup(RentalGroups rentalGroup)
        {
            throw new NotImplementedException();
        }

        public void AddRental(Rentals rental)
        {
            throw new NotImplementedException();
        }

        public List<PayTypes> GetPayTypes()
        {
            throw new NotImplementedException();
        }

        public long GetCustomersVersion()
        {
            throw new NotImplementedException();
        }

        public long GetToolsVersion()
        {
            throw new NotImplementedException();
        }

        public long AddTool(Tools tool)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomerById(long id)
        {
            throw new NotImplementedException();
        }

        public List<RentalGroups> GetAllRentalGroups()
        {
            throw new NotImplementedException();
        }

        public long GetRentalVersion()
        {
            throw new NotImplementedException();
        }

        public void UpdateRentalGroup(RentalGroups rentalGroup)
        {
            throw new NotImplementedException();
        }

        public void UpdateRental(Rentals rental)
        {
            throw new NotImplementedException();
        }
    }
}
