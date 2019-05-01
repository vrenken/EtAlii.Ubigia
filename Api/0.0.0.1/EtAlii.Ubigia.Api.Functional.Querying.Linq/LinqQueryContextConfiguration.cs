namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public class LinqQueryContextConfiguration : Configuration<ILinqQueryContextExtension, LinqQueryContextConfiguration>, ILinqQueryContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }

        public LinqQueryContextConfiguration()
        {
        }

        public ILinqQueryContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }
    }
}