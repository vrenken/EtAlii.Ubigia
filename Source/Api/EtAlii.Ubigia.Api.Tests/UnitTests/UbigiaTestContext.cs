namespace EtAlii.Ubigia.Api.Tests
{
    using Microsoft.EntityFrameworkCore;

    public class UbigiaTestContext : DbContext
    {
        public UbigiaTestContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}