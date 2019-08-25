namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureConfiguration : Configuration, IInfrastructureConfiguration, IEditableInfrastructureConfiguration
    {
        ILogicalContext IEditableInfrastructureConfiguration.Logical { get => Logical; set => Logical = value; }
        public ILogicalContext Logical { get; private set; }

        string IEditableInfrastructureConfiguration.Name { get => Name; set => Name = value; }
        public string Name { get; private set; }

        Uri IEditableInfrastructureConfiguration.Address { get => Address; set => Address = value; }
        public Uri Address { get; private set; }
        
        ISystemConnectionCreationProxy IEditableInfrastructureConfiguration.SystemConnectionCreationProxy { get => SystemConnectionCreationProxy; set => SystemConnectionCreationProxy = value; }
        public ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; private set; }

        Func<Container, IInfrastructure> IEditableInfrastructureConfiguration.GetInfrastructure { get => _getInfrastructure; set => _getInfrastructure = value; }
        private Func<Container, IInfrastructure> _getInfrastructure;

        public InfrastructureConfiguration(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            SystemConnectionCreationProxy = systemConnectionCreationProxy;
        }

        public IInfrastructure GetInfrastructure(Container container)
        {
            return _getInfrastructure(container);
        }
    }
}
