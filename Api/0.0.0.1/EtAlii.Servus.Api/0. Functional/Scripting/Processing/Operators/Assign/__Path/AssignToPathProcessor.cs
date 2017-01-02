//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Dynamic;
//    using System.Linq;
//    using System.Reflection;
//    using EtAlii.xTechnology.Structure;

//    internal class AssignToPathProcessor : IAssignToPathProcessor
//    {
//        private readonly TypeInfo _dynamicObjectTypeInfo = typeof(DynamicObject).GetTypeInfo();

//        private readonly ISelector<object, IAssignOperatorPathSubProcessor> _selector;

//        public AssignToPathProcessor(
//            IAssignNodeToPathProcessor assignNodeToPathProcessor,
//            IAssignDynamicObjectToPathProcessor assignDynamicObjectToPathProcessor,
//            IAssignDictionaryToPathProcessor assignDictionaryToPathProcessor)
//        {
//            _selector = new Selector<object, IAssignOperatorPathSubProcessor>()
//                .Register(o => IsDynamicObject(o), assignDynamicObjectToPathProcessor)
//                .Register(o => o is IInternalNode, assignNodeToPathProcessor)
//                .Register(o => o is IPropertyDictionary, assignDictionaryToPathProcessor);

//        }

//        public void Assign(OperatorParameters parameters)
//        {
//            parameters.RightInput.Subscribe(
//            onError: (e) => parameters.Output.OnError(e),
//            onCompleted: () => parameters.Output.OnCompleted(),
//            onNext: o =>
//            {
//                // TODO: Refactor so that results are returned immediately.
//                var assignProcessor = _selector.Select(o);
//                assignProcessor.Assign(parameters, o);
//            });
//        }




//        private bool IsDynamicObject(object value)
//        {
//            var result = false;
//            if (value != null)
//            {
//                var typeInfo = value.GetType().GetTypeInfo();
//                result = (IsAnonymousType(typeInfo) || IsDynamicType(typeInfo));
//            }
//            return result;
//        }

//        private bool IsDynamicType(TypeInfo typeInfo)
//        {
//            return _dynamicObjectTypeInfo.IsAssignableFrom(typeInfo);
//        }

//        private bool IsAnonymousType(TypeInfo typeInfo)
//        {
//            return
//            //typeInfo.IsGenericType
//            (typeInfo.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
//            && (typeInfo.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase)
//            || typeInfo.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
//            && typeInfo.Name.Contains("AnonymousType")
//            && typeInfo.CustomAttributes.Any(a => a.AttributeType.Name == "CompilerGeneratedAttribute");
//        }

//    }
//}
