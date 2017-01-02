namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Structure;

    internal class AddOperatorInputConverterSelector : Selector<object, Func<object, IEnumerable<Identifier>>>
    {
        public AddOperatorInputConverterSelector()
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
