namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public class SystemConnectionInfrastructure : InfrastructureBase
    {
        public SystemConnectionInfrastructure(
            IInfrastructureConfiguration configuration,
            IInfrastructureDataContext dataContext,
            IInfrastructureManagementContext managementContext,
            ILogicalContext logicalContext)
            : base(configuration, dataContext, managementContext, logicalContext)
        {
        }

        public override void Start()
        {
            // This action is needed because the Logical layer needs a fully functional system connection to do 
            // the initialization of the storage and spaces.
            // The functional is the only one that can provide these kind of connections.
            Configuration.SystemConnectionCreationProxy.Initialize(() =>
            {
                var configuration = new SystemConnectionConfiguration()
                    .Use(SystemTransportProvider.Create(this))
                    .Use(this);
                return new SystemConnectionFactory().Create(configuration);
            });

            base.Start();
        }
    }
}