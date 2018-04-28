namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    public interface ISystemConnectionCreationProxy
    {
        ISystemConnection Request();

        void Initialize(Func<ISystemConnection> create);
    }
}