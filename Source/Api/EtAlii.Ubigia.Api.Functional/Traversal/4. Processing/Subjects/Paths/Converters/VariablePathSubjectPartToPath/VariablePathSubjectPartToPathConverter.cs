// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

internal class VariablePathSubjectPartToPathConverter : IVariablePathSubjectPartToPathConverter
{
    private readonly IPathParser _pathParser;

    public VariablePathSubjectPartToPathConverter(IPathParser pathParser)
    {
        _pathParser = pathParser;
    }

    public async Task<PathSubjectPart[]> Convert(ScopeVariable variable)
    {
        // At this moment we only allow single items to be used as path variables.
        // Would this make sense to improve?
        // More info can be found in the GitHub item linked below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/67

        var value = await variable.Value.SingleAsync();

        return value switch
        {
            string s => ToPath(s, variable.Source),
            Node node => ToPath(node),
            NonRootedPathSubject nonRootedPathSubject => nonRootedPathSubject.Parts,
            StringConstantSubject stringConstantSubject => ToPath(stringConstantSubject),
            RootedPathSubject rootedPathSubject => ToPath(rootedPathSubject),
            _ => throw new InvalidOperationException($"Unable to select option for criteria: {(value != null ? value.ToString() : "[NULL]")}")
        };
    }

    private PathSubjectPart[] ToPath(RootedPathSubject rootedPathSubject)
    {
        return Array.Empty<PathSubjectPart>()
            .Concat(new PathSubjectPart[]
            {
                new ParentPathSubjectPart(),
                new ConstantPathSubjectPart(rootedPathSubject.Root),
                new ParentPathSubjectPart(),
            })
            .Concat(rootedPathSubject.Parts)
            .ToArray();
    }

    private PathSubjectPart[] ToPath(Node node)
    {
        return new PathSubjectPart[] { new ParentPathSubjectPart(), new IdentifierPathSubjectPart(node.Id) };
    }

    private PathSubjectPart[] ToPath(StringConstantSubject subject )
    {
        return new PathSubjectPart[] {new ConstantPathSubjectPart(subject.Value) };
    }

    private PathSubjectPart[] ToPath(string value, string variableName)
    {
        Subject pathSubject;
        try
        {
            pathSubject = _pathParser.ParsePath(value);
        }
        catch (Exception e)
        {
            throw new ScriptParserException($"Unable to parse variable for path (variable: {variableName}, value: {value})", e);
        }

        return pathSubject switch
        {
            NonRootedPathSubject nonRootedPathSubject => nonRootedPathSubject.Parts,
            StringConstantSubject stringConstantSubject => ToPath(stringConstantSubject),
            RootedPathSubject rootedPathSubject => ToPath(rootedPathSubject),
            _ => throw new InvalidOperationException($"Unable to select option for criteria: {value ?? "[NULL]"}")
        };
    }
}
