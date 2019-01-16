namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ResultConverterSelector : Selector<object, Action<object, ExecutionScope, IObserver<object>>>, IResultConverterSelector
    {
        private readonly IProcessingContext _context;

        public ResultConverterSelector(IProcessingContext context)
        {
            _context = context;

            Register(o => o is IEnumerable<INode>, (o, s, output) => Convert(((IEnumerable<INode>)o).Cast<IInternalNode>(), s, output))
                .Register(o => o is INode[], (o, s, output) => Convert(((INode[])o).Cast<IInternalNode>(), s, output))
                .Register(o => o is INode, (o, s, output) => output.OnNext(o))
                .Register(o => o is IEnumerable<IReadOnlyEntry>, (o, s, output) => Convert<IReadOnlyEntry>(o, output))
                .Register(o => o is IReadOnlyEntry[], (o, s, output) => Convert<IReadOnlyEntry>(o, output))
                .Register(o => o is IReadOnlyEntry, (o, s, output) => output.OnNext(o))
                .Register(o => o is IEnumerable<Identifier>, (o, s, output) => Convert<Identifier>(o, output))
                .Register(o => o is Identifier[], (o, s, output) => Convert<Identifier>(o, output))
                .Register(o => o is Identifier, (o, s, output) => output.OnNext(o))
                .Register(o => o is string, (o, s, output) => output.OnNext(o))
                .Register(o => o is int, (o, s, output) => output.OnNext(o))
                .Register(o => o is float, (o, s, output) => output.OnNext(o))
                .Register(o => o is bool, (o, s, output) => output.OnNext(o))
                .Register(o => o is DateTime, (o, s, output) => output.OnNext(o))
                .Register(o => o is TimeSpan, (o, s, output) => output.OnNext(o))
                .Register(o => o == null, (o, s, output) => output.OnNext(o))
                .Register(o => true, (o, s, output) => output.OnNext(o));
        }

        private void Convert(IEnumerable<IInternalNode> nodes, ExecutionScope scope, IObserver<object> output)
        {
            Task.Run(async () =>
            {
                foreach (var node in nodes)
                {
                    var properties = await _context.Logical.Properties.Get(node.Id, scope) ?? new PropertyDictionary();
                    node.Update(properties, node.Entry);

                    output.OnNext(node);
                }
            });
        }

        private void Convert<T>(object enumerable, IObserver<object> output)
        {
            var items = (IEnumerable<T>)enumerable;
            foreach (var item in items)
            {
                output.OnNext(item);
            }
        }
    }
}
