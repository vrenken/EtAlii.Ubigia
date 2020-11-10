namespace EtAlii.Ubigia.Api.Tests
{
    using Microsoft.EntityFrameworkCore;

    public class UbigiaTestContext : UbigiaDbContext
    {
        public UbigiaTestContext(DbContextOptions options)
            : base(options)
        {
        }

        public IUbigiaDbSet<Customer> Customers { get; set; } 
        public IUbigiaDbSet<Invoice> Invoices { get; set; }
        public IUbigiaDbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}