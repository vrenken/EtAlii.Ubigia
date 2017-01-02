namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal class UpdateItemHelper
    {
        private readonly IPathHelper _pathHelper;
        private readonly ScriptScope _scope;
        private readonly IDataConnection _connection;
        private readonly IContentManager _contentManager;
        private readonly IPropertiesSerializer _propertiesSerializer;

        private readonly IEnumerable<KeyValuePair<Func<ScopeVariable, string, bool>, Func<ScopeVariable, Path, object>>> _handlers;

        private readonly TypeInfo _dynamicObjectTypeInfo = typeof (DynamicObject).GetTypeInfo();

        public UpdateItemHelper(
            IContentManager contentManager,
            IPropertiesSerializer propertiesSerializer,
            IPathHelper pathHelper, 
            ScriptScope scope, 
            IDataConnection connection)
        {
            _contentManager = contentManager;
            _propertiesSerializer = propertiesSerializer;
            _pathHelper = pathHelper;
            _scope = scope;
            _connection = connection;

            _handlers = new[]
            {
                new KeyValuePair<Func<ScopeVariable, string, bool>, Func<ScopeVariable, Path, object>>(ShouldHandleAsNode, HandleAsNode),
                new KeyValuePair<Func<ScopeVariable, string, bool>, Func<ScopeVariable, Path, object>>(ShouldHandleAsDynamicObject, HandleAsDynamicObject),
                new KeyValuePair<Func<ScopeVariable, string, bool>, Func<ScopeVariable, Path, object>>(UnableToHandle, null), 
            };
        }

        private bool ShouldHandleAsNode(ScopeVariable variable, string variableName)
        {
            return variable.Value is IInternalNode;
        }

        private object HandleAsNode(ScopeVariable variable, Path path)
        {
            var entry = _pathHelper.GetEntry(path);

            var newEntry = CreateNewEntry(entry);

            var node = (IInternalNode)variable.Value;
            if (node.IsModified)
            {
                using (var stream = new MemoryStream())
                {
                    _propertiesSerializer.Serialize(node.GetProperties(), stream);
                    _contentManager.Upload(stream, (ulong)stream.Length, newEntry.Id);
                }
                node.Entry = (IReadOnlyEntry)newEntry;
                node.ClearIsModified();
            }
            return node;
        }

        private bool ShouldHandleAsDynamicObject(ScopeVariable variable, string variableName)
        {
            var typeInfo = variable.Type.GetTypeInfo();
            var canHandle = variable.Value != null && (IsAnonymousType(typeInfo) || IsDynamicType(typeInfo));
            return canHandle;
        }

        private object HandleAsDynamicObject(ScopeVariable variable, Path path)
        {
            var entry = _pathHelper.GetEntry(path);

            var newEntry = CreateNewEntry(entry);

            var dynamicObject = variable.Value;
            var properties = ToDictionary(dynamicObject);

            using (var stream = new MemoryStream())
            {
                _propertiesSerializer.Serialize(properties, stream);
                _contentManager.Upload(stream, (ulong)stream.Length, newEntry.Id);
            }
            
            var node = (IInternalNode)new DynamicNode((IReadOnlyEntry)newEntry);
            node.SetProperties(properties);
            node.ClearIsModified();

            return node;
        }

        private bool IsDynamicType(TypeInfo typeInfo)
        {
            return _dynamicObjectTypeInfo.IsAssignableFrom(typeInfo);
        }

        private bool IsAnonymousType(TypeInfo typeInfo)
        {
            return
            typeInfo.IsGenericType
            && (typeInfo.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
            && (typeInfo.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase)
            || typeInfo.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
            && typeInfo.Name.Contains("AnonymousType")
            && typeInfo.CustomAttributes.Any(a => a.AttributeType.Name == "CompilerGeneratedAttribute");
        }

        private IDictionary<string, object> ToDictionary(object data)
        {
            var type = data.GetType();

            return type.GetRuntimeProperties()
                .Where(p => p.CanRead)
                .ToDictionary(property => property.Name, property => property.GetValue(data, null));
        }

        private bool UnableToHandle(ScopeVariable variable, string variableName)
        {
            var message = String.Format("Unable to handle variable '{0}'.", variableName);
            throw new InvalidOperationException(message);
        }


        internal object Update(ScopeVariable variable, string variableName, Path path)
        {
            var handler = _handlers.First(h => h.Key(variable, variableName)).Value;
            return handler(variable, path);
        }

        private IEditableEntry CreateNewEntry(IReadOnlyEntry entry)
        {
            var newEntry = _connection.Entries.Prepare();
            newEntry.Downdate = Relation.NewRelation(entry.Id);
            _connection.Entries.Change(newEntry);
            return newEntry;
        }
    }
}
