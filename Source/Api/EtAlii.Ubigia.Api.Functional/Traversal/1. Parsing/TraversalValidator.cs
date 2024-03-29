// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Linq;

public class TraversalValidator : ITraversalValidator
{
    public void Validate(Script script)
    {
        foreach (var sequence in script.Sequences)
        {
            Validate(sequence);
        }
    }

    public void Validate(Sequence sequence)
    {
        var parts = sequence.Parts;

        // We skip the initial assignment operator to make validation more easy.
        var startOffset = parts.Length > 0 && parts[0] is AssignOperator
            ? 1
            : 0;

        for (var i = startOffset; i < parts.Length; i++)
        {
            var before = i > 0 ? parts[i - 1] : null;
            var after = i < parts.Length - 1 ? parts[i + 1] : null;
            var part = parts[i];
            Validate(before, part, i - startOffset, after);
        }

    }

    private void Validate(SequencePart before, SequencePart part, int sequencePartPosition, SequencePart after)
    {
        switch (part)
        {
            case Subject subject: ValidateSubject(before, subject, sequencePartPosition, after); break;
            case Operator @operator: ValidateOperator(before, @operator, after); break;
            case Comment:
                if (after != null)
                {
                    throw new ScriptParserException("A comment cannot be followed by another sequence part.");
                }
                break;
        }
    }

    public void Validate(Subject subject)
    {
        ValidateSubject(null, subject, 1, null);
    }

    // ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
    private void ValidateOperator(SequencePart before, Operator @operator, SequencePart after)
        // ReSharper enable ParameterOnlyUsedForPreconditionCheck.Local
    {
        if (before is Operator || after is Operator)
        {
            throw new ScriptParserException("Two operators cannot be combined.");
        }
        if(before is Comment)
        {
            throw new ScriptParserException("A operator cannot used in combination with comments.");
        }

        switch (@operator)
        {
            case AddOperator:
                //var pathToAdd = after as PathSubject
                //if [pathToAdd ! = null]
                //[
                //    var firstPath = pathToAdd.Parts.FirstOrDefault()
                //    var startsWithRelation = firstPath is ParentPathSubjectPart
                //    if [!startsWithRelation]
                //    [
                //        throw new ScriptParserException("The add operation requires a path to start with a relation symbol.")
                //    ]
                //]
                break;
            case RemoveOperator:
                break;
            case AssignOperator:
                if (before is PathSubject && after is PathSubject)
                {
                    throw new ScriptParserException("The assign operator cannot assign a path to another path.");
                }
                break;
        }
    }

    private void ValidateSubject(SequencePart before, Subject subject, int subjectPosition, SequencePart after)
    {
        if (before is Subject || after is Subject)
        {
            throw new ScriptParserException("Two subjects cannot be combined.");
        }
        if (before is Comment)
        {
            throw new ScriptParserException("A subject cannot used in combination with comments.");
        }

        switch (subject)
        {
            case ObjectConstantSubject:
                ValidateObjectConstantSubject(subjectPosition);
                break;
            case StringConstantSubject:
                ValidateStringConstantSubject(subjectPosition);
                break;
            case FunctionSubject functionSubject:
                ValidateFunctionSubject(after, functionSubject);
                break;
            case RootSubject:
                ValidateRootSubject(before, subjectPosition, after);
                break;
            case RootDefinitionSubject:
                ValidateRootDefinitionSubject(before, subjectPosition, after);
                break;
            case RootedPathSubject rootedPathSubject:
                ValidateRootedPathSubject(rootedPathSubject);
                break;
            case NonRootedPathSubject nonRootedPathSubject:
                ValidateNonRootedPathSubject(nonRootedPathSubject, subjectPosition);
                break;
            case VariableSubject:
                // Validate the Subject in combination with the before/after SequencePart combination.
                break;
        }
    }

    private void ValidateFunctionSubject(SequencePart after, FunctionSubject functionSubject)
    {
        var arguments = functionSubject.Arguments;
        foreach (var argument in arguments)
        {
            ValidateFunctionSubjectArgument(argument);
        }

        functionSubject.ShouldAcceptInput = after != null;
    }

    private void ValidateStringConstantSubject(int subjectPosition)
    {
        if (subjectPosition == 0)
        {
            throw new ScriptParserException("A string constant cannot be used as first subject.");
        }
    }

    private void ValidateObjectConstantSubject(int subjectPosition)
    {
        if (subjectPosition == 0)
        {
            throw new ScriptParserException("An object constant cannot be used as first subject.");
        }
    }

    private void ValidateRootSubject(SequencePart before, int subjectPosition, SequencePart after)
    {
        if (subjectPosition != 0 || before != null)
        {
            throw new ScriptParserException("A root subject can only be used as first subject.");
        }

        if (!(after is AssignOperator))
        {
            throw new ScriptParserException("Root subjects can only be modified using the assignment operator.");
        }
    }

    private void ValidateRootDefinitionSubject(SequencePart before, int subjectPosition, SequencePart after)
    {
        if (subjectPosition == 0 || before == null)
        {
            throw new ScriptParserException("A root definition subject can not be used as first subject.");
        }

        if (!(before is AssignOperator))
        {
            throw new ScriptParserException("Root definition subjects can only be used with the assignment operator.");
        }

        if (after is not null && after is not Comment)
        {
            throw new ScriptParserException("Root definition subjects can only be used as the last subject in a sequence.");
        }
    }

    private void ValidateNonRootedPathSubject(NonRootedPathSubject nonRootedPathSubject, int subjectPosition)
    {
        for (var pathPartPosition = 0; pathPartPosition < nonRootedPathSubject.Parts.Length; pathPartPosition++)
        {
            var beforePathPart = pathPartPosition > 0 ? nonRootedPathSubject.Parts[pathPartPosition - 1] : null;
            var afterPathPart = pathPartPosition < nonRootedPathSubject.Parts.Length - 1
                ? nonRootedPathSubject.Parts[pathPartPosition + 1]
                : null;
            var pathPart = nonRootedPathSubject.Parts[pathPartPosition];
            ValidatedPathSubjectPart(beforePathPart, pathPart, pathPartPosition, afterPathPart, nonRootedPathSubject);
        }

        if (subjectPosition == 0 && nonRootedPathSubject.Parts.FirstOrDefault() is ConstantPathSubjectPart)
        {
            throw new ScriptParserException("A relative path part cannot be used as first subject.");
        }
    }

    private void ValidateRootedPathSubject(RootedPathSubject rootedPathSubject)
    {
        for (var pathPartPosition = 0; pathPartPosition < rootedPathSubject.Parts.Length; pathPartPosition++)
        {
            var beforePathPart = pathPartPosition > 0 ? rootedPathSubject.Parts[pathPartPosition - 1] : null;
            var afterPathPart = pathPartPosition < rootedPathSubject.Parts.Length - 1
                ? rootedPathSubject.Parts[pathPartPosition + 1]
                : null;
            var pathPart = rootedPathSubject.Parts[pathPartPosition];
            ValidatedPathSubjectPart(beforePathPart, pathPart, pathPartPosition, afterPathPart, rootedPathSubject);
        }
    }

    private void ValidatedPathSubjectPart(PathSubjectPart beforePathPart, PathSubjectPart pathPart, int pathPartPosition, PathSubjectPart afterPathPart, Subject subject)
    {
        switch (pathPart)
        {
            case TraversingWildcardPathSubjectPart:
                ValidateTraversingWildcardPathSubjectPart(beforePathPart, afterPathPart);
                break;
            case WildcardPathSubjectPart:
                ValidateWildcardPathSubjectPart(beforePathPart, pathPartPosition, afterPathPart, subject);
                break;
            case TaggedPathSubjectPart:
                ValidateTaggedPathSubjectPart(beforePathPart, pathPartPosition, afterPathPart, subject);
                break;
            case ConditionalPathSubjectPart:
                ValidateConditionalPathSubjectPart(beforePathPart, pathPartPosition);
                break;
            case ConstantPathSubjectPart constantPathSubjectPart:
                ValidateConstantPathSubjectPart(beforePathPart, pathPartPosition, afterPathPart, constantPathSubjectPart);
                break;
            case VariablePathSubjectPart:
                ValidateVariablePathSubjectPart(beforePathPart, afterPathPart);
                break;
            case IdentifierPathSubjectPart:
                ValidateIdentifierPathSubjectPart(beforePathPart, pathPartPosition);
                break;
            case AllParentsPathSubjectPart:
                ValidateAllParentsPathSubjectPart(beforePathPart, afterPathPart);
                break;
            case ParentPathSubjectPart:
                ValidateParentPathSubjectPart(beforePathPart, afterPathPart);
                break;
            // Why is this AllChildrenPathSubjectPart case below commented out?
            // More details can be found in the GitHub issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/87

            // should make us reflect on the other parent/child traversers.
            // case AllChildrenPathSubjectPart:
            //     if [beforePathPart is ChildrenPathSubjectPart or afterPathPart is ChildrenPathSubjectPart or
            //         beforePathPart is AllChildrenPathSubjectPart or afterPathPart is AllChildrenPathSubjectPart]
            //     [
            //         throw new ScriptParserException["The all children path separator cannot be combined."]
            //     ]
            //     if [afterPathPart is ParentPathSubjectPart]
            //     [
            //         throw new ScriptParserException["The all children path separator cannot be followed by a parent path separator."]
            //     ]
            //     if [afterPathPart is AllParentsPathSubjectPart]
            //     [
            //         throw new ScriptParserException["The all children path separator cannot be followed by an all parents path separator."]
            //     ]
            //     break
            case ChildrenPathSubjectPart:
                ValidateChildrenPathSubjectPart(beforePathPart, afterPathPart);
                break;
            case AllDowndatesPathSubjectPart:
                ValidateAllDowndatesPathSubjectPart(beforePathPart, pathPartPosition, afterPathPart);
                break;
            case DowndatePathSubjectPart:
                ValidateDowndatePathSubjectPart(pathPartPosition);
                break;
            case AllUpdatesPathSubjectPart:
                ValidateAllUpdatesPathSubjectPart(beforePathPart, pathPartPosition, afterPathPart);
                break;
            case UpdatesPathSubjectPart:
                ValidateUpdatesPathSubjectPart(pathPartPosition);
                break;
            case AllPreviousPathSubjectPart:
                ValidateAllPreviousPathSubjectPart(beforePathPart, pathPartPosition, afterPathPart);
                break;
            case PreviousPathSubjectPart:
                ValidatePreviousPathSubjectPart(pathPartPosition);
                break;
            case AllNextPathSubjectPart:
                ValidateAllNextPathSubjectPart(beforePathPart, pathPartPosition, afterPathPart);
                break;
            case NextPathSubjectPart:
                ValidateNextPathSubjectPart(pathPartPosition);
                break;
            case TypedPathSubjectPart:
                // Validate.
                break;
            case RegexPathSubjectPart:
                // Validate.
                break;
        }
    }

    private void ValidateNextPathSubjectPart(int pathPartPosition)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The next path separator cannot be used to start a path.");
        }
    }

    private void ValidatePreviousPathSubjectPart(int pathPartPosition)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The previous path separator cannot be used to start a path.");
        }
    }

    private void ValidateUpdatesPathSubjectPart(int pathPartPosition)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The updates path separator cannot be used to start a path.");
        }
    }

    private void ValidateDowndatePathSubjectPart(int pathPartPosition)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The downdate path separator cannot be used to start a path.");
        }
    }

    private void ValidateChildrenPathSubjectPart(PathSubjectPart beforePathPart, PathSubjectPart afterPathPart)
    {
        if (beforePathPart is ChildrenPathSubjectPart || afterPathPart is ChildrenPathSubjectPart)
        {
            throw new ScriptParserException("Two child path separators cannot be combined.");
        }

        if (afterPathPart is ParentPathSubjectPart)
        {
            throw new ScriptParserException("The child path separator cannot be followed by a parent path separator.");
        }
    }

    private void ValidateVariablePathSubjectPart(PathSubjectPart beforePathPart, PathSubjectPart afterPathPart)
    {
        if (beforePathPart is VariablePathSubjectPart || afterPathPart is VariablePathSubjectPart)
        {
            throw new ScriptParserException("A variable path part cannot be combined with other variable path parts.");
        }
    }

    private void ValidateConditionalPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition)
    {
        if (pathPartPosition == 0 || pathPartPosition == 1 && !(beforePathPart is VariablePathSubjectPart))
        {
            throw new ScriptParserException("A conditional path part cannot be used at the beginning of a graph path.");
        }
    }

    private void ValidateConstantPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition, PathSubjectPart afterPathPart, ConstantPathSubjectPart constantPathSubjectPart)
    {
        if (beforePathPart is ConstantPathSubjectPart || afterPathPart is ConstantPathSubjectPart)
        {
            throw new ScriptParserException("Two constant path parts cannot be combined.");
        }

        if ((pathPartPosition != 0 || afterPathPart == null) && constantPathSubjectPart.Name == string.Empty)
        {
            throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
        }

        if ((pathPartPosition == 0 && afterPathPart != null) && constantPathSubjectPart.Name == string.Empty)
        {
            throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
        }
    }

    private void ValidateIdentifierPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition)
    {
        if ((beforePathPart == null || beforePathPart is ParentPathSubjectPart) && pathPartPosition <= 1)
        {
            // All is ok.
        }
        else
        {
            throw new ScriptParserException("A identifier path part can only be used at the start of a path");
        }
    }

    private void ValidateAllParentsPathSubjectPart(PathSubjectPart beforePathPart, PathSubjectPart afterPathPart)
    {
        if (beforePathPart is ParentPathSubjectPart || afterPathPart is ParentPathSubjectPart ||
            beforePathPart is AllParentsPathSubjectPart || afterPathPart is AllParentsPathSubjectPart)
        {
            throw new ScriptParserException("The all parents path separator cannot be combined.");
        }

        if (afterPathPart is ChildrenPathSubjectPart)
        {
            throw new ScriptParserException("The all parents path separator cannot be followed by a child path separator.");
        }

        if (afterPathPart is AllChildrenPathSubjectPart)
        {
            throw new ScriptParserException("The all parents path separator cannot be followed by an all child path separator.");
        }
    }

    private void ValidateParentPathSubjectPart(PathSubjectPart beforePathPart, PathSubjectPart afterPathPart)
    {
        if (beforePathPart is ParentPathSubjectPart || afterPathPart is ParentPathSubjectPart)
        {
            throw new ScriptParserException("Two parent path separators cannot be combined.");
        }

        if (afterPathPart is ChildrenPathSubjectPart)
        {
            throw new ScriptParserException("The parent path separator cannot be followed by a child path separator.");
        }
    }

    private void ValidateAllDowndatesPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition, PathSubjectPart afterPathPart)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The all downdates path separator cannot be used to start a path.");
        }

        if (beforePathPart is DowndatePathSubjectPart || afterPathPart is DowndatePathSubjectPart ||
            beforePathPart is AllDowndatesPathSubjectPart || afterPathPart is AllDowndatesPathSubjectPart)
        {
            throw new ScriptParserException("The all downdates path separator cannot be combined.");
        }

        if (afterPathPart is UpdatesPathSubjectPart)
        {
            throw new ScriptParserException("The all downdates path separator cannot be followed by a update path separator.");
        }

        if (afterPathPart is AllUpdatesPathSubjectPart)
        {
            throw new ScriptParserException("The all downdates path separator cannot be followed by an all update path separator.");
        }
    }

    private void ValidateAllUpdatesPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition, PathSubjectPart afterPathPart)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The all updates path separator cannot be used to start a path.");
        }

        if (beforePathPart is UpdatesPathSubjectPart || afterPathPart is UpdatesPathSubjectPart ||
            beforePathPart is AllUpdatesPathSubjectPart || afterPathPart is AllUpdatesPathSubjectPart)
        {
            throw new ScriptParserException("The all updates path separator cannot be combined.");
        }

        if (afterPathPart is DowndatePathSubjectPart)
        {
            throw new ScriptParserException("The all updates path separator cannot be followed by a downdate path separator.");
        }

        if (afterPathPart is AllDowndatesPathSubjectPart)
        {
            throw new ScriptParserException("The all updates path separator cannot be followed by an all downdates path separator.");
        }
    }

    private void ValidateAllPreviousPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition, PathSubjectPart afterPathPart)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The all previous path separator cannot be used to start a path.");
        }

        if (beforePathPart is PreviousPathSubjectPart || afterPathPart is PreviousPathSubjectPart ||
            beforePathPart is AllPreviousPathSubjectPart || afterPathPart is AllPreviousPathSubjectPart)
        {
            throw new ScriptParserException("The all previous path separator cannot be combined.");
        }

        if (afterPathPart is NextPathSubjectPart)
        {
            throw new ScriptParserException("The all previous path separator cannot be followed by a next path separator.");
        }

        if (afterPathPart is AllNextPathSubjectPart)
        {
            throw new ScriptParserException("The all previous path separator cannot be followed by an all next path separator.");
        }
    }

    private void ValidateAllNextPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition, PathSubjectPart afterPathPart)
    {
        if (pathPartPosition == 0)
        {
            throw new ScriptParserException("The all next path separator cannot be used to start a path.");
        }

        if (beforePathPart is NextPathSubjectPart || afterPathPart is NextPathSubjectPart ||
            beforePathPart is AllNextPathSubjectPart || afterPathPart is AllNextPathSubjectPart)
        {
            throw new ScriptParserException("The all next path separator cannot be combined.");
        }

        if (afterPathPart is PreviousPathSubjectPart)
        {
            throw new ScriptParserException("The all next path separator cannot be followed by a previous path separator.");
        }

        if (afterPathPart is AllPreviousPathSubjectPart)
        {
            throw new ScriptParserException("The all next path separator cannot be followed by an all previous path separator.");
        }
    }

    private void ValidateTaggedPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition, PathSubjectPart afterPathPart, Subject subject)
    {
        if (beforePathPart is ConstantPathSubjectPart || afterPathPart is ConstantPathSubjectPart ||
            beforePathPart is WildcardPathSubjectPart || afterPathPart is WildcardPathSubjectPart ||
            beforePathPart is TaggedPathSubjectPart || afterPathPart is TaggedPathSubjectPart ||
            beforePathPart is TraversingWildcardPathSubjectPart || afterPathPart is TraversingWildcardPathSubjectPart)
        {
            throw new ScriptParserException("A tagged path part cannot be combined with other constant, tagged, wildcard or string path parts.");
        }

        if (pathPartPosition == 0 && subject is NonRootedPathSubject ||
            pathPartPosition == 1 && beforePathPart is ParentPathSubjectPart && beforePathPart is not VariablePathSubjectPart)
        {
            throw new ScriptParserException("A tagged path part cannot be used at the beginning of a graph path.");
        }
    }

    private void ValidateWildcardPathSubjectPart(PathSubjectPart beforePathPart, int pathPartPosition, PathSubjectPart afterPathPart, Subject subject)
    {
        if (beforePathPart is ConstantPathSubjectPart || afterPathPart is ConstantPathSubjectPart ||
            beforePathPart is WildcardPathSubjectPart || afterPathPart is WildcardPathSubjectPart ||
            beforePathPart is TaggedPathSubjectPart || afterPathPart is TaggedPathSubjectPart ||
            beforePathPart is TraversingWildcardPathSubjectPart || afterPathPart is TraversingWildcardPathSubjectPart)
        {
            throw new ScriptParserException("A wildcard path part cannot be combined with other constant, tagged, wildcard or string path parts.");
        }

        if (pathPartPosition == 0 && subject is NonRootedPathSubject ||
            pathPartPosition == 1 && beforePathPart is ParentPathSubjectPart && beforePathPart is not VariablePathSubjectPart)
        {
            throw new ScriptParserException("A wildcard path part cannot be used at the beginning of a graph path.");
        }
    }

    private void ValidateTraversingWildcardPathSubjectPart(PathSubjectPart beforePathPart, PathSubjectPart afterPathPart)
    {
        if (beforePathPart is ConstantPathSubjectPart || afterPathPart is ConstantPathSubjectPart ||
            beforePathPart is WildcardPathSubjectPart || afterPathPart is WildcardPathSubjectPart ||
            beforePathPart is TaggedPathSubjectPart || afterPathPart is TaggedPathSubjectPart ||
            beforePathPart is TraversingWildcardPathSubjectPart || afterPathPart is TraversingWildcardPathSubjectPart)
        {
            throw new ScriptParserException("A traversing wildcard path part cannot be combined with other constant, tagged, wildcard or string path parts.");
        }
        //else if [partIndex = = 0 | | partIndex = = 1 & & [before is VariablePathSubjectPart] = = false]
        //[
        //    throw new ScriptParserException["A traversing wildcard path part cannot be used at the beginning of a graph path."]
        //    Not true with rooted paths.
        //]
    }

    private void ValidateFunctionSubjectArgument(FunctionSubjectArgument argument)
    {
        switch (argument)
        {
            case ConstantFunctionSubjectArgument:
                // Make sure the argument can can actually be applied on the before/after FunctionSubjectArgument combination.
                break;
            case VariableFunctionSubjectArgument:
                break;
            case RootedPathFunctionSubjectArgument rootedPathFunctionSubjectArgument:
                ValidateRootedPathSubject(rootedPathFunctionSubjectArgument.Subject);
                break;
            case NonRootedPathFunctionSubjectArgument nonRootedPathFunctionSubjectArgument:
                ValidateNonRootedPathSubject(nonRootedPathFunctionSubjectArgument.Subject, 0);
                break;
        }
    }
}
