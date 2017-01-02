namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;
    using EtAlii.xTechnology.Structure;
    using EtAlii.xTechnology.MicroContainer;

    internal class NodeToPathInputConverterSelector : Selector<object, Func<object, IReadOnlyEntry[]>>
    {
        private readonly ProcessingContext _context;
        public NodeToPathInputConverterSelector(ProcessingContext context)
        {
            _context = context;
            Register(items => items is IEnumerable<Identifier>, items => ((IEnumerable<Identifier>)items).Select(id => _context.Connection.Entries.Get(id)).ToArray())
                .Register(items => items is IEnumerable<IReadOnlyEntry>, items => ((IEnumerable<IReadOnlyEntry>)items).ToArray())
                .Register(items => items is IEnumerable<INode>, items => ((IEnumerable<INode>)items).Select(node => _context.Connection.Entries.Get(node.Id)).ToArray());
        }
    }
}
