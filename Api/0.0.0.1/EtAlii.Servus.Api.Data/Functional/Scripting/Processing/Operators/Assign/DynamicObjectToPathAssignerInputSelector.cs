namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using EtAlii.Servus.Api.Data.Model;
    using EtAlii.xTechnology.Structure;
    using EtAlii.xTechnology.MicroContainer;

    internal class DynamicObjectToPathAssignerInputSelector : Selector<object, Func<object, IEnumerable<IReadOnlyEntry>>>
    {
        private readonly ProcessingContext _context;

        public DynamicObjectToPathAssignerInputSelector(
            ProcessingContext context)
        {
            _context = context;
            Register(items => items is IEnumerable<Identifier>, items => ((IEnumerable<Identifier>)items).Select(id => _context.Connection.Entries.Get(id)))
                .Register(items => items is IEnumerable<IReadOnlyEntry>, items => (IEnumerable<IReadOnlyEntry>)items)
                .Register(items => items is IEnumerable<INode>, items => ((IEnumerable<INode>)items).Select(node => _context.Connection.Entries.Get(node.Id)));
        }
    }
}
