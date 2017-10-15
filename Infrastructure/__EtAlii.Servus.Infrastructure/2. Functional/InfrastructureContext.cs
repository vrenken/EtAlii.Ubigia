namespace EtAlii.Servus.Infrastructure
{
    using System;

    public class InfrastructureContext : IInfrastructureContext
    {
        public IInfrastructureConfiguration Configuration
        {
            get { return _configuration; }
        }
        private IInfrastructureConfiguration _configuration;

        public IInfrastructure Infrastructure
        {
            get { return _infrastructure.Value; }
        }
        private Lazy<IInfrastructure> _infrastructure;

        public InfrastructureContext(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            // This action is needed because the Logical layer needs a fully functional system connection to do 
            // the initialization of the storage and spaces.
            // The functional is the only one that can provide these kind of connections.
            Func<ISystemConnection> createConnection = () =>  {
                var configuration = new SystemConnectionConfiguration()
                    .Use(Infrastructure);
                return new SystemConnectionFactory().Create(configuration);
            };
            systemConnectionCreationProxy.Initialize(createConnection);
        }

        public void Initialize(IInfrastructureConfiguration configuration, Func<IInfrastructure> infrastructureGetter)
        {
            _configuration = configuration;
            _infrastructure = new Lazy<IInfrastructure>(infrastructureGetter);
        }
    }
}