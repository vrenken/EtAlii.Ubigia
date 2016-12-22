namespace EtAlii.Servus.Api
{
    using System;

    public class Storage : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}