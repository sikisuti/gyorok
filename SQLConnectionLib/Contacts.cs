//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLConnectionLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contacts
    {
        public long contactID { get; set; }
        public long firmID { get; set; }
        public long agentID { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual Customers Customers1 { get; set; }
    }
}
