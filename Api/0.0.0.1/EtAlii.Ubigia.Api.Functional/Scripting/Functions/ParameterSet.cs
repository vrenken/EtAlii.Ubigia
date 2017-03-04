namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Reflection;

    public class ParameterSet
    {
        public Parameter[] Parameters => _parameters;
        private readonly Parameter[] _parameters;

        public TypeInfo[] ParameterTypeInfos => _parameterTypeInfos;
        private readonly TypeInfo[] _parameterTypeInfos;

        public bool RequiresInput => _requiresInput;
        private readonly bool _requiresInput;

        public ParameterSet(bool requiresInput, params Parameter[] parameters)
        {
            _requiresInput = requiresInput;
            _parameters = parameters;

            _parameterTypeInfos = parameters
                .Select(p => p != null ? p.Type.GetTypeInfo() : null)
                .ToArray();
        }
    }
}