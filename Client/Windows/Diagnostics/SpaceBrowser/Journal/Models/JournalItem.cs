namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;

    public class JournalItem
    {
        public int Id { get; set; }
        public DateTime Moment { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
