namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional;

    public interface IMultiResultFactory
    {
        Result[] Convert(object o, object group);
    }
}