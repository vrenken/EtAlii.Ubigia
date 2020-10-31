namespace EtAlii.Ubigia.Api.Tests
{
    using System.Collections.Generic;

    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }
}