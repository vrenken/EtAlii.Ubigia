namespace EtAlii.Servus.Api
{
    using System;

    public class Root : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Identifier Identifier { get; set; }
    }
}