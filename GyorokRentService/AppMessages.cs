using MiddleLayer.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using SQLConnectionLib;


namespace GyorokRentService
{
    public static class AppMessages
    {
        public static class CustomerListModified
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.customerListModified);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.customerListModified, action);
            }
        }

        public static class CustomerToSelect
        {
            public static void Send(CustomerBase_Representation selectedCustomer)
            {
                Messenger.Default.Send(selectedCustomer, MessageTypes.customerToSelect);
            }

            public static void Register(object recipient, Action<CustomerBase_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.customerToSelect, action);
            }
        }

        public static class CustomerToRent
        {
            public static void Send(CustomerBase_Representation selectedCustomer)
            {
                Messenger.Default.Send(selectedCustomer, MessageTypes.customerToRent);
            }

            public static void Register(object recipient, Action<CustomerBase_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.customerToRent, action);
            }
        }

        public static class CustomerModified
        {
            public static void Send(CustomerBase_Representation c)
            {
                Messenger.Default.Send(c, MessageTypes.customerModified);
            }

            public static void Register(object recipient, Action<CustomerBase_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.customerModified, action);
            }
        }

        public static class CloseNewCustomerWindow
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.closeNewCustomerWindow);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.closeNewCustomerWindow, action);
            }
        }

        public static class ContactPersonToSelect
        {
            public static void Send(CustomerBase_Representation selectedContactPerson)
            {
                Messenger.Default.Send(selectedContactPerson, MessageTypes.contactPersonToSelect);
            }

            public static void Register(object recipient, Action<CustomerBase_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.contactPersonToSelect, action);
            }
        }

        public static class ContactSelected
        {
            public static void Send(CustomerBase_Representation selectedContactPerson)
            {
                Messenger.Default.Send(selectedContactPerson, MessageTypes.contactSelected);
            }

            public static void Register(object recipient, Action<CustomerBase_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.contactSelected, action);
            }
        }

        public static class ToolListModified
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.toolListModified);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.toolListModified, action);
            }
        }

        public static class ToolToSelect
        {
            public static void Send(Tools selectedTool)
            {
                Messenger.Default.Send(selectedTool, MessageTypes.toolToSelect);
            }

            public static void Register(object recipient, Action<Tools> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.toolToSelect, action);
            }
        }

        public static class ToolModified
        {
            public static void Send(Tool_Representation t)
            {
                Messenger.Default.Send(t, MessageTypes.toolModified);
            }

            public static void Register(object recipient, Action<Tool_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.toolModified, action);
            }
        }

        public static class CustomerExpandChanged
        {
            public static void Send(bool e)
            {
                Messenger.Default.Send(e, MessageTypes.customerExpandChanged);
            }

            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.customerExpandChanged, action);
            }
        }

        public static class ToolExpandChanged
        {
            public static void Send(bool e)
            {
                Messenger.Default.Send(e, MessageTypes.toolExpandChanged);
            }

            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.toolExpandChanged, action);
            }
        }

        public static class RentExpandChanged
        {
            public static void Send(bool e)
            {
                Messenger.Default.Send(e, MessageTypes.rentExpandChanged);
            }

            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.rentExpandChanged, action);
            }
        }

        public static class NewRentAdded
        {
            public static void Send(Rentals r)
            {
                Messenger.Default.Send(r, MessageTypes.newRentAdded);
            }

            public static void Register(object recipient, Action<Rentals> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.newRentAdded, action);
            }
        }

        public static class NewWorksheetAdded
        {
            public static void Send(ServiceWorksheets sw)
            {
                Messenger.Default.Send(sw, MessageTypes.newWorksheetAdded);
            }

            public static void Register(object recipient, Action<ServiceWorksheets> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.newWorksheetAdded, action);
            }
        }

        public static class NewRentRemoved
        {
            public static void Send(Rental_Representation r)
            {
                Messenger.Default.Send(r, MessageTypes.newRentRemoved);
            }

            public static void Register(object recipient, Action<Rental_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.newRentRemoved, action);
            }
        }

        public static class NewWorksheetRemoved
        {
            public static void Send(ServiceWorksheets sw)
            {
                Messenger.Default.Send(sw, MessageTypes.newWorksheetRemoved);
            }

            public static void Register(object recipient, Action<ServiceWorksheets> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.newWorksheetRemoved, action);
            }
        }

        public static class RentGroupClosed
        {
            public static void Send(List<Rentals> r)
            {
                Messenger.Default.Send(r, MessageTypes.rentGroupClosed);
            }

            public static void Register(object recipient, Action<List<Rentals>> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.rentGroupClosed, action);
            }
        }

        public static class ServiceGroupClosed
        {
            public static void Send(List<ServiceWorksheets> sw)
            {
                Messenger.Default.Send(sw, MessageTypes.serviceGroupClosed);
            }

            public static void Register(object recipient, Action<List<ServiceWorksheets>> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.serviceGroupClosed, action);
            }
        }

        public static class GroupToSelect
        {
            public static void Send(ViewModel.GroupList selectedGroup)
            {
                Messenger.Default.Send(selectedGroup, MessageTypes.groupToSelect);
            }

            public static void Register(object recipient, Action<ViewModel.GroupList> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.groupToSelect, action);
            }
        }

        public static class RentalPaid
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.rentalPaid);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.rentalPaid, action);
            }
        }

        public static class PartAdded
        {
            public static void Send(Parts p)
            {
                Messenger.Default.Send(p, MessageTypes.partAdded);
            }

            public static void Register(object recipient, Action<Parts> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.partAdded, action);
            }
        }

        public static class PartModified
        {
            public static void Send(Parts p)
            {
                Messenger.Default.Send(p, MessageTypes.partModified);
            }

            public static void Register(object recipient, Action<Parts> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.partModified, action);
            }
        }

        public static class SettingsSaved
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.settingsSaved);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.settingsSaved, action);
            }
        }

        public static class CityListModified
        {
            public static void Send(long cityID)
            {
                Messenger.Default.Send(cityID, MessageTypes.cityListModified);
            }

            public static void Register(object recipient, Action<long> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.cityListModified, action);
            }
        }

        public static class CityToSelect
        {
            public static void Send(City_Representation selectedCustomer)
            {
                Messenger.Default.Send(selectedCustomer, MessageTypes.cityToSelect);
            }

            public static void Register(object recipient, Action<City_Representation> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.cityToSelect, action);
            }
        }

        public static class PartsChanged
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.partsChanged);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.partsChanged, action);
            }
        }

        public static class RentalsChanged
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.rentalsChanged);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.rentalsChanged, action);
            }
        }

        public static class ServiceWorksheetsChanged
        {
            public static void Send(string s)
            {
                Messenger.Default.Send(s, MessageTypes.serviceWorksheetsChanged);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.serviceWorksheetsChanged, action);
            }
        }
    }

    public enum MessageTypes
    {
        customerListModified,
        customerToSelect,
        customerToRent,
        customerModified,
        closeNewCustomerWindow,
        contactPersonToSelect,
        toolListModified,
        toolToSelect,
        toolModified,
        customerExpandChanged,
        toolExpandChanged,
        contactSelected,
        newRentAdded,
        newWorksheetAdded,
        newRentRemoved,
        newWorksheetRemoved,
        rentGroupClosed,
        serviceGroupClosed,
        rentExpandChanged,
        groupToSelect,
        rentalPaid,
        partAdded,
        partModified,
        settingsSaved,
        cityListModified,
        cityToSelect,
        partsChanged,
        rentalsChanged,
        serviceWorksheetsChanged
    }
}
