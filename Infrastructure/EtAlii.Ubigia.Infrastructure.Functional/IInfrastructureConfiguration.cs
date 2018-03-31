namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public interface IInfrastructureConfiguration
    {
        ILogicalContext Logical { get; }

        Uri Address { get; }

        string Name { get; }

        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }

        IInfrastructureExtension[] Extensions { get; }

        // TODO: These two properties should be typed.
        Func<Container, Func<Container, object>[], object>[] ComponentManagerFactories { get; }
        Func<Container, object>[] ComponentFactories { get; }

        IInfrastructureConfiguration Use(string name, Uri address);

        IInfrastructureConfiguration Use(IInfrastructureExtension[] extensions);
        IInfrastructureConfiguration Use(ILogicalContext logical);

        // TODO: These two methods should be typed.
        IInfrastructureConfiguration Use(Func<Container, Func<Container, object>[], object> componentManagerFactory);

        IInfrastructureConfiguration Use<TComponentInterface, TComponent>()
            where TComponent: class, TComponentInterface;

        IInfrastructure GetInfrastructure(Container container);

        IInfrastructureConfiguration Use<TInfrastructure>()
            where TInfrastructure : class, IInfrastructure;

    }
}