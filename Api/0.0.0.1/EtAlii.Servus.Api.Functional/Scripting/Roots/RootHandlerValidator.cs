namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Collections;

    internal class RootHandlerValidator
    {
        public void Validate(IRootHandlersProvider rootHandlersProvider)
        {
            // Root Handler names.
            ValidateRootHandlerNaming(rootHandlersProvider);

            // Argument sets.
            ValidateSubHandlers(rootHandlersProvider);
        }

        private void ValidateSubHandlers(IRootHandlersProvider rootHandlersProvider)
        {
            foreach (var rootHandler in rootHandlersProvider.RootHandlers)
            {
                var firstSet = rootHandler.AllowedPaths.ToArray();
                var secondSet = rootHandler.AllowedPaths.ToArray();
                Compare(firstSet, secondSet, rootHandler);
            }
        }
        private void ValidateRootHandlerNaming(IRootHandlersProvider rootHandlersProvider)
        {
            // Duplicate names.
            var invalidNamedRootHandlers = rootHandlersProvider.RootHandlers
                .GroupBy(fh => fh.Name)
                .Where(fhg => fhg.Count() > 1)
                .Select(fhg => fhg.First())
                .ToArray();

            if (invalidNamedRootHandlers.Any())
            {
                var message = String.Format("{0} found with the same name: {1}",
                    invalidNamedRootHandlers.Multiple()
                        ? "Multiple root handlers"
                        : "One root handler",
                    invalidNamedRootHandlers.Multiple()
                        ? String.Join(", ", invalidNamedRootHandlers.Select(c => c.Name))
                        : invalidNamedRootHandlers.Single().Name);
                throw new InvalidOperationException(message);
            }

            // Invalid names.
            invalidNamedRootHandlers = rootHandlersProvider.RootHandlers
                .Where(fh => fh.Name.ToCharArray().Any(c => !char.IsLetterOrDigit(c)))
                .ToArray();

            if (invalidNamedRootHandlers.Any())
            {
                var message = String.Format("{0} found with invalid naming: {1}",
                    invalidNamedRootHandlers.Multiple()
                        ? "Multiple root handlers"
                        : "One root handler",
                    invalidNamedRootHandlers.Multiple()
                        ? String.Join(", ", invalidNamedRootHandlers.Select(fh => fh.Name))
                        : invalidNamedRootHandlers.Single().Name);
                throw new InvalidOperationException(message);
            }
        }


        private void Compare(IRootSubHandler[] firstSet, IRootSubHandler[] secondSet, IRootHandler rootHandler)
        {
            foreach (var first in firstSet)
            {
                foreach (var second in secondSet)
                {
                    var areEqual = Compare(first.AllowedPaths, second.AllowedPaths);
                    if (areEqual && second != first)
                    {
                        var message = String.Format("A root handler with multiple matching allowed paths was found: {0}", rootHandler.Name);
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