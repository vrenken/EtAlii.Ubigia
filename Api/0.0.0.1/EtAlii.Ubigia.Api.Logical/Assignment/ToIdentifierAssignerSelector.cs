namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using EtAlii.xTechnology.Structure;

    internal class ToIdentifierAssignerSelector : Selector<object,IToIdentifierAssigner>, IToIdentifierAssignerSelector
    {
        private readonly TypeInfo _dynamicObjectTypeInfo = typeof(DynamicObject).GetTypeInfo();

        public ToIdentifierAssignerSelector(
            IPropertiesToIdentifierAssigner propertiesToIdentifierAssigner,
            IDynamicObjectToIdentifierAssigner dynamicObjectToIdentifierAssigner,
            INodeToIdentifierAssigner nodeToIdentifierAssigner)
        {
            this.Register(o => o is IInternalNode, nodeToIdentifierAssigner)
                .Register(o => o is IPropertyDictionary, propertiesToIdentifierAssigner)
                .Register(o => IsDynamicObject(o), dynamicObjectToIdentifierAssigner);
        }

        public new IToIdentifierAssigner TrySelect(object o)
        {
            return base.TrySelect(o);
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