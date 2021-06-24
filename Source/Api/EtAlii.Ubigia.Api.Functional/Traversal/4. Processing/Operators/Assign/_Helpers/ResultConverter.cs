// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class ResultConverter : IResultConverter
    {
        private readonly IScriptProcessingContext _context;

        public ResultConverter(IScriptProcessingContext context)
        {
            _context = context;
        }

        public Task Convert(object input, ExecutionScope scope, IObserver<object> output)
        {
            return input switch
            {
                IEnumerable<INode> nodes => Convert(nodes.Cast<IInternalNode>(), scope, output),
                INode => OnNext(input, output),
                IEnumerable<IReadOnlyEntry> entries => Convert(entries, output),
                IReadOnlyEntry => OnNext(input, output),
                IEnumerable<Identifier> identifiers => Convert(identifiers, output),
                Identifier identifier => OnNext(identifier, output),
                _ =>  OnNext(input,  output)
            };
        }
        private Task OnNext(object o, IObserver<object> output)
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

        private Task Convert<T>(IEnumerable<T> enumerable, IObserver<object> output)
        {
            foreach (var item in enumerable)
            {
                output.OnNext(item);
            }
            return Task.CompletedTask;
        }
    }
}
