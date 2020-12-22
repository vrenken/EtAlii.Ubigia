namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    internal class ArgumentSetFinder : IArgumentSetFinder
    {
        private readonly IFunctionSubjectParameterConverterSelector _parameterConverterSelector;

        public ArgumentSetFinder(IFunctionSubjectParameterConverterSelector parameterConverterSelector)
        {
            _parameterConverterSelector = parameterConverterSelector;
        }

        public ArgumentSet Find(FunctionSubject functionSubject)
        {
            var arguments = functionSubject.Arguments
                .Select(p =>
                {
                    var converter = _parameterConverterSelector.Select(p);
                    return converter.Convert(p);
                })
                .ToArray();
            return new ArgumentSet(arguments);
        }
    }
}
