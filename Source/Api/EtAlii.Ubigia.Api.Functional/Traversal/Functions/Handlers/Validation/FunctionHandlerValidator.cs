// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;
using EtAlii.xTechnology.Collections;

internal class FunctionHandlerValidator : IFunctionHandlerValidator
{
    public void Validate(IFunctionHandlersProvider functionHandlersProvider)
    {
        // Function Handler names.
        ValidateFunctionHandlerNaming(functionHandlersProvider);

        // Argument sets.
        ValidateParameterSets(functionHandlersProvider);
    }

    private void ValidateParameterSets(IFunctionHandlersProvider functionHandlersProvider)
    {
        foreach (var functionHandler in functionHandlersProvider.FunctionHandlers)
        {
            var firstSet = functionHandler.ParameterSets.ToArray();
            var secondSet = functionHandler.ParameterSets.ToArray();
            Compare(firstSet, secondSet, functionHandler);
        }
    }

    private void ValidateFunctionHandlerNaming(IFunctionHandlersProvider functionHandlersProvider)
    {
        // Duplicate names.
        var invalidNamedFunctionHandlers = functionHandlersProvider.FunctionHandlers
            .GroupBy(fh => fh.Name)
            .Where(fhg => fhg.Count() > 1)
            .Select(fhg => fhg.First())
            .ToArray();

        if (invalidNamedFunctionHandlers.Any())
        {
            var message =
                $"{(invalidNamedFunctionHandlers.Multiple() ? "Multiple function handlers" : "One function handler")} found with the same name: {(invalidNamedFunctionHandlers.Multiple() ? string.Join(", ", invalidNamedFunctionHandlers.Select(c => c.Name)) : invalidNamedFunctionHandlers.Single().Name)}";
            throw new InvalidOperationException(message);
        }

        // Invalid names.
        invalidNamedFunctionHandlers = functionHandlersProvider.FunctionHandlers
            .Where(fh => fh.Name.ToCharArray().Any(c => !char.IsLetterOrDigit(c)))
            .ToArray();

        if (invalidNamedFunctionHandlers.Any())
        {
            var message =
                $"{(invalidNamedFunctionHandlers.Multiple() ? "Multiple function handlers" : "One function handler")} found with invalid naming: {(invalidNamedFunctionHandlers.Multiple() ? string.Join(", ", invalidNamedFunctionHandlers.Select(fh => fh.Name)) : invalidNamedFunctionHandlers.Single().Name)}";
            throw new InvalidOperationException(message);
        }
    }

    private void Compare(ParameterSet[] firstSet, ParameterSet[] secondSet, IFunctionHandler functionHandler)
    {
        foreach (var first in firstSet)
        {
            foreach (var second in secondSet)
            {
                var areEqual = Compare(first.Parameters, second.Parameters);
                if (areEqual && second != first)
                {
                    var message =
                        $"A function handler with multiple matching parameter sets was found: {functionHandler.Name}";
                    throw new InvalidOperationException(message);
                }
            }
        }
    }

    private bool Compare(Parameter[] first, Parameter[] second)
    {
        var result = false;

        if (first.Length == second.Length)
        {
            result = true;

            for (var i = 0; i < first.Length; i++)
            {
                if (first[i].Type != second[i].Type)
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }
}
