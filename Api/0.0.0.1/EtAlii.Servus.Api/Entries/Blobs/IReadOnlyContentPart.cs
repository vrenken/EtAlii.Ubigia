namespace EtAlii.Servus.Api
{
    using System;

    public interface IReadOnlyContentPart
    {
        byte[] Data { get; }
    }
}