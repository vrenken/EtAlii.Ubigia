namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    public class LinqQueryContextConfiguration : ILinqQueryContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }

        public ILinqQueryContextExtension[] Extensions { get; private set; }

        public LinqQueryContextConfiguration()
        {
            Extensions = new ILinqQueryContextExtension[0];
        }

        public ILinqQueryContextConfiguration Use(ILinqQueryContextExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public ILinqQueryContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }
    }
}