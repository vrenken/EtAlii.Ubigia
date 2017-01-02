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

    internal class DynamicObjectToPathAssigner : IAssigner
    {
        private readonly Lazy<IContentManager> _contentManager;
        private readonly IPropertiesSerializer _propertiesSerializer;
        private readonly UpdateEntryFactory _updateEntryFactory;
        private ISelector<object, Func<object, IEnumerable<IReadOnlyEntry>>> _inputConverterSelector;

        public DynamicObjectToPathAssigner(
            Container container,
            IPropertiesSerializer propertiesSerializer,
            UpdateEntryFactory updateEntryFactory,
            ISelector<object, Func<object, IEnumerable<IReadOnlyEntry>>> inputConverterSelector)
        {
            _contentManager = new Lazy<IContentManager>(container.GetInstance<IContentManager>);
            _propertiesSerializer = propertiesSerializer;
            _updateEntryFactory = updateEntryFactory;
            _inputConverterSelector = inputConverterSelector;
        }

        public object Assign(ProcessParameters<Operator, SequencePart> parameters)
        {
            var items = parameters.LeftResult;
            var inputConvertor = _inputConverterSelector.Select(items);
            var entries = inputConvertor(items);
            if (entries == null || !entries.Any())
            {
                throw new ScriptProcessingException("The DynamicObjectToPathAssigner requires queryable entries from the previous path part");
            }

            var results = new List<INode>();
            var dynamicObject = parameters.RightResult;
            foreach (var entry in entries)
            {
                var result = Assign(dynamicObject, entry);
                results.Add(result);
            }
            return results.ToArray();
        }

        private INode Assign(object dynamicObject, IReadOnlyEntry entry)
        {
            var newEntry = _updateEntryFactory.Create(entry);

            var properties = ToDictionary(dynamicObject);
            using (var stream = new MemoryStream())
            {
                _propertiesSerializer.Serialize(properties, stream);
                _contentManager.Value.Upload(stream, (ulong)stream.Length, newEntry.Id);
            }

            var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)newEntry, properties);
            return newNode;
        }

        private IDictionary<string, object> ToDictionary(object data)
        {
            var type = data.GetType();

            return type.GetRuntimeProperties()
                .Where(p => p.CanRead)
                .ToDictionary(property => property.Name, property => property.GetValue(data, null));
        }
    }
}
