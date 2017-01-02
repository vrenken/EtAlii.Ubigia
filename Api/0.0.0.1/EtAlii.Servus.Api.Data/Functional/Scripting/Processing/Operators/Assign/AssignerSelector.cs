namespace EtAlii.Servus.Api.Data
{
    using System.Collections;
    using System.Dynamic;
    using System.Reflection;
    using EtAlii.Servus.Api.Data.Model;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Structure;

    internal class AssignerSelector : Selector<ProcessParameters<Operator, SequencePart>, IAssigner>
    {
        private readonly TypeInfo _dynamicObjectTypeInfo = typeof(DynamicObject).GetTypeInfo();

        public AssignerSelector(
            VariableAssigner variableAssigner,
            OutputAssigner outputAssigner,
            NodeToPathAssigner nodeToPathAssigner,
            DynamicObjectToPathAssigner dynamicObjectToPathAssigner)
        {
                Register(p => p.LeftPart is VariableSubject, variableAssigner)
                .Register(p => p.LeftPart is PathSubject && IsDynamicObject(p.RightResult), dynamicObjectToPathAssigner)
                .Register(p => p.LeftPart is PathSubject && p.RightResult is IInternalNode, nodeToPathAssigner)
                .Register(p => p.LeftPart == null, outputAssigner);
            
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
            typeInfo.IsGenericType
            && (typeInfo.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
            && (typeInfo.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase)
            || typeInfo.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
            && typeInfo.Name.Contains("AnonymousType")
            && typeInfo.CustomAttributes.Any(a => a.AttributeType.Name == "CompilerGeneratedAttribute");
        }

    }
}
