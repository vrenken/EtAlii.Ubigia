namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ResultConverterSelector : Selector<object, Func<object, ExecutionScope, IObserver<object>, Task>>, IResultConverterSelector
    {
        private readonly IProcessingContext _context;

        public ResultConverterSelector(IProcessingContext context)
        {
            _context = context;

            Register(o => o is IEnumerable<INode>, (o, s, output) => Convert(((IEnumerable<INode>)o).Cast<IInternalNode>(), s, output))
                .Register(o => o is INode[], (o, s, output) => Convert(((INode[])o).Cast<IInternalNode>(), s, output))
                .Register(o => o is INode, (o, s, output) => OnNext(output, o))
                .Register(o => o is IEnumerable<IReadOnlyEntry>, (o, s, output) => Convert<IReadOnlyEntry>(o, output))
                .Register(o => o is IReadOnlyEntry[], (o, s, output) => Convert<IReadOnlyEntry>(o, output))
                .Register(o => o is IReadOnlyEntry, (o, s, output) => OnNext(output, o))
                .Register(o => o is IEnumerable<Identifier>, (o, s, output) => Convert<Identifier>(o, output))
                .Register(o => o is Identifier[], (o, s, output) => Convert<Identifier>(o, output))
                .Register(o => o is Identifier, (o, s, output) => OnNext(output, o))
                .Register(o => o is string, (o, s, output) => OnNext(output, o))
                .Register(o => o is int, (o, s, output) => OnNext(output, o))
                .Register(o => o is float, (o, s, output) => OnNext(output, o))
                .Register(o => o is bool, (o, s, output) => OnNext(output, o))
                .Register(o => o is DateTime, (o, s, output) => OnNext(output, o))
                .Register(o => o is TimeSpan, (o, s, output) => OnNext(output, o))
                .Register(o => o == null, (o, s, output) => OnNext(output, o))
                .Register(o => true, (o, s, output) => OnNext(output, o));
        }

        private async Task OnNext(IObserver<object> output, object o)
        {
            output.OnNext(o);

            await Task.CompletedTask;
        }
        
        private async Task Convert(IEnumerable<IInternalNode> nodes, ExecutionScope scope, IObserver<object> output)
        {
            foreach (var node in nodes)
            {
                var properties = await _context.Logical.Properties.Get(node.Id, scope) ?? new PropertyDictionary();
                node.Update(properties, node.Entry);

                output.OnNext(node);
            }
        }

        private async Task Convert<T>(object enumerable, IObserver<object> output)
        {
            var items = (IEnumerable<T>)enumerable;
            foreach (var item in items)
            {
                output.OnNext(item);
            }

            await Task.CompletedTask;
        }
    }
}
