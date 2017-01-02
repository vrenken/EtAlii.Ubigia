namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class ItemsToIdentifiersConverterSelector : Selector<object, Func<object, Identifier[]>> 
    {
        public ItemsToIdentifiersConverterSelector()
        {
            Register(items => items is IEnumerable<Identifier>, items => ((IEnumerable<Identifier>)items).Select(item => item).ToArray())
                .Register(items => items is IEnumerable<IReadOnlyEntry>, items => ((IEnumerable<IReadOnlyEntry>)items).Select(item => item.Id).ToArray())
                .Register(items => items is IEnumerable<INode>, items => ((IEnumerable<INode>)items).Select(item => item.Id).ToArray())
                .Register(items => items is Identifier, item => new Identifier[] { (Identifier)item })
                .Register(items => items is IReadOnlyEntry, item => new Identifier[] { ((IReadOnlyEntry)item).Id })
                .Register(items => items is INode, item => new Identifier[] { ((INode)item).Id });
        }
    }
}
