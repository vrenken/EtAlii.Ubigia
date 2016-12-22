namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using EtAlii.Servus.Infrastructure.Logical;
    using EtAlii.Servus.Infrastructure.Transport;
    using SimpleInjector;

    public interface IInfrastructureConfiguration
    {
        ILogicalContext Logical { get; }

        string Address { get; }
        string Name { get; }
        string Account { get; }
        string Password { get; }

        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }

        IInfrastructureExtension[] Extensions { get; }

        Type[] ComponentManagerTypes { get; }
        IInfrastructureConfiguration Use(string name, string address, string account, string password);

        IInfrastructureConfiguration Use(IInfrastructureExtension[] extensions);
        IInfrastructureConfiguration Use(ILogicalContext logical);

        IInfrastructureConfiguration Use(Type componentManagerType);

        IInfrastructure GetInfrastructure(Container container);

        IInfrastructureConfiguration Use<TInfrastructure>()
            where TInfrastructure : class, IInfrastructure;

    }
}