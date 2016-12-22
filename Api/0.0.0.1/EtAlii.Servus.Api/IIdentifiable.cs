namespace EtAlii.Servus.Api
{
    using System;

    public interface IIdentifiable
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}