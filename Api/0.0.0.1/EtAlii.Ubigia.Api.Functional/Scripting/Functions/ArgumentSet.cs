namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class ArgumentSet
    {
        public object[] Arguments => _arguments;
        private readonly object[] _arguments;

        public TypeInfo[] ArgumentTypeInfos => _argumentTypeInfos;
        private readonly TypeInfo[] _argumentTypeInfos;

        public ArgumentSet(params object[] arguments)
        {
            _arguments = arguments;

            _argumentTypeInfos = arguments
                .Select(a => a != null ? a.GetType().GetTypeInfo() : null)
                .ToArray();
        }

        public override string ToString()
        {
            return String.Join(", ", _arguments.Select(a => a != null ? a.GetType().Name : "NULL"));
        }
    }
}