// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class VariableFinder : IVariableFinder
    {
        public string[] FindVariables(Schema schema)
        {
            var variables = new List<string>();

            FindVariables(schema.Structure, variables);

            return variables.ToArray();
        }

        private void FindVariables(StructureFragment structureFragment, List<string> variables)
        {
            if (structureFragment.Annotation?.Source is { } structurePathSubject)
            {
                var pathVariables = structurePathSubject.Parts
                    .OfType<VariablePathSubjectPart>()
                    .ToArray();
                variables.AddRange(pathVariables.Select(pv => pv.Name));
            }

            foreach (var valueFragment in structureFragment.Values)
            {
                if (valueFragment.Annotation?.Source is { } valuePathSubject)
                {
                    var pathVariables = valuePathSubject.Parts
                        .OfType<VariablePathSubjectPart>()
                        .ToArray();
                    variables.AddRange(pathVariables.Select(pv => pv.Name));
                }
            }

            foreach (var childStructureFragment in structureFragment.Children)
            {
                FindVariables(childStructureFragment, variables);
            }
        }
    }
}
