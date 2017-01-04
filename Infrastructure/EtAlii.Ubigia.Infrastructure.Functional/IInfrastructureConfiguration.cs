namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public interface IInfrastructureConfiguration
    {
        ILogicalContext Logical { get; }

        string Address { get; }
        string Name { get; }
        string Account { get; }
        string Password { get; }

        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }

        IInfrastructureExtension[] Extensions { get; }

        // TODO: These two properties should be typed.
        Func<Container, object[], object>[] ComponentManagerFactories { get; }
        object[] Components { get; }

        IInfrastructureConfiguration Use(string name, string address, string account, string password);

        IInfrastructureConfiguration Use(IInfrastructureExtension[] extensions);
        IInfrastructureConfiguration Use(ILogicalContext logical);

        // TODO: These two methods should be typed.
        IInfrastructureConfiguration Use(Func<Container, object[], object> componentManagerFactory);

        IInfrastructureConfiguration UseComponents(object components);

        IInfrastructure GetInfrastructure(Container container);

        IInfrastructureConfiguration Use<TInfrastructure>()
            where TInfrastructure : class, IInfrastructure;

    }
}