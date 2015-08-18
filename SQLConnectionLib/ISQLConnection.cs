using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace SQLConnectionLib
{
    public interface ISQLConnection
    {
        ObjectSet<Customers> CustomersTable();
        ObjectSet<Contacts> ContactsTable();
        ObjectSet<Parts> PartsTable();
        ObjectSet<Rentals> RentalsTable();
        ObjectSet<PayTypes> payTypesTable();
        ObjectSet<RentalGroups> rentalGroupsTable();
        ObjectSet<Tools> toolsTable();
        ObjectSet<ServiceWorksheets> serviceWorksheetsTable();
        ObjectSet<ServiceGroups> serviceGroupsTable();
        ObjectSet<ErrorTypes> errorTypesTable();
        ObjectSet<ToolStatuses> toolStatusesTable();
        ObjectSet<ServiceSum> serviceSumView();
        ObjectSet<RentalSum> rentalSumView();
    }
}
