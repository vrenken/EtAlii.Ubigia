namespace EtAlii.Servus.Api.Functional
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
                var firstSet = rootHandlerMapper.AllowedPaths.ToArray();
                var secondSet = rootHandlerMapper.AllowedPaths.ToArray();
                Compare(firstSet, secondSet, rootHandlerMapper);
            }
        }
        private void ValidateRootHandlerNaming(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            // Duplicate names.
            var invalidNamedRootHandlerMappers = rootHandlerMappersProvider.RootHandlerMappers
                .GroupBy(fh => fh.Name)
                .Where(fhg => fhg.Count() > 1)
                .Select(fhg => fhg.First())
                .ToArray();

            if (invalidNamedRootHandlerMappers.Any())
            {
                var message = String.Format("{0} found with the same name: {1}",
                    invalidNamedRootHandlerMappers.Multiple()
                        ? "Multiple root handler mappers"
                        : "One root handler mapper",
                    invalidNamedRootHandlerMappers.Multiple()
                        ? String.Join(", ", invalidNamedRootHandlerMappers.Select(c => c.Name))
                        : invalidNamedRootHandlerMappers.Single().Name);
                throw new InvalidOperationException(message);
            }

            // Invalid names.
            invalidNamedRootHandlerMappers = rootHandlerMappersProvider.RootHandlerMappers
                .Where(fh => fh.Name.ToCharArray().Any(c => !char.IsLetterOrDigit(c)))
                .ToArray();

            if (invalidNamedRootHandlerMappers.Any())
            {
                var message = String.Format("{0} found with invalid naming: {1}",
                    invalidNamedRootHandlerMappers.Multiple()
                        ? "Multiple root handler mappers"
                        : "One root handler mapper",
                    invalidNamedRootHandlerMappers.Multiple()
                        ? String.Join(", ", invalidNamedRootHandlerMappers.Select(fh => fh.Name))
                        : invalidNamedRootHandlerMappers.Single().Name);
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
                        var message = String.Format("A root handler mapper with multiple matching allowed paths was found: {0}", rootHandlerMapper.Name);
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
                var firstAsString = String.Concat(first.Select(part => part.ToString()));
                var secondAsString = String.Concat(second.Select(part => part.ToString()));

                result = firstAsString == secondAsString; // TODO: Is this enough to compare root subject paths?!
            }
            return result;
        }
    }
}