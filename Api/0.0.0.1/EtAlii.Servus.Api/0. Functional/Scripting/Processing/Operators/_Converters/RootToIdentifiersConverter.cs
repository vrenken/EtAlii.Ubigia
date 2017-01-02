namespace EtAlii.Servus.Api.Functional
{
    internal class RootToIdentifiersConverter : IItemsToIdentifiersConverter
    {
        private readonly ProcessingContext _context;

        public RootToIdentifiersConverter(ProcessingContext context)
        {
            _context = context;
        }

        public Identifier[] Convert(object item)
        {
            var root = _context.Logical.Roots.Get(DefaultRoot.Tail);
            return new Identifier[] { root.Identifier };
        }
    }
}
