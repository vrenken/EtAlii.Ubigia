﻿namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.xTechnology.MicroContainer;

    public class LinqQueryContextFactory : Factory<ILinqQueryContext, LinqQueryContextConfiguration, ILinqQueryContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(LinqQueryContextConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new LinqQueryContextScaffolding(configuration), 
            };
        }
    }
}