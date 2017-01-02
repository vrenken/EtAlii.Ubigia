namespace EtAlii.Servus.Api.Data
{
    using System.Collections;
    using System.IO;
    using EtAlii.Servus.Api.Data.Model;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Structure;
    using EtAlii.xTechnology.MicroContainer;

    internal class OutputAssigner : IAssigner
    {
        private readonly ProcessingContext _context;
        private readonly ISelector<object, Action<object>> _outputConverterSelector;
        private readonly Lazy<IContentManager> _contentManager;
        private readonly IPropertiesSerializer _propertiesSerializer;

        public OutputAssigner(
            ProcessingContext context, 
            Container container, 
            IPropertiesSerializer propertiesSerializer)
        {
            _context = context;
            _contentManager = new Lazy<IContentManager>(container.GetInstance<IContentManager>);
            _propertiesSerializer = propertiesSerializer;

            _outputConverterSelector = new Selector<object, Action<object>>()
                .Register(o => o is IEnumerable<INode>, OutputNodeToScope)
                .Register(o => o is IEnumerable<IReadOnlyEntry>, OutputToScope<IReadOnlyEntry>)
                .Register(o => o is IEnumerable<Identifier>, OutputToScope<Identifier>);
        }

        private void OutputNodeToScope(object items)
        {
            var nodes = ((IEnumerable<INode>)items).Cast<IInternalNode>().ToArray();
            foreach (var node in nodes)
            {
                using (var stream = new MemoryStream())
                {
                    _contentManager.Value.Download(stream, node.Id, false); // TODO: We should validate the checksum.
                    stream.Position = 0;
                    var properties = _propertiesSerializer.Deserialize<IDictionary<string, object>>(stream);
                    node.Update(properties, node.Entry);
                }
            }

            OutputToScope<IInternalNode>(nodes);
        }

        public object Assign(ProcessParameters<Operator, SequencePart> parameters)
        {
            var items = parameters.RightResult;
            var outputConverter = _outputConverterSelector.TrySelect(items);
            if (outputConverter != null)
            {
                outputConverter(items);
            }
            else
            {
                _context.Scope.Output(items);
            }
            return null;
        }

        private void OutputToScope<T>(object items)
        {
            var enumerableItems = (IEnumerable<T>)items;
            if (HasMultiple(enumerableItems))
            {
                foreach (var item in enumerableItems)
                {
                    _context.Scope.Output(item);
                }
            }
            else
            {
                _context.Scope.Output(enumerableItems.SingleOrDefault());
            }
        }

        // TODO: Refactor to extension method.
        private bool HasMultiple<T>(IEnumerable<T> items)
        {
            var count = 0;
            foreach (var item in items)
            {
                count++;
                if (count > 1)
                {
                    return true;
                }
            }
            return false;
        }
    }


}
