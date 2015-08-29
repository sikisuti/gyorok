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
            return new CustomerBase_Representation()
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
                workplace = customer.workPlace
            };
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
