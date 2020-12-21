namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Tests
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class InvoiceItem
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public string Code { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
    }
}