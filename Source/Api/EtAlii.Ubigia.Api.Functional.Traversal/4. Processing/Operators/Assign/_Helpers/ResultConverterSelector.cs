namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ResultConverterSelector : Selector<object, Func<object, ExecutionScope, IObserver<object>, Task>>, IResultConverterSelector
    {
        private readonly IScriptProcessingContext _context;

        public ResultConverterSelector(IScriptProcessingContext context)
        {
            _context = context;

            Register(o => o is IEnumerable<INode>, (o, s, output) => Convert(((IEnumerable<INode>)o).Cast<IInternalNode>(), s, output))
                .Register(o => o is INode[], (o, s, output) => Convert(((INode[])o).Cast<IInternalNode>(), s, output))
                .Register(o => o is INode, OnNext)
                .Register(o => o is IEnumerable<IReadOnlyEntry>, (o, _, output) => Convert<IReadOnlyEntry>(o, output))
                .Register(o => o is IReadOnlyEntry[], (o, _, output) => Convert<IReadOnlyEntry>(o, output))
                .Register(o => o is IReadOnlyEntry, OnNext)
                .Register(o => o is IEnumerable<Identifier>, (o, _, output) => Convert<Identifier>(o, output))
                .Register(o => o is Identifier[], (o, _, output) => Convert<Identifier>(o, output))
                .Register(o => o is Identifier, OnNext)
                .Register(o => o is string, OnNext)
                .Register(o => o is int, OnNext)
                .Register(o => o is float, OnNext)
                .Register(o => o is bool, OnNext)
                .Register(o => o is DateTime, OnNext)
                .Register(o => o is TimeSpan, OnNext)
                .Register(o => o == null, OnNext)
                .Register(_ => true, OnNext);
        }

        private Task OnNext(object o, ExecutionScope scope, IObserver<object> output)
        {
            output.OnNext(o);
            return Task.CompletedTask;
        }
        private async Task Convert(IEnumerable<IInternalNode> nodes, ExecutionScope scope, IObserver<object> output)
        {
            foreach (var node in nodes)
            {
                var properties = await _context.Logical.Properties.Get(node.Id, scope).ConfigureAwait(false) ?? new PropertyDictionary();
                node.Update(properties, node.Entry);

                output.OnNext(node);
            }
        }

        private Task Convert<T>(object enumerable, IObserver<object> output)
        {
            var items = (IEnumerable<T>)enumerable;
            foreach (var item in items)
            {
                output.OnNext(item);
            }
            return Task.CompletedTask;
        }
    }
}
