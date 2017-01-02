namespace EtAlii.Servus.Infrastructure
{
    using System;

    public interface IInfrastructureContext
    {
        IInfrastructureConfiguration Configuration { get; }

        IInfrastructure Infrastructure { get; }

        void Initialize(IInfrastructureConfiguration configuration, Func<IInfrastructure> infrastructureGetter);
    }
}