// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class PathVariableExpander : IPathVariableExpander
    {
        private readonly IScriptProcessingContext _context;
        private readonly IVariablePathSubjectPartToPathConverter _variablePathSubjectPartToPathConverter;

        public PathVariableExpander(
            IScriptProcessingContext context,
            IVariablePathSubjectPartToPathConverter variablePathSubjectPartToGraphPathPartsConverter)
        {
            _context = context;
            _variablePathSubjectPartToPathConverter = variablePathSubjectPartToGraphPathPartsConverter;
        }

        public async Task<PathSubjectPart[]> Expand(PathSubjectPart[] path)
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

                    var parts = await _variablePathSubjectPartToPathConverter.Convert(variable).ConfigureAwait(false);
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
