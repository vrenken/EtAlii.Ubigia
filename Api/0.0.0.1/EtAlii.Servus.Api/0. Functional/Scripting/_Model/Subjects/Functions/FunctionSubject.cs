namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    internal class FunctionSubject : Subject
    {
        public string Name { get; private set; }

        public FunctionSubjectArgument[] Arguments { get; private set; }

        public IFunctionHandler FunctionHandler { get; set; }
        public ParameterSet[] PossibleParameterSets { get; set; }
        
        public FunctionSubject(string name)
        {
            Arguments = new FunctionSubjectArgument[] {};
        }

        public FunctionSubject(string name, FunctionSubjectArgument argument)
        {
            Name = name;
            Arguments = new[] { argument };
        }

        public FunctionSubject(string name, params FunctionSubjectArgument[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public ArgumentSet GetArgumentSet(IFunctionSubjectParameterConverterSelector parameterConverterSelector)
        {
            var arguments = this.Arguments
                .Select(p =>
                {
                    var converter = parameterConverterSelector.Select(p);
                    return converter.Convert(p);
                })
                .ToArray();
            return new ArgumentSet(arguments);
        }
        public ParameterSet FindParameterSet(ArgumentSet argumentSet)
        {
            var parameterSet = this.PossibleParameterSets
                .SingleOrDefault(parameters => HasSameParameters(parameters, argumentSet));

            if (parameterSet == null)
            {
                var message = String.Format("No function found with name '{0}' and parameters [{1}]", this.Name, argumentSet);
                throw new ScriptProcessingException(message);
            }

            return parameterSet;
        }

        private bool HasSameParameters(ParameterSet parameterSet, ArgumentSet argumentSet)
        {
            var result = true;
            for (int i = 0; i < argumentSet.Arguments.Length; i++)
            {
                var parameterTypeInfo = parameterSet.ParameterTypeInfos[i];
                var argumentTypeInfo = argumentSet.ArgumentTypeInfos[i];
                if (!parameterTypeInfo.IsAssignableFrom(argumentTypeInfo))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public override string ToString()
        {
            return String.Format("{0}({1})",Name, String.Join(",", Arguments.Select(parameter => parameter.ToString())));
        }
    }
}
