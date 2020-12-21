namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    internal class ParameterSetFinder : IParameterSetFinder
    {
        public ParameterSet Find(FunctionSubject functionSubject, IFunctionHandler functionHandler, ArgumentSet argumentSet)
        {
            var possibleParameterSets = FindWithMatchingLength(functionSubject, functionHandler);

            var parameterSet = possibleParameterSets
                .SingleOrDefault(parameters => HasSameParameters(parameters, argumentSet));

            if (parameterSet == null)
            {
                var message = $"No function found with name '{functionSubject.Name}' and parameters [{argumentSet}]";
                throw new ScriptProcessingException(message);
            }

            return parameterSet;
        }

        private bool HasSameParameters(ParameterSet parameterSet, ArgumentSet argumentSet)
        {
            var result = true;
            for (var i = 0; i < argumentSet.Arguments.Length; i++)
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

        private ParameterSet[] FindWithMatchingLength(FunctionSubject functionSubject, IFunctionHandler functionHandler)
        {
            var parameterCount = functionSubject.Arguments.Length;
            var matchingParameterSets =
                functionHandler.ParameterSets
                .Where(parameterSet => parameterSet.Parameters.Length == parameterCount)
                .ToArray();
            if (!matchingParameterSets.Any())
            {
                var message = $"No function '{functionSubject.Name}' found with {parameterCount} parameters";
                throw new ScriptProcessingException(message);
            }

            matchingParameterSets = matchingParameterSets
                .Where(args => args.RequiresInput == functionSubject.ShouldAcceptInput)
                .ToArray();
            if (!matchingParameterSets.Any())
            {
                var message =
                    $"No function '{functionSubject.Name}' found with {parameterCount} parameters that also accepts input";
                throw new ScriptProcessingException(message);
            }

            return matchingParameterSets;
        }


    }
}
