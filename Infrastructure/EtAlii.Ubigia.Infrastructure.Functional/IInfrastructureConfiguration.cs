namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public interface IInfrastructureConfiguration : IConfiguration
    {
        ILogicalContext Logical { get; }

        Uri Address { get; }

        string Name { get; }

        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }
    }
}