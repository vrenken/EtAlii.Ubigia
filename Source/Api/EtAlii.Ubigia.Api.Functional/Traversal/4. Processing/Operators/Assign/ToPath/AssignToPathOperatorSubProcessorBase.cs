// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal abstract class AssignToPathOperatorSubProcessorBase
    {
        private readonly IScriptProcessingContext _context;
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IPathSubjectToGraphPathConverter _pathSubjectToGraphPathConverter;
        private readonly IEntriesToDynamicNodesConverter _entriesToDynamicNodesConverter;

        private readonly TypeInfo _dynamicObjectTypeInfo = typeof(DynamicObject).GetTypeInfo();

        protected AssignToPathOperatorSubProcessorBase(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IEntriesToDynamicNodesConverter entriesToDynamicNodesConverter,
            IScriptProcessingContext context)
        {
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _pathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
            _context = context;
            _entriesToDynamicNodesConverter = entriesToDynamicNodesConverter;
        }

        public async Task Assign(OperatorParameters parameters)
        {
            var value = await parameters.RightInput.SingleAsync(); // We do not support multiple constants (yet)

            parameters.LeftInput
                .SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onCompleted: () => parameters.Output.OnCompleted(),
                    onNext: async o =>
                    {
                        try
                        {
                            var identifier = _itemToIdentifierConverter.Convert(o);
                            var leftPathSubject = (PathSubject)parameters.LeftSubject;
                            var graphPath = await _pathSubjectToGraphPathConverter.Convert(leftPathSubject, parameters.Scope).ConfigureAwait(false);
                            var entry = await Assign(graphPath, identifier, value, parameters.Scope).ConfigureAwait(false);
                            var result = await _entriesToDynamicNodesConverter.Convert(entry, parameters.Scope).ConfigureAwait(false);
                            parameters.Output.OnNext(result);
                        }
                        catch (Exception e)
                        {
                            var message = "Unable to assign to path";
                            parameters.Output.OnError(new InvalidOperationException(message, e));
                        }
                    });
        }

        private async Task<IReadOnlyEntry> Assign(GraphPath path, Identifier location, object o, ExecutionScope scope)
        {
            if(path.Last() is GraphTaggedNode && o is string tag)
            {
                return await _context.Logical.Nodes.AssignTag(location, tag, scope).ConfigureAwait(false);
            }
            switch (o)
            {
                case Node node: return await _context.Logical.Nodes.AssignNode(location, node, scope).ConfigureAwait(false);
                case IPropertyDictionary properties: return await _context.Logical.Nodes.AssignProperties(location, properties, scope).ConfigureAwait(false);
            }

            if (IsDynamicObject(o))
            {
                return await _context.Logical.Nodes.AssignDynamic(location, o, scope).ConfigureAwait(false);
            }
            throw new AssignmentException("Object not supported for assignment operations: " + (o != null ? o.ToString() : "NULL"));
        }

        private bool IsDynamicObject(object value)
        {
            var result = false;
            if (value != null)
            {
                var typeInfo = value.GetType().GetTypeInfo();
                result = (IsAnonymousType(typeInfo) || IsDynamicType(typeInfo));
            }
            return result;
        }

        private bool IsDynamicType(TypeInfo typeInfo)
        {
            return _dynamicObjectTypeInfo.IsAssignableFrom(typeInfo);
        }

        private bool IsAnonymousType(TypeInfo typeInfo)
        {
            return
                //typeInfo.IsGenericType
                //[typeInfo.Attributes & TypeAttributes.NotPublic] = = TypeAttributes.NotPublic & &
                typeInfo.Attributes.HasFlag(TypeAttributes.NotPublic) &&
                (typeInfo.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) || typeInfo.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase)) &&
                typeInfo.Name.Contains("AnonymousType") &&
                typeInfo.CustomAttributes.Any(a => a.AttributeType.Name == "CompilerGeneratedAttribute");
        }
    }
}
