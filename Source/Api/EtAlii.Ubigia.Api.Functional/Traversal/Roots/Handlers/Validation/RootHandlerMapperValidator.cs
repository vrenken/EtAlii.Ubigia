// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Collections;

    internal class RootHandlerMapperValidator : IRootHandlerMapperValidator
    {
        public void Validate(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            // Root Handler names.
            ValidateRootHandlerNaming(rootHandlerMappersProvider);

            // Argument sets.
            ValidateSubHandlers(rootHandlerMappersProvider);
        }

        private void ValidateSubHandlers(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            foreach (var rootHandlerMapper in rootHandlerMappersProvider.RootHandlerMappers)
            {
                var firstSet = rootHandlerMapper.AllowedRootHandlers.ToArray();
                var secondSet = rootHandlerMapper.AllowedRootHandlers.ToArray();
                Compare(firstSet, secondSet, rootHandlerMapper);
            }
        }
        private void ValidateRootHandlerNaming(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            // Duplicate names.
            var invalidNamedRootHandlerMappers = rootHandlerMappersProvider.RootHandlerMappers
                .GroupBy(fh => fh.Type)
                .Where(fhg => fhg.Count() > 1)
                .Select(fhg => fhg.First())
                .ToArray();

            if (invalidNamedRootHandlerMappers.Any())
            {
                var message =
                    $"{(invalidNamedRootHandlerMappers.Multiple() ? "Multiple root handler mappers" : "One root handler mapper")} found with the same name: {(invalidNamedRootHandlerMappers.Multiple() ? string.Join(", ", invalidNamedRootHandlerMappers.Select(c => c.Type)) : invalidNamedRootHandlerMappers.Single().Type)}";
                throw new InvalidOperationException(message);
            }

            // Invalid names.
            invalidNamedRootHandlerMappers = rootHandlerMappersProvider.RootHandlerMappers
                .Where(fh => fh.Type.Value.ToCharArray().Any(c => !char.IsLetterOrDigit(c)))
                .ToArray();

            if (invalidNamedRootHandlerMappers.Any())
            {
                var message =
                    $"{(invalidNamedRootHandlerMappers.Multiple() ? "Multiple root handler mappers" : "One root handler mapper")} found with invalid naming: {(invalidNamedRootHandlerMappers.Multiple() ? string.Join(", ", invalidNamedRootHandlerMappers.Select(fh => fh.Type)) : invalidNamedRootHandlerMappers.Single().Type)}";
                throw new InvalidOperationException(message);
            }
        }


        private void Compare(IRootHandler[] firstSet, IRootHandler[] secondSet, IRootHandlerMapper rootHandlerMapper)
        {
            foreach (var first in firstSet)
            {
                foreach (var second in secondSet)
                {
                    var areEqual = Compare(first.Template, second.Template);
                    if (areEqual && second != first)
                    {
                        var message =
                            $"A root handler mapper with multiple matching allowed paths was found: {rootHandlerMapper.Type}";
                        throw new InvalidOperationException(message);
                    }
                }
            }
        }

        private bool Compare(PathSubjectPart[] first, PathSubjectPart[] second)
        {
            var result = false;

            if (first.Length == second.Length)
            {
                var firstAsString = string.Concat(first.Select(part => part.ToString()));
                var secondAsString = string.Concat(second.Select(part => part.ToString()));

                // Is this comparison enough to compare root subject paths?!
                // More details can be found in the Github issue below:
                // https://github.com/vrenken/EtAlii.Ubigia/issues/70
                result = firstAsString == secondAsString;
            }
            return result;
        }
    }
}
