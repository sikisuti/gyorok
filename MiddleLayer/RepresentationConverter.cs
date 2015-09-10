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
        public static DbSettings_Representation convertDbSettings(DbSettings settings)
        {
            return new DbSettings_Representation()
            {
                ApplicationName = settings.ApplicationName,
                InitialCatalog = settings.InitialCatalog,
                IntegratedSecurity = settings.IntegratedSecurity,
                MetaData = settings.MetaData,
                MultipleActiveResultSets = settings.MultipleActiveResultSets,
                Password = settings.Password,
                PersistSecurityInfo = settings.PersistSecurityInfo,
                primaryBackupPath = settings.primaryBackupPath,
                Provider = settings.Provider,
                secondaryBackupPath = settings.secondaryBackupPath,
                ServerInstanceName = settings.ServerInstanceName,
                ServerIP = settings.ServerIP,
                UserName = settings.UserName
            };
        }

        public static CustomerBase_Representation convertCustomer(Customers customer)
        {
            if (customer == null)
            {
                return null;
            }

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
                    customerRepresentation.contacts.Add(DataProxy.Instance.GetCustomerById(contact.agentID));
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
            var newCustomer = new Customers()
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

            if (customer.city != null)
            {
                newCustomer.cityID = customer.city.id;
            }

            return newCustomer;
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

        public static Tools convertTool(Tool_Representation tool)
        {
            Tools convertedTool = new Tools()
            {
                toolID = tool.id,
                defaultDeposit = tool.defaultDeposit,
                fromDate = tool.fromDate,
                IDNumber = tool.IDNumber,
                isDeleted = tool.isDeleted,
                rentCounter = tool.rentCounter,
                rentPrice = tool.rentPrice,
                serialNumber = tool.serialNumber,
                toolManufacturer = tool.toolManufacturer,
                toolName = tool.toolName,
                toolStatusID = tool.toolStatus.id
            };

            return convertedTool;
        }
        public static Tool_Representation convertTool(Tools tool)
        {
            Tool_Representation convertedTool = new Tool_Representation()
            {
                id = tool.toolID,
                defaultDeposit = tool.defaultDeposit,
                fromDate = tool.fromDate,
                IDNumber = tool.IDNumber,
                isDeleted = tool.isDeleted,
                rentCounter = tool.rentCounter,
                rentPrice = tool.rentPrice,
                serialNumber = tool.serialNumber,
                toolManufacturer = tool.toolManufacturer,
                toolName = tool.toolName,
                toolStatus = RepresentationConverter.convertToolStatus(tool.ToolStatuses)
            };

            return convertedTool;
        }

        public static ToolStatus_Representation convertToolStatus(ToolStatuses toolStatus)
        {
            return new ToolStatus_Representation()
            {
                id = toolStatus.toolStatusID,
                statusName = toolStatus.statusName
            };
        }

        public static Rental_Representation convertRental(Rentals rental)
        {
            Rental_Representation convertedRental = new Rental_Representation()
            {
                actualPrice = rental.actualPrice,
                contact = convertCustomer(rental.Customers1),
                customer = convertCustomer(rental.Customers),
                discount = rental.discount,
                //group = DataProxy.Instance.GetRentalGroupById(rental.groupID),    - Couses infinite cycle
                isClean = rental.isClean,
                id = rental.rentalID,
                isPaid = rental.isPaid,
                payType = convertPayType(rental.PayTypes),
                rentalEnd = rental.rentalEnd,
                rentalRealEnd = rental.rentalRealEnd,
                rentalStart = rental.rentalStart,
                tool = convertTool(rental.Tools)
            };

            //int selfIndex = convertedRental.group.rentals.IndexOf(convertedRental.group.rentals.SingleOrDefault(r => r.id == convertedRental.id));
            //convertedRental.group.rentals[selfIndex] = convertedRental;

            return convertedRental;
        }
        public static Rentals convertRental(Rental_Representation rental)
        {
            Rentals convertedRental = new Rentals()
            {
                actualPrice = rental.actualPrice,
                contactID = rental.contact.id,
                customerID = rental.customer.id,
                discount = (float)rental.discount,
                isClean = rental.isClean,
                rentalID = rental.id,
                isPaid = rental.isPaid,
                payTypeID = rental.payType.id,
                rentalEnd = rental.rentalEnd,
                rentalRealEnd = rental.rentalRealEnd,
                rentalStart = rental.rentalStart,
                toolID = rental.tool.id
            };

            return convertedRental;
        }

        public static RentalGroup_Representation convertRentalGroup(RentalGroups rentalGroup)
        {
            RentalGroup_Representation convertedRentalGroup = new RentalGroup_Representation()
            {

            };

            foreach (Rentals rental in rentalGroup.Rentals)
            {
                convertedRentalGroup.rentals.Add(convertRental(rental));
            }

            return convertedRentalGroup;
        }
        public static RentalGroups convertRentalGroup(RentalGroup_Representation rentalGroup)
        {
            RentalGroups convertedRentalGroup = new RentalGroups()
            {
                comment = rentalGroup.comment,
                deposit = rentalGroup.deposit,
                isOpen = rentalGroup.isOpen
            };

            return convertedRentalGroup;
        }

        public static PayType_Representation convertPayType(PayTypes payType)
        {
            PayType_Representation convertedPayType = new PayType_Representation()
            {
                id = payType.payTypeID,
                payTypeName = payType.payTypeName
            };

            return convertedPayType;
        }
    }
}
