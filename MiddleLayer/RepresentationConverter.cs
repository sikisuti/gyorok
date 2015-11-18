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
        public static DbSettingsRepresentation convertDbSettings(DbSettings settings)
        {
            return new DbSettingsRepresentation()
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

        public static CustomerBaseRepresentation convertCustomer(Customers customer)
        { 
            if (customer == null) return null;

            CustomerBaseRepresentation convertedCustomer;

            if (customer.isFirm)
                convertedCustomer = new FirmRepresentation();
            else
                convertedCustomer = new PersonRepresentation();

            // TODO: Implement concurency checking (rowVersion)
            convertedCustomer.id = customer.customerID;
            convertedCustomer.comment = customer.comment;
            convertedCustomer.customerAddress = customer.customerAddress;
            convertedCustomer.customerName = customer.customerName;
            convertedCustomer.customerPhone = customer.customerPhone;
            convertedCustomer.defaultDiscount = customer.defaultDiscount == null ? 0 : customer.defaultDiscount ?? 0 / 100;
            convertedCustomer.IDNumber = customer.IDNumber;
            convertedCustomer.isDeleted = customer.isDeleted;
            convertedCustomer.isFirm = customer.isFirm;
            convertedCustomer.problems = customer.problems;
            convertedCustomer.rentCounter = customer.rentCounter;
            convertedCustomer.serviceCounter = customer.serviceCounter;
            if (customer.isFirm)
            {
                ((FirmRepresentation)convertedCustomer).contacts = new ObservableCollection<CustomerBaseRepresentation>();
                if (customer.Contacts != null)
                {
                    foreach (Contacts contact in customer.Contacts)
                    {
                        ((FirmRepresentation)convertedCustomer).contacts.Add(DataProxy.Instance.GetCustomerById(contact.agentID));
                    }
                }
            }
            else
            {
                ((PersonRepresentation)convertedCustomer).birthDate = customer.birthDate;
                ((PersonRepresentation)convertedCustomer).mothersName = customer.mothersName;
                ((PersonRepresentation)convertedCustomer).workplace = customer.workPlace;
            }


            if (customer.Cities != null)
            {
                convertedCustomer.city = convertCity(customer.Cities);
            }

            return convertedCustomer;
        }
        public static Customers convertCustomer(CustomerBaseRepresentation customer)
        {
            // TODO: Implement concurency checking (rowVersion)
            var convertedCustomer = new Customers()
            {
                customerID = customer.id,
                comment = customer.comment,
                customerAddress = customer.customerAddress,
                customerName = customer.customerName,
                customerPhone = customer.customerPhone,
                defaultDiscount = customer.defaultDiscount * 100,
                IDNumber = customer.IDNumber,
                isDeleted = customer.isDeleted,
                isFirm = customer.isFirm,
                problems = customer.problems,
                rentCounter = customer.rentCounter,
                serviceCounter = customer.serviceCounter
            };

            if (customer.GetType() == typeof(PersonRepresentation))
            {
                convertedCustomer.birthDate = ((PersonRepresentation)customer).birthDate;
                convertedCustomer.mothersName = ((PersonRepresentation)customer).mothersName;
                convertedCustomer.workPlace = ((PersonRepresentation)customer).workplace;
            }

            if (customer.city != null)
            {
                convertedCustomer.cityID = customer.city.id;
            }

            return convertedCustomer;
        }

        public static CityRepresentation convertCity(Cities city)
        {
            return new CityRepresentation()
            {
                city = city.city,
                id = city.cityID,
                isDeleted = city.isDeleted,
                postalCode = city.postalCode
            };
        }

        public static Tools convertTool(ToolRepresentation tool)
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
        public static ToolRepresentation convertTool(Tools tool)
        {
            ToolRepresentation convertedTool = new ToolRepresentation()
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

        public static ToolStatusRepresentation convertToolStatus(ToolStatuses toolStatus)
        {
            return new ToolStatusRepresentation()
            {
                id = toolStatus.toolStatusID,
                statusName = toolStatus.statusName
            };
        }

        public static RentalRepresentation convertRental(Rentals rental)
        {
            RentalRepresentation convertedRental = new RentalRepresentation()
            {
                actualPrice = rental.actualPrice,
                contact = convertCustomer(rental.Customers1),
                customer = convertCustomer(rental.Customers),
                discount = rental.discount / 100,
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
        public static Rentals convertRental(RentalRepresentation rental)
        {
            Rentals convertedRental = new Rentals()
            {
                actualPrice = rental.actualPrice,
                customerID = rental.customer.id,
                discount = (float)rental.discount * 100,
                isClean = rental.isClean,
                rentalID = rental.id,
                rentalEnd = rental.rentalEnd,
                isPaid = rental.isPaid,
                payTypeID = rental.payType.id,
                rentalRealEnd = rental.rentalRealEnd,
                rentalStart = rental.rentalStart,
                toolID = rental.tool.id,
                groupID = rental.group.id
            };

            convertedRental.Tools = convertTool(rental.tool);

            if (rental.customer.isFirm && rental.contact != null)
            {
                convertedRental.contactID = rental.contact.id;
            }

            return convertedRental;
        }

        public static RentalGroup_Representation convertRentalGroup(RentalGroups rentalGroup)
        {
            RentalGroup_Representation convertedRentalGroup = new RentalGroup_Representation()
            {
                comment = rentalGroup.comment,
                deposit = rentalGroup.deposit ?? 0,
                id = rentalGroup.groupID,
                isOpen = rentalGroup.isOpen
            };

            foreach (Rentals rental in rentalGroup.Rentals)
            {
                RentalRepresentation rentalToAdd = convertRental(rental);
                rentalToAdd.group = convertedRentalGroup;
                convertedRentalGroup.rentals.Add(rentalToAdd);
            }

            return convertedRentalGroup;
        }
        public static RentalGroups convertRentalGroup(RentalGroup_Representation rentalGroup)
        {
            RentalGroups convertedRentalGroup = new RentalGroups()
            {
                groupID = rentalGroup.id,
                comment = rentalGroup.comment,
                deposit = rentalGroup.deposit,
                isOpen = rentalGroup.isOpen
            };

            return convertedRentalGroup;
        }

        public static PayTypeRepresentation convertPayType(PayTypes payType)
        {
            PayTypeRepresentation convertedPayType = new PayTypeRepresentation()
            {
                id = payType.payTypeID,
                payTypeName = payType.payTypeName
            };

            return convertedPayType;
        }
    }
}
