﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.xTechnology.MicroContainer;

internal partial class SubjectProcessingScaffolding : IScaffolding
{
    public SubjectProcessingScaffolding(
        IFunctionHandlersProvider functionHandlersProvider)
    {
        _functionHandlersProvider = functionHandlersProvider;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IAbsolutePathSubjectProcessor, AbsolutePathSubjectProcessor>();
        container.Register<IRelativePathSubjectProcessor, RelativePathSubjectProcessor>();
        container.Register<IRootedPathSubjectProcessor, RootedPathSubjectProcessor>();
        container.Register<IVariableSubjectProcessor, VariableSubjectProcessor>();
        container.Register<IPathSubjectForOutputConverter, PathSubjectForOutputConverter>();
        container.Register<IStringConstantSubjectProcessor, StringConstantSubjectProcessor>();
        container.Register<IObjectConstantSubjectProcessor, ObjectConstantSubjectProcessor>();

        container.Register<IRootSubjectProcessor, RootSubjectProcessor>();
        container.Register<IRootDefinitionSubjectProcessor, RootDefinitionSubjectProcessor>();

        container.Register<IFunctionHandlerFactory, FunctionHandlerFactory>();
        container.Register(GetFunctionHandlersProvider);
    }
}
