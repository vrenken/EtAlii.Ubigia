namespace EtAlii.Ubigia.Api.Tests
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