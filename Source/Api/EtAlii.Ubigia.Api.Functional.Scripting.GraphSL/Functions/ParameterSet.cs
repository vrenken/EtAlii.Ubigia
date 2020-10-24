namespace EtAlii.Ubigia.Api.Functional.Scripting
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
                .Select(p => p != null ? p.Type.GetTypeInfo() : null)
                .ToArray();
        }
    }
}