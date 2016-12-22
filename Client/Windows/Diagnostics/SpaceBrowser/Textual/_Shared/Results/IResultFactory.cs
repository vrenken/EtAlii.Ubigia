namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Functional;

    public interface IResultFactory
    {
        Result Convert(object o, object group);
    }
}