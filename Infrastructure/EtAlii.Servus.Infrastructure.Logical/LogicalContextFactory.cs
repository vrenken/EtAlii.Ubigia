namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalContextFactory : ILogicalContextFactory
    {
        public ILogicalContext Create(ILogicalContextConfiguration configuration)
        {
            if (configuration.Fabric == null)
            {
                throw new NotSupportedException("A Fabric is required to construct a LogicalContext instance");
            }

            var container = new Container();

            var scaffoldings = new EtAlii.xTechnology.MicroContainer.IScaffolding[]
            {
                new LogicalContextScaffolding(configuration),
                new IdentifierScaffolding() 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container.GetInstance<ILogicalContext>();
        }
    }
}