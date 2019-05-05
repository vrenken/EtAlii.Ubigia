﻿namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public class LinqQueryContextConfiguration : Configuration<LinqQueryContextConfiguration>, ILinqQueryContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }

        public LinqQueryContextConfiguration()
        {
        }

        public LinqQueryContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }
    }
}