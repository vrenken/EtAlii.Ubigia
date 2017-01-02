﻿namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class FunctionSubjectProcessingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IFunctionSubjectProcessor, FunctionSubjectProcessor>();
            container.Register<IParameterSetFinder, ParameterSetFinder>();
            container.Register<IFunctionHandlerFinder, FunctionHandlerFinder>();
            container.Register<IArgumentSetFinder, ArgumentSetFinder>();

            container.Register<IFunctionContext, FunctionContext>();
            container.Register<IFunctionSubjectParameterConverterSelector, FunctionSubjectParameterConverterSelector>();

            container.Register<INonRootedPathSubjectFunctionParameterConverter, NonRootedPathSubjectFunctionParameterConverter>();
            container.Register<IRootedPathSubjectFunctionParameterConverter, RootedPathSubjectFunctionParameterConverter>();
            container.Register<IConstantSubjectFunctionParameterConverter, ConstantSubjectFunctionParameterConverter>();
            container.Register<IVariableSubjectFunctionParameterConverter, VariableSubjectFunctionParameterConverter>();
        }
    }
}
