// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class FunctionSubjectProcessingScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IFunctionSubjectProcessor, FunctionSubjectProcessor>();
            container.Register<IParameterSetFinder, ParameterSetFinder>();
            container.Register<IFunctionHandlerFinder, FunctionHandlerFinder>();
            container.Register<IArgumentSetFinder, ArgumentSetFinder>();

            container.Register<IFunctionContext, FunctionContext>();

            container.Register<INonRootedPathSubjectFunctionParameterConverter, NonRootedPathSubjectFunctionParameterConverter>();
            container.Register<IRootedPathSubjectFunctionParameterConverter, RootedPathSubjectFunctionParameterConverter>();
            container.Register<IConstantSubjectFunctionParameterConverter, ConstantSubjectFunctionParameterConverter>();
            container.Register<IVariableSubjectFunctionParameterConverter, VariableSubjectFunctionParameterConverter>();
        }
    }
}
