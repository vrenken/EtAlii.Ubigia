namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    internal class RootedPathSubjectProcessor : IRootedPathSubjectProcessor
    {
        private readonly IRootContext _rootContext;
        private readonly IRootHandlerMapperFinder _rootHandlerMapperFinder;
        private readonly IRootHandlerFinder _rootHandlerFinder;
        private readonly IPathSubjectForOutputConverter _converter;

        public RootedPathSubjectProcessor(
            IRootContext rootContext,
            IRootHandlerMapperFinder rootHandlerMapperFinder, 
            IRootHandlerFinder rootHandlerFinder, 
            IPathSubjectForOutputConverter converter)
        {
            _rootContext = rootContext;
            _rootHandlerMapperFinder = rootHandlerMapperFinder;
            _rootHandlerFinder = rootHandlerFinder;
            _converter = converter;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var pathSubject = (PathSubject)subject;
            if (pathSubject is RelativePathSubject)
            {
                // We pass through a relative path.
                output.OnNext(pathSubject);
                output.OnCompleted();
            }
            else
            {
                // And convert absolute and rooted paths.
                _converter.Convert(pathSubject, scope, output);
            }

            //var rootedPathSubject = (RootedPathSubject) subject;

            //// Find root handler mapper.
            //var rootHandlerMapper = _rootHandlerMapperFinder.Find(rootedPathSubject);
            //// Find the root handler.
            //var scope2 = new ScriptScope();
            //System.Diagnostics.Debugger.Break();
            //var rootHandler = _rootHandlerFinder.Find(scope2, rootHandlerMapper, rootedPathSubject);
            //// And process...
            //rootHandler.Process(_rootContext, System.Reactive.Linq.Observable.Empty<object>(), scope, output, true);

            //throw new InvalidOperationException("Output conversion should now be handled by the root sub handlers");
            ////_converter.Convert(rootedPathSubject, scope, output);
        }
    }
}
