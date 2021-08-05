// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalContextFactory : ILogicalContextFactory
    {
        /// <inheritdoc />
        public ILogicalContext Create(ILogicalContextOptions options)
        {
            if (options.Fabric == null)
            {
                throw new NotSupportedException("A Fabric is required to construct a LogicalContext instance");
            }

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new LogicalContextScaffolding(options),
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
