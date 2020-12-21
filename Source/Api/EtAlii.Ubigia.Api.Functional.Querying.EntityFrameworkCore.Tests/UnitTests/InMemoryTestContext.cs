namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Tests
{
    using Microsoft.EntityFrameworkCore;

    public class InMemoryTestContext : DbContext
    {
        public InMemoryTestContext(DbContextOptions<InMemoryTestContext> options)
            : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}