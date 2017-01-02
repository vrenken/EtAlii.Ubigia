using EtAlii.Servus.Api.Data.Model;
using EtAlii.xTechnology.Structure;

namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;
    using System;
    using Newtonsoft.Json;

    internal class ScriptProcessorFactory
    {
        public IScriptProcessor Create(IDiagnosticsConfiguration diagnostics = null)

        {
            var container = new Container();
            container.Register<ISequencePartProcessor, SequencePartProcessor>(Lifestyle.Singleton);
            return Create(container, diagnostics);
        }

        internal IScriptProcessor Create(ISequencePartProcessor sequencePartProcessor, IDiagnosticsConfiguration diagnostics = null)
        {
            var container = new Container();
            container.Register<ISequencePartProcessor>(() => sequencePartProcessor, Lifestyle.Singleton);
            return Create(container, diagnostics);
        }

        private IScriptProcessor Create(Container container, IDiagnosticsConfiguration diagnostics)
        {
            container.Register<IScriptProcessor, ScriptProcessor>(Lifestyle.Singleton);

            // Sequence
            container.Register<OperatorProcessor>(Lifestyle.Singleton);
            container.Register<SubjectProcessor>(Lifestyle.Singleton);
            container.Register<CommentProcessor>(Lifestyle.Singleton);
            container.Register<ISelector<SequencePart, ISequencePartProcessor>, SequencePartProcessorSelector>(Lifestyle.Singleton);

            // Operators
            container.Register<AssignOperatorProcessor>(Lifestyle.Singleton);
            container.Register<AddOperatorProcessor>(Lifestyle.Singleton);
            container.Register<RemoveOperatorProcessor>(Lifestyle.Singleton);
            container.Register<ISelector<Operator, IOperatorProcessor>, OperatorProcessorSelector>(Lifestyle.Singleton);
            // Subjects
            container.Register<PathSubjectProcessor>(Lifestyle.Singleton);
            container.Register<ConstantSubjectProcessor>(Lifestyle.Singleton);
            container.Register<VariableSubjectProcessor>(Lifestyle.Singleton);
            container.Register<ISelector<Subject, ISubjectProcessor>, SubjectProcessorSelector>(Lifestyle.Singleton);

            container.Register<ConstantPathSubjectPartProcessor>(Lifestyle.Singleton);
            container.Register<IdentifierPathSubjectPartProcessor>(Lifestyle.Singleton);
            container.Register<VariablePathSubjectPartProcessor>(Lifestyle.Singleton);
            container.Register<IsParentOfPathSubjectPartProcessor>(Lifestyle.Singleton);
            container.Register<ISelector<PathSubjectPart, IPathSubjectPartProcessor>, PathSubjectPartProcessorSelector>(Lifestyle.Singleton);

            container.Register<StringConstantSubjectProcessor>(Lifestyle.Singleton);
            container.Register<ISelector<ConstantSubject, IConstantSubjectProcessor>, ConstantSubjectProcessorSelector>(Lifestyle.Singleton);
            // Paths.

            // Helpers.
            container.Register<ITimeTraverser, TimeTraverser>(Lifestyle.Singleton);
            
            // Selectors
            container.Register<ISelector<object, Func<object, IEnumerable<IReadOnlyEntry>>>, DynamicObjectToPathAssignerInputSelector>(Lifestyle.Singleton);
            container.Register<ISelector<ProcessParameters<Operator, SequencePart>, IAssigner>, AssignerSelector>(Lifestyle.Singleton);
            container.Register<ISelector<object, Func<object, IEnumerable<Identifier>>>, AddOperatorInputConverterSelector>(Lifestyle.Singleton);
            container.Register<ISelector<object, Func<object, Identifier[]>>, ItemsToIdentifiersConverterSelector>(Lifestyle.Singleton);
            container.Register<ISelector<object, Func<object, IReadOnlyEntry[]>>, NodeToPathInputConverterSelector>(Lifestyle.Singleton);

            // Content
            container.Register<ContentManagerFactory>(Lifestyle.Singleton);
            container.Register<IContentManager>(() => GetContentManager(container), Lifestyle.Singleton);
            container.Register<JsonSerializer>(() => new SerializerFactory().Create(), Lifestyle.Singleton);
            //_container.Register<IPropertiesSerializer, JsonPropertiesSerializer>(Lifestyle.Singleton);
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>(Lifestyle.Singleton);

            container.Register<OutputAssigner>(Lifestyle.Singleton);
            container.Register<VariableAssigner>(Lifestyle.Singleton);
            

            container.Register<ProcessingContext>(Lifestyle.Singleton);

            // Additional processing (for path variable parts).
            RegisterPathParsing(container);
            RegisterDiagnostics(container, diagnostics);

            return container.GetInstance<IScriptProcessor>();
        }

        private void RegisterPathParsing(Container container)
        {
            container.Register<PathSubjectParser>(Lifestyle.Singleton);
            container.Register<IEnumerable<IPathSubjectPartParser>>(() => ScriptParserFactory.GetPathSubjectParsers(container), Lifestyle.Singleton);
            // Helpers
            container.Register<IParserHelper, ParserHelper>(Lifestyle.Singleton);
            container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>(Lifestyle.Singleton);
            container.Register<IConstantHelper, ConstantHelper>(Lifestyle.Singleton);
        }

        private void RegisterDiagnostics(Container container, IDiagnosticsConfiguration diagnostics)
        {
            if (diagnostics == null)
            {
                // We do not need to frustrate end clients with development diagnostics, 
                // so lets create a failsafe for when no diagnostics configuration is provided. 
                diagnostics = new DiagnosticsFactory().Create(false, false, false,
                    () => new DisabledLogFactory(),
                    () => new DisabledProfilerFactory(),
                    (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"),
                    (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"));
            }

            container.Register<ILogFactory>(() => diagnostics.CreateLogFactory(), Lifestyle.Singleton);
            container.Register<ILogger>(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()), Lifestyle.Transient);
            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IScriptProcessor), typeof(LoggingScriptProcessor), Lifestyle.Singleton);
            }

            container.Register<IProfilerFactory>(() => diagnostics.CreateProfilerFactory(), Lifestyle.Singleton);
            container.Register<IProfiler>(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()), Lifestyle.Singleton);
            if (diagnostics.EnableProfiling) // profiling is enabled
            {
            }

            if (diagnostics.EnableDebugging) // diagnostics is enabled
            {
            }
        }

        private IContentManager GetContentManager(Container container)
        {
            var connection = container.GetInstance<ProcessingContext>().Connection;
            return container.GetInstance<ContentManagerFactory>().Create(connection);
        }
    }
}
