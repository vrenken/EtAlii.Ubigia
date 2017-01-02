namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;

    public interface ISystemConnectionCreationProxy
    {
        ISystemConnection Request();

        void Initialize(Func<ISystemConnection> create);
    }
}