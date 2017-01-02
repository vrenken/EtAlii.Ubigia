namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ToIdentifierConverter : IToIdentifierConverter
    {
        private readonly IProcessingContext _context;
        private readonly ISelector<object, Func<object, Identifier>> _selector; 
        public ToIdentifierConverter(
            IProcessingContext context)
        {
            _context = context;
            _selector = new Selector<object, Func<object, Identifier>>()
                .Register(item => item is Identifier, item => (Identifier)item)
                .Register(item => item is IReadOnlyEntry, item => ((IReadOnlyEntry)item).Id)
                .Register(item => item is INode, item => ((INode)item).Id);
        }

        public Identifier Convert(object o)
        {
            var converter = _selector.Select(o);
            return converter(o);
        }
    }
}
