namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;

    class PathVariableExpander : IPathVariableExpander
    {
        private readonly IProcessingContext _context;
        private readonly IVariablePathSubjectPartToPathConverter _variablePathSubjectPartToPathConverter;

        public PathVariableExpander(
            IProcessingContext context,
            IVariablePathSubjectPartToPathConverter variablePathSubjectPartToGraphPathPartsConverter)
        {
            _context = context;
            _variablePathSubjectPartToPathConverter = variablePathSubjectPartToGraphPathPartsConverter;
        }

        public PathSubjectPart[] Expand(PathSubjectPart[] path)
        {
            var result = new List<PathSubjectPart>();

            foreach (var part in path)
            {
                if (part is VariablePathSubjectPart variablePart)
                {
                    var variableName = variablePart.Name;
                    if (!_context.Scope.Variables.TryGetValue(variableName, out var variable))
                    {
                        throw new ScriptProcessingException($"Variable {variableName} not set");
                    }

                    if (variable == null)
                    {
                        throw new ScriptProcessingException($"Variable {variableName} not assigned");
                    }

                    var parts = _variablePathSubjectPartToPathConverter.Convert(variable);
                    result.AddRange(parts);

                }
                else
                {
                    result.Add(part);
                }             
            }

            return result.ToArray();
        }
    }
}