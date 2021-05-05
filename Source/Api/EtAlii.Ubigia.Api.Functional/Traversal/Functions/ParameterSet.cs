namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Reflection;

    public class ParameterSet
    {
        public Parameter[] Parameters { get; }

        public TypeInfo[] ParameterTypeInfos { get; }

        public bool RequiresInput { get; }

        public ParameterSet(bool requiresInput, params Parameter[] parameters)
        {
            RequiresInput = requiresInput;
            Parameters = parameters;

            ParameterTypeInfos = parameters
                .Select(p => p?.Type.GetTypeInfo())
                .ToArray();
        }
    }
}
