// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class ProfilingSchemaProcessorExtension : IExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public ProfilingSchemaProcessorExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options.InjectProfiling)
            {
                // container.Register(() =gt _profiler)
                // container.RegisterDecorator(typeof(IQueryProcessor), typeof(ProfilingQueryProcessor))
                // //container.RegisterDecorator(typeof(ISequenceProcessor), typeof(ProfilingSequenceProcessor))
                //
                // //container.RegisterDecorator(typeof(IOperatorsProcessor2), typeof(ProfilingOperatorsProcessor))
                // //container.RegisterDecorator(typeof(ISubjectsProcessor2), typeof(ProfilingSubjectsProcessor))
                // //container.RegisterDecorator(typeof(ICommentProcessor2), typeof(ProfilingCommentProcessor))
                //
                // container.RegisterDecorator(typeof(IAbsolutePathSubjectProcessor), typeof(ProfilingAbsolutePathSubjectProcessor))
                // container.RegisterDecorator(typeof(IRelativePathSubjectProcessor), typeof(ProfilingRelativePathSubjectProcessor))
                // container.RegisterDecorator(typeof(IPathSubjectToGraphPathConverter), typeof(ProfilingPathSubjectToGraphPathConverter))
                // container.RegisterDecorator(typeof(IPathProcessor), typeof(ProfilingPathProcessor))
                //
                // container.RegisterDecorator(typeof(IEntriesToDynamicNodesConverter), typeof(ProfilingEntriesToDynamicNodesConverter))
                //
                // container.RegisterDecorator(typeof(IPathSubjectForOutputConverter), typeof(ProfilingPathSubjectForOutputConverter))
                // //container.RegisterDecorator(typeof(IPathSubjectForFunctionAssignmentOperationConverter2), typeof(ProfilingPathSubjectForFunctionAssignmentOperationConverter))
                // //container.RegisterDecorator(typeof(IPathSubjectForPathAddOperationConverter2), typeof(ProfilingPathSubjectForPathAddOperationConverter))
                // //container.RegisterDecorator(typeof(IPathSubjectForPathAssignmentOperationConverter2), typeof(ProfilingPathSubjectForPathAssignmentOperationConverter))
                // //container.RegisterDecorator(typeof(IPathSubjectForPathRemoveOperationConverter2), typeof(ProfilingPathSubjectForPathRemoveOperationConverter))
                // //container.RegisterDecorator(typeof(IPathSubjectForVariableAddOperationConverter2), typeof(ProfilingPathSubjectForVariableAddOperationConverter))
                // //container.RegisterDecorator(typeof(IPathSubjectForVariableAssignmentOperationConverter2), typeof(ProfilingPathSubjectForVariableAssignmentOperationConverter))
            }
        }
    }
}
