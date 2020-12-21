namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual List<InvoiceItem> Items { get; set; }
    }
}