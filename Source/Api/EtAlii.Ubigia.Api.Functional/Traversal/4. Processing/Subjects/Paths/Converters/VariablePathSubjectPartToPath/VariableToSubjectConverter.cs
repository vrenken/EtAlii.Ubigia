// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

internal class VariableToSubjectConverter : IVariableToSubjectConverter
{
    private readonly IPathParser _pathParser;

    public VariableToSubjectConverter(IPathParser pathParser)
    {
        _pathParser = pathParser;
    }

    public async Task<Subject> Convert(ScopeVariable variable)
    {
        // At this moment we only allow single items to be used as subjects.
        // Would this make sense to improve?
        // More info can be found in the GitHub item linked below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/67

        var value = await variable.Value.SingleAsync();

        return value switch
        {
            string s => ToSubject(s, variable.Source),
            Node node => ToAbsolutePathSubject(node),
            NonRootedPathSubject nonRootedPathSubject => nonRootedPathSubject,
            StringConstantSubject stringConstantSubject => stringConstantSubject,
            RootedPathSubject rootedPathSubject => rootedPathSubject,
            _ => throw new InvalidOperationException($"Unable to select option for criteria: {(value != null ? value.ToString() : "[NULL]")}")
        };
    }

    private AbsolutePathSubject ToAbsolutePathSubject(Node node)
    {
        return new AbsolutePathSubject(new PathSubjectPart[] { new ParentPathSubjectPart(), new IdentifierPathSubjectPart(node.Id) });
    }

    private Subject ToSubject(string value, string variableName)
    {
        try
        {
            return _pathParser.ParsePath(value);
        }
        catch (Exception e)
        {
            throw new ScriptParserException($"Unable to parse variable for subject (variable: {variableName}, value: {value})", e);
        }
    }
}
