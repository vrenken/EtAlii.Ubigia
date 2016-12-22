namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Functional;

    public interface IMultiResultFactory
    {
        Result[] Convert(object o, object group);
    }
}