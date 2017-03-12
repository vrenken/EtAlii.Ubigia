namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class PathProcessor : IPathProcessor
    {
        public IProcessingContext Context => _context;
        private readonly IProcessingContext _context;

        private readonly IPathSubjectToGraphPathConverter _pathSubjectToGraphPathConverter;

        public PathProcessor(
            IProcessingContext context, 
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter)
        {
            _context = context;
            _pathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
        }

        public async Task Process(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output)
        {
            // TODO: Continue here to root processing logical implementation. 
            if (pathSubject is RootedPathSubject)
            {
                var rootedPathSubject = pathSubject as RootedPathSubject;
                var parts = new List<PathSubjectPart>(rootedPathSubject.Parts);
                var hasParts = parts.Count > 0;
                if (hasParts)
                {
                    parts.Insert(0, new IsParentOfPathSubjectPart());
                }
                parts.Insert(0, new ConstantPathSubjectPart(rootedPathSubject.Root));
                parts.Insert(0, new IsParentOfPathSubjectPart());
                pathSubject = new AbsolutePathSubject(parts.ToArray());
            }

            var graphPath = await _pathSubjectToGraphPathConverter.Convert(pathSubject, scope);
            // Path processing should always expect multiple results. So we should always use the Nodes.SelectMany().

            try
            {
                _context.Logical.Nodes.SelectMany(graphPath, scope, output);
            }
            catch (Exception e)
            {
                var message = String.Format("Unable to process query path '{0}'", pathSubject);
                throw new ScriptProcessingException(message, e);
            }
        }
    }
}
