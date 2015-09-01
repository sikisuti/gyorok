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
    public static class RepresentationConverter
    {
        public static CustomerBase_Representation convertCustomer(Customers customer)
        {
            // TODO: Implement concurency checking (rowVersion)
            CustomerBase_Representation customerRepresentation = new CustomerBase_Representation()
            {
                id = customer.customerID,
                birthDate = customer.birthDate,
                comment = customer.comment,
                customerAddress = customer.customerAddress,
                customerName = customer.customerName,
                customerPhone = customer.customerPhone,
                defaultDiscount = customer.defaultDiscount,
                IDNumber = customer.IDNumber,
                isDeleted = customer.isDeleted,
                isFirm = customer.isFirm,
                mothersName = customer.mothersName,
                problems = customer.problems,
                rentCounter = customer.rentCounter,
                serviceCounter = customer.serviceCounter,
                workplace = customer.workPlace,
                contacts = new ObservableCollection<CustomerBase_Representation>()
            };

            if (customer.Contacts != null)
            {
                foreach (Contacts contact in customer.Contacts)
                {
                    customerRepresentation.contacts.Add(convertCustomer(contact.Customers1));
                }
            }

            if (customer.Cities != null)
            {
                customerRepresentation.city = convertCity(customer.Cities);
            }

            return customerRepresentation;
        }
        public static Customers convertCustomer(CustomerBase_Representation customer)
        {
            // TODO: Implement concurency checking (rowVersion)
            return new Customers()
            {
                customerID = customer.id,
                birthDate = customer.birthDate,
                comment = customer.comment,
                customerAddress = customer.customerAddress,
                customerName = customer.customerName,
                customerPhone = customer.customerPhone,
                defaultDiscount = customer.defaultDiscount,
                IDNumber = customer.IDNumber,
                isDeleted = customer.isDeleted,
                isFirm = customer.isFirm,
                mothersName = customer.mothersName,
                problems = customer.problems,
                rentCounter = customer.rentCounter,
                serviceCounter = customer.serviceCounter,
                workPlace = customer.workplace
            };
        }

        // TODO: Is it needed?
        //public static DetailedCustomer_Representatiton convertDetailedCustomer(DetailedCustomers customer)
        //{
        //    return new DetailedCustomer_Representatiton()
        //    {
        //        birthDate = customer.birthDate,
        //        city = customer.city,
        //        cityDeleted = customer.cityDeleted,
        //        comment = customer.comment,
        //        customerAddress = customer.customerAddress,
        //        customerName = customer.customerName,
        //        customerPhone = customer.customerPhone,
        //        defaultDiscount = customer.defaultDiscount,
        //        id = customer.customerID,
        //        IDNumber = customer.IDNumber,
        //        isFirm = customer.isFirm,
        //        mothersName = customer.mothersName,
        //        postalCode = customer.postalCode,
        //        problems = customer.problems,
        //        rentCounter = customer.rentCounter,
        //        serviceCounter = customer.serviceCounter,
        //        workplace = customer.workPlace
        //    };
        //}

        public static City_Representation convertCity(Cities city)
        {
            return new City_Representation()
            {
                city = city.city,
                id = city.cityID,
                isDeleted = city.isDeleted,
                postalCode = city.postalCode
            };
        }
    }
}
