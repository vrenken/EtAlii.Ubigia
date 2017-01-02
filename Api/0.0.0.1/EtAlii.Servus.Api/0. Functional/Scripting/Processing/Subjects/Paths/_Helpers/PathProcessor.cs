namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class PathProcessor : IPathProcessor
    {
        public IProcessingContext Context { get { return _context; } }
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
            var graphPath = await _pathSubjectToGraphPathConverter.Convert(pathSubject, scope);
            // Path processing should always expect multiple results. So we should always use the Nodes.SelectMany().

            try
            {
                _context.Logical.Nodes.SelectMany(graphPath, scope, output);
            }
            catch (Exception e)
            {
                var message = String.Format("Unable to process query path '{0}'", pathSubject.ToString());
                throw new ScriptProcessingException(message, e);
            }
        }
    }
}
