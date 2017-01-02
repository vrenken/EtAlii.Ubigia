namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserFactory
    {
        public IScriptParser Create(IDiagnosticsConfiguration diagnostics = null)
        {
            var container = new Container();
            container.Register<IScriptParser, ScriptParser>(Lifestyle.Singleton);
            // Sequence
            container.Register<IEnumerable<ISequencePartParser>>(() => GetSequencePartParser(container), Lifestyle.Singleton);
            // Operators
            container.Register<IEnumerable<IOperatorParser>>(() => GetOperatorParsers(container), Lifestyle.Singleton);
            // Subjects.
            container.Register<IEnumerable<ISubjectParser>>(() => GetSubjectParsers(container), Lifestyle.Singleton);
            container.Register<IEnumerable<IConstantSubjectParser>>(() => GetConstantSubjectParsers(container), Lifestyle.Singleton);
            container.Register<IEnumerable<IPathSubjectPartParser>>(() => GetPathSubjectParsers(container), Lifestyle.Singleton);
            // Helpers
            container.Register<IParserHelper, ParserHelper>(Lifestyle.Singleton);
            container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>(Lifestyle.Singleton);
            container.Register<IConstantHelper, ConstantHelper>(Lifestyle.Singleton);

            RegisterDiagnostics(container, diagnostics);
            return container.GetInstance<IScriptParser>();
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
                container.RegisterDecorator(typeof(IScriptParser), typeof(LoggingScriptParser), Lifestyle.Singleton);
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

        internal static IEnumerable<ISequencePartParser> GetSequencePartParser(Container container)
        {
            return new ISequencePartParser[]
            {
                container.GetInstance<OperatorParser>(),
                container.GetInstance<SubjectParser>(),
                container.GetInstance<CommentParser>(),
            };
        }

        internal static IEnumerable<ISubjectParser> GetSubjectParsers(Container container)
        {
            return new ISubjectParser[]
            {
                container.GetInstance<VariableSubjectParser>(),
                container.GetInstance<ConstantSubjectParser>(),
                container.GetInstance<PathSubjectParser>(),
            };
        }

        internal static IEnumerable<IOperatorParser> GetOperatorParsers(Container container)
        {
            return new IOperatorParser[]
            {
                container.GetInstance<AssignOperatorParser>(),
                container.GetInstance<AddOperatorParser>(),
                container.GetInstance<RemoveOperatorParser>(),
            };
        }

        internal static IEnumerable<IConstantSubjectParser> GetConstantSubjectParsers(Container container)
        {
            return new IConstantSubjectParser[]
            {
                container.GetInstance<StringConstantSubjectParser>(),
            };
        }

        internal static IEnumerable<IPathSubjectPartParser> GetPathSubjectParsers(Container container)
        {
            return new IPathSubjectPartParser[]
            {
                container.GetInstance<ConstantPathSubjectPartParser>(),
                container.GetInstance<VariablePathSubjectPartParser>(),
                container.GetInstance<IdentifierPathSubjectPartParser>(),
                container.GetInstance<IsParentOfPathSubjectPartParser>(),
            };
        }
    }
}
