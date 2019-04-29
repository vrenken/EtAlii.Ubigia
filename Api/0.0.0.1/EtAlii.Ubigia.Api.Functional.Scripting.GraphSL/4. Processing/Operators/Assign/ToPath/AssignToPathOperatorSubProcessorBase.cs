namespace EtAlii.Ubigia.Api.Functional
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
        private readonly IProcessingContext _context;
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IPathSubjectToGraphPathConverter _pathSubjectToGraphPathConverter;

        private readonly TypeInfo _dynamicObjectTypeInfo = typeof(DynamicObject).GetTypeInfo();

        protected AssignToPathOperatorSubProcessorBase(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
        {
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _pathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
            _context = context;
        }

        public async Task Assign(OperatorParameters parameters)
        {
            var value = await parameters.RightInput.SingleAsync(); // We do not support multiple constants (yet)

            parameters.LeftInput
                .SubscribeAsync(
                    onError: (e) => parameters.Output.OnError(e),
                    onCompleted: () => parameters.Output.OnCompleted(),
                    onNext: async (o) =>
                    {
                        var identifier = _itemToIdentifierConverter.Convert(o);
                        var leftPathSubject = (PathSubject)parameters.LeftSubject;
                        var graphPath = await _pathSubjectToGraphPathConverter.Convert(leftPathSubject, parameters.Scope);
                        var result = await Assign(graphPath, identifier, value, parameters.Scope);
                        //var result = await _context.Logical.Nodes.Assign(graphPath, o, value, parameters.Scope)
                        parameters.Output.OnNext(result);
                    });
        }

        private async Task<INode> Assign(GraphPath path, Identifier location, object o, ExecutionScope scope)
        {
            if(path.Last() is GraphTaggedNode && o is string tag)
            {
                return await _context.Logical.Nodes.AssignTag(location, tag, scope);
            }
            switch (o)
            {
                case IInternalNode node: return await _context.Logical.Nodes.AssignNode(location, node, scope);
                case IPropertyDictionary properties: return await _context.Logical.Nodes.AssignProperties(location, properties, scope);
            }

            if (IsDynamicObject(o))
            {
                return await _context.Logical.Nodes.AssignDynamic(location, o, scope);
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
                (typeInfo.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
                && (typeInfo.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase)
                    || typeInfo.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
                && typeInfo.Name.Contains("AnonymousType")
                && typeInfo.CustomAttributes.Any(a => a.AttributeType.Name == "CompilerGeneratedAttribute");
        }
    }
}