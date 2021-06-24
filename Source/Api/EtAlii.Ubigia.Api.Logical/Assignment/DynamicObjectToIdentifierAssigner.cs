// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    internal class DynamicObjectToIdentifierAssigner : IDynamicObjectToIdentifierAssigner
    {
        private readonly IPropertiesToIdentifierAssigner _propertiesToIdentifierAssigner;

        public DynamicObjectToIdentifierAssigner(IPropertiesToIdentifierAssigner propertiesToIdentifierAssigner)
        {
            _propertiesToIdentifierAssigner = propertiesToIdentifierAssigner;
        }

        public Task<IReadOnlyEntry> Assign(object dynamicObject, Identifier id, ExecutionScope scope)
        {
            var properties = ToDictionary(dynamicObject);
            return _propertiesToIdentifierAssigner.Assign(properties, id, scope);
        }

        private PropertyDictionary ToDictionary(object data)
        {
            var result = new PropertyDictionary();

            var runtimeProperties = data
                .GetType()
                .GetRuntimeProperties()
                .Where(p => p.CanRead);

            foreach (var runtimeProperty in runtimeProperties)
            {
                var propertyName = runtimeProperty.Name;
                var propertyValue = runtimeProperty.GetValue(data, null);
                result[propertyName] = propertyValue;
            }
            return result;
        }
    }
}
